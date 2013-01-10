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

            SetTask("Logging in...");
            Login login = new Login(userText.Text, passText.Text);
            String username = login.username;
            String sessionID = login.sessionID;
            if (sessionID != "-") { //TODO: offline-mode support
                if (rememberBox.Checked) {
                    lastLogin.SetLastLogin(userText.Text, passText.Text);
                }
                if (updateBox.Checked) {
                    //TODO: Contact webserver here
                    SetTask("Checking for Updates...");
                    ServerQuery query = new ServerQuery();
                    bool downloadModPack = false;
                    bool downloadMC = false;
                    if (File.Exists(Path.Combine(rootPath, "version"))) {
                        //Ok, We have saved version before, we mean business
                        using (StreamReader sr = File.OpenText(Path.Combine(rootPath, "rememberMe.txt"))) {
                            String modpackVersion = sr.ReadLine();
                            String version = sr.ReadLine();
                            if (modpackVersion != query.modPackVersion) {
                                downloadModPack = true;
                                //Fuck, we have to download the modpack
                            }
                            if (version != query.version) {
                                downloadMC = true;
                                //Gotta download minecraft =S
                            }
                            sr.Close();
                        }

                    }


                    //TODO: Download stuff here

                    if (downloadModPack) {
                        DownloadMods modDownload = new DownloadMods();
                    }

                    if (downloadMC) {
                        DownloadLWJGL lwjgl = new DownloadLWJGL(this, path);
                        SetTask("Downloading Minecraft");
                        DownloadMinecraft minecraft = new DownloadMinecraft(this, query.version, Path.Combine(path, "dl" + Path.DirectorySeparatorChar, "mc" + Path.DirectorySeparatorChar));
                    }

                    //TODO: Install stuff here
                    if (downloadModPack || downloadMC) {
                        SetTask("Installing Modpack");
                        InstallJar jar = new InstallJar(Path.Combine(path, "dl" + Path.DirectorySeparatorChar));
                    }

                    //Save version file
                    using (StreamWriter sw = File.CreateText(Path.Combine(rootPath, "version"))) {
                        sw.WriteLine(query.modPackVersion);
                        sw.WriteLine(query.version);
                        sw.Close();
                    }
                }
                Application.Exit();
                SetTask("Starting Minecraft!");
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

        private void sinZationalMinecraftToolStripMenuItem_Click(object sender, EventArgs e) {
            webBrowser.Url = new Uri("http://SinZationalMinecraft.mca.d3s.co/launcher");
        }

        private void mCUpdateToolStripMenuItem_Click(object sender, EventArgs e) {
            webBrowser.Url = new Uri("http://mcupdate.tumblr.com");
        }
    }
}
