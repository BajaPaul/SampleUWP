﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

// This file is used in the Incremental Loading sample.  This is the base clsss/file which GeneratorIncrementalLoadingClass/file
// is derived from, also located in the Common folder.
// More about incremental loading virtualization see:
// https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780657.aspx
// https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.data.isupportincrementalloading.aspx

namespace SampleUWP.Common
{
    // This class can used as a jumpstart for implementing ISupportIncrementalLoading. 
    // Implementing the ISupportIncrementalLoading interfaces allows you to create a list that loads
    // more data automatically when the user scrolls to the end of of a GridView or ListView.
    public abstract class IncrementalLoadingBase: IList, ISupportIncrementalLoading, INotifyCollectionChanged
    {
        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            return _storage.Contains(value);
        }

        public int IndexOf(object value)
        {
            return _storage.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get
            {
                return _storage[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)_storage).CopyTo(array, index);
        }

        public int Count
        {
            get { return _storage.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        public bool HasMoreItems
        {
            get { return HasMoreItemsOverride(); }
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_busy)
            {
                throw new InvalidOperationException("Only one operation in flight at a time");
            }

            _busy = true;

            return AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                var items = await LoadMoreItemsOverrideAsync(c, count);
                var baseIndex = _storage.Count;

                _storage.AddRange(items);

                // Now notify of the new items
                NotifyOfInsertedItems(baseIndex, items.Count);

                return new LoadMoreItemsResult { Count = (uint)items.Count };
            }
            finally
            {
                _busy = false;
            }
        }

        void NotifyOfInsertedItems(int baseIndex, int count)
        {
            if (CollectionChanged == null)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _storage[i + baseIndex], i + baseIndex);
                CollectionChanged(this, args);
            }
        }

        protected abstract Task<IList<object>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);
        protected abstract bool HasMoreItemsOverride();

        readonly List<object> _storage = new List<object>();
        bool _busy = false;

    }
}
