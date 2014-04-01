namespace Recognized.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;
    using Parsing;


    [TestFixture, Explicit]
    public class NFL_Specs
    {
        StringStringCursor _stringCursor;
        string _text;
        const string FilePath = @"d:\temp\2012_nfl_pbp_data.csv";

        [TestFixtureSetUp]
        public void Setup()
        {
            _text = File.ReadAllText(FilePath);

            _stringCursor = new StringStringCursor(_text);
        }

        [Test]
        public void Should_get_the_columns_for_each_line()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            StringCursor text;

            Assert.IsTrue(splitter.TryGetText(0, out text));

            var columnSplitter = new DelimiterTextSplitter(text, ',');

            var columns = columnSplitter.ToList();

            Assert.AreEqual(13, columns.Count);

            Assert.AreEqual("gameid", columns[0].String);
            Assert.AreEqual("qtr", columns[1].String);
            Assert.AreEqual("min", columns[2].String);
            Assert.AreEqual("sec", columns[3].String);
            Assert.AreEqual("off", columns[4].String);
            Assert.AreEqual("def", columns[5].String);
            Assert.AreEqual("down", columns[6].String);
            Assert.AreEqual("togo", columns[7].String);
            Assert.AreEqual("ydline", columns[8].String);
            Assert.AreEqual("description", columns[9].String);
            Assert.AreEqual("offscore", columns[10].String);
            Assert.AreEqual("defscore", columns[11].String);
            Assert.AreEqual("season", columns[12].String);
        }

        [Test]
        public void Should_have_the_same_number_of_every_column()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            var lines = splitter.ToList();

            var counts = lines
                .Select(x => new DelimiterTextSplitter(x, ',').ToList())
                .Select(x => x.Count).ToList();
            var maxColumnCountA = counts.Max();
            var minColumnCountB = counts.Min();

            Stopwatch timer = Stopwatch.StartNew();
            splitter = new SeparatorTextSplitter(_stringCursor);
            lines = splitter.ToList(x => x.Count > 0);

            var counts2 = lines
                .Select(x => new DelimiterTextSplitter(x, ',').ToList())
                .Select(x => x.Count).ToList();
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
        public void Should_parse_all_lines()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            var lines = splitter.ToList();

            Stopwatch timer = Stopwatch.StartNew();
            splitter = new SeparatorTextSplitter(_stringCursor);
            lines = splitter.ToList();
            timer.Stop();

            Console.WriteLine("Line Count: {0}, Time: {1}ms", lines.Count, timer.ElapsedMilliseconds);

            Assert.AreEqual(44855, lines.Count);
        }

        [Test]
        public void Should_parse_all_lines_and_columns()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                var columnSplitter = new DelimiterTextSplitter(text, ',');

                var columns = columnSplitter.ToList();
            }

            Stopwatch timer = Stopwatch.StartNew();
            int count = 0;
            for (int loop = 0; loop < 10; loop++)
            {
                splitter = new SeparatorTextSplitter(_stringCursor);
                for (count = 0;; count++)
                {
                    StringCursor text;
                    if (!splitter.TryGetText(count, out text))
                        break;

                    var columnSplitter = new DelimiterTextSplitter(text, ',');

                    var columns = columnSplitter.ToList();
                }
            }
            timer.Stop();

            Console.WriteLine("Line Count: {0}, Time: {1}ms", count, timer.ElapsedMilliseconds / 10);

            Assert.AreEqual(44855, count);
        }

        [Test]
        public void Should_parse_all_lines_and_columns_as_strings()
        {
            var splitter = new SeparatorStringSplitter(_text);
            for (int i = 0;; i++)
            {
                string text;
                if (!splitter.TryGetText(i, out text))
                    break;

                var columnSplitter = new DelimiterStringSplitter(text, ',');

                var columns = columnSplitter.ToList();
            }

            Stopwatch timer = Stopwatch.StartNew();
            splitter = new SeparatorStringSplitter(_text);
            int count;
            for (count = 0;; count++)
            {
                string text;
                if (!splitter.TryGetText(count, out text))
                    break;

                var columnSplitter = new DelimiterStringSplitter(text, ',');

                var columns = columnSplitter.ToList();
            }
            timer.Stop();

            Console.WriteLine("Line Count: {0}, Time: {1}ms", count, timer.ElapsedMilliseconds);

            Assert.AreEqual(44855, count);
        }

        [Test]
        public void Should_quickly_get_the_first_column()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            var firstColumns = splitter
                .Where(x => x.Count > 0)
                .Select(x => new DelimiterTextSplitter(x, '.').First()).ToList();

            Stopwatch timer = Stopwatch.StartNew();
            splitter = new SeparatorTextSplitter(_stringCursor);
            firstColumns = splitter
                .Where(x => x.Count > 0)
                .Select(x => new DelimiterTextSplitter(x, '.').First()).ToList();
            timer.Stop();

            Console.WriteLine("Elapsed: {0}ms", timer.ElapsedMilliseconds);

            Console.WriteLine("Rows per second: {0}", firstColumns.Count * 1000L / timer.ElapsedMilliseconds);
        }

        [Test]
        public void Should_quickly_get_the_last_column()
        {
            var splitter = new SeparatorTextSplitter(_stringCursor);
            var firstColumns = splitter
                .Where(x => x.Count > 0)
                .Select(x => new DelimiterTextSplitter(x, '.').Last()).ToList();

            Stopwatch timer = Stopwatch.StartNew();
            splitter = new SeparatorTextSplitter(_stringCursor);
            firstColumns = splitter
                .Where(x => x.Count > 0)
                .Select(x => new DelimiterTextSplitter(x, '.').Last()).ToList();
            timer.Stop();

            Console.WriteLine("Elapsed: {0}ms", timer.ElapsedMilliseconds);

            Console.WriteLine("Rows per second: {0}", firstColumns.Count * 1000L / timer.ElapsedMilliseconds);
        }
    }
}