using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

namespace SinZational_Minecraft_Launcher {
    class DownloadMods {
        String path;
        String downloadLink;

        Boolean isDownloading = true;
        MainForm form;
        public DownloadMods(MainForm form,String path, String downloadLink) {
            this.form = form;
            this.path = path;
            this.downloadLink = downloadLink;

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(form.SetProgressBar);
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Downloaded);
            client.DownloadFileAsync(new Uri(downloadLink), path + "modpack.zip");
            while (isDownloading) {
                Application.DoEvents();
            }
        }

        public void Downloaded(object sender, AsyncCompletedEventArgs e) {
            isDownloading = false;
        }
    }
}
