using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class NameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("         ")]
        public void EmptyNameInCtorThrowsException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Name(name));
        }

        [Theory]
        [InlineData("validName")]
        public void NameImplicitlyCastsToNameString(string expected)
        {
            var actual = new Name(expected);
            Assert.Equal(expected, (string)actual);
        }

        [Theory]
        [InlineData("testName","testName", true)]
        [InlineData("testName", "anotherTestName", false)]
        public void TwoNameWithSameValueAreEqual(
            string name1,
            string name2,
            bool expected)
        {
            var aName = new Name(name1);
            var anotherName = new Name(name2);

            Assert.Equal(expected, aName.Equals(anotherName));
        }

        [Theory, AutoData]
        public void EqualsWithNullNameReturnsFalse(Name validName)
        {
            Name aNullName = null;

            Assert.Equal(false, validName.Equals(aNullName));
        }

        [Theory, AutoData]
        public void ToStringReturnNameValue(Name validName)
        {
            Assert.Equal((string)validName, validName.ToString());
        }

        [Fact]
        public void ImplicitlyCastNullToStringThrowsException()
        {
            Name nullName = null;
            Assert.Throws<ArgumentNullException>(() => (string)nullName);
        }

        [Fact]
        public void ExplicitConversionOfStringRetursValidName()
        {
            var anonymousName = "test Name";
            Assert.Throws<ArgumentNullException>(() => (Name)string.Empty);
            Assert.Equal(new Name(anonymousName), (Name)anonymousName);
        }

        [Theory, AutoData]
        public void GetHashCodeIsImplementedCorrectly(string name)
        {
            var aName = new Name(name);
            var sameName = new Name(name);
            Assert.Equal(aName.GetHashCode(), sameName.GetHashCode());
        }
    }
}
