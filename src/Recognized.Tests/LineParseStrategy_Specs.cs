namespace Recognized.Tests
{
    using System.Linq;
    using Fragments;
    using NUnit.Framework;
    using Parsing;


    [TestFixture]
    public class LineParseStrategy_Specs
    {
        [Test]
        public void Should_parse_a_single_line()
        {
            string line = "Hello, World.";

            var textRef = new StringTextRef(line);

            ITextParser textParser = new NewLineTextParser();
            IFragmentProvider fragmentProvider = new ParsedFragmentProvider();
            Fragment delimitedFragment = new DelimitedFragment(textRef, new TextFragmentParser(textRef, textParser, fragmentProvider));

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

            ITextParser textParser = new NewLineTextParser();
            IFragmentProvider fragmentProvider = new ParsedFragmentProvider();
            Fragment delimitedFragment = new DelimitedFragment(textRef, new TextFragmentParser(textRef, textParser, fragmentProvider));

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

            ITextParser textParser = new NewLineTextParser();
            IFragmentProvider fragmentProvider = new ParsedFragmentProvider();
            Fragment delimitedFragment = new DelimitedFragment(textRef, new TextFragmentParser(textRef, textParser, fragmentProvider));

            var fragments = delimitedFragment.ToList();

            Assert.AreEqual(3, fragments.Count);

            Assert.AreEqual("Hello, World.", fragments[0].GetString());
            Assert.AreEqual("", fragments[1].GetString());
            Assert.AreEqual("How are you?", fragments[2].GetString());
        }
    }
}