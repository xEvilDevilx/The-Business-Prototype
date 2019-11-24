using System;
using System.Collections.Generic;

namespace BP.Point.Data.Layout
{
    /// <summary>
    /// Presents Point Layout data object
    /// 
    /// 2019/01/22 - Created, VTyagunov 
    /// </summary>
    [Serializable]
    public class PointLayoutData
    {
        public int LayoutID { get; set; }
        public string LayoutName { get; set; }
        public List<PointLayoutArea> Areas { get; set; }
    }
}