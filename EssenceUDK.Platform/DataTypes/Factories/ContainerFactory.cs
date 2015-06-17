using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssenceUDK.Platform.DataTypes.FileFormat.Containers;

namespace EssenceUDK.Platform.DataTypes.Factories
{
    public static class ContainerFactory
    {
        public static IDataContainer CreateVirtualMul(string idxFile, string mulFile)
        {
            return MulContainer.GetVirtual(idxFile, mulFile, true);
        }

        public static IDataContainer CreateMul(IDataContainer mulContainer, uint fromIndex = 0, uint count = 0)
        {
            var mul = mulContainer as MulContainer;

            return new MulContainer(mul, fromIndex, count, 0);
        }

        public static IDataContainer CreateMul(IDataContainer mulContainer, uint entrySize, uint startOffset = 0, uint count = 0)
        {
            var mul = mulContainer as MulContainer;

            return new MulContainer(mul, startOffset, count, entrySize);
        }

        public static IDataContainer CreateMul(IDataContainer mulContainer, uint entryHeaderSize, uint entryItemSize, uint entryItemsCount, uint startOffset = 0, uint count = 0)
        {
            var mul = mulContainer as MulContainer;

            return new MulContainer(mul, entryHeaderSize, entryItemSize, entryItemsCount, startOffset, count);
        }

        public static IDataContainer CreateMul(string idxFile, string mulFile)
        {
            return new MulContainer(idxFile, mulFile, true);
        }

        public static IDataContainer CreateMul(uint entrySize, string mulFile)
        {
            return new MulContainer(entrySize, mulFile, true);
        }

        public static IDataContainer CreateMul(uint entryHeaderSize, uint entryItemSize, uint entryItemsCount, string mulFile)
        {
            return new MulContainer(entryHeaderSize, entryItemSize, entryItemsCount, mulFile, true);
        }

    }
}
