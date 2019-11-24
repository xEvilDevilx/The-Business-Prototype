using System;
using System.Windows.Forms;

using BP.Visual.Forms;

namespace BP.Administrator.App
{
    public partial class MainForm : RibbonForm
    {
        // private IPluginManager _pluginManager;

        public MainForm()
        {
            PreStart();

            InitializeComponent();
        }

        private void PreStart()
        {
            ShowSplash();
            PreLoadLoginData();
        }

        private void ShowSplash()
        {
            try
            {
                // 1. Load SplashScreen implementation
                // 2. Prepare Load Statistics data, prepare status string for SplashScreen
                // 3. Load an app version, database information and set it to a SplashScreen and to the Statusbar
                // 4. Show SplashScreen
                // 5. Show statistics in the SplashScreen
                // 6. When all loaded we should Hide SplashScreen and exit from method
            }
            catch(Exception ex)
            {
                // Log
                // Show Message
                // Close app after user will close error message
            }
        }

        private void PreLoadLoginData()
        {
            try
            {
                // 1. Load LoginPanel implementation
                // 2. Load Config data from local config file and next we should to set a config data to a LoginPanel,
                // if file is empty or file isn't exists, we should to create a config file and set to LoginPanel a default data
                // 3. Show a LoginPanel control(method 'Show')
            }
            catch (Exception ex)
            {
                // Log
                // Show Message
                // Close app after user will close error message
            }
        }

        private void frmMainDemo_Load(object sender, EventArgs e)
        {
            //btForm1_Click(null, null);
        }

        private void btForm1_Click(object sender, EventArgs e)
        {
            //OpenForm(typeof(frmForm1));
        }

        private void btForm2_Click(object sender, EventArgs e)
        {
            //OpenForm(typeof(frmForm2));
        }

        private void btForm3_Click(object sender, EventArgs e)
        {
            //OpenForm(typeof(frmForm3));
            CloseForms();
        }

        private void OpenForm(Type t)
        {
            CloseForms();

            Form f = (Form)Activator.CreateInstance(t);
            f.FormBorderStyle = FormBorderStyle.None;
            f.TopLevel = false;
            f.Parent = panel1;
            f.WindowState = FormWindowState.Maximized;
            f.Show();

            GC.Collect();
        }

        private void CloseForms()
        {
            foreach (var c in panel1.Controls)
            {
                if (c is Form form)
                {
                    form.Close();
                    form.Dispose();
                }
            }
        }
    }
}