using System;
using System.Drawing;
using System.Windows.Forms;

using BP.Visual.Base.Enums;

namespace BP.Point.Data.Layout
{
    /// <summary>
    /// Presents Point Layout Item data object
    /// 
    /// 2019/01/22 - Created, VTyagunov 
    /// </summary>
    [Serializable]
    public class PointLayoutItem
    {
        public int ItemID { get; set; }
        public ComponentTypes ItemType { get; set; }
        public string ItemName { get; set; }
        public string Text { get; set; }
        public int OperationID { get; set; }
        public System.Drawing.Point PointLocation { get; set; }
        public Size PointSize { get; set; }
        public Font PointFont { get; set; }
        public Color BackColor { get; set; }
        public Image Image { get; set; }
        public PictureBoxSizeMode ImageSizeMode { get; set; }
        public object ItemObject { get; set; }
        public Theme Theme { get; set; }
    }
}