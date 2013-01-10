using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class DownloadMinecraft {
        String URL;
        String version;
        String path;

        MainForm form;

        public DownloadMinecraft(MainForm form, String version, String path) {
            this.URL = String.Format("http://assets.minecraft.net/{0}/minecraft.jar", version);
            this.form = form;
            this.version = version;
            this.path = path;

            Thread thread = new Thread(new ThreadStart(Download));
            thread.Start();
            while (thread.IsAlive) {
                Application.DoEvents();
            }
        }
        void Download() {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(form.SetProgressBar); //TODO: Don't use mainForm
            client.DownloadFile(URL, path + "minecraft.jar");
        }
    }
}
