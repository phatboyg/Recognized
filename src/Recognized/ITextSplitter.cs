namespace Recognized
{
    public interface ITextSplitter
    {
        bool TryGetText(int index, out StringCursor text);
    }


    public interface IStringSplitter
    {
        bool TryGetText(int index, out string text);
    }
}