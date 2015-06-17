using System.Collections.ObjectModel;
using System.IO;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Interfaces;
using EssenceUDK.Platform.MiscHelper.TileInfo.Components.MultiStruct;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public class TxtFile : IFactory 
    {
        private readonly string _location = "";

        public readonly MultiCollection _multi;

        public TxtFile(string location,UODataManager data)
        {
            _location = location;
            _multi = new MultiCollection(data);
        }

        public ObservableCollection<TileCategory> Categories
        {
            get { return _multi.Categories; }
        }


        public void Populate()
        {
            var lines = File.ReadAllLines(_location);

            for (int index = 4; index < lines.Length; index++)
            {
                var line = lines[index];
                var strings = line.Split(' ');
                _multi.AddTile(uint.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]),
                               int.Parse(strings[3]), int.Parse(strings[4]));
            }
        }
    }
}
