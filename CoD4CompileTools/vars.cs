using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace CoD4CompileTools
{
    internal sealed class vars
    {

        public static string strTreePath;
        public static string strMapSourcePath;
        public static string strPCMapsPath;
        public static string strPCMapsPath_MP;
        public static string strBinPath;
        public static string strZoneSourcePath;
        public static string strWorkingDir;
        public static string strMainPath;
        public static string strPCGamePath;
        public static bool allowSaveLastMap;
        public static string selectedMap_Name;
        public static int selectedMap_Index;

        public static string language;

        public static int mainFormColor = 5;
        public static double mainFormOpacity = 0.95;
        public static bool mainFormAnimEnabled = true;
        public static int mainFormAnimSpeed = 5;

        public static bool checkFileExists(string strFileName)
        {
            return StringType.StrCmp(strFileName, "", false) != 0 && StringType.StrCmp(FileSystem.Dir(strFileName, FileAttribute.Normal), "", false) != 0;
        }
    }
}
