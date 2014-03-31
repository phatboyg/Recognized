namespace Recognized.Fragments
{
    /// <summary>
    ///     A fragment provider that goes in front of another and skips empty text
    /// </summary>
    public class SkipEmptyFragmentFactory :
        IFragmentFactory
    {
        readonly IFragmentFactory _fragmentFactory;

        public SkipEmptyFragmentFactory(IFragmentFactory fragmentFactory)
        {
            _fragmentFactory = fragmentFactory;
        }

        public bool TryCreateFragment(TextRef text, out Fragment fragment)
        {
            if (text.Count == 0)
            {
                fragment = default(Fragment);
                return false;
            }

            return _fragmentFactory.TryCreateFragment(text, out fragment);
        }
    }
}