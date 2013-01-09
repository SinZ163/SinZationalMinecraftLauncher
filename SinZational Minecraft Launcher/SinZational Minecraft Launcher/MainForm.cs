using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    public partial class MainForm : Form {

        private LastLogin lastLogin;

        public String rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".sinzationalminecraft" + Path.DirectorySeparatorChar);
        public String path;

        public String username;
        public String sessionID;

        public MainForm() {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e) {
            
            Login login = new Login(userText.Text, passText.Text);
            String username = login.username;
            String sessionID = login.sessionID;
            if (sessionID != "-") { //TODO: offline-mode support
                if (rememberBox.Checked) {
                    lastLogin.SetLastLogin(userText.Text, passText.Text);
                }
                if (updateBox.Checked) {
                    //TODO: Check modpack download server here


                    //TODO: Download stuff here
                    //DownloadLWJGL lwjgl = new DownloadLWJGL(this, path);

                    //TODO: Install stuff here
                }
                Environment.SetEnvironmentVariable("APPDATA", rootPath);
                Launch launch = new Launch(path, username, sessionID, consoleBox.Checked);
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            path = Path.Combine(rootPath, ".minecraft", "bin" + Path.DirectorySeparatorChar);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);

            lastLogin = new LastLogin();
            String[] loginInfo = lastLogin.GetLastLogin();
            userText.Text = loginInfo[0];
            passText.Text = loginInfo[1];
        }


        //TODO: Do somewhere else
        public void SetProgressBar(object sender, DownloadProgressChangedEventArgs e) {
            //TODO: Show progress bar, somehow
            progressBar.Value = e.ProgressPercentage;

        }
        public void SetTask(String taskName) {
            //TODO: Show progress task, somehow
            progressLabel.Text = taskName;
        }
    }
}
