namespace EssenceUDKMVVM.Models.Model.Option
{
    public class LayoutOptionModel : OptionTreeMenu
    {
        /// <summary>
        /// const for Grid Size
        /// </summary>
        private const double GridConst = 5;

        /// <summary>
        /// Const for image size
        /// </summary>
        private const double ImageConst = 15;

        #region field

        /// <summary>
        /// image size value;
        /// </summary>
        private double _imageSize = 80;

        #endregion field

        #region CTOR

        public LayoutOptionModel()
        {
            Name = "Layout";
        }

        #endregion CTOR

        #region Props

        /// <summary>
        /// Size of the Image
        /// </summary>
        public double ImageSize
        {
            get { return _imageSize; }
            set
            {
                _imageSize = value;
                RaisePropertyChanged(() => ImageSize);
                RaisePropertyChanged(() => GridSize);
                RaisePropertyChanged(() => TileImage);
            }
        }

        /// <summary>
        /// Size Of the Grid
        /// </summary>
        public double GridSize
        {
            get { return _imageSize + GridConst; }
        }

        /// <summary>
        /// Size of the tile
        /// </summary>
        public double TileImage { get { return _imageSize + ImageConst; } }

        #endregion Props
    }
}