﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class Login {

        public String username;
        public String sessionID = "-";
        protected internal String password;
        public Login(String username, String password) {
            this.username = username;
            this.password = password;

            Thread thread = new Thread(new ThreadStart(DoLogin));
            thread.Start();
            while (thread.IsAlive) {
                Application.DoEvents();
            }
        }

        private void DoLogin() {
            WebClient client = new WebClient();
            try {
                String result = client.DownloadString(new Uri("https://login.minecraft.net?user=" + username + "&password=" + password + "&version=1337"));
                if (result.Contains(':')) {
                    //YAY LOGIN WORKED
                    String[] output = result.Split(':');
                    this.username = output[2];
                    this.sessionID = output[3];
                }
                else {
                    //TODO: LoginFailed
                }
            }
            catch (Exception e) {
                //TODO: Document it more
                MessageBox.Show(e.StackTrace);
            }
        }
    }
}
