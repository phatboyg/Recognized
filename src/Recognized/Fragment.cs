namespace Recognized
{
    using System.Collections.Generic;


    public interface Fragment :
        TextRef,
        IEnumerable<Fragment>
    {
        /// <summary>
        ///     Return the fragment at the specified index, if present
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fragment"></param>
        /// <returns></returns>
        bool TryGetFragment(int index, out Fragment fragment);
    }
}