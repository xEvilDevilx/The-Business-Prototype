using System.Windows.Forms;

using BP.Visual.Forms.Components;

namespace BP.Administrator.App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainRibbon = new BP.Visual.Forms.Components.Ribbon();
            this.ribbonContext1 = new BP.Visual.Forms.Components.RibbonContext();
            this.ribbonContext2 = new BP.Visual.Forms.Components.RibbonContext();
            this.ribbonOrbMenuItem9 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem10 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem11 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem12 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonButton15 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton16 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton17 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton5 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton2 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonTab1 = new BP.Visual.Forms.Components.RibbonTab();
            this.ribbonPanel3 = new BP.Visual.Forms.Components.RibbonPanel();
            this.btForm1 = new BP.Visual.Forms.Components.RibbonButton();
            this.btForm2 = new BP.Visual.Forms.Components.RibbonButton();
            this.btForm3 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton6 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton7 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonPanel1 = new BP.Visual.Forms.Components.RibbonPanel();
            this.ribbonButton1 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonTab2 = new BP.Visual.Forms.Components.RibbonTab();
            this.ribbonTab3 = new BP.Visual.Forms.Components.RibbonTab();
            this.ribbonTab4 = new BP.Visual.Forms.Components.RibbonTab();
            this.ribbonOrbMenuItem1 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem2 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem3 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem4 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem5 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem6 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonSeparator1 = new BP.Visual.Forms.Components.RibbonSeparator();
            this.ribbonOrbMenuItem7 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbMenuItem8 = new BP.Visual.Forms.Components.RibbonOrbMenuItem();
            this.ribbonOrbRecentItem1 = new BP.Visual.Forms.Components.RibbonOrbRecentItem();
            this.ribbonOrbRecentItem2 = new BP.Visual.Forms.Components.RibbonOrbRecentItem();
            this.ribbonOrbRecentItem3 = new BP.Visual.Forms.Components.RibbonOrbRecentItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ribbonButton3 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonButton4 = new BP.Visual.Forms.Components.RibbonButton();
            this.ribbonDescriptionMenuItem1 = new BP.Visual.Forms.Components.RibbonDescriptionMenuItem();
            this.loginPanelControl1 = new BP.Administrator.Login.Views.LoginPanelControl();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainRibbon
            // 
            this.mainRibbon.BorderMode = BP.Visual.Forms.Enums.RibbonWindowModeTypes.NonClientAreaCustomDrawn;
            this.mainRibbon.Contexts.Add(this.ribbonContext1);
            this.mainRibbon.Contexts.Add(this.ribbonContext2);
            this.mainRibbon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mainRibbon.Location = new System.Drawing.Point(1, 1);
            this.mainRibbon.Minimized = false;
            this.mainRibbon.Name = "mainRibbon";
            // 
            // 
            // 
            this.mainRibbon.OrbDropDown.AllowDrop = true;
            this.mainRibbon.OrbDropDown.BorderRoundness = 8;
            this.mainRibbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.mainRibbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItem9);
            this.mainRibbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItem10);
            this.mainRibbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItem11);
            this.mainRibbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItem12);
            this.mainRibbon.OrbDropDown.Name = "";
            this.mainRibbon.OrbDropDown.OptionItems.Add(this.ribbonButton15);
            this.mainRibbon.OrbDropDown.OptionItems.Add(this.ribbonButton16);
            this.mainRibbon.OrbDropDown.OptionItems.Add(this.ribbonButton17);
            this.mainRibbon.OrbDropDown.RecentItems.Add(this.ribbonButton5);
            this.mainRibbon.OrbDropDown.Size = new System.Drawing.Size(527, 224);
            this.mainRibbon.OrbDropDown.TabIndex = 0;
            this.mainRibbon.OrbStyle = BP.Visual.Forms.Enums.RibbonOrbStyleTypes.BP_2018;
            this.mainRibbon.OrbText = "File";
            // 
            // 
            // 
            this.mainRibbon.QuickAccessToolbar.Items.Add(this.ribbonButton2);
            this.mainRibbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.mainRibbon.Size = new System.Drawing.Size(1257, 162);
            this.mainRibbon.TabIndex = 0;
            this.mainRibbon.Tabs.Add(this.ribbonTab1);
            this.mainRibbon.Tabs.Add(this.ribbonTab2);
            this.mainRibbon.Tabs.Add(this.ribbonTab3);
            this.mainRibbon.Tabs.Add(this.ribbonTab4);
            this.mainRibbon.TabsMargin = new System.Windows.Forms.Padding(22, 26, 20, 0);
            this.mainRibbon.TabSpacing = 1;
            this.mainRibbon.Text = "ribbon1";
            // 
            // ribbonContext1
            // 
            this.ribbonContext1.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ribbonContext1.Text = "3";
            this.ribbonContext1.Visible = true;
            // 
            // ribbonContext2
            // 
            this.ribbonContext2.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ribbonContext2.Text = "4";
            this.ribbonContext2.Visible = true;
            // 
            // ribbonOrbMenuItem9
            // 
            this.ribbonOrbMenuItem9.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem9.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem9.Image")));
            this.ribbonOrbMenuItem9.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem9.LargeImage")));
            this.ribbonOrbMenuItem9.Name = "ribbonOrbMenuItem9";
            this.ribbonOrbMenuItem9.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem9.SmallImage")));
            this.ribbonOrbMenuItem9.Text = "ribbonOrbMenuItem9";
            this.ribbonOrbMenuItem9.Visible = false;
            // 
            // ribbonOrbMenuItem10
            // 
            this.ribbonOrbMenuItem10.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem10.Enabled = false;
            this.ribbonOrbMenuItem10.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem10.Image")));
            this.ribbonOrbMenuItem10.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem10.LargeImage")));
            this.ribbonOrbMenuItem10.Name = "ribbonOrbMenuItem10";
            this.ribbonOrbMenuItem10.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem10.SmallImage")));
            this.ribbonOrbMenuItem10.Text = "ribbonOrbMenuItem10";
            // 
            // ribbonOrbMenuItem11
            // 
            this.ribbonOrbMenuItem11.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem11.Image")));
            this.ribbonOrbMenuItem11.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem11.LargeImage")));
            this.ribbonOrbMenuItem11.Name = "ribbonOrbMenuItem11";
            this.ribbonOrbMenuItem11.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem11.SmallImage")));
            this.ribbonOrbMenuItem11.Text = "ribbonOrbMenuItem11";
            // 
            // ribbonOrbMenuItem12
            // 
            this.ribbonOrbMenuItem12.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem12.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem12.Image")));
            this.ribbonOrbMenuItem12.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem12.LargeImage")));
            this.ribbonOrbMenuItem12.Name = "ribbonOrbMenuItem12";
            this.ribbonOrbMenuItem12.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem12.SmallImage")));
            this.ribbonOrbMenuItem12.Text = "ribbonOrbMenuItem12";
            // 
            // ribbonButton15
            // 
            this.ribbonButton15.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton15.Image")));
            this.ribbonButton15.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton15.LargeImage")));
            this.ribbonButton15.Name = "ribbonButton15";
            this.ribbonButton15.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton15.SmallImage")));
            this.ribbonButton15.Text = "Save";
            // 
            // ribbonButton16
            // 
            this.ribbonButton16.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton16.Image")));
            this.ribbonButton16.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton16.LargeImage")));
            this.ribbonButton16.Name = "ribbonButton16";
            this.ribbonButton16.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton16.SmallImage")));
            this.ribbonButton16.Text = "New";
            // 
            // ribbonButton17
            // 
            this.ribbonButton17.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton17.Image")));
            this.ribbonButton17.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton17.LargeImage")));
            this.ribbonButton17.Name = "ribbonButton17";
            this.ribbonButton17.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton17.SmallImage")));
            this.ribbonButton17.Text = "Open";
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.Image")));
            this.ribbonButton5.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.LargeImage")));
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.SmallImage")));
            this.ribbonButton5.Text = "ribBtn5";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.Image")));
            this.ribbonButton2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.LargeImage")));
            this.ribbonButton2.MaxSizeMode = BP.Visual.Forms.Enums.RibbonElementSizeModeTypes.Compact;
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.SmallImage")));
            this.ribbonButton2.Text = "ribbonButton2";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.ribbonPanel3);
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Text = "Start";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ButtonMoreEnabled = false;
            this.ribbonPanel3.ButtonMoreVisible = false;
            this.ribbonPanel3.Enabled = false;
            this.ribbonPanel3.Items.Add(this.btForm1);
            this.ribbonPanel3.Items.Add(this.btForm2);
            this.ribbonPanel3.Items.Add(this.btForm3);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "Open Form";
            // 
            // btForm1
            // 
            this.btForm1.Image = ((System.Drawing.Image)(resources.GetObject("btForm1.Image")));
            this.btForm1.LargeImage = ((System.Drawing.Image)(resources.GetObject("btForm1.LargeImage")));
            this.btForm1.Name = "btForm1";
            this.btForm1.SmallImage = ((System.Drawing.Image)(resources.GetObject("btForm1.SmallImage")));
            this.btForm1.Text = "Form1";
            this.btForm1.Click += new System.EventHandler(this.btForm1_Click);
            // 
            // btForm2
            // 
            this.btForm2.Image = ((System.Drawing.Image)(resources.GetObject("btForm2.Image")));
            this.btForm2.LargeImage = ((System.Drawing.Image)(resources.GetObject("btForm2.LargeImage")));
            this.btForm2.Name = "btForm2";
            this.btForm2.SmallImage = ((System.Drawing.Image)(resources.GetObject("btForm2.SmallImage")));
            this.btForm2.Text = "Form2";
            this.btForm2.Click += new System.EventHandler(this.btForm2_Click);
            // 
            // btForm3
            // 
            this.btForm3.DropDownItems.Add(this.ribbonButton6);
            this.btForm3.DropDownItems.Add(this.ribbonButton7);
            this.btForm3.Image = ((System.Drawing.Image)(resources.GetObject("btForm3.Image")));
            this.btForm3.LargeImage = ((System.Drawing.Image)(resources.GetObject("btForm3.LargeImage")));
            this.btForm3.Name = "btForm3";
            this.btForm3.SmallImage = ((System.Drawing.Image)(resources.GetObject("btForm3.SmallImage")));
            this.btForm3.Style = BP.Visual.Forms.Enums.RibbonButtonStyleTypes.SplitDropDown;
            this.btForm3.Text = "Form3";
            this.btForm3.Click += new System.EventHandler(this.btForm3_Click);
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonButton6.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton6.Image")));
            this.ribbonButton6.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton6.LargeImage")));
            this.ribbonButton6.Name = "ribbonButton6";
            this.ribbonButton6.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton6.SmallImage")));
            this.ribbonButton6.Text = "01";
            // 
            // ribbonButton7
            // 
            this.ribbonButton7.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonButton7.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton7.Image")));
            this.ribbonButton7.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton7.LargeImage")));
            this.ribbonButton7.Name = "ribbonButton7";
            this.ribbonButton7.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton7.SmallImage")));
            this.ribbonButton7.Text = "02";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreEnabled = false;
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Enabled = false;
            this.ribbonPanel1.Items.Add(this.ribbonButton1);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "ribbonPanel1";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.LargeImage")));
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "TestButton";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "Ribbon Tab 2";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Context = this.ribbonContext1;
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Text = "ribbonTab3";
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Context = this.ribbonContext2;
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Text = "ribbonTab4";
            // 
            // ribbonOrbMenuItem1
            // 
            this.ribbonOrbMenuItem1.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.Image")));
            this.ribbonOrbMenuItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.LargeImage")));
            this.ribbonOrbMenuItem1.Name = "ribbonOrbMenuItem1";
            this.ribbonOrbMenuItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem1.SmallImage")));
            this.ribbonOrbMenuItem1.Text = "ribbonOrbMenuItem1";
            // 
            // ribbonOrbMenuItem2
            // 
            this.ribbonOrbMenuItem2.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem2.Image")));
            this.ribbonOrbMenuItem2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem2.LargeImage")));
            this.ribbonOrbMenuItem2.Name = "ribbonOrbMenuItem2";
            this.ribbonOrbMenuItem2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem2.SmallImage")));
            this.ribbonOrbMenuItem2.Text = "ribbonOrbMenuItem2";
            // 
            // ribbonOrbMenuItem3
            // 
            this.ribbonOrbMenuItem3.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem3.Image")));
            this.ribbonOrbMenuItem3.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem3.LargeImage")));
            this.ribbonOrbMenuItem3.Name = "ribbonOrbMenuItem3";
            this.ribbonOrbMenuItem3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem3.SmallImage")));
            this.ribbonOrbMenuItem3.Text = "ribbonOrbMenuItem3";
            // 
            // ribbonOrbMenuItem4
            // 
            this.ribbonOrbMenuItem4.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem4.Image")));
            this.ribbonOrbMenuItem4.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem4.LargeImage")));
            this.ribbonOrbMenuItem4.Name = "ribbonOrbMenuItem4";
            this.ribbonOrbMenuItem4.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem4.SmallImage")));
            this.ribbonOrbMenuItem4.Text = "ribbonOrbMenuItem4";
            // 
            // ribbonOrbMenuItem5
            // 
            this.ribbonOrbMenuItem5.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem5.Image")));
            this.ribbonOrbMenuItem5.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem5.LargeImage")));
            this.ribbonOrbMenuItem5.Name = "ribbonOrbMenuItem5";
            this.ribbonOrbMenuItem5.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem5.SmallImage")));
            this.ribbonOrbMenuItem5.Text = "ribbonOrbMenuItem5";
            // 
            // ribbonOrbMenuItem6
            // 
            this.ribbonOrbMenuItem6.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem6.Image")));
            this.ribbonOrbMenuItem6.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem6.LargeImage")));
            this.ribbonOrbMenuItem6.Name = "ribbonOrbMenuItem6";
            this.ribbonOrbMenuItem6.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem6.SmallImage")));
            this.ribbonOrbMenuItem6.Text = "ribbonOrbMenuItem6";
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // ribbonOrbMenuItem7
            // 
            this.ribbonOrbMenuItem7.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem7.Image")));
            this.ribbonOrbMenuItem7.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem7.LargeImage")));
            this.ribbonOrbMenuItem7.Name = "ribbonOrbMenuItem7";
            this.ribbonOrbMenuItem7.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem7.SmallImage")));
            this.ribbonOrbMenuItem7.Text = "ribbonOrbMenuItem7";
            // 
            // ribbonOrbMenuItem8
            // 
            this.ribbonOrbMenuItem8.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonOrbMenuItem8.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem8.Image")));
            this.ribbonOrbMenuItem8.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem8.LargeImage")));
            this.ribbonOrbMenuItem8.Name = "ribbonOrbMenuItem8";
            this.ribbonOrbMenuItem8.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbMenuItem8.SmallImage")));
            this.ribbonOrbMenuItem8.Text = "ribbonOrbMenuItem8";
            // 
            // ribbonOrbRecentItem1
            // 
            this.ribbonOrbRecentItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem1.Image")));
            this.ribbonOrbRecentItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem1.LargeImage")));
            this.ribbonOrbRecentItem1.Name = "ribbonOrbRecentItem1";
            this.ribbonOrbRecentItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem1.SmallImage")));
            this.ribbonOrbRecentItem1.Text = "ribbonOrbRecentItem1";
            // 
            // ribbonOrbRecentItem2
            // 
            this.ribbonOrbRecentItem2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem2.Image")));
            this.ribbonOrbRecentItem2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem2.LargeImage")));
            this.ribbonOrbRecentItem2.Name = "ribbonOrbRecentItem2";
            this.ribbonOrbRecentItem2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem2.SmallImage")));
            this.ribbonOrbRecentItem2.Text = "ribbonOrbRecentItem2";
            // 
            // ribbonOrbRecentItem3
            // 
            this.ribbonOrbRecentItem3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem3.Image")));
            this.ribbonOrbRecentItem3.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem3.LargeImage")));
            this.ribbonOrbRecentItem3.Name = "ribbonOrbRecentItem3";
            this.ribbonOrbRecentItem3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem3.SmallImage")));
            this.ribbonOrbRecentItem3.Text = "ribbonOrbRecentItem3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.loginPanelControl1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 163);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1257, 360);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(144)))), ((int)(((byte)(25)))));
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 336);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(1257, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 19);
            this.toolStripStatusLabel1.Text = "v1.0.0.0";
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.Checked = true;
            this.ribbonButton3.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonButton3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.Image")));
            this.ribbonButton3.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.LargeImage")));
            this.ribbonButton3.Name = "ribbonButton3";
            this.ribbonButton3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.SmallImage")));
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.Image")));
            this.ribbonButton4.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.LargeImage")));
            this.ribbonButton4.Name = "ribbonButton4";
            this.ribbonButton4.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.SmallImage")));
            // 
            // ribbonDescriptionMenuItem1
            // 
            this.ribbonDescriptionMenuItem1.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem1.DropDownArrowDirection = BP.Visual.Forms.Enums.RibbonArrowDirectionTypes.Left;
            this.ribbonDescriptionMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.Image")));
            this.ribbonDescriptionMenuItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.LargeImage")));
            this.ribbonDescriptionMenuItem1.Name = "ribbonDescriptionMenuItem1";
            this.ribbonDescriptionMenuItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.SmallImage")));
            this.ribbonDescriptionMenuItem1.Text = "ribbonDescriptionMenuItem1";
            // 
            // loginPanelControl1
            // 
            this.loginPanelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginPanelControl1.BackColor = System.Drawing.Color.NavajoWhite;
            this.loginPanelControl1.Location = new System.Drawing.Point(440, 15);
            this.loginPanelControl1.Name = "loginPanelControl1";
            this.loginPanelControl1.Size = new System.Drawing.Size(439, 305);
            this.loginPanelControl1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainRibbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.frmMainDemo_Load);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
    
        private Ribbon mainRibbon;
        private RibbonTab ribbonTab1;
        private RibbonTab ribbonTab2;
        private RibbonOrbMenuItem ribbonOrbMenuItem1;
        private RibbonOrbMenuItem ribbonOrbMenuItem2;
        private RibbonOrbMenuItem ribbonOrbMenuItem3;
        private RibbonOrbMenuItem ribbonOrbMenuItem4;
        private RibbonOrbMenuItem ribbonOrbMenuItem5;
        private RibbonOrbMenuItem ribbonOrbMenuItem6;
        private RibbonSeparator ribbonSeparator1;
        private RibbonOrbMenuItem ribbonOrbMenuItem7;
        private RibbonOrbMenuItem ribbonOrbMenuItem8;
        private RibbonButton ribbonButton15;
        private RibbonButton ribbonButton16;
        private RibbonButton ribbonButton17;
        private RibbonOrbMenuItem ribbonOrbMenuItem9;
        private RibbonOrbMenuItem ribbonOrbMenuItem10;
        private RibbonOrbMenuItem ribbonOrbMenuItem11;
        private RibbonOrbMenuItem ribbonOrbMenuItem12;
        private RibbonOrbRecentItem ribbonOrbRecentItem1;
        private RibbonOrbRecentItem ribbonOrbRecentItem2;
        private RibbonOrbRecentItem ribbonOrbRecentItem3;
        private RibbonButton btForm1;
        private RibbonButton btForm2;
        private RibbonButton btForm3;
        private Panel panel1;
        private RibbonPanel ribbonPanel1;
        private RibbonButton ribbonButton1;
        private RibbonButton ribbonButton2;
        private RibbonButton ribbonButton3;
        private RibbonButton ribbonButton4;
        private RibbonTab ribbonTab3;
        private RibbonContext ribbonContext1;
        private RibbonContext ribbonContext2;
        private RibbonTab ribbonTab4;
        private RibbonButton ribbonButton5;
        private RibbonDescriptionMenuItem ribbonDescriptionMenuItem1;
        private RibbonButton ribbonButton6;
        private RibbonButton ribbonButton7;
        private RibbonPanel ribbonPanel3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;

        #endregion

        private Login.Views.LoginPanelControl loginPanelControl1;
    }
}

