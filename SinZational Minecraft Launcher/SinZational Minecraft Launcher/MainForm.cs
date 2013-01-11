using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
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
                        using (StreamReader sr = File.OpenText(Path.Combine(rootPath, "version"))) {
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
                    else {
                        downloadModPack = true;
                        downloadMC = true;
                    }


                    //TODO: Download stuff here

                    if (downloadModPack) {
                        SetTask("Downloading Modpack...");
                        DownloadMods modDownload = new DownloadMods(this, rootPath, query.downloadLink);

                        SetTask("Installing Modpack...");
                        InstallMods modInstall = new InstallMods(rootPath);
                    }

                    if (downloadMC) {
                        DownloadLWJGL lwjgl = new DownloadLWJGL(this, path);
                        SetTask("Downloading Minecraft");
                        DownloadMinecraft minecraft = new DownloadMinecraft(this, query.version, Path.Combine(rootPath, "mcdl" + Path.DirectorySeparatorChar));
                    }

                    //TODO: Install stuff here
                    if (downloadModPack || downloadMC) {
                        SetTask("Making Minecraft.jar");
                        InstallJar jar = new InstallJar(Path.Combine(rootPath));
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
            if (File.Exists(rootPath + "lastlogin")) {
                lastLogin = new LastLogin();
                String[] loginInfo = lastLogin.GetLastLogin();
                userText.Text = loginInfo[0];
                passText.Text = loginInfo[1];
            }
        }


        private delegate void SetTask_(string text);
        private delegate void SetProgress_(object sender, DownloadProgressChangedEventArgs e);
 
        public void SetTask(string text) {
            if (this.InvokeRequired) {
                 this.Invoke(new SetTask_(SetTask), text);
            }
            else {
                progressLabel.Text = text;
            }
        }

        public void SetProgressBar(object sender, DownloadProgressChangedEventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new SetProgress_(SetProgressBar), sender, e);
            }
            else {
                progressBar.Value = e.ProgressPercentage;
            }
        }

        private void sinZationalMinecraftToolStripMenuItem_Click(object sender, EventArgs e) {
            webBrowser.Url = new Uri("https://googledrive.com/host/0By-3RIh0CDzbS1U1cDFPeXlORzQ/");
        }

        private void mCUpdateToolStripMenuItem_Click(object sender, EventArgs e) {
            webBrowser.Url = new Uri("http://mcupdate.tumblr.com");
        }
    }
}
