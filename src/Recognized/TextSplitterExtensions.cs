namespace Recognized
{
    using System;
    using System.Collections.Generic;


    public static class TextSplitterExtensions
    {
        public static IList<StringCursor> ToList(this ITextSplitter splitter)
        {
            var results = new List<StringCursor>();
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                results.Add(text);
            }

            return results;
        }

        public static IList<StringCursor> ToList(this ITextSplitter splitter, Func<StringCursor, bool> filter)
        {
            var results = new List<StringCursor>();
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                if (filter(text))
                    results.Add(text);
            }

            return results;
        }

        public static IEnumerable<StringCursor> Where(this ITextSplitter splitter, Func<StringCursor, bool> filter)
        {
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                if (filter(text))
                    yield return text;
            }
        }


        public static IEnumerable<TResult> Select<TResult>(this ITextSplitter splitter, Func<StringCursor, TResult> projection)
        {
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                yield return projection(text);
            }
        }

        public static StringCursor First(this ITextSplitter splitter)
        {
            StringCursor text;
            if (!splitter.TryGetText(0, out text))
                throw new InvalidOperationException("Sequence contains no elements");

            return text;
        }

        public static StringCursor Last(this ITextSplitter splitter)
        {
            StringCursor result = null;
            for (int i = 0;; i++)
            {
                StringCursor text;
                if (!splitter.TryGetText(i, out text))
                    break;

                result = text;
            }

            if (result == null)
                throw new InvalidOperationException("Sequence contains no elements");

            return result;
        }

        public static IList<string> ToList(this IStringSplitter splitter)
        {
            var results = new List<string>();
            for (int i = 0;; i++)
            {
                string text;
                if (!splitter.TryGetText(i, out text))
                    break;

                results.Add(text);
            }

            return results;
        }
    }
}