using System.Drawing;

namespace BP.Point.App
{
    public class GreenTheme : IThemeColorTable
    {
        public Color CLabelBackColor { get; private set; } = Color.FromArgb(92, 176, 93);
        public Color CLabelForeColor { get; private set; } = Color.White;
        public Color CLabelOutlineForeColor { get; private set; } = Color.FromArgb(42, 126, 43);

        public Color ImageBackColor { get; private set; } = Color.FromArgb(102, 186, 103);

        public Color LabelBackColor { get; private set; } = Color.FromArgb(102, 186, 103);
    }
}