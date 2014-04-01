namespace Recognized
{
    public interface IFragmentFactory
    {
        bool TryCreateFragment(StringCursor text, out Fragment fragment);
    }
}