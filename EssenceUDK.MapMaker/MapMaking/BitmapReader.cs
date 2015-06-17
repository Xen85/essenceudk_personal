using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.Interfaces;

namespace EssenceUDK.MapMaker.MapMaking
{
	public static class BitmapReader 
	{
		//public Color[] BitmapColors { get; private set; }


		public static byte[] RawBitmapReader(string location)
		{
			const byte pixelsize =3;
             int offset;
             byte dummy;
			int WIDTH, HEIGHT;
			byte[] data;
 
             var fs = new FileStream(location, FileMode.Open, FileAccess.Read);
             var r = new BinaryReader(fs);
 
             for (int i = 0; i < 10; i++)
             {
                 dummy = r.ReadByte();
             }
 
             offset = r.ReadByte();
             offset += r.ReadByte() * 256;
             offset += r.ReadByte() * 256 * 256;
             offset += r.ReadByte() * 256 * 256 * 256;
 
             for (int i = 0; i < 4; i++)
             {
                 dummy = r.ReadByte();
             }
 
             WIDTH = r.ReadByte();
             WIDTH += r.ReadByte() * 256;
             WIDTH += r.ReadByte() * 256 * 256;
             WIDTH += r.ReadByte() * 256 * 256 * 256;
 
             HEIGHT = r.ReadByte();
             HEIGHT += r.ReadByte() * 256;
             HEIGHT += r.ReadByte() * 256 * 256;
             HEIGHT += r.ReadByte() * 256 * 256 * 256;

			data = new byte[HEIGHT*WIDTH*pixelsize];
             for (int i = 0; i < (offset - 26); i++)
             {
                 dummy = r.ReadByte();
             }
 
             for (var y = 0; y < HEIGHT; y++)
             {
                 for (var x = 0; x < WIDTH; x++)
                 {
	                var pixel = new byte[pixelsize];
                    pixel[0] = (r.ReadByte());
					pixel[1] =(r.ReadByte());
					pixel[2] += (r.ReadByte());
					 //data[WIDTH - 1 - y, HEIGHT - 1 - i] = height;
					data[( WIDTH*pixelsize*(HEIGHT - 1-y) ) + ( x*pixelsize )] = pixel[0];
					data[( WIDTH*pixelsize*(HEIGHT -1 -y) ) + ( x*pixelsize )] = pixel[1];
					data[(WIDTH*pixelsize*(HEIGHT -1 - y)) + (x*pixelsize)] = pixel[2];
                 }
             }
			r.Close();
			r.Dispose();
			fs.Close();
			fs.Dispose();

			return data;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="collectionAreaColor"></param>
		/// <param name="location"></param>
		/// <exception>
		///   <cref>ExecutionEngineException</cref>
		/// </exception>
		/// <returns></returns>
		public static AreaColor[] ProduceMap(CollectionAreaColor collectionAreaColor, string location)
		{
			AreaColor[] areaColors = null;
			using (var bitmap = new Bitmap(location))
			{

				areaColors = new AreaColor[bitmap.Width * bitmap.Height];
				
				// Lock the bitmap's bits.  
				var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

				//lock the bitmap bits
				BitmapData bmpData;

				bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

				// Get the address of the first line.
				var ptr = bmpData.Scan0;

				// Declare an array to hold the bytes of the bitmap.
				var bytes = bmpData.Stride * bitmap.Height;
				var rgbValues = new byte[bytes];

				var list = new List<String>();
				// Copy the RGB values into the array.
				Marshal.Copy(ptr, rgbValues, 0, bytes);

				var stride = bmpData.Stride;

				for (var coulmn = bmpData.Height - 1; coulmn >= 0; coulmn--)
				{
					for (var row = 0; row < bmpData.Width; row++)
					{
						areaColors[(coulmn*(bmpData.Width)) + row] =
							collectionAreaColor.FindByByteArray(new[]
																	{
																		rgbValues[(coulmn*stride) + (row*3) + 2],
																		rgbValues[(coulmn*stride) + (row*3) + 1],
																		rgbValues[(coulmn*stride) + (row*3)]
																	});

						if (areaColors[(coulmn*(bmpData.Width)) + row] != null) continue;

						var str = "Color =" + 
							System.Windows.Media.Color.FromRgb(
								rgbValues[(coulmn * stride) + (row * 3) + 2],
								rgbValues[(coulmn * stride) + (row * 3) + 1],
								rgbValues[(coulmn * stride) + (row * 3)]) 
							+ " not found.";
							
						if(!list.Contains(str))
						{
							list.Add(str);
						}
					}
				}

				if(list.Count>0)
				{
					var message = list.Aggregate("", (current, str) => current + (str + '\n'));
					throw new Exception(message);
				}
			}

			return areaColors;
		}


		

		public static sbyte[] AltitudeFromBitmapVersion2( string originalLocation, int XMax, int YMax )
			{
			var original = new Bitmap(originalLocation);
			if ( original.Width != XMax || original.Height != YMax )
				throw new Exception("Altitude Bitmap Dimentions are not correct.");
			using ( original )
				{
				var array = new sbyte[original.Height * original.Width];


				unsafe
					{
					//lock the original bitmap in memory
					var originalData = original.LockBits(
						new Rectangle(0, 0, original.Width, original.Height),
						ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);


					//set the number of bytes per pixel
					const int pixelSize = 3;

					for ( int y = original.Height-1; y >=0; y-- )
						{
						
						//get the data from the original image
						byte* oRow = ( byte* ) originalData.Scan0 + ( y*originalData.Stride );

						for ( int x = 0; x < original.Width; x++ )
							{
							var red = oRow[x*pixelSize + 2];
							var green = oRow[x*pixelSize + 1];
							var blue = oRow[x*pixelSize];

							if ( blue > 0 && red == 0 && green == 0 )
								{
								var partialresult = blue - 128;
								if ( partialresult > sbyte.MaxValue )
									partialresult = sbyte.MaxValue;
								if ( partialresult < sbyte.MinValue )
									partialresult = sbyte.MinValue;

								array[( y * ( original.Width ) ) + x] = ( sbyte ) partialresult;
								continue;
								}

							if ( red == green && red == blue )
								{
								var partialresult = red - 128;
								if ( partialresult > sbyte.MaxValue )
									partialresult = sbyte.MaxValue;
								if ( partialresult < sbyte.MinValue )
									partialresult = sbyte.MinValue;

								array[( y * ( original.Width ) ) + x] = ( sbyte ) partialresult;
								continue;
								}

							if ( red == blue && green == 0 )
								{
								var partialresult = red - 128;
								if ( partialresult > sbyte.MaxValue )
									partialresult = sbyte.MaxValue;
								if ( partialresult < sbyte.MinValue )
									partialresult = sbyte.MinValue;

								array[( y * ( original.Width ) ) + x] = ( sbyte ) partialresult;
								continue;
								}

							if ( green > 0 && blue == 0 && red == 0 )
								{
								var partialresult = green - 128;
								if ( partialresult > sbyte.MaxValue )
									partialresult = sbyte.MaxValue;
								if ( partialresult < sbyte.MinValue )
									partialresult = sbyte.MinValue;

								array[( y * ( original.Width ) ) + x] = ( sbyte ) partialresult;
								continue;
								}


							array[( y*( original.Width ) ) + x] = 0;

							}
						}

					original.UnlockBits(originalData);

					}
				return array;

				}
			}

		public static AreaColor[] ColorsFromBitmap(CollectionAreaColor collectionAreaColor, string originalLocation, int XMax, int YMax)
		{
			var original = new Bitmap(originalLocation);
			if(original.Height != YMax || original.Width != XMax )
				throw new Exception("Terrain Bitmap has wrong dimentions");
			var areaColors = new AreaColor[original.Height * original.Width];
			var list = new List<string>();
			using (original)
			{
				unsafe
				{


					//lock the original bitmap in memory
					BitmapData originalData = original.LockBits(
						new Rectangle(0, 0, original.Width, original.Height),
						ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);


					//set the number of bytes per pixel
					const int pixelSize = 3;


					for (int y = originalData.Height - 1; y >= 0; y--)
					{
						//get the data from the original image
						byte* oRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

						for (int x = 0; x < originalData.Width; x++)
						{
							var red = oRow[x * pixelSize + 2];
							var green = oRow[x * pixelSize + 1];
							var blue = oRow[x * pixelSize];

							var area =
							collectionAreaColor.FindByByteArray(new[]
																	{
																		red,
																		green,
																		blue
																	});
							areaColors[(y*(originalData.Width)) + x] = area;
							if (area != null) continue;


							var str = "Color =" +
								System.Windows.Media.Color.FromRgb(
									red,
									green,
									blue)
								+ " not found.";

							if (!list.Contains(str))
							{
								list.Add(str);
							}
						}
					}

					original.UnlockBits(originalData);

				}
				if (list.Count > 0)
				{
					var message = list.Aggregate("", (current, str) => current + (str + '\n'));
					throw new Exception(message);
				}
				return areaColors;

			}
		}

		public static Bitmap ExportAltitude( IUltimaMapDataProvider provider, int width, int height, EventHandler eventProgress )
			{

			if ( eventProgress != null )
				eventProgress(null, new ProgressEventArgs() { PayLoad = "Extracting Altitude", Progress = 0 });
			//set the number of bytes per pixel
			const int pixelsize = 3;
			var stride = width*pixelsize;
			var providerCoord = new BlockCoordinatesProvider(width, height, 0, 0);
			var array = new byte[height*stride];

			while ( providerCoord.HasNextCoord() )
					{
						int x,y;
						providerCoord.GetNext(out x, out y);

					provider.GetCoordinates(( uint ) x, ( uint ) y);

					var grayScale = ( byte ) ( provider.Atitude + 128 );



					for ( int i = 0; i < pixelsize; i++ )
					{
						array[( y*stride ) + ( x*pixelsize + i )] = grayScale;
					}

					var percent1 = ( byte ) ( Math.Round(( double ) ( ( 100*providerCoord.Progress )/( height ) )) );
					if ( eventProgress != null )
						eventProgress(null, new ProgressEventArgs() { PayLoad = "Extracting Altitude", Progress = percent1 });
					
					}

			Bitmap bmp;
			unsafe 
				{
			   fixed (byte* ptr = array) 
				   {
					bmp = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, new IntPtr(ptr));

					}
			   }

			return bmp;
			}




	}
}
