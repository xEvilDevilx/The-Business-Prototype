using BP.Point.Operations.Attributes;
using System;

namespace BP.Point.App
{
    [PointOperations(0)]
    public class ScanScreenOperations
    {
        public ScanScreenOperations()
        {

        }
        
        [PointOperationsMethod(0)]
        private void BtnShares_Click(object sender, EventArgs e)
        {
            //if (Controls.ContainsKey("btnSettings"))
            //    Controls.RemoveByKey("btnSettings");

            //if (Controls.ContainsKey("btnShares"))
            //    Controls.RemoveByKey("btnShares");

            //if (Controls.ContainsKey("lblScanBarcodePlease"))
            //    Controls.RemoveByKey("lblScanBarcodePlease");

            //LoadSharesScreen();
        }
    }
}