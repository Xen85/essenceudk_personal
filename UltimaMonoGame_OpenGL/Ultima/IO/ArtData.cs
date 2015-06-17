/***************************************************************************
 *   ArtData.cs
 *   Based on code from UltimaSDK: http://ultimasdk.codeplex.com/
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.Core;
using UltimaXNA.Core.Diagnostics;
using UltimaXNA.Core.IO;

#endregion

namespace UltimaXNA.Ultima.IO
{
    class ArtData
    {
        private static readonly Texture2D[][] Cache;
        private static readonly FileIndex Index;
        private static readonly ushort[][] Dimensions;
        private static GraphicsDevice _graphics;

        static ArtData()
        {
            Cache = new Texture2D[0x10000][];
            Index = new FileIndex("artidx.mul", "art.mul", 0x10000, -1); // !!! must find patch file reference for artdata.
            Dimensions = new ushort[0x4000][];
        }

        public static void Initialize(GraphicsDevice graphics)
        {
            _graphics = graphics;
        }

        public static Texture2D GetLandTexture(int index)
        {
            index &= 0x3FFF;

            if (Cache[index] == null) { Cache[index] = new Texture2D[0x1000]; }
            var data = Cache[index][0];

            if (data == null)
            {
                Cache[index][0] = data = ReadLandTexture(index);
            }

            return data;
        }

        public static Texture2D GetStaticTexture(int index)
        {
            index &= 0x3FFF;
            index += 0x4000;

            if (Cache[index] == null) { Cache[index] = new Texture2D[0x1000]; }
            var data = Cache[index][0];

            if (data == null)
            {
                Cache[index][0] = data = ReadStaticTexture(index);
            }

            return data;
        }

        private static unsafe Texture2D ReadLandTexture(int index)
        {
            int length, extra;
            bool is_patched;

            var reader = Index.Seek(index, out length, out extra, out is_patched);
            if (reader == null)
                return null;

            var data = new uint[44 * 44];

            var fileData = reader.ReadUShorts(((44 + 2) / 2) * 44);
            var i = 0;

            var count = 2;
            var offset = 21;

            fixed (uint* pData = data)
            {
                uint* dataRef = pData;

                for (int y = 0; y < 22; y++, count += 2, offset--, dataRef += 44)
                {
                    uint* start = dataRef + offset;
                    uint* end = start + count;

                    Metrics.ReportDataRead(count * 2);

                    while (start < end)
                    {
                        uint color = fileData[i++];
                        *start++ = 0xFF000000 + (
                                    ((((color >> 10) & 0x1F) * multiplier)) |
                                    ((((color >> 5) & 0x1F) * multiplier) << 8) |
                                    (((color & 0x1F) * multiplier) << 16)
                                    );
                    }
                }

                count = 44;
                offset = 0;

                for (var y = 0; y < 22; y++, count -= 2, offset++, dataRef += 44)
                {
                    uint* start = dataRef + offset;
                    uint* end = start + count;

                    Metrics.ReportDataRead(count * 2);

                    while (start < end)
                    {
                        uint color = fileData[i++];
                        *start++ = 0xFF000000 + (
                                    ((((color >> 10) & 0x1F) * multiplier)) |
                                    ((((color >> 5) & 0x1F) * multiplier) << 8) |
                                    (((color & 0x1F) * multiplier) << 16)
                                    );
                    }
                }
            }

            var texture = new Texture2D(_graphics, 44, 44);

            texture.SetData<uint>(data);

            return texture;
        }

        public static void GetStaticDimensions(int index, out int width, out int height)
        {
            index &= 0x3FFF;

            var dimensions = Dimensions[index];

            if (dimensions == null)
            {
                Dimensions[index] = dimensions = ReadStaticDimensions(index + 0x4000);
            }

            width = dimensions[0];
            height = dimensions[1];
        }

        private static ushort[] ReadStaticDimensions(int index)
        {
            int length, extra;
            bool isPatched;

            var reader = Index.Seek(index, out length, out extra, out isPatched);
            reader.ReadInt();

            return new[] { reader.ReadUShort(), reader.ReadUShort() };
        }

        const int multiplier = 0xFF / 0x1F;

        static int GetMaxLookup(ushort[] lookups)
        {
            var max = 0;
            foreach (ushort t in lookups)
            {
                if (t > max)
                    max = t;
            }
            return max;
        }

        private static unsafe Texture2D ReadStaticTexture(int index)
        {
            int length, extra;
            bool isPatched;

            var reader = Index.Seek(index, out length, out extra, out isPatched);

            if (reader == null) return null;
            reader.ReadInt(); // this data is discarded. Why?

            var width = reader.ReadShort();
            var height = reader.ReadShort();

            if (width <= 0 || height <= 0) return null;

            if (Dimensions[index - 0x4000] == null)
            {
                Dimensions[index - 0x4000] = new ushort[] {(ushort) width, (ushort) height};
            }


            var lookups = reader.ReadUShorts(height);

            var dataStart = (int) reader.Position + (height*2);
            var readLength = (GetMaxLookup(lookups) + width*2);
            // we don't know the length of the last line, so we read width * 2, anticipating worst case compression.
            if (dataStart + readLength*2 > reader.Stream.Length)
                readLength = ((int) reader.Stream.Length - dataStart) >> 1;
            var fileData = reader.ReadUShorts(readLength);
            var pixelData = new uint[width*height];

            fixed (uint* pData = pixelData)
            {
                var dataRef = pData;
                int i;

                for (var y = 0; y < height; y++, dataRef += width)
                {
                    i = lookups[y];

                    var start = dataRef;

                    int count, offset;

                    while (((offset = fileData[i++]) + (count = fileData[i++])) != 0)
                    {
                        start += offset;
                        uint* end = start + count;

                        while (start < end)
                        {
                            uint color = fileData[i++];
                            *start++ = 0xFF000000 + (
                                ((((color >> 10) & 0x1F)*multiplier)) |
                                ((((color >> 5) & 0x1F)*multiplier) << 8) |
                                (((color & 0x1F)*multiplier) << 16)
                                );
                        }
                    }
                }
            }

            Metrics.ReportDataRead(sizeof (ushort)*(fileData.Length + lookups.Length + 2));

            var texture = new Texture2D(_graphics, width, height);

            texture.SetData<uint>(pixelData);

            return texture;
        }
    }
}