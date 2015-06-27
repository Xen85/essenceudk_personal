using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Controls.Tiles
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public interface ITileContainerViewModel
    {
        object Selected { get; set; }

        int SelectedIndex { get; set; }
    }
}