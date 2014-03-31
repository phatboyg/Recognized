namespace Recognized.Fragments
{
    using System.Collections.Generic;


    /// <summary>
    ///     Simply returns parsed text as a parsed fragment that cannot be further parsed
    /// </summary>
    public class ParsedFragmentProvider :
        IFragmentProvider
    {
        public IEnumerable<Fragment> GetFragments(TextRef text)
        {
            yield return new ParsedFragment(text);
        }
    }
}