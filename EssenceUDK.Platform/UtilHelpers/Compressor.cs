﻿using System;
using System.IO;
//using System.Windows.Forms;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Checksums;
using Microsoft.Win32.SafeHandles;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksums;

namespace EssenceUDK.Platform.UtilHelpers
{
    public enum CompressorType : byte 
    {
        None         = 0x00,
        Zip          = 0x01,
        BZip2        = 0x02,
        NativeZip    = 0x10 | Zip,
        ManagedZip   = 0x20 | Zip,
        NativeBZip2  = 0x10 | BZip2,
        ManagedBZip2 = 0x20 | BZip2
    }

    public interface ICompressor
    {
        /// <summary>
        /// Library Version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Numeric indifikator compressor
        /// </summary>
        CompressorType TypeId { get; }

        /// <summary>
        /// Data Compression
        /// </summary>
        /// <param name="data">Not compressed data because the byte array</param>
        /// <param name="size">Estimated size after compression</param>
        /// <returns>Compressed data in view of an array of bytes</returns>
        byte[] Compress(byte[] data, UInt32 size);

        /// <summary>
        /// Decompression of data
        /// </summary>
        /// <param name="data">In view of the compressed data byte array </param>
        /// <param name="size">Estimated size after compression</param>
        /// <returns>Not compressed data because the byte array</returns>
        byte[] Decompress(byte[] data, UInt32 size);
    }

    /// <summary>
    /// Compresarator (wrapper comperosorov)
    /// </summary>
    public class Compressor : ICompressor
    {
        private readonly ICompressor _Compressor;

        CompressorType ICompressor.TypeId { get { return _Compressor.TypeId; } }

        string ICompressor.Version { get { return _Compressor.Version; } }

        byte[] ICompressor.Compress(byte[] data, UInt32 size)
        {
            return _Compressor.Compress(data, size);
        }

        byte[] ICompressor.Decompress(byte[] data, UInt32 size)
        {
            return _Compressor.Decompress(data, size);
        }

        internal static ICompressor New(Type compressorType)
        {
            return Activator.CreateInstance(compressorType) as ICompressor;
        }

        public static ICompressor New(CompressorType typeId)
        {
            ICompressor compressor = null;
            switch (typeId) {
                case CompressorType.None        : compressor = new NoneCompressor();        break;
                case CompressorType.Zip         : try { compressor = new ZipNativeCompressor();  } 
                                                catch { compressor = new ZipManagedCompressor(); }   break;
                case CompressorType.BZip2       : try { compressor = new BZ2NativeCompressor();  } 
                                                catch { compressor = new BZ2ManagedCompressor(); }   break;
                case CompressorType.NativeZip   : compressor = new ZipNativeCompressor();   break;
                case CompressorType.ManagedZip  : compressor = new ZipManagedCompressor();  break;
                case CompressorType.NativeBZip2 : compressor = new BZ2NativeCompressor();   break;
                case CompressorType.ManagedBZip2: compressor = new BZ2ManagedCompressor();  break;
                default: throw new ArgumentOutOfRangeException("Unknown compressor typeId.");
            } return compressor;
        }

        private Compressor()
        {
        }

        internal Compressor(CompressorType typeId)
        {
            _Compressor = Compressor.New(typeId);
        }      
    }

    /// <summary>
    /// Does not use compression (fast, but high volume)
    /// </summary>
    internal class NoneCompressor : ICompressor
    {
        public CompressorType TypeId { get { return CompressorType.None; } }

        public NoneCompressor()
        {
            m_Version = "1.0.0, 01-Aug-2011";
        }

        public string Version { get{ return m_Version; } }
        private readonly string m_Version;

        public byte[] Compress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();
            return data;
        }

        public byte[] Decompress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();
            return data;
        }
    }

    /// <summary>
    /// Built compressors SharpZipLib (http://sharpdevelop.net/OpenSource/SharpZipLib/) does not require third-party libraries, and does not use native code
    /// </summary>
    internal class ZipManagedCompressor : ICompressor
    {
        public CompressorType TypeId { get { return CompressorType.ManagedZip; } }

        public string Version { get { return "0.86.0.518, 2010/05/25"; } }

        public ZipManagedCompressor()
        {
        }

        public byte[] Compress(byte[] data, UInt32 size)
        {
            byte[] result;
            Stream inStream  = new MemoryStream(data);
            Stream outStream = new MemoryStream((int)size);
			try {
                //var d = new Deflater(Deflater.BEST_COMPRESSION, true);
                //d.Deflate()

				using (ZipOutputStream zipOutput = new ZipOutputStream(outStream)) {
					zipOutput.IsStreamOwner = true;
                    zipOutput.SetLevel(9);

                    ZipEntry newEntry = new ZipEntry(String.Empty);
                    newEntry.DateTime = DateTime.Now;
                    var crc32 = new Crc32();
                    crc32.Update(data);
                    newEntry.Crc  = crc32.Value;
                    newEntry.Size = data.Length;
                    zipOutput.PutNextEntry(newEntry);

					StreamUtils.Copy(inStream, zipOutput, new byte[4096]);
                    zipOutput.CloseEntry();

                    result = (outStream as MemoryStream).ToArray();
				}
            } catch (Exception e) {
                result = null;
			} finally { 
                inStream.Close();
                outStream.Close(); 
			}
            return result;
        }

        public byte[] Decompress(byte[] data, UInt32 size)
        {
            byte[] result;
            Stream inStream  = new MemoryStream(data);
            Stream outStream = new MemoryStream((int)size);
			try {
				using (ZipInputStream zipInput = new ZipInputStream(inStream)) {
					zipInput.IsStreamOwner = true;
 
                    ZipEntry zipEntry = zipInput.GetNextEntry();
                    //while (zipEntry != null) {
                        long startPosition = outStream.Position;
                        StreamUtils.Copy(zipInput, outStream, new byte[4096]);

                        if (zipEntry.Size != -1L && outStream.Position - startPosition != zipEntry.Size)
                            throw new Exception("Decompression error.");
                        var crc32 = new Crc32();
                        crc32.Update((outStream as MemoryStream).ToArray(), (int)startPosition, (int)(outStream.Position - startPosition));
                        if (zipEntry.HasCrc && crc32.Value != zipEntry.Crc)
                            throw new Exception("Decompression error.");

                        //zipEntry = zipInput.GetNextEntry();                       
                    //}					
                    result = (outStream as MemoryStream).ToArray();
				}
			} catch (Exception e) {
                result = null;
			} finally {
                inStream.Close();
                outStream.Close();
			}
            return result;
        }
    }

    /// <summary>
    /// External native compressors zLib (http://zlib.net/), slightly faster but less compression quality
    /// </summary>
    internal class ZipNativeCompressor : ICompressor
    {
        public CompressorType TypeId { get { return CompressorType.NativeZip; } }

        public enum ZLibError : int
        {
            VersionError    = -6,
            BufferError     = -5,
            MemoryError     = -4,
            DataError       = -3,
            StreamError     = -2,
            FileError       = -1,
            Okay            =  0,
            StreamEnd       =  1,
            NeedDictionary  =  2
        }

        public enum ZLibQuality : int
        {
            Default = -1,
            None    =  0,
            Speed   =  1,
            Size    =  9
        }

        [DllImport("zlib(x86).dll", EntryPoint = "zlibVersion")]
        private static extern unsafe string _LibVersion86();

        [DllImport("zlib(x64).dll", EntryPoint = "zlibVersion")]
        private static extern unsafe string _LibVersion64();

        [DllImport("zlib(x86).dll", EntryPoint = "compress2")]
        private static extern unsafe ZLibError _Compress86(byte[] dest, ref Int32 destLenght, byte[] source, Int32 sourceLenght, ZLibQuality quality);

        [DllImport("zlib(x64).dll", EntryPoint = "compress2")]
        private static extern unsafe ZLibError _Compress64(byte[] dest, ref Int32 destLenght, byte[] source, Int32 sourceLenght, ZLibQuality quality);

        [DllImport("zlib(x86).dll", EntryPoint = "uncompress")]
        private static extern unsafe ZLibError _Decompress86(byte[] dest, ref Int32 destLenght, byte[] source, Int32 sourceLenght);

        [DllImport("zlib(x64).dll", EntryPoint = "uncompress")]
        private static extern unsafe ZLibError _Decompress64(byte[] dest, ref Int32 destLenght, byte[] source, Int32 sourceLenght);

        public ZipNativeCompressor()
        {
            string dllName = Environment.Is64BitProcess ? "zlib(x64).dll" : "zlib(x86).dll";
            string dllPath = Path.Combine(DynamicExecutor.ApplicationDir, dllName);
            if (!File.Exists(dllPath))
                throw new DllNotFoundException(String.Format("Not found library {0}.", dllName));

            m_Version = Environment.Is64BitProcess ? _LibVersion64() : _LibVersion86();
            //if (Version != "1.2.5, 10-Dec-2007")
            //    throw new DllNotFoundException(String.Format("Version \"{0}\" library \"{1}\" is not supported.", Version, dllName));
        }

        public string Version { get{ return m_Version; } }
        private readonly string m_Version;

        public byte[] Compress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();

            unsafe {
                Int32 length = (Int32)size;
                byte[] result = new byte[length];

                fixed (byte* p1 = &data[0], p2 = &result[0]) {
                    var status = Environment.Is64BitProcess 
                        ? _Compress64(result, ref length, data, (Int32)data.Length, ZLibQuality.Default)
                        : _Compress86(result, ref length, data, (Int32)data.Length, ZLibQuality.Default);

                    if (status != ZLibError.Okay) {
                        throw new ApplicationException(String.Format("Error {0} ocurse in data compression.", Enum.GetName(typeof(ZLibError), status)));
                    }
                }

                byte[] compress = new byte[length];
                Array.Copy(result, compress, length);
                return compress;
            }
        }

        public byte[] Decompress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();
            
            unsafe {
                Int32 length = (Int32)size;
                byte[] result = new byte[length];

                fixed (byte* p1 = &data[0], p2 = &result[0]) {
                    var status = Environment.Is64BitProcess
                                 ? _Decompress64(result, ref length, data, (Int32)data.Length)
                                 : _Decompress86(result, ref length, data, (Int32)data.Length);
                    if (status != ZLibError.Okay) {
                        throw new ApplicationException(String.Format("Error {0} ocurse in data decompression.", Enum.GetName(typeof(ZLibError), status)));
                    }
                }

                byte[] decompress = new byte[length];
                Array.Copy(result, decompress, length);
                return decompress;
            }
        }
    }

    /// <summary>
    /// External native compressors bzip2 (http://bzip.org/), slightly slower but better quality compression
    /// </summary>
    internal class BZ2NativeCompressor : ICompressor
    {
        public CompressorType TypeId { get { return CompressorType.NativeBZip2; } }

        [DllImport("bzip2(x86).dll", EntryPoint = "BZ2_bzlibVersion")]
        private static extern unsafe string _LibVersion86();

        [DllImport("bzip2(x64).dll", EntryPoint = "BZ2_bzlibVersion")]
        private static extern unsafe string _LibVersion64();

        [DllImport("bzip2(x86).dll", EntryPoint = "BZ2_bzBuffToBuffCompress")]
        private static extern unsafe Int32 _Compress86(byte[] dest, ref UInt32 destLenght, byte[] source, UInt32 sourceLenght, Int32 blockSize100K, Int32 verbosity, Int32 workFactor);

        [DllImport("bzip2(x64).dll", EntryPoint = "BZ2_bzBuffToBuffCompress")]
        private static extern unsafe Int32 _Compress64(byte[] dest, ref UInt32 destLenght, byte[] source, UInt32 sourceLenght, Int32 blockSize100K, Int32 verbosity, Int32 workFactor);

        [DllImport("bzip2(x86).dll", EntryPoint = "BZ2_bzBuffToBuffDecompress")]
        private static extern unsafe Int32 _Decompress86(byte[] dest, ref UInt32 destLenght, byte[] source, UInt32 sourceLenght, Int32 small, Int32 verbosity);

        [DllImport("bzip2(x64).dll", EntryPoint = "BZ2_bzBuffToBuffDecompress")]
        private static extern unsafe Int32 _Decompress64(byte[] dest, ref UInt32 destLenght, byte[] source, UInt32 sourceLenght, Int32 small, Int32 verbosity);

        public BZ2NativeCompressor()
        {
            string dllName = Environment.Is64BitProcess ? "bzip2(x64).dll" : "bzip2(x86).dll";
            string dllPath = Path.Combine(DynamicExecutor.ApplicationDir, dllName);
            if (!File.Exists(dllPath))
                throw new DllNotFoundException(String.Format("Not Found library {0}.", dllName));

            m_Version = Environment.Is64BitProcess ? _LibVersion64() : _LibVersion86();
            //if (Version != "1.0.6, 6-Sept-2010")
            //    throw new DllNotFoundException(String.Format("Version \"{0}\" library \"{1}\" is not supported.", Version, dllName));
        }

        public string Version { get{ return m_Version; } }
        private readonly string m_Version;

        public byte[] Compress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();

            unsafe {
                UInt32 length = size;
                byte[] result = new byte[length];

                fixed (byte* p1 = &data[0], p2 = &result[0]) {
                    Int32 status = Int32.MaxValue;
                    while (status != 0) {
                        status = Environment.Is64BitProcess
                                 ? _Compress64(result, ref length, data, (UInt32)data.Length, 9, 0, 0)
                                 : _Compress86(result, ref length, data, (UInt32)data.Length, 9, 0, 0);
                        if (status == -8) {
                            length *= 2;
                            result = new byte[length];
                        } else if (status != 0) {
                            throw new ApplicationException("Unnespectible error ocurse in data compression.");
                        }
                    }
                }

                byte[] compress = new byte[length];
                Array.Copy(result, compress, length);
                return compress;
            }
        }

        public byte[] Decompress(byte[] data, UInt32 size)
        {
            if (data != null ? data.Length < 1 : true)
                throw new ArgumentNullException();
            if (size == 0)
                throw new ArgumentOutOfRangeException();

            unsafe {
                UInt32 length = size;
                byte[] result = new byte[length];

                fixed (byte* p1 = &data[0], p2 = &result[0]) {
                    Int32 status = Int32.MaxValue;
                    while (status != 0) {
                        status = Environment.Is64BitProcess
                                 ? _Decompress64(result, ref length, data, (UInt32)data.Length, 0, 0)
                                 : _Decompress86(result, ref length, data, (UInt32)data.Length, 0, 0);
                        if (status == -8) {
                            length *= 2;
                            result = new byte[length];
                        } else if (status != 0) {
                            throw new ApplicationException("Unnespectible error ocurse in data decompression.");
                        }
                    }
                }

                byte[] decompress = new byte[length];
                Array.Copy(result, decompress, length);
                return decompress;
            }
        }
    }

    /// <summary>
    /// Built compressors SharpZipLib (http://sharpdevelop.net/OpenSource/SharpZipLib/) does not require third-party libraries, and does not use native code
    /// </summary>
    internal class BZ2ManagedCompressor : ICompressor
    {
        public CompressorType TypeId { get { return CompressorType.ManagedBZip2; } }

        public string Version { get { return "0.86.0.518, 2010/05/25"; } }

        public BZ2ManagedCompressor()
        {
        }

        public byte[] Compress(byte[] data, UInt32 size)
        {
            byte[] result;
            Stream inStream  = new MemoryStream(data);
            Stream outStream = new MemoryStream((int)size);
			try {
				using (BZip2OutputStream bzipOutput = new BZip2OutputStream(outStream, 9)) {
					bzipOutput.IsStreamOwner = false;
					StreamUtils.Copy(inStream, bzipOutput, new byte[4096]);
                    bzipOutput.Close();
                    result = (outStream as MemoryStream).ToArray();
				}
            } catch (Exception e) {
                result = null;
			} finally { 
                inStream.Close();
                outStream.Close(); 
			}
            return result;
        }

        public byte[] Decompress(byte[] data, UInt32 size)
        {
            byte[] result;
            Stream inStream  = new MemoryStream(data);
            Stream outStream = new MemoryStream((int)size);
			try {
				using (BZip2InputStream bzipInput = new BZip2InputStream(inStream)) {
					bzipInput.IsStreamOwner = false;
					StreamUtils.Copy(bzipInput, outStream, new byte[4096]);
                    bzipInput.Close();
                    result = (outStream as MemoryStream).ToArray();
				}
			} catch (Exception e){
                result = null;
			} finally {
                inStream.Close();
                outStream.Close();
			}
            return result;
        }
    }

}
