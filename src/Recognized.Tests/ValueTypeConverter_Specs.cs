﻿namespace Recognized.Tests
{
    using System;
    using NUnit.Framework;
    using TypeConverters;


    [TestFixture]
    public class ValueTypeConverter_Specs
    {
        [Test]
        public void Should_convert_a_string_to_a_guid()
        {
            Guid expected = Guid.NewGuid();

            var s = expected.ToString();

            Guid result;

            Assert.IsTrue(ValueTypeConverter.Guid.TryGetValue(new TextRef(s), out result));
            Assert.AreEqual(expected, result);
        }
    }
}