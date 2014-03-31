namespace Recognized
{
    public static class FragmentExtensions
    {
        public static int Count(this Fragment fragment)
        {
            int i;
            for (i = 0;; i++)
            {
                Fragment result;
                if (!fragment.TryGetFragment(i, out result))
                    break;
            }

            return i;
        }
    }
}