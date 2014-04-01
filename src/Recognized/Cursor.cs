namespace Recognized
{
    /// <summary>
    ///     A cursor references an input position
    /// </summary>
    /// <typeparam name="T">The input type</typeparam>
    public interface Cursor<out T>
    {
        /// <summary>
        ///     The offset into the input where the cursor is pointing
        /// </summary>
        int Offset { get; }

        /// <summary>
        ///     The number of input elements available using the cursor
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     The input data (which should only be accessed within the offset/count range
        /// </summary>
        T Data { get; }

        /// <summary>
        /// The input being traversed (this is used to avoid copying memory)
        /// </summary>
//        Input<T> Input { get; }
    }


    public static class Cursor
    {
        public static Cursor<T> Empty<T>()
        {
            return CursorCache<T>.EmptyCursor;
        }


        static class CursorCache<T>
        {
            public static readonly Cursor<T> EmptyCursor = new Empty();


            class Empty :
                Cursor<T>
            {
                //              Input<T> _input;

                public int Offset
                {
                    get { return 0; }
                }

                public int Count
                {
                    get { return 0; }
                }

                public T Data
                {
                    get { return default(T); }
                }

                //            public Input<T> Input
                //          {
                //            get { return _input; }
                //      }
            }
        }
    }
}