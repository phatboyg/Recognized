namespace Recognized.Tests
{
    using NUnit.Framework;
    using Parsing;


    [TestFixture]
    public class LineParseStrategy_Specs
    {
        ITextSplitter GetSeparatorSplitter(string text)
        {
            var textRef = new StringStringCursor(text);

            return new SeparatorTextSplitter(textRef);
        }

        [Test]
        public void Should_parse_a_single_line()
        {
            const string line = "Hello, World.";

            var splitter = GetSeparatorSplitter(line);

            var lines = splitter.ToList();

            Assert.AreEqual(1, lines.Count);

            Assert.AreEqual(line, lines[0].String);
        }

        [Test]
        public void Should_parse_a_two_lines()
        {
            const string line = @"Hello, World.
How are you?";

            var splitter = GetSeparatorSplitter(line);

            var lines = splitter.ToList();

            Assert.AreEqual(2, lines.Count);

            Assert.AreEqual("Hello, World.", lines[0].String);
            Assert.AreEqual("How are you?", lines[1].String);
        }

        [Test]
        public void Should_parse_a_two_lines_with_a_blank_one()
        {
            const string line = @"Hello, World.

How are you?";

            var splitter = GetSeparatorSplitter(line);

            var lines = splitter.ToList();

            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual("Hello, World.", lines[0].String);
            Assert.AreEqual("", lines[1].String);
            Assert.AreEqual("How are you?", lines[2].String);
        }

        [Test]
        public void Should_parse_three_lines()
        {
            const string line = @"Hello, World.  
  
  How are you?";

            var splitter = GetSeparatorSplitter(line);

            var lines = splitter.ToList();

            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual("Hello, World.  ", lines[0].String);
            Assert.AreEqual("  ", lines[1].String);
            Assert.AreEqual("  How are you?", lines[2].String);
        }
    }
}