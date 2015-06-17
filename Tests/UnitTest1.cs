using System;
using System.Drawing.Imaging;
using System.IO;
using EssenceUDK.Platform.UtilHelpers;
using MapMakerApplication.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssenceUDK.Platform;
namespace Tests
	{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var _manager = UODataManager.GetInstance(new Uri(@"C:\The Miracle"), UODataType.ClassicMondainsLegacy,Language.English);
			UltimaMapDataProvider provider = new UltimaMapDataProvider();
			var map = _manager.GetMapFacet(4);
			provider.mapIndex(4, _manager.GetMapFacet(4));
			var bmp = EssenceUDK.MapMaker.MapMaking.BitmapReader.ExportAltitude(provider, (int) map.Width, (int) map.Height, null);
			bmp.Save(Path.Combine(@"C:\Users\Fabio\Desktop\map", "test.bmp"), ImageFormat.Bmp);
		}
		}
	}
