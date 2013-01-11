using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace SinZational_Minecraft_Launcher {
    class InstallMods {
        public InstallMods(String path) {
            String minecraftFolder = Path.Combine(path, ".minecraft" + Path.DirectorySeparatorChar);

            if (Directory.Exists(Path.Combine(path, "jarmods")))
                Directory.Delete(Path.Combine(path, "jarmods"), true);

            if (Directory.Exists(Path.Combine(minecraftFolder, "config" + Path.DirectorySeparatorChar)))
                Directory.Delete(Path.Combine(minecraftFolder, "config" + Path.DirectorySeparatorChar), true);

            if (Directory.Exists(Path.Combine(minecraftFolder , "mods"+Path.DirectorySeparatorChar)))
                Directory.Delete(Path.Combine(minecraftFolder , "mods"+Path.DirectorySeparatorChar), true);

            if (Directory.Exists(Path.Combine(minecraftFolder , "coremods"+Path.DirectorySeparatorChar)))
                Directory.Delete(Path.Combine(minecraftFolder, "coremods" + Path.DirectorySeparatorChar), true);

            //We need to delete the existing files before we can install.

            //We need to extract modpack.zip into path
            ZipFile zip = new ZipFile(path + "modpack.zip");
            zip.ExtractAll(path);
        }
    }
}
