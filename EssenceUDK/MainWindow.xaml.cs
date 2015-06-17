﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using EssenceUDK.Platform;
﻿using EssenceUDK.Platform.DataTypes;
using EssenceUDK.Platform.UtilHelpers;
﻿using EssenceUDK.Resources;
﻿using PixelFormat = EssenceUDK.Platform.DataTypes.PixelFormat;
﻿using UOLang = EssenceUDK.Platform.UtilHelpers.Language;

namespace EssenceUDK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ApplyTheme("ExpressionDark"); 
            // ThemeManager.GetThemes()[0];

            // test - lets switch to our tab at startup
            tabControl1.SelectedIndex = 6;

            // Lets try to find clients at system
			//var clients = ClientInfo.GetInSystem();
			//if (clients.Length != 0) { // we must remember that we have no warinties that client is cirtanly valid

			//	// now we need create base uo data storage class, it base class we will use to do any work with uo data, so generally we need to store it as static.
			//	// but just now for testing we dont do it. (Remember shilw we will write controls in EsseceUDK.Add-ins we need to get manager at EsseceUDK assembly)
			//	var manager = new UODataManager(new Uri(clients[0].DirectoryPath), UODataType.ClassicAdventuresOnHighSeas, UOLang.Russian, new UODataOptions(), true);// false);
			//	userControlTileMerger.UODataManager = manager;

			//	// ok, we get manager just now let get tiles and set them as sourse to our list. Yeh, it's really simple)
			//	var items = manager.GetItemTile(TileFlag.None, true);//TileFlag.Wall); // lets get all walls to look throw
			//	//foreach (var item in items)
			//	//    item.Surface.GetSurface().GetHammingDistanceForAvrHash(null);
			//	tileItemView1.ItemsSource = items;
                

			//	// just now we use same souce for binding to differen controls. So we represent different data viewer for same data.
			//	var lands = manager.GetLandTile(TileFlag.None).Where(t => t.EntryId < 1000); // just now we get first 1000 valid lands for testing (we dont take care what is this)
			//	tileLandView1.ItemsSource = lands;
			//	tileTexmView1.ItemsSource = lands;

			//	//manager.GetLandTile(0x0001).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x002A.bmp");
			//	//manager.GetLandTile(0x0002).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x0089.bmp");
			//	//manager.GetLandTile(0x0003).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x321D.bmp");
			//	//manager.GetLandTile(0x0004).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x3472.bmp");
			//	//manager.GetLandTile(0x0005).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x346E.bmp");
			//	//manager.GetLandTile(0x0006).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\L0x3475.bmp");

			//	//manager.GetLandTile(0x0001).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x002A.bmp");
			//	//manager.GetLandTile(0x0002).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x0089.bmp");
			//	//manager.GetLandTile(0x0003).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x321D.bmp");
			//	//manager.GetLandTile(0x0004).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x3472.bmp");
			//	//manager.GetLandTile(0x0005).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x346E.bmp");
			//	//manager.GetLandTile(0x0006).Texture = manager.CreateSurface(@"E:\______________________\3d\++\ss\T0x3475.bmp");

			//	//manager.GetItemTile(0x0001).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0xF6C2.bmp");
			//	//manager.GetItemTile(0x0002).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0xF6FC.bmp");
			//	//manager.GetItemTile(0x0003).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0xF6BA.bmp");
			//	//manager.GetItemTile(0x0004).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0x3BB4.bmp");
			//	//manager.GetItemTile(0x0005).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0xF6A2.bmp");
			//	//manager.GetItemTile(0x0006).Surface = manager.CreateSurface(@"E:\______________________\3d\++\ss\I0x248B.bmp");

			//	// PS xaml is good, but lets devide all properties of controls in two types: visual-style and visual-logic.
			//	// The first one is part of theme or control design. The second are user customizable or controll changeble,
			//	// for example - sizes of tiles in tileItemView1 (we just add some Properties to it later). The idea is that if
			//	// we decide ti rewrite control in future to own we can easily change it without any problems.  
			//} else {
			//	// it's seems we cant find clients so we just throw Exception
			//	throw new Exception("No one \"Ultima Online\" client was founded.");
			//}

            
            
        }

        private static bool defaultflat = true;
        private int cmdlast = defaultflat ? 10 : 20;
        private UODataManager UOManager;
        private ISurface surf = null;
        private int avrg_ticks = 0;
        private int avrg_draws = 0;

        private void Render(int cmd)
        {
            if (UOManager == null) {
                MessageBox.Show("Enter client path and load client mul files to be able to draw facet.");
                return;
            }

            var flt = cmd < 20;
            //if (flt)
            //    cmd -= 10;
            //else
            //    cmd -= 20;
            var _cmdlast = flt ? 10 : 20;
            if (cmdlast != _cmdlast ) {
                avrg_ticks = avrg_draws = 0;
                cmdlast = _cmdlast ;
            }

            switch (cmd) {
                case 17: goto case 28;
                case 13: goto case 22;
                case 19: goto case 26;
                case 11: goto case 24;
                case 18: goto case 29;
                case 12: goto case 21;
                case 14: goto case 27;
                case 16: goto case 23;
                case 27: nudX.Value -= 1; break;
                case 23: nudX.Value += 1; break;
                case 29: nudY.Value -= 1; break;
                case 21: nudY.Value += 1; break;
                case 28: nudX.Value -= 1; nudY.Value -= 1; break;
                case 22: nudX.Value += 1; nudY.Value += 1; break;
                case 24: nudX.Value -= 1; nudY.Value += 1; break;
                case 26: nudX.Value += 1; nudY.Value -= 1; break;
                default: break;
            }

            if (nudX.Value < nudX.Minimum)
                nudX.Value = nudX.Minimum;
            if (nudY.Value < nudY.Minimum)
                nudY.Value = nudY.Minimum;
            if (nudX.Value > nudX.Maximum)
                nudX.Value = nudX.Maximum;
            if (nudY.Value > nudY.Maximum)
                nudY.Value = nudY.Maximum;

            var surw = (ushort)nudW.Value;
            var surh = (ushort)nudH.Value;
            var map = (byte)nudM.Value;
            var range = (byte)nudR.Value;
            var tcx = (ushort)nudX.Value;
            var tcy = (ushort)nudY.Value;
            var minz = (sbyte)nudMinZ.Value;
            var maxz = (sbyte)nudMaxZ.Value;
            var alt = (short)nudS.Value;

            


            surf = UOManager.CreateSurface(surw, surh, PixelFormat.Bpp16X1R5G5B5);
            var ticks = Environment.TickCount;
            if (flt)
                UOManager.FacetRender.DrawFlatMap(map, alt, ref surf, range, tcx, tcy, minz, maxz);
            else
                UOManager.FacetRender.DrawObliqueMap(map, alt, ref surf, range, tcx, tcy, minz, maxz);

            var take_ticks = Environment.TickCount - ticks;
            avrg_ticks += take_ticks;
            ++avrg_draws;
            lblStatus.Content = String.Format("Draw in: {0:00000} ms / Avrg: {1:00000}ms", take_ticks, avrg_ticks / avrg_draws);

            //var bid = UOManager.GetMapFacet(map).GetBlockId((uint)nudX.Value, (uint)nudY.Value);
            //UOManager.FacetRender.DrawBlock(ref surf, map, bid);
            imgFacet.Source = surf.GetSurface().Image;
        }

        private void btnRender_Click(object sender, RoutedEventArgs e)
        {
            var tag = Convert.ToInt32((sender as Button).Tag);
            Render(tag);
        }

        private void keyRender_MoveU(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 8);
        }

        private void keyRender_MoveD(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 2);
        }

        private void keyRender_MoveL(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 4);
        }

        private void keyRender_MoveR(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 6);
        }

        private void keyRender_MoveUL(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 7);
        }

        private void keyRender_MoveUR(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 9);
        }

        private void keyRender_MoveDL(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 1);
        }

        private void keyRender_MoveDR(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 3);
        }

        private void keyRender_MoveO(object sender, ExecutedRoutedEventArgs e)
        {
            Render(cmdlast + 0);
        }

        private void AddHotKeys(ExecutedRoutedEventHandler handler, Key key, ModifierKeys mod = ModifierKeys.None)
        {
            try {
                RoutedCommand firstSettings = new RoutedCommand();
                firstSettings.InputGestures.Add(new KeyGesture(key, mod));
                CommandBindings.Add(new CommandBinding(firstSettings, handler));
                //private void My_first_event_handler(object sender, ExecutedRoutedEventArgs e) 
                //private void My_second_event_handler(object sender, RoutedEventArgs e)
            }
            catch (Exception err)
            {
                //handle exception error
            }
        }

        private void btnLoadMuls_Click(object sender, RoutedEventArgs e)
        {
            try {
                UOManager = null;
                var datauri = new Uri(tbPath.Text);
                var dataopt = new UODataOptions();
                dataopt.majorFacet[0] = new FacetDesc("FacetMap-0", (ushort)nudM0W.Value, (ushort)nudM0H.Value, (ushort)nudM0W.Value, (ushort)nudM0H.Value);
                dataopt.majorFacet[1] = new FacetDesc("FacetMap-1", (ushort)nudM1W.Value, (ushort)nudM1H.Value, (ushort)nudM1W.Value, (ushort)nudM1H.Value);
                dataopt.majorFacet[2] = new FacetDesc("FacetMap-2", (ushort)nudM2W.Value, (ushort)nudM2H.Value, (ushort)nudM2W.Value, (ushort)nudM2H.Value);
                dataopt.majorFacet[3] = new FacetDesc("FacetMap-3", (ushort)nudM3W.Value, (ushort)nudM3H.Value, (ushort)nudM3W.Value, (ushort)nudM3H.Value);
                dataopt.majorFacet[4] = new FacetDesc("FacetMap-4", (ushort)nudM4W.Value, (ushort)nudM4H.Value, (ushort)nudM4W.Value, (ushort)nudM4H.Value);
                dataopt.majorFacet[5] = new FacetDesc("FacetMap-5", (ushort)nudM5W.Value, (ushort)nudM5H.Value, (ushort)nudM5W.Value, (ushort)nudM5H.Value);
                var _manager =  UODataManager.GetInstance(datauri,
                  
                    UODataType.ClassicMondainsLegacy, UOLang.English, dataopt, true);
                UOManager = _manager;
            } catch (Exception ex) {
                UOManager = null;
                MessageBox.Show("While loading data exception was raised.\nCheck path, map sizes and make sure that all data muls are present.");
            }
        }

        private void btnSaveFacet_Click(object sender, RoutedEventArgs e)
        {
            var map  = (byte)nudM.Value;
            var alt  = (short)nudS.Value;
            var minz = (sbyte)nudMinZ.Value;
            var maxz = (sbyte)nudMaxZ.Value;
            var scal = (byte)nudPpt.Value;

            var posx1 = (uint)nudSX1.Value;
            var posy1 = (uint)nudSY1.Value;
            var posx2 = (uint)nudSX2.Value;
            var posy2 = (uint)nudSY2.Value;

            SaveFacetTickStart = Environment.TickCount;
            ISurface surf;
            UOManager.FacetRender.SaveFlatMap(out surf, scal, map, posx1, posy1, posx2, posy2, alt, minz, maxz, SaveFacetCallback);

            var name = tbSFile.Text;
            if (!name.EndsWith(".png"))
                name += ".png";
            if (name[1] != ':')
                name = System.IO.Path.Combine(Environment.CurrentDirectory, name);
            surf.GetSurface().SavePNG(name);

            imgFacet.Source = surf.GetSurface().Image;
        }

        private void SaveFacetCallback(float done)
        {
            var ticks = Environment.TickCount - SaveFacetTickStart;
            lblStatus.Content = String.Format("Rendering: {0,7:0.000}% {1}m {2:00}s {3:000}ms", done, ticks / 60000, ticks % 60000 / 1000, ticks % 1000);
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        private int SaveFacetTickStart;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddHotKeys(keyRender_MoveU, Key.Up);
            AddHotKeys(keyRender_MoveL, Key.Left);
            AddHotKeys(keyRender_MoveR, Key.Right);
            AddHotKeys(keyRender_MoveD, Key.Down);

            AddHotKeys(keyRender_MoveU, Key.NumPad8);
            AddHotKeys(keyRender_MoveL, Key.NumPad4);
            AddHotKeys(keyRender_MoveR, Key.NumPad6);
            AddHotKeys(keyRender_MoveD, Key.NumPad2);

            AddHotKeys(keyRender_MoveUL, Key.NumPad7);
            AddHotKeys(keyRender_MoveUR, Key.NumPad9);
            AddHotKeys(keyRender_MoveDL, Key.NumPad1);
            AddHotKeys(keyRender_MoveDR, Key.NumPad3);

            AddHotKeys(keyRender_MoveUL, Key.Home);
            AddHotKeys(keyRender_MoveUR, Key.PageUp);
            AddHotKeys(keyRender_MoveDL, Key.End);
            AddHotKeys(keyRender_MoveDR, Key.PageDown);

            AddHotKeys(keyRender_MoveO,  Key.Space);

            nudM0W.Minimum = nudM1W.Minimum = nudM2W.Minimum = nudM3W.Minimum = nudM4W.Minimum = nudM5W.Minimum = 1;
            nudM0H.Minimum = nudM1H.Minimum = nudM2H.Minimum = nudM3H.Minimum = nudM4H.Minimum = nudM5H.Minimum = 1;
            nudM0W.Maximum = nudM1W.Maximum = nudM2W.Maximum = nudM3W.Maximum = nudM4W.Maximum = nudM5W.Maximum = 1;
            nudM0H.Maximum = nudM1H.Maximum = nudM2H.Maximum = nudM3H.Maximum = nudM4H.Maximum = nudM5H.Maximum = 1;
            nudM0W.Value = 896;     nudM0H.Value = 512;
			nudM1W.Value = 896; nudM1H.Value = 512;
            nudM2W.Value = 288;     nudM2H.Value = 200;
            nudM3W.Value = 320;     nudM3H.Value = 256;
            nudM4W.Value = 181;     nudM4H.Value = 181;
            nudM5W.Value = 160;     nudM5H.Value = 512;

            nudS.Minimum = -512;
            nudS.Minimum = +512;
            nudS.Value   = -45;

            nudSX1.Value = 774;//860; //774;//909;
            nudSY1.Value = 8;//360; //8;//410;
            nudSX2.Value = 1492;//920; //1492;//920;
            nudSY2.Value = 604;//424; //604;//424;

            nudPpt.Minimum = 1;
            nudPpt.Maximum = 16;
            nudPpt.Value = 2;

            nudMinZ.Minimum = nudMaxZ.Minimum = nudMinZ.Value = - 128;
            nudMinZ.Maximum = nudMaxZ.Maximum = nudMaxZ.Value = + 127;

            nudW.Minimum = nudH.Minimum = 176;
            nudW.Minimum = nudH.Maximum = 10240;
            #if DEBUG
			tbPath.Text = @"C:\Ultima\TM-2";
                nudW.Value = 1200;
                nudH.Value = 1200;
            #else
                var clients = ClientInfo.GetInSystem();
                tbPath.Text = clients.Length > 0 ? clients[0].DirectoryPath : "Enter path to client directory";
                nudW.Value = 2560;
                nudH.Value = 1600;
            #endif

            nudM.Minimum = 0;
            nudM.Maximum = 5;
            nudR.Minimum = 0;
            nudR.Maximum = 255;

            nudX.Minimum = 0;
            nudX.Maximum = 12288- 1;
            nudY.Minimum = 0;
            nudY.Maximum = 8192 - 1;

            nudM.Value = 1;
            nudR.Value = 20;
            nudX.Value = 7087;//7320;//913 * 8 + 3;
            nudY.Value = 3028;//3364;//411 * 8 + 3;
            this.Width = 1278;
            this.Height = 938;

            #if DEBUG
                btnLoadMuls_Click(null, null);
            #endif
        }

        
    }

}
