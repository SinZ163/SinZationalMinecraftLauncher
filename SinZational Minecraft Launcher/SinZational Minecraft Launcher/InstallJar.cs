using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ionic.Zip;
using System.Windows.Forms;
using System.Diagnostics;

namespace SinZational_Minecraft_Launcher {
    class InstallJar {

        String path;
        String tmp;
        public InstallJar(String path) {
            this.path = path;
            this.tmp = Path.Combine(path, "tmp" + Path.DirectorySeparatorChar);

            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);

            ExtractMinecraft();
            ExtractJarMods();
            FixMetaInf();
            MakeModpack();
        }
        public void ExtractMinecraft() {
            //Extract /mc/minecraft.jar to /tmp

            try {
                String args = "/q /c 7z.exe x \"" + Path.Combine(path, "mcdl" + Path.DirectorySeparatorChar, "minecraft.jar") + "\" -o\"" + tmp+ "\" -y";
                Console.WriteLine(args);

                ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", args);
                procStartInfo.WorkingDirectory = @"C:\Program Files\7-Zip\";
                Process proc = new System.Diagnostics.Process();
#if DEBUG
                procStartInfo.Arguments += " & pause";
#endif
#if RELEASE
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
#endif
                proc.StartInfo = procStartInfo;
                proc.Start();
                while (!proc.HasExited) {
                    Application.DoEvents();
                }
            }
            catch (Exception e) {
                MessageBox.Show(@"7-Zip is required, to be installed in C:\Program Files\7-Zip");
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("| "+e.Message);
            }
        }
        public void ExtractJarMods() {
            //Get the filenames of every jar we want to merge
            String[] jarModFiles = Directory.GetFiles(Path.Combine(path, "jarmods"+Path.DirectorySeparatorChar)).Where(file => file.ToLower().EndsWith("jar") || file.ToLower().EndsWith("zip")).ToArray<String>();
            foreach (String jarMod in jarModFiles) {
                ZipFile zip = new ZipFile(Path.Combine(path, "jarmods" + Path.DirectorySeparatorChar, jarMod));
                zip.ExtractAll(tmp, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        public void FixMetaInf() {
            Directory.Delete(Path.Combine(tmp, "META-INF"), true);
        }
        public void MakeModpack() {
            if (File.Exists(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar")))
                File.Delete(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
            ZipFile zip = new ZipFile();
            zip.AddDirectory(tmp);
            zip.Save(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
        }
    }
}
