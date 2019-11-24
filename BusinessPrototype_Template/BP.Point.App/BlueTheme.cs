using System.Drawing;

namespace BP.Point.App
{
    public class BlueTheme : IThemeColorTable
    {
        public Color CLabelBackColor { get; private set; } = Color.FromArgb(255, 58, 125, 218);
        public Color CLabelForeColor { get; private set; } = Color.White;
        public Color CLabelOutlineForeColor { get; private set; } = Color.FromArgb(31, 72, 161);

        public Color ImageBackColor { get; private set; } = Color.FromArgb(255, 68, 135, 228);

        public Color LabelBackColor { get; private set; } = Color.FromArgb(255, 68, 135, 228);
    }
}