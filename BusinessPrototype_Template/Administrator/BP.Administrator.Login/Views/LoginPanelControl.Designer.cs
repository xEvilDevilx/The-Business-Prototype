namespace BP.Administrator.Login.Views
{
    partial class LoginPanelControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLoginPanel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.NavajoWhite;
            this.panel1.Controls.Add(this.lblLoginPanel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 65);
            this.panel1.TabIndex = 0;
            // 
            // lblLoginPanel
            // 
            this.lblLoginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(144)))), ((int)(((byte)(25)))));
            this.lblLoginPanel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoginPanel.ForeColor = System.Drawing.Color.NavajoWhite;
            this.lblLoginPanel.Location = new System.Drawing.Point(0, 0);
            this.lblLoginPanel.Name = "lblLoginPanel";
            this.lblLoginPanel.Size = new System.Drawing.Size(439, 65);
            this.lblLoginPanel.TabIndex = 0;
            this.lblLoginPanel.Text = "Business Prototype: Administrator";
            this.lblLoginPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.Controls.Add(this.panel1);
            this.Name = "LoginPanelControl";
            this.Size = new System.Drawing.Size(439, 305);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLoginPanel;
    }
}
