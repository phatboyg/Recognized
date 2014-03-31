namespace Recognized
{
    using System.Collections.Generic;


    public interface IFragmentFactory
    {
        IEnumerable<Fragment> CreateFragments(TextRef text);
    }
}