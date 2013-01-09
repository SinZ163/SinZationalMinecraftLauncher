using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    public partial class MainForm : Form {
        private LastLogin lastLogin;
        public String rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".sinzationalminecraft" + Path.DirectorySeparatorChar);
        public MainForm() {
            InitializeComponent();
        }

        public String username;
        public String sessionID;
        private void loginButton_Click(object sender, EventArgs e) {
            Login login = new Login(userText.Text, passText.Text);
            String username = login.username;
            String sessionID = login.sessionID;
            if (sessionID != "-") { //TODO: offline-mode support
                if (rememberBox.Checked) {
                    lastLogin.SetLastLogin(userText.Text, passText.Text);
                }
                if (updateBox.Checked) {
                    //TODO: Download stuff here

                    //TODO: Install stuff here
                }
                else {
                    Environment.SetEnvironmentVariable("APPDATA", rootPath);
                    String path = Path.Combine(rootPath, ".minecraft", "bin"+Path.DirectorySeparatorChar);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    Launch launch = new Launch(path, username, sessionID, consoleBox.Checked);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            lastLogin = new LastLogin();
            String[] loginInfo = lastLogin.GetLastLogin();
            userText.Text = loginInfo[0];
            passText.Text = loginInfo[1];
        }
    }
}
