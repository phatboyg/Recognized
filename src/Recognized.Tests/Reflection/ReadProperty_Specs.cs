namespace Recognized.Tests.Reflection
{
    using NUnit.Framework;
    using Recognized.Reflection;


    [TestFixture]
    public class ReadProperty_Specs
    {
        class Subject
        {
            public int IntValue { get; set; }

            public InnerSubject Inner { get; set; }
        }


        class InnerSubject
        {
        }


        [Test]
        public void Should_read_a_reference_type_property()
        {
            var property = new ReadOnlyProperty(typeof(Subject).GetProperty("Inner"));

            var innerSubject = new InnerSubject();
            var subject = new Subject {Inner = innerSubject};

            object value = property.Get(subject);

            Assert.AreEqual(innerSubject, value);
        }

        [Test]
        public void Should_read_a_value_type_property()
        {
            var property = new ReadOnlyProperty(typeof(Subject).GetProperty("IntValue"));

            var subject = new Subject {IntValue = 27};

            object value = property.Get(subject);

            Assert.AreEqual(27, value);
        }
    }
}