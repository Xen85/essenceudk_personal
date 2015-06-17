using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Runtime.CompilerServices;

using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;
using EssenceUDK.Platform.UtilHelpers;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using UOLang = EssenceUDK.Platform.UtilHelpers.Language;
using UserControl = System.Windows.Controls.UserControl;

namespace EssenceUDK.Add_ins.Client
{
    /// <summary>
    /// Логика взаимодействия для TileMerger.xaml
    /// </summary>
    public partial class TileMerger : UserControl
    {
        private UODataManager _UODataManager = null;
        public  UODataManager UODataManager { get { return _UODataManager; } set
        {
            if (_UODataManager == value)
                return;
            _UODataManager = value;
            var items = _UODataManager.GetItemTile(TileFlag.None, true);//TileFlag.Wall); // lets get all walls to look throw
            //tileItemView1.ItemsSource = items;
            //tileItemView1.UpdateLayout();

            DirectoryPath = value.Location.AbsolutePath;
            tbDirectory_KeyDown(null, null);
        } }

        private IEnumerable<ModelItemData> items = null;

        public TileMerger()
        {
            InitializeComponent();

            //nudHamming.OnValueChanged += OnHammingValueChanged;
            //tileItemView1.SelectionChanged += OnTileViewSelectionChanged;
        }

	    private void OnTileViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
		    OnTileViewSelectionChanged(sender);
	    }

	    protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //if (!WpfHelper.IsInDesignMode)
            //    this.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //// Бред, но иначе чекбоксы по умолчанию не отображаются правильно
            //foreach (var rb in this.FindVisualChildren<System.Windows.Controls.RadioButton>().Where(r => r.IsChecked.Value)) {
            //    rb.IsChecked = false;
            //    rb.IsChecked = true;
            //}

            tbDirectory_KeyDown(null, null);
            cbComparisonType_SelectionChanged(null, null);
        }

        // -------------

        private void Comparison()
        {
            var surf = (imgSelectedFile.Tag as IImageSurface);
            if (surf == null)
                return;
            //var dif = nudHamming.Value;
            //var arg = new object[] { surf };
            //var prc = (string)(cbComparisonType.SelectedItem as ComboBoxItem).Tag;

            //if ((int)tileItemView1.Tag == 0)
            //    tileItemView1.ItemsSource = _UODataManager.GetItemTile(TileFlag.None, true).Where(t =>
            //                                DynamicExecutor.InvokeMethod<ushort>(t.Surface.GetSurface(), typeof(IImageSurface), prc, arg) <= dif);
            //else if ((int)tileItemView1.Tag == 1)
            //    tileItemView1.ItemsSource = _UODataManager.GetLandTile(TileFlag.None, true).Where(t => t.Surface != null &&
            //                                DynamicExecutor.InvokeMethod<ushort>(t.Surface.GetSurface(), typeof(IImageSurface), prc, arg) <= dif);
            //else if ((int)tileItemView1.Tag == 2)
            //    tileItemView1.ItemsSource = _UODataManager.GetLandTile(TileFlag.None, true).Where(t => t.Texture != null &&
            //                                DynamicExecutor.InvokeMethod<ushort>(t.Texture.GetSurface(), typeof(IImageSurface), prc, arg) <= dif);
            //else if ((int)tileItemView1.Tag == 3)
            //    tileItemView1.ItemsSource = _UODataManager.GetGumpSurf(true).Where(t => 
            //                                DynamicExecutor.InvokeMethod<ushort>(t.Surface.GetSurface(), typeof(IImageSurface), prc, arg) <= dif);

            //tbStatusLabel.Text = String.Format((string)tbStatusLabel.Tag, (tileItemView1.ItemsSource as IEnumerable<object>).Count());
        }

        private void OnHammingValueChanged(object sender)
        {
            Comparison();
        }

        private void cbComparisonType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imgSelectedFile != null)
                Comparison();

            var prc = (string)(cbComparisonType.SelectedItem as ComboBoxItem).Tag;

            //if (prc == "GetHammingDistanceForAvrHash008")
            //    nudHamming.Value = Math.Min(nudHamming.Value, nudHamming.Maximum =   64);
            //else if (prc == "GetHammingDistanceForAvrHash032")
            //    nudHamming.Value = Math.Min(nudHamming.Value, nudHamming.Maximum =  256);
            //else if (prc == "GetHammingDistanceForAvrHash128")
            //    nudHamming.Value = Math.Min(nudHamming.Value, nudHamming.Maximum = 1024);
            //else
            //    nudHamming.Maximum = 0xFFFF;
        }

        // -------------

        internal IEnumerable<FileEntry> FileEntries = null;
        private void lvFileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var file = e.AddedItems[e.AddedItems.Count - 1] as FileEntry;
            var surf = UODataManager.CreateSurface(System.IO.Path.Combine(_DirectoryPath, file.FileName + file.FileExts)).GetSurface();
            imgSelectedFile.Source = surf.Image;
            imgSelectedFile.Tag = surf;

            Comparison();
        }

        private void rbTileType_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as System.Windows.Controls.RadioButton;
            if (rb == null || _UODataManager == null) return;
            int tag = -1;
            if (rb.Tag is int) tag = (int)rb.Tag;
            if (rb.Tag is string) tag = int.Parse((string)rb.Tag);

            //tileItemView1.Texture = tag == 2;
            //tileItemView1.Tag = tag;
            //tileItemView1.UpdateLayout();

            if ((imgSelectedFile.Tag as IImageSurface) != null) {
                Comparison();
                return;
            }
            //if ((int)tileItemView1.Tag == 0)
            //    tileItemView1.ItemsSource = _UODataManager.GetItemTile(TileFlag.None, true);
            //else if ((int)tileItemView1.Tag == 1)
            //    tileItemView1.ItemsSource = _UODataManager.GetLandTile(TileFlag.None, true);
            //else if ((int)tileItemView1.Tag == 2)
            //    tileItemView1.ItemsSource = _UODataManager.GetLandTile(TileFlag.None, true);
            //else if ((int)tileItemView1.Tag == 3)
            //    tileItemView1.ItemsSource = _UODataManager.GetGumpSurf(true);

            //imgSelectedFile.Source = _UODataManager.GetGumpSurf(true).ElementAt(1).Surface.GetSurface().Image;
        }

        private void OnTileViewSelectionChanged(object sender)
        {
            //if ((int)tileItemView1.Tag == 0)
            //    imgSelectedItem.Source = (tileItemView1.SelectedItem as ModelItemData).Surface.GetSurface().Image;
            //else if ((int)tileItemView1.Tag == 1)
            //    imgSelectedItem.Source = (tileItemView1.SelectedItem as ModelLandData).Surface.GetSurface().Image;
            //else if ((int)tileItemView1.Tag == 2)
            //    imgSelectedItem.Source = (tileItemView1.SelectedItem as ModelLandData).Texture.GetSurface().Image;
            //else if ((int)tileItemView1.Tag == 3)
            //    imgSelectedItem.Source = (tileItemView1.SelectedItem as ModelGumpSurf).Surface.GetSurface().Image;
        }


        // -------------

        private  string _DirectoryPath { get; set; }
        internal string DirectoryPath { get { return _DirectoryPath; } set
        {
            _DirectoryPath = value;
            tbDirectory.Text = value;
            //NotifyPropertyChanged("DirectoryPath");
        } }

        private void tbDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null || e.Key == Key.Return) {
                if (!Directory.Exists(tbDirectory.Text)) {
                    tbDirectory.Text = _DirectoryPath;
                    return;
                }

                _DirectoryPath = tbDirectory.Text;
                var searchPattern = new Regex(@"\.(bmp|png|tif|tiff|gif)$", RegexOptions.IgnoreCase);
                FileEntries = Directory.GetFiles(_DirectoryPath).Where(f => searchPattern.IsMatch(f)).Select(f => new FileEntry(f));
                lvFileList.ItemsSource = FileEntries;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = DirectoryPath;
            dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog() == DialogResult.OK) {
                DirectoryPath = dialog.SelectedPath;
                tbDirectory_KeyDown(null, null);
            }
        }

        

        // -------------

        private void MenuItem_ImportAll(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (var file in FileEntries) {
                var id = file.GetFileIndex();
                var sr = _UODataManager.CreateSurface(System.IO.Path.Combine(_DirectoryPath, file.FileName));

                //++count;
                //if ((int)tileItemView1.Tag == 0 && id < _UODataManager.ItemCapacity)
                //    _UODataManager.GetItemTile(id).Surface = sr;
                //else if ((int)tileItemView1.Tag == 1 && id < _UODataManager.LandCapacity)
                //    _UODataManager.GetLandTile(id).Surface = sr;
                //else if ((int)tileItemView1.Tag == 2 && id < _UODataManager.LandCapacity)
                //    _UODataManager.GetLandTile(id).Texture = sr;
                //else if ((int) tileItemView1.Tag == 3 && id < _UODataManager.GumpCapacity)
                //    _UODataManager.GetGumpSurf(id).Surface = sr;
                //else
                //    --count;
            }
            System.Windows.MessageBox.Show(String.Format("Import done, loaded {0} surfaces", count));
        }
         
    }

    internal class FileEntry
    { 
        public string FileName { get; set; }
        public string FileExts { get; set; }
        public ImageSource FileIcon { get; set; }

        public uint GetFileIndex()
        {
            var res = 0xFFFFFFFF;
            var hex = false;
            var str = FileName.Substring(1);
            if (str.StartsWith("0x", true, CultureInfo.CurrentCulture)) { 
                str = str.Substring(2);
                hex = true;
            }
            if (hex)
                UInt32.TryParse(str, NumberStyles.AllowHexSpecifier, null, out res);
            else
                UInt32.TryParse(str, NumberStyles.None, null, out res);

            return res;
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        internal FileEntry(string path)
        {
            FileName = System.IO.Path.GetFileNameWithoutExtension(path);
            FileExts = System.IO.Path.GetExtension(path);

            IntPtr ptr = Icon.ExtractAssociatedIcon(path).ToBitmap().GetHbitmap();
            FileIcon =  System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ptr);
        }
    }
}
