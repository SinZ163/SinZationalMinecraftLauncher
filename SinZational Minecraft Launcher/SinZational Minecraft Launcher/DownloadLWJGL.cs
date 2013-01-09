using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class DownloadLWJGL {
        String downloadLink = "http://s3.amazonaws.com/MinecraftDownload/";

        Uri native;
        Uri lwjgl;
        Uri jinput;
        Uri util;

        String path;

        Boolean isDownloading = true;

        MainForm form;
        public DownloadLWJGL(MainForm form, String path) {
            this.form = form;
            this.path = path;
            native = new Uri(downloadLink + "windows_natives.jar");
            lwjgl = new Uri(downloadLink + "lwjgl.jar");
            jinput = new Uri(downloadLink + "jinput.jar");
            util = new Uri(downloadLink + "lwjgl_util.jar");

            Thread thread = new Thread(new ThreadStart(Download));
            thread.Start();
            while (thread.IsAlive) {
                Application.DoEvents();
            }
        }

        private void Download() {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(form.SetProgressBar); //TODO: Don't use mainForm

            form.SetTask("Downloading Natives");                                                            //TODO: ^
            client.DownloadFile(native, path+"natives.jar");

            form.SetTask("Downloading LWJGL");                                                              //TODO: ^
            client.DownloadFile(lwjgl, path + "lwjgl.jar");

            form.SetTask("Downloading JInput");                                                             //TODO: ^
            client.DownloadFile(jinput, path + "jinput.jar");

            form.SetTask("Downloading LWJGL Util");                                                         //TODO: ^
            client.DownloadFile(util, path + "jwjgl_util.jar");
        }
    }
}
