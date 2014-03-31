namespace Recognized.Fragments
{
    using System.Collections.Generic;
    using System.Linq;


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

        public IEnumerable<Fragment> CreateFragments(TextRef text)
        {
            if (text.Count == 0)
                return Enumerable.Empty<Fragment>();

            return _fragmentFactory.CreateFragments(text);
        }
    }
}