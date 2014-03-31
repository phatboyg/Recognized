namespace Recognized.Tests
{
    using System.Linq;
    using Fragments;
    using NUnit.Framework;
    using Parsing;


    [TestFixture]
    public class LineParseStrategy_Specs
    {
        static Fragment GetDelimitedFragment(TextRef textRef)
        {
            ITextParser textParser = new NewLineTextParser();
            IFragmentFactory lineFragmentFactory = new ParsedFragmentFactory();
            var fragmentParser = new TextFragmentParser(textRef, textParser, lineFragmentFactory);

            return new DelimitedFragment(textRef, fragmentParser);
        }

        [Test]
        public void Should_parse_a_single_line()
        {
            string line = "Hello, World.";

            var textRef = new StringTextRef(line);

            var delimitedFragment = GetDelimitedFragment(textRef);

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(1, fragments.Count);

            Assert.AreEqual(line, fragments[0].GetString());
        }

        [Test]
        public void Should_parse_a_two_lines()
        {
            string line = @"Hello, World.  
  How are you?";

            var textRef = new StringTextRef(line);

            var delimitedFragment = GetDelimitedFragment(textRef);

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(2, fragments.Count);

            Assert.AreEqual("Hello, World.", fragments[0].GetString());
            Assert.AreEqual("How are you?", fragments[1].GetString());
        }

        [Test]
        public void Should_parse_a_two_lines_and_retain_whitespace()
        {
            string line = @"Hello, World.  
  
  How are you?";

            var textRef = new StringTextRef(line);

            var settings = new NewLineTextParserSettings {WhiteSpaceHandling = WhiteSpaceHandling.Retain};
            ITextParser textParser = new NewLineTextParser(settings);
            IFragmentFactory lineFragmentFactory = new ParsedFragmentFactory();
            var fragmentParser = new TextFragmentParser(textRef, textParser, lineFragmentFactory);
            Fragment delimitedFragment = new DelimitedFragment(textRef, fragmentParser);

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(3, fragments.Count);

            Assert.AreEqual("Hello, World.  ", fragments[0].GetString());
            Assert.AreEqual("  ", fragments[1].GetString());
            Assert.AreEqual("  How are you?", fragments[2].GetString());
        }

        [Test]
        public void Should_parse_a_two_lines_and_strip_blank_lines()
        {
            string line = @"Hello, World.

How are you?";

            var textRef = new StringTextRef(line);

            ITextParser textParser = new NewLineTextParser();
            IFragmentFactory lineFragmentFactory = new ParsedFragmentFactory();
            var skipFragmentProvider = new SkipEmptyFragmentFactory(lineFragmentFactory);
            var fragmentParser = new TextFragmentParser(textRef, textParser, skipFragmentProvider);
            var delimitedFragment = (Fragment) new DelimitedFragment(textRef, fragmentParser);

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(2, fragments.Count);

            Assert.AreEqual("Hello, World.", fragments[0].GetString());
            Assert.AreEqual("How are you?", fragments[1].GetString());
        }

        [Test]
        public void Should_parse_a_two_lines_with_a_blank_one()
        {
            string line = @"Hello, World.

How are you?";

            var textRef = new StringTextRef(line);

            var delimitedFragment = GetDelimitedFragment(textRef);

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(3, fragments.Count);

            Assert.AreEqual("Hello, World.", fragments[0].GetString());
            Assert.AreEqual("", fragments[1].GetString());
            Assert.AreEqual("How are you?", fragments[2].GetString());
        }
    }
}