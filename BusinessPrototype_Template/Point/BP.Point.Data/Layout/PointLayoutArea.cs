using System;
using System.Collections.Generic;
using System.Drawing;

namespace BP.Point.Data.Layout
{
    /// <summary>
    /// Presents Point Layout Area data object
    /// 
    /// 2019/01/22 - Created, VTyagunov 
    /// </summary>
    [Serializable]
    public class PointLayoutArea
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public System.Drawing.Point AreaLocation { get; set; }
        public Size AreaSize { get; set; }
        public List<PointLayoutItem> Items { get; set; }
    }
}