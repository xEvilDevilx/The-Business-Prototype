using System.Drawing;

namespace BP.Point.App
{
    public class RedTheme : IThemeColorTable
    {
        public Color CLabelBackColor { get; private set; } = Color.FromArgb(237, 87, 55);
        public Color CLabelForeColor { get; private set; } = Color.White;
        public Color CLabelOutlineForeColor { get; private set; } = Color.FromArgb(187, 37, 5);

        public Color ImageBackColor { get; private set; } = Color.FromArgb(247, 97, 65);

        public Color LabelBackColor { get; private set; } = Color.FromArgb(247, 97, 65);
    }
}