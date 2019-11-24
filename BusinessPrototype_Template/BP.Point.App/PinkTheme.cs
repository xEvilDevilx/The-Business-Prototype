using System.Drawing;

namespace BP.Point.App
{
    public class PinkTheme : IThemeColorTable
    {
        public Color CLabelBackColor { get; private set; } = Color.FromArgb(195, 26, 97);
        public Color CLabelForeColor { get; private set; } = Color.White;
        public Color CLabelOutlineForeColor { get; private set; } = Color.FromArgb(145, 0, 47);

        public Color ImageBackColor { get; private set; } = Color.FromArgb(205, 36, 107);

        public Color LabelBackColor { get; private set; } = Color.FromArgb(205, 36, 107);
    }
}