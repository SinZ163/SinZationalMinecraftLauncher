using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;
using System.IO;
using System.Windows.Forms;

namespace SinZational_Minecraft_Launcher {
    class InstallJar {

        String path;
        String tmp;
        public InstallJar(String path) {
            this.path = path;
            this.tmp = Path.Combine(path, "tmp" + Path.DirectorySeparatorChar);
            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);
            MessageBox.Show(tmp);
            ExtractMinecraft();
            ExtractJarMods();
            MakeModpack();
        }
        public void ExtractMinecraft() {
            using (ZipArchive archive = ZipFile.OpenRead(Path.Combine(path, "mc"+Path.DirectorySeparatorChar, "minecraft.jar"))) {
                
                foreach (ZipArchiveEntry file in archive.Entries) {
                    String fullPath = Path.Combine(tmp, file.FullName);
                    String filePath = Path.GetDirectoryName(fullPath);
                    Console.WriteLine(filePath);
                    Console.WriteLine(fullPath);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);
                    file.ExtractToFile(fullPath, true);
                }
            }
        }
        public void ExtractJarMods() {
            //Get the filenames of every jar we want to merge
            String[] jarModFiles = Directory.GetFiles(Path.Combine(path)).Where(file => file.ToLower().EndsWith("jar") || file.ToLower().EndsWith("zip")).ToArray<String>();

            foreach (String filename in jarModFiles) {
                using (ZipArchive archive = ZipFile.OpenRead(filename)) {
                }
            }
        }

        public void MakeModpack() {
            //Create new zip/jar file called modpack.jar AKA minecraf.jar with mods
            using (ZipArchive modPack = ZipFile.Open(Path.Combine(path,".."+Path.DirectorySeparatorChar,"modpack.jar"), ZipArchiveMode.Create)) {
                String[] fileList = Directory.GetFiles(tmp, "*", SearchOption.AllDirectories);
                foreach (String file in fileList) {
                    String fileName = file.Split(Path.DirectorySeparatorChar).Last();
                    modPack.CreateEntryFromFile(file, fileName);
                }
            }
        }
    }
}
