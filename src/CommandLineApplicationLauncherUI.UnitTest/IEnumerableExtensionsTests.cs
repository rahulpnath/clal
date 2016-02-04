using CommandLineApplicationLauncherViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void ToObservableCollectionOnNullCollectionThrowsException()
        {
            IEnumerable<string> NullCollection = null;
            Assert.Throws<ArgumentNullException>(() => NullCollection.ToObservableCollection());
        }

        [Theory, AutoMoqData]
        public void ToObservableCollectionReturnsObservableCollection(IEnumerable<object> collection)
        {
            var actual = collection.ToObservableCollection();
            Assert.NotNull(actual);
            Assert.Equal(collection.Count(), actual.Count);
        }
    }
}
