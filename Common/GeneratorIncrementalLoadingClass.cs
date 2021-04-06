using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This class is used in the Incremental Loading sample.
// Found copy of this code at: http://www.getcodesamples.com/src/FA57D693/C0C4C167
// https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.data.isupportincrementalloading.aspx
// https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780657.aspx

namespace SampleUWP.Common
{
    // This class implements IncrementalLoadingBase. 
    // To create your own Infinite List, you can create a class like this one that doesn't have 'generator' or 'maxcount', 
    //  and instead downloads items from a live data source in LoadMoreItemsOverrideAsync.

    public class GeneratorIncrementalLoadingClass<T>: IncrementalLoadingBase
    {
        readonly Func<int, T> _generator;
        uint _count = 0;
        readonly uint _maxCount;

        public GeneratorIncrementalLoadingClass(uint maxCount, Func<int, T> generator)
        {
            _generator = generator;
            _maxCount = maxCount;
        }

        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            uint toGenerate = System.Math.Min(count, _maxCount - _count);

            // Wait for work 
            await Task.Delay(10);

            // This code simply generates
            var values = from j in Enumerable.Range((int)_count, (int)toGenerate)
                         select (object)_generator(j);
            _count += toGenerate;

            return values.ToArray();
        }
      
        protected override bool HasMoreItemsOverride()
        {
            return _count < _maxCount;
        }

    }
}
