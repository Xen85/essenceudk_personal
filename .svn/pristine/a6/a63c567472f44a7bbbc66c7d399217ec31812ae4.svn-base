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

            // test - lets switch to our tab at startup
            tabControl1.SelectedIndex = 2;

            // Lets try to find clients at system
            var clients = ClientInfo.Get();
            if (clients.Length != 0) { // we must remember that we have no warinties that client is cirtanly valid

                // now we need create base uo data storage class, it base class we will use to do any work with uo data, so generally we need to store it as static.
                // but just now for testing we dont do it. (Remember shilw we will write controls in EsseceUDK.Add-ins we need to get manager at EsseceUDK assembly)
                var manager = new UODataManager(new Uri(clients[0].DirectoryPath), UODataType.ClassicAdventuresOnHighSeas, UOLang.Russian, false);

                // ok, we get manager just now let get tiles and set them as sourse to our list. Yeh, it's really simple)
                tileItemView1.ItemsSource = manager.GetItemTile(TileFlag.Wall); // lets get all walls to look throw

                // just now we use same souce for binding to differen controls. So we represent different data viewer for same data.
                var lands = manager.GetLandTile(TileFlag.None).Where(t => t.TileId < 1000); // just now we get first 1000 valid lands for testing (we dont take care what is this)
                tileLandView1.ItemsSource = lands;
                tileTexmView1.ItemsSource = lands;

                // PS xaml is good, but lets devide all properties of controls in two types: visual-style and visual-logic.
                // The first one is part of theme or control design. The second are user customizable or controll changeble,
                // for example - sizes of tiles in tileItemView1 (we just add some Properties to it later). The idea is that if
                // we decide ti rewrite control in future to own we can easily change it without any problems.

            } else {
                // it's seems we cant find clients so we just throw Exception
                throw new Exception("No one \"Ultima Online\" client was founded.");
            }

            
        }
    }

}
