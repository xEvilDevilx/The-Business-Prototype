using System.Drawing;

namespace BP.Point.App
{
    public class YellowTheme : IThemeColorTable
    {
        public Color CLabelBackColor { get; private set; } = Color.FromArgb(255, 171, 18); // 251, 191, 45
        public Color CLabelForeColor { get; private set; } = Color.White;
        public Color CLabelOutlineForeColor { get; private set; } = Color.FromArgb(202, 121, 0); // 201, 141, 0

        public Color ImageBackColor { get; private set; } = Color.FromArgb(255, 186, 33); // 255, 201, 55

        public Color LabelBackColor { get; private set; } = Color.FromArgb(255, 186, 33);
    }
}