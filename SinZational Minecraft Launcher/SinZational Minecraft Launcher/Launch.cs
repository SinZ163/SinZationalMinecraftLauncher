﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class Launch {

        String path;
        String username;
        String sessionID;
        Boolean consoleEnabled;

        public Launch(String path, String username, String sessionID, Boolean consoleEnabled, Boolean demo = false) {
            this.path = path;
            this.username = username;
            this.sessionID = sessionID;
            this.consoleEnabled = consoleEnabled;

            launchMinecraft();
        }

        void launchMinecraft() {
            String[] binFiles = Directory.GetFiles(path, "*.jar");
            String message = String.Join(";", binFiles);
            //String[] jarModFiles = Directory.GetFiles(Path.Combine(path, "jarmods" + Path.DirectorySeparatorChar)).Where(file => file.ToLower().EndsWith("jar") || file.ToLower().EndsWith("zip")).ToArray<String>();
            //message += String.Join(";", jarModFiles);
            MessageBox.Show(message);
            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/q /c java -Djava.library.path=\"natives\" -cp "+message+" net.minecraft.client.Minecraft " + username + " " + sessionID);
            procStartInfo.WorkingDirectory = path;
            Process proc = new System.Diagnostics.Process();

            if (consoleEnabled == false) {
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            else {
                procStartInfo.Arguments += " &pause";
            }
            proc.StartInfo = procStartInfo;
            proc.Start();
            Application.Exit();
        }
    }
}