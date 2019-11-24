using System.Drawing;

namespace BP.Point.App
{
    public interface IThemeColorTable
    {
        Color CLabelBackColor { get; }
        Color CLabelForeColor { get; }
        Color CLabelOutlineForeColor { get; }

        Color ImageBackColor { get; }

        Color LabelBackColor { get; }
    }
}