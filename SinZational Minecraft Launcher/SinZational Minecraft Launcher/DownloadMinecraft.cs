using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        Boolean isDownloading = true;

        public DownloadMinecraft(MainForm form, String version, String path) {
            this.URL = String.Format("http://assets.minecraft.net/{0}/minecraft.jar", version);
            this.form = form;
            this.version = version;
            this.path = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(form.SetProgressBar);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloaded);
            client.DownloadFileAsync(new Uri(URL), path + "minecraft.jar");
            while (isDownloading) {
                Application.DoEvents();
            }
        }

        public void Downloaded(object sender, AsyncCompletedEventArgs e) {
            isDownloading = false;
        }
    }
}
