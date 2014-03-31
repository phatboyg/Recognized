namespace Recognized.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Fragments;
    using NUnit.Framework;
    using Parsing;


    [TestFixture, Explicit]
    public class NFL_Specs
    {
        Fragment _fragment;
        StringTextRef _textRef;
        string _text;
        const string FilePath = @"d:\temp\2012_nfl_pbp_data.csv";

        [TestFixtureSetUp]
        public void Setup()
        {
            _text = File.ReadAllText(FilePath);

            _textRef = new StringTextRef(_text);

            _fragment = GetDelimitedFragment(_textRef);
        }

        static Fragment GetDelimitedFragment(TextRef textRef)
        {
            ITextParser textParser = new NewLineTextParser();
            ITextParser commaDelimitedParser = new CommaDelimitedTextParser();
            var parsedFragmentFactory = new ParsedFragmentFactory();
            IFragmentFactory lineFragmentFactory = new DelimitedFragmentFactory(parsedFragmentFactory, commaDelimitedParser);
            var skipBlankLines = new SkipEmptyFragmentFactory(lineFragmentFactory);
            var fragmentParser = new TextFragmentParser(textRef, textParser, skipBlankLines);

            return new DelimitedFragment(textRef, fragmentParser);
        }

        [Test]
        public void Should_parse_the_entire_file()
        {
            var fragments = _fragment.ToList();

            Console.WriteLine("Line Count: {0}", fragments.Count);

            Assert.AreEqual(44854, fragments.Count);
        }

        [Test]
        public void Should_get_the_columns_for_each_line()
        {
            var fragment = _fragment.First();

            var columns = fragment.ToList();

            Assert.AreEqual(13, columns.Count);

            Assert.AreEqual("gameid", columns[0].GetString());
            Assert.AreEqual("qtr", columns[1].GetString());
            Assert.AreEqual("min", columns[2].GetString());
            Assert.AreEqual("sec", columns[3].GetString());
            Assert.AreEqual("off", columns[4].GetString());
            Assert.AreEqual("def", columns[5].GetString());
            Assert.AreEqual("down", columns[6].GetString());
            Assert.AreEqual("togo", columns[7].GetString());
            Assert.AreEqual("ydline", columns[8].GetString());
            Assert.AreEqual("description", columns[9].GetString());
            Assert.AreEqual("offscore", columns[10].GetString());
            Assert.AreEqual("defscore", columns[11].GetString());
            Assert.AreEqual("season", columns[12].GetString());
        }

        [Test]
        public void Should_have_the_same_number_of_every_column()
        {
            var counts = _fragment.SelectMany(x => x).Select(x => x.Count()).ToArray();
            var maxColumnCountA = counts.Max();
            var minColumnCountB = counts.Min();

            var fragment = GetDelimitedFragment(_textRef);

            Stopwatch timer = Stopwatch.StartNew();
            var counts2 = fragment.SelectMany(x => x).Select(x => x.Count()).ToArray();
            var maxColumnCount = counts2.Max();
            timer.Stop();

            Stopwatch timer2 = Stopwatch.StartNew();
            var minColumnCount = counts2.Min();
            timer2.Stop();

            Assert.AreEqual(minColumnCount, maxColumnCount);

            Console.WriteLine("Max: {0}ms", timer.ElapsedMilliseconds);
            Console.WriteLine("Min: {0}ms", timer2.ElapsedMilliseconds);
        }

        [Test]
        public void Should_quickly_get_the_first_column()
        {
            var counts = _fragment.Select(x => x.First()).Count();

            var fragment = GetDelimitedFragment(_textRef);

            Stopwatch timer = Stopwatch.StartNew();
            var counts2 = fragment.Select(x => x.First()).Count();
            timer.Stop();


            Console.WriteLine("Max: {0}ms", timer.ElapsedMilliseconds);

            Console.WriteLine("Rows per second: {0}", counts2 * 1000L / timer.ElapsedMilliseconds);
        }

        [Test]
        public void Should_quickly_get_the_last_column()
        {
            var counts = _fragment.Select(x => x.Last()).ToArray();

            var fragment = GetDelimitedFragment(_textRef);

            Stopwatch timer = Stopwatch.StartNew();
            var counts2 = fragment.Select(x => x.Last()).ToArray();
            timer.Stop();


            Console.WriteLine("Max: {0}ms", timer.ElapsedMilliseconds);

            Console.WriteLine("Rows per second: {0}", counts2.Length * 1000L / timer.ElapsedMilliseconds);
        }
    }
}