namespace BP.Administrator.Splash.Forms
{
    partial class SplashScreenForm
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
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblAppVersionValue = new System.Windows.Forms.Label();
            this.lblAppVersionText = new System.Windows.Forms.Label();
            this.lblDBVersionText = new System.Windows.Forms.Label();
            this.lblDBVersionValue = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCaption
            // 
            this.lblCaption.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCaption.ForeColor = System.Drawing.Color.NavajoWhite;
            this.lblCaption.Location = new System.Drawing.Point(24, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(393, 64);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Business Prototype: Administrator";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAppVersionValue
            // 
            this.lblAppVersionValue.Location = new System.Drawing.Point(102, 290);
            this.lblAppVersionValue.Name = "lblAppVersionValue";
            this.lblAppVersionValue.Size = new System.Drawing.Size(77, 19);
            this.lblAppVersionValue.TabIndex = 1;
            this.lblAppVersionValue.Text = "1.0.0.0";
            this.lblAppVersionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAppVersionText
            // 
            this.lblAppVersionText.Location = new System.Drawing.Point(12, 290);
            this.lblAppVersionText.Name = "lblAppVersionText";
            this.lblAppVersionText.Size = new System.Drawing.Size(84, 19);
            this.lblAppVersionText.TabIndex = 2;
            this.lblAppVersionText.Text = "Build version:";
            this.lblAppVersionText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDBVersionText
            // 
            this.lblDBVersionText.Location = new System.Drawing.Point(259, 290);
            this.lblDBVersionText.Name = "lblDBVersionText";
            this.lblDBVersionText.Size = new System.Drawing.Size(84, 19);
            this.lblDBVersionText.TabIndex = 4;
            this.lblDBVersionText.Text = "DB version:";
            this.lblDBVersionText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDBVersionValue
            // 
            this.lblDBVersionValue.Location = new System.Drawing.Point(349, 290);
            this.lblDBVersionValue.Name = "lblDBVersionValue";
            this.lblDBVersionValue.Size = new System.Drawing.Size(77, 19);
            this.lblDBVersionValue.TabIndex = 3;
            this.lblDBVersionValue.Text = "00001";
            this.lblDBVersionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoading.Location = new System.Drawing.Point(102, 267);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(238, 23);
            this.lblLoading.TabIndex = 5;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(144)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(441, 318);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.lblDBVersionText);
            this.Controls.Add(this.lblDBVersionValue);
            this.Controls.Add(this.lblAppVersionText);
            this.Controls.Add(this.lblAppVersionValue);
            this.Controls.Add(this.lblCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreenForm";
            this.Text = "SplashScreenForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblAppVersionValue;
        private System.Windows.Forms.Label lblAppVersionText;
        private System.Windows.Forms.Label lblDBVersionText;
        private System.Windows.Forms.Label lblDBVersionValue;
        private System.Windows.Forms.Label lblLoading;
    }
}