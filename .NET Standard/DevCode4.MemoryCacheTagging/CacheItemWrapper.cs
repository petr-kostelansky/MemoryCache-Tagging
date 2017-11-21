using System;
using System.Collections.Generic;

namespace DevCode4.MemoryCacheTagging
{
    public class CacheItemWrapper
    {
        public object Value { get; set; }

        public string Key { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
