using BP.Point.Data.Layout;
using BP.SDK.Interfaces.Serialize;
using BP.SDK.Serialize;
using BP.Visual.Base.Enums;
using BP.Visual.Components;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace BP.Point.App
{
    /// <summary>
    /// Implementation of the Layout Manager functionality
    /// 
    /// 2019/01/25 - Created, VTyagunov
    /// </summary>
    public class LayoutManager
    {
        #region Variables

        private PointLayoutData _layout;
        private ControlCollection _controls;
        private int _currentAreaID;

        #endregion

        #region Properties

        public int CurrentAreaID
        {
            get { return _currentAreaID; }
            private set
            {
                LastAreaID = _currentAreaID;
                _currentAreaID = value;
            }
        }
        public int LastAreaID { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controls">Control Collection</param>
        /// <param name="filePath">Path to layout(.lay) file</param>
        public LayoutManager(ControlCollection controls, string filePath)
        {
            if (controls == null)
                throw new ArgumentNullException("controls");

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("There is no layout(.lay) file in the root app folder!");

            _controls = controls;
            _currentAreaID = 0;
            LastAreaID = 0;

            Load(filePath);
        }

        #endregion

        #region Methods

        private void Load(string filePath)
        {
            using (IFileManager fileManager = new FileManager())
            {
                _layout = fileManager.ReadData<PointLayoutData>(filePath);
            }

            if (_layout.Areas == null || _layout.Areas.Count < 0)
                throw new Exception("There are no Areas in the layout file!");

            LoadArea(_layout.Areas[CurrentAreaID]);
        }

        private void ClearArea(PointLayoutArea area)
        {
            foreach (var item in area.Items)
            {
                _controls.RemoveByKey(item.ItemName);
            }
        }

        private void LoadArea(PointLayoutArea area)
        {            
            foreach (var item in area.Items)
            {
                switch (item.ItemType)
                {
                    case ComponentTypes.Label:
                        var lbl = CreateLabel(item);
                        _controls.Add(lbl);
                        break;
                    case ComponentTypes.CLabel:
                        var clbl = CreateCLabel(item);
                        _controls.Add(clbl);
                        break;
                    case ComponentTypes.Button:
                        var btn = CreateButton(item);
                        _controls.Add(btn);
                        break;
                    case ComponentTypes.Image:
                        var img = CreateImage(item);
                        _controls.Add(img);
                        break;
                }
            }
            CurrentAreaID = area.AreaID;
        }

        private IThemeColorTable GetThemeColorTable(Theme theme)
        {
            IThemeColorTable colorTable;
            switch (theme)
            {
                case Theme.Blue:
                    colorTable = new BlueTheme();
                    break;
                case Theme.Green:
                    colorTable = new GreenTheme();
                    break;
                case Theme.Pink:
                    colorTable = new PinkTheme();
                    break;
                case Theme.Publisher:
                    colorTable = new BlueTheme();
                    break;
                case Theme.Red:
                    colorTable = new RedTheme();
                    break;
                case Theme.White:
                    colorTable = new BlueTheme();
                    break;
                case Theme.Yellow:
                    colorTable = new YellowTheme();
                    break;
                default:
                    colorTable = new BlueTheme();
                    break;
            }

            return colorTable;
        }

        private CButton CreateButton(PointLayoutItem item)
        {
            var btn = new CButton()
            {
                Font = item.PointFont,
                Location = item.PointLocation,
                Text = item.Text,
                Size = item.PointSize,
                Theme = item.Theme,
                TabIndex = 1
            };

            btn.Name = item.ItemName;
            
            switch(item.OperationID)
            {
                case 1:
                    if (item.ItemObject != null)
                        btn.Click += (sender, e) => NextAreaBtn_Click(sender, e, ((PointLayoutButton)item.ItemObject).LoadAreaID);
                    break;
                case 2:
                    btn.Click += (sender, e) => NextAreaBtn_Click(sender, e, LastAreaID);
                    break;
            }
            
            return btn;
        }

        private Label CreateLabel(PointLayoutItem item)
        {
            var lbl = new Label()
            {
                BackColor = Color.FromArgb(100, 153, 153, 255),
                ForeColor = Color.White,
                Font = item.PointFont,
                Location = item.PointLocation,
                Text = item.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = item.PointSize,
                TabIndex = 0
            };
            lbl.Name = item.ItemName;
            SetLabelColorTheme(lbl, item.Theme);
            return lbl;
        }

        private void SetLabelColorTheme(Label lbl, Theme theme)
        {
            IThemeColorTable colorTable = GetThemeColorTable(theme);

            if (colorTable == null)
                throw new Exception("IThemeColorTable is null!");

            lbl.BackColor = colorTable.LabelBackColor;
        }

        private CLabel CreateCLabel(PointLayoutItem item)
        {
            var lbl = new CLabel()
            {                               
                OutlineAlignment = ((PointLayoutCLabel)item.ItemObject).OutlineAlignment,
                OutlineLineAlignment = ((PointLayoutCLabel)item.ItemObject).OutlineLineAlignment,
                OutlineWidth = ((PointLayoutCLabel)item.ItemObject).OutlineWidth,
                Font = item.PointFont,
                Location = item.PointLocation,
                Text = item.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = item.PointSize,
                TabIndex = 0
            };
            lbl.Name = item.ItemName;

            if (item.Theme == Theme.None)
            {
                lbl.BackColor = item.BackColor;
                lbl.ForeColor = ((PointLayoutCLabel)item.ItemObject).ForeColor;
                lbl.OutlineForeColor = ((PointLayoutCLabel)item.ItemObject).OutlineForeColor;
            }
            else
            {
                SetCLabelColorTheme(lbl, item.Theme);
            }

            return lbl;
        }

        private void SetCLabelColorTheme(CLabel lbl, Theme theme)
        {
            IThemeColorTable colorTable = GetThemeColorTable(theme);

            if (colorTable == null)
                throw new Exception("IThemeColorTable is null!");

            lbl.BackColor = colorTable.CLabelBackColor;
            lbl.ForeColor = colorTable.CLabelForeColor;
            lbl.OutlineForeColor = colorTable.CLabelOutlineForeColor;
        }

        private PictureBox CreateImage(PointLayoutItem item)
        {
            var img = new PictureBox()
            {
                Font = item.PointFont,
                Image = item.Image,
                SizeMode = item.ImageSizeMode,
                Location = item.PointLocation,
                Text = item.ItemName,
                Size = item.PointSize,
                TabIndex = 0
            };
            img.Name = item.ItemName;
            SetImageColorTheme(img, item.Theme);            

            return img;
        }

        private void SetImageColorTheme(PictureBox img, Theme theme)
        {
            IThemeColorTable colorTable = GetThemeColorTable(theme);

            if (colorTable == null)
                throw new Exception("IThemeColorTable is null!");

            img.BackColor = colorTable.ImageBackColor;            
        }

        #region Events

        private void NextAreaBtn_Click(object sender, EventArgs e, int areaID)
        {
            if (_layout.Areas.Count <= areaID || areaID < 0)
            {
                MessageBox.Show($"Warning! There is no Area ID = '{areaID}'");
                return;
            }

            ClearArea(_layout.Areas[CurrentAreaID]);
            LoadArea(_layout.Areas[areaID]);
        }

        #endregion

        #endregion
    }
}