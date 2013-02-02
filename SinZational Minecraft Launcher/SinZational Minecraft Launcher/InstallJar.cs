using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Diagnostics;

using System.IO.Compression;
using Ionic.Zip;

namespace SinZational_Minecraft_Launcher {
    class InstallJar {

        String mcFile;
        String path;
        String tmp;
        public InstallJar(String path) {
            this.path = path;
            this.tmp = Path.Combine(path, "tmp" + Path.DirectorySeparatorChar);
            this.mcFile = Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar");

            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);

            ExtractMinecraft();
            ExtractJarMods();
            MakeModpack();
            FixMetaInf();
        }
        public void ExtractMinecraft() {
            //Extract /mc/minecraft.jar to /tmp
            if (File.Exists(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar")))
                File.Delete(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
            File.Copy(Path.Combine(path, "mcdl" + Path.DirectorySeparatorChar, "minecraft.jar"),
                      Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
            /*try {
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
            */
        }
        public void ExtractJarMods() {
            //Get the filenames of every jar we want to merge
            String[] jarModFiles = Directory.GetFiles(Path.Combine(path, "jarmods" + Path.DirectorySeparatorChar)).Where(file => file.ToLower().EndsWith("jar") || file.ToLower().EndsWith("zip")).ToArray<String>();
            foreach (String jarMod in jarModFiles) {
                Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Path.Combine(path, "jarmods" + Path.DirectorySeparatorChar, jarMod));
                zip.ExtractAll(tmp, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        public void FixMetaInf() {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(mcFile)) {
                zip.RemoveSelectedEntries("META-INF/*.*");
                zip.Save();
            }
        }
        public void MakeModpack() {
            String[] fileList = Directory.GetFiles(tmp, "*", SearchOption.AllDirectories);

            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(mcFile)) {
                foreach (String file in fileList) {
                    String newFile = file.Substring(tmp.Length);
                    Console.WriteLine(newFile);
                    if (zip.ContainsEntry(newFile))
                        zip.RemoveEntry(newFile);
                    zip.AddFile(file, Path.GetDirectoryName(newFile));
                }
                zip.Save();
                MessageBox.Show("Done...");
            }
           /* using (ZipArchive zip = System.IO.Compression.ZipFile.Open(mcFile, ZipArchiveMode.Update)) {
                foreach (String file in fileList) {
                    var fileInZip = (from f in zip.Entries
                                     where f.Name == Path.GetFileName(file)
                                     select f).FirstOrDefault();
                    if (fileInZip != null)
                        fileInZip.Delete();
                    zip.CreateEntryFromFile(file, file.Remove(0, tmp.Length), CompressionLevel.Optimal);
                }
            }*/

            //ZipFile zip = new ZipFile(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
            //zip.AddDirectory(tmp);
            //zip.Save(Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar, "bin" + Path.DirectorySeparatorChar, "minecraft.jar"));
        }
    }
}
