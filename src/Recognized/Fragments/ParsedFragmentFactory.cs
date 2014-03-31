namespace Recognized.Fragments
{
    /// <summary>
    ///     Simply returns parsed text as a parsed fragment that cannot be further parsed
    /// </summary>
    public class ParsedFragmentFactory :
        IFragmentFactory
    {
        public bool TryCreateFragment(TextRef text, out Fragment fragment)
        {
            fragment = new ParsedFragment(text);
            return true;
        }
    }
}