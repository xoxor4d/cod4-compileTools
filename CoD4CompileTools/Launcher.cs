using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

// Credits for some of the code goes to: DidUknowiPwn ( World at War Mod Tools V1.1 - PWNED w/ linker_pc fix )
// https://www.ugx-mods.com/forum/3rd-party-applications-and-tools/48/world-at-war-mod-tools-v1-1-pwned-w-linkerpc-fix/10245/

namespace CoD4CompileTools
{
    class Launcher
    {
        public static string CanonicalDirectory(string path)
        {
            FileInfo fileInfo = new FileInfo(path + "." + (object)Path.DirectorySeparatorChar);
            Launcher.MakeDirectory(fileInfo.DirectoryName);
            return fileInfo.DirectoryName + (object)Path.DirectorySeparatorChar;
        }

        public static void MakeDirectory(string directoryName)
        {
            while (!Directory.Exists(directoryName))
            {
                string directoryName1 = Path.GetDirectoryName(directoryName);
                if (directoryName1 != directoryName)
                    Launcher.MakeDirectory(directoryName1);
                Directory.CreateDirectory(directoryName);
            }
        }

        public static string GetMapSourceDirectory()
        {
            return Launcher.CanonicalDirectory(Path.Combine(Launcher.GetRootDirectory(), "map_source"));
        }

        public static string GetRawDirectory()
        {
            return vars.strTreePath + "raw\\";
        }

        public static string GetRawMapsDirectory()
        {
            return vars.strTreePath + "raw\\maps\\";
        }

        public static string GetUsermapsDirectory()
        {
            return vars.strTreePath + "usermaps\\";
        }

        public static string GetUsermapDirectory(string mapName)
        {
            if (mapName == null)
                return (string)null;

            return vars.strTreePath + "usermaps\\" + mapName + "\\";
        }

        public static string GetRootDirectory()
        {
            return vars.strTreePath;
        }

        public static string GetStartupDirectory()
        {
            return Launcher.CanonicalDirectory(Path.GetFullPath("."));
        }

        public static string GetZoneSourceDirectory()
        {
            return Launcher.CanonicalDirectory(Path.Combine(Launcher.GetRootDirectory(), "zone_source"));
        }

        public static string GetLanguage()
        {
            return vars.language;
        }

        public static string[] GetFilesRecursively(string directory)
        {
            return Launcher.GetFilesRecursively(directory, "*");
        }

        public static string[] GetFilesRecursively(string directory, string filesToIncludeFilter)
        {
            string[] files = new string[0];
            Launcher.GetFilesRecursively(directory, filesToIncludeFilter, ref files);
            return files;
        }

        public static void GetFilesRecursively(string directory, string filesToIncludeFilter, ref string[] files)
        {
            foreach (DirectoryInfo directory1 in new DirectoryInfo(directory).GetDirectories())
                Launcher.GetFilesRecursively(Path.Combine(directory, directory1.Name), filesToIncludeFilter, ref files);
            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles(filesToIncludeFilter))
                Launcher.StringArrayAdd(ref files, Path.Combine(directory, file.Name.ToLower()));
        }

        public static void StringArrayAdd(ref string[] stringArray, string stringItem)
        {
            Array.Resize<string>(ref stringArray, stringArray.Length + 1);
            stringArray[stringArray.Length - 1] = stringItem;
        }

        public static string[] GetIWDFiles()
        {
            return Launcher.GetFilesRecursively(Launcher.GetMainDirectory(), "*.iwd");
        }

        public static string GetMainDirectory()
        {
            return Launcher.CanonicalDirectory(Path.Combine(Launcher.GetRootDirectory(), "main"));
        }

        public static string[] GetMainImages()
        {
            string[] stringArray = new string[0];
            foreach (string iwdFile in Launcher.GetIWDFiles())
            {
                using (ZipArchive archive = ZipFile.OpenRead(iwdFile))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.Contains(".iwi") && entry.FullName.Contains("images/"))
                        {
                            string stringItem = entry.FullName.ToString().Substring(7);
                            Launcher.StringArrayAdd(ref stringArray, stringItem);
                        }
                    }
                }
            }

            return stringArray;
        }

        public static string[] MainImages = Launcher.GetMainImages();

        public static string[] GetNonStockImages(string[] modImages)
        {
            List<string> stringList1 = new List<string>((IEnumerable<string>)Launcher.MainImages);
            List<string> stringList2 = new List<string>();
            foreach (string modImage in modImages)
            {
                if (!stringList1.Contains(modImage))
                    stringList2.Add(modImage);
            }
            return stringList2.ToArray();
        }

        public static string[] LoadCSVFile(string csvFile)
        {
            return Launcher.LoadCSVFile(csvFile, (string)null, (string)null);
        }

        public static string[] LoadCSVFile(string csvFile, string findsWordsWith)
        {
            return Launcher.LoadCSVFile(csvFile, findsWordsWith, (string)null);
        }

        public static string[] LoadCSVFile(string textFile, string findsWordsWith, string skipCommentLinesStartingWith)
        {
            string[] stringArray = new string[0];
            string str1 = "";
            switch (findsWordsWith)
            {
                case "image":
                    str1 = ".iwi";
                    break;
            }

            try
            {
                using (StreamReader streamReader = new StreamReader(textFile))
                {
                    string str2;
                    while ((str2 = streamReader.ReadLine()) != null)
                    {
                        str2.Trim();
                        if (str2 != "" && (skipCommentLinesStartingWith == null || !str2.StartsWith(skipCommentLinesStartingWith)) && str2.StartsWith(findsWordsWith))
                            Launcher.StringArrayAdd(ref stringArray, str2.Substring(findsWordsWith.Length + 1) + str1);
                    }
                }
            }

            catch
            {
            }

            return stringArray;
        }

        public static string[] GetImagesToCopy(string mapName)
        {
            string str = vars.strTreePath + "zone_source\\" + vars.language + "\\assetlist\\" +mapName + ".csv";
            if (!File.Exists(str))
                return (string[])null;

            return Launcher.GetNonStockImages(Launcher.LoadCSVFile(str, "image"));
        }
    }
}
