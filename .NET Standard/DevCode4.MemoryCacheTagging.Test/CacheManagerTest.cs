using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DevCode4.MemoryCacheTagging.Test
{
    [TestClass]
    public class CacheManagerTest
    {
        [TestMethod]
        public void SaveObject()
        {
            string key = "MyTestProductKey";
            var product = new Product
            {
                Id = 10,
                Name = "My Product 10",
                Price = 25
            };

            var cache = new CacheManager();

            cache.SetValue<Entities>(key, product, 1, Entities.Product, new int[] { 10 });
            var cachedObject = cache.GetValue<Product>(key);

            Assert.IsNotNull(cachedObject);
            Assert.AreSame(product, cachedObject);
        }

        [TestMethod]
        public void RemoveByTag()
        {
            string key = "MyTestProductKey";
            var product = new Product
            {
                Id = 10,
                Name = "My Product 10",
                Price = 25
            };

            var cache = new CacheManager();

            cache.SetValue<Entities>(key, product, 1, Entities.Product, new int[] { 10 });
            var cachedObject = cache.GetValue<Product>(key);

            cache.RemoveByTag(Entities.Product, 10);
            var cachedObject2 = cache.GetValue<Product>(key);

            var tagName = cache.GetTagName(Entities.Product, 10);
            cache.TagCollection.TryGetValue(tagName, out HashSet<string> tagItems);

            Assert.IsNotNull(cachedObject);
            Assert.AreSame(product, cachedObject);
            Assert.IsNull(cachedObject2);
            Assert.IsNull(tagItems);
        }

        [TestMethod]
        public void IsRemovedFromTags()
        {
            string key = "MyTestProductKey";
            var product = new Product
            {
                Id = 10,
                Name = "My Product 10",
                Price = 25
            };

            var cache = new CacheManager();

            cache.SetValue<Entities>(key, product, 1, Entities.Product, new int[] { 10 });
            var cachedObject = cache.GetValue<Product>(key);

            cache.Remove(key);
            var cachedObject2 = cache.GetValue<Product>(key);

            var tagName = cache.GetTagName(Entities.Product, 10);
            cache.TagCollection.TryGetValue(tagName, out HashSet<string> tagItems);

            Assert.IsNotNull(cachedObject);
            Assert.AreSame(product, cachedObject);
            Assert.IsNull(cachedObject2);

            // wait for callback event inside CacheManager
            System.Threading.Thread.Sleep(100);
            var keyInTag = tagItems.FirstOrDefault(s => s == key);
            Assert.IsNull(keyInTag);
        }

        [TestMethod]
        public void Clear()
        {
            string key = "MyTestProductKey";
            var product = new Product
            {
                Id = 10,
                Name = "My Product 10",
                Price = 25
            };

            var cache = new CacheManager();

            cache.SetValue<Entities>(key, product, 1, Entities.Product, new int[] { 10 });
            var cachedObject = cache.GetValue<Product>(key);

            cache.Clear();

            var cachedObject2 = cache.GetValue<Product>(key);

            Assert.IsNotNull(cachedObject);
            Assert.AreSame(product, cachedObject);
            Assert.IsNull(cachedObject2);
        }
    }
}
