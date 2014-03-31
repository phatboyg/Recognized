namespace Recognized
{
    public interface IFragmentFactory
    {
        bool TryCreateFragment(TextRef text, out Fragment fragment);
    }
}