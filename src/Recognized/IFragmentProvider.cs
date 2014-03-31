namespace Recognized
{
    using System.Collections.Generic;


    public interface IFragmentProvider
    {
        IEnumerable<Fragment> GetFragments(TextRef text);
    }
}