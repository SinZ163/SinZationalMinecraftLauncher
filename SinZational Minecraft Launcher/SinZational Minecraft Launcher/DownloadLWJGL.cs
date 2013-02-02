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
using Ionic.Zip;

namespace SinZational_Minecraft_Launcher {
    class DownloadLWJGL {
        String downloadLink = "http://s3.amazonaws.com/MinecraftDownload/";

        Uri native;
        Uri lwjgl;
        Uri jinput;
        Uri util;

        String path;

        MainForm form;

        Boolean isDownloading = true;


        public DownloadLWJGL(MainForm form, String path) {
            this.form = form;
            this.path = path;

            if (File.Exists(path + "natives.zip"))
                File.Delete(path + "natives.zip");
            if (File.Exists(path + "lwjgl.jar"))
                File.Delete(path + "lwjgl.jar");
            if (File.Exists(path + "jinput.jar"))
                File.Delete(path + "jinput.jar");
            if (File.Exists(path + "lwjgl_util.jar"))
                File.Delete(path + "lwjgl_util.jar");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            native = new Uri(downloadLink + "windows_natives.jar");
            lwjgl = new Uri(downloadLink + "lwjgl.jar");
            jinput = new Uri(downloadLink + "jinput.jar");
            util = new Uri(downloadLink + "lwjgl_util.jar");

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(form.SetProgressBar); //TODO: Don't use mainForm
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Downloaded);

            form.SetTask("Downloading Natives");                                                            //TODO: ^
            client.DownloadFileAsync(native, path + "natives.zip");

            while (isDownloading) {
                Application.DoEvents();
            }
            isDownloading = true;
            form.SetTask("Downloading LWJGL");                                                              //TODO: ^
            client.DownloadFileAsync(lwjgl, path + "lwjgl.jar");

            while (isDownloading) {
                Application.DoEvents();
            }
            isDownloading = true;
            form.SetTask("Downloading JInput");                                                             //TODO: ^
            client.DownloadFileAsync(jinput, path + "jinput.jar");

            while (isDownloading) {
                Application.DoEvents();
            }
            isDownloading = true;
            form.SetTask("Downloading LWJGL Util");                                                         //TODO: ^
            client.DownloadFileAsync(util, path + "jwjgl_util.jar");

            while (isDownloading) {
                Application.DoEvents();
            }
        }
        public void Downloaded(object sender, AsyncCompletedEventArgs e) {
            isDownloading = false;
        }

        public void Install() {
#if DEBUG
            MessageBox.Show("Natives time.");
#endif
            if (Directory.Exists(Path.Combine(path, "natives" + Path.DirectorySeparatorChar)))
                Directory.Delete(Path.Combine(path, "natives" + Path.DirectorySeparatorChar), true);
            form.SetTask("Installing Natives");
            ZipFile zip = new ZipFile(path + "natives.zip");
            zip.ExtractAll(Path.Combine(path, "natives" + Path.DirectorySeparatorChar), ExtractExistingFileAction.OverwriteSilently);
        }

    }
}
