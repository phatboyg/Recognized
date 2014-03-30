namespace Recognized.Tests.Reflection
{
    using System;
    using NUnit.Framework;
    using Recognized.Reflection;


    [TestFixture]
    public class Nullable_Specs
    {
        [Test]
        public void Should_find_nullable_int()
        {
            Assert.IsTrue(typeof(int?).AllowsNull());
        }

        [Test]
        public void Should_find_nullable_int_type()
        {
            Type underlyingType;
            Assert.IsTrue(typeof(int?).IsNullable(out underlyingType));

            Assert.AreEqual(typeof(int), underlyingType);
        }

        [Test]
        public void Should_find_nullable_string()
        {
            Assert.IsTrue(typeof(string).AllowsNull());
        }

        [Test]
        public void Should_find_nullable_type()
        {
            Assert.IsTrue(typeof(int?).IsNullable());
        }

        [Test]
        public void Should_not_find_int()
        {
            Assert.IsFalse(typeof(int).AllowsNull());
        }

        [Test]
        public void Should_not_find_nullable_string()
        {
            Assert.IsFalse(typeof(string).IsNullable());
        }
    }
}