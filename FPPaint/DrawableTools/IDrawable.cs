using System.Windows;

namespace FPPaint
{
    /// <summary>
    /// Interface of Drawing Tools objects IDrawable
    /// </summary>
    public interface IDrawable
    {
        void Draw(Point coordinates);
    }
}
