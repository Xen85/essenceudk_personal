/*
 * ===============================================================================
 * ===============================================================================
 * 
 * !! This file is part of ULTIMA logic and cant be containd in this general library !!!!
 * 
 * ===============================================================================
 * ===============================================================================
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using EssenceUDK.Resources.Libraries.MiscUtil.DataVirtualization;
using System.Linq;

namespace EssenceUDK.Platform.MiscHelper
{
    public class ItemProviderModelItemData : IItemsProvider<ModelItemData>
    {
        #region Declarations

        private IList<ModelItemData> _list;
        private int _count;
        
        #endregion // Declarations

        #region Ctor

        public ItemProviderModelItemData(IList<ModelItemData> list)
        {
            _list = list;
            _count = list.Count;
        }

        public ItemProviderModelItemData(IList<ModelItemData> list, int count)
        {
	        _list = new List<ModelItemData>();
	       
			for ( var i = 0; i < list.Count && i < count; i++ )
				{
				_list.Add(list[i]);
				}
			_count = _list.Count;
        } 

        #endregion

        #region IItemsProvider

        public int FetchCount()
        {
            return _count;
        }

        public IList<ModelItemData> FetchRange(int startIndex, int count)
        {
            IList<ModelItemData> items = new List<ModelItemData>();
            for (int i = startIndex; i < _list.Count && i < startIndex+count; i++)
            {
                items.Add(_list[i]);
            }

            return items;
        } 

        #endregion
    }


    public class ItemProviderModelLandData : IItemsProvider<ModelLandData>
    {
        #region Declarations

        private readonly IList<ModelLandData> _list;
        private ObservableCollection<ModelLandData> _collection;
	    private int _count;
        
        #endregion

        #region Ctor

        public ItemProviderModelLandData(IList<ModelLandData> list)
        {
            _list = list;
			_count = _list.Count;
        }

        public ItemProviderModelLandData(IList<ModelLandData> list, int count)
        {
	        _list = new List<ModelLandData>();
	        for (var i = 0; i < list.Count && i < count; i++)
	        {
		        _list.Add(list[i]);
	        }
			_count = _list.Count;
        } 
        #endregion

        #region IItemsProvider
        public int FetchCount()
        {
	        return _count;
        }

        public IList<ModelLandData> FetchRange(int startIndex, int count)
        {
            var collection = new ObservableCollection<ModelLandData>();
	        if (startIndex > _list.Count)
	        {
		        int a =0;
				 a++;
	        }
            for (int i = startIndex; i < startIndex+count && i < _list.Count; i++)
            {
                collection.Add(_list[i]);
            }
            return collection;
        } 
        #endregion
    }
}
