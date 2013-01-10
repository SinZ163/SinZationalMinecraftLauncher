using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class ServerQuery {
        public String modPackVersion;
        public String version;
        public String downloadLink;
        public ServerQuery() {
            WebClient client = new WebClient();
            String rawOutput = client.DownloadString("http://sinzationalminecraft.mca.d3s.co/launcher/download.html");
            String[] output = rawOutput.Split('|');
            MessageBox.Show(String.Join(", ", output));

            modPackVersion = output[0];
            version = output[1];
            downloadLink = output[2];
        }
    }
}
