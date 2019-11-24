using System;
using System.Drawing;

namespace BP.Point.Data.Layout
{
    [Serializable]
    public class PointLayoutCLabel
    {
        public Color ForeColor { get; set; }
        public Color OutlineForeColor { get; set; }
        public StringAlignment OutlineAlignment { get; set; }
        public StringAlignment OutlineLineAlignment { get; set; }
        public float OutlineWidth { get; set; }
    }
}