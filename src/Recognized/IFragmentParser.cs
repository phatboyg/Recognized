namespace Recognized
{
    public interface IFragmentParser
    {
        bool TryGetFragment(int index, out Fragment fragment);
    }
}