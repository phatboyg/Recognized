namespace Recognized.Fragments
{
    using System.Collections.Generic;


    /// <summary>
    ///     Simply returns parsed text as a parsed fragment that cannot be further parsed
    /// </summary>
    public class ParsedFragmentFactory :
        IFragmentFactory
    {
        public IEnumerable<Fragment> CreateFragments(TextRef text)
        {
            yield return new ParsedFragment(text);
        }
    }
}