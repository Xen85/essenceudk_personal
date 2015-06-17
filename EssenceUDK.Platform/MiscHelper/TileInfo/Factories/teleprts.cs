using System;
using System.Collections.Generic;
using EssenceUDK.Platform.MiscHelper.Interfaces;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public class Teleprts : Factory,  IFactory
    {
        public Teleprts(UODataManager location) : base(location)
        {
        }

        public List<Components.TileCategory> Categories
        {
            get { throw new NotImplementedException(); }
        }

        public void Populate()
        {
            throw new NotImplementedException();
        }
    }
}
