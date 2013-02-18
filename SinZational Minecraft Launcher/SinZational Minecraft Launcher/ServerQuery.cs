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
            try {
                WebClient client = new WebClient();
                String rawOutput = client.DownloadString("https://googledrive.com/host/0By-3RIh0CDzbS1U1cDFPeXlORzQ/download.html");
                String[] output = rawOutput.Split('|');
#if DEBUG
            MessageBox.Show(String.Join(", ", output));
#endif

                modPackVersion = output[0];
                version = output[1];
                downloadLink = output[2];
            }
            catch (Exception e) {
                modPackVersion = "-1";
                MessageBox.Show(e.Message);
            }
        }
    }
}
