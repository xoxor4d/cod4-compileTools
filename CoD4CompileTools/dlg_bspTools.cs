using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using MetroFramework.Forms;
using MaterialSkin;
using System.Runtime.InteropServices;

namespace CoD4CompileTools
{
    public partial class dlg_bspTools : MetroForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string language;

        internal virtual FolderBrowserDialog FolderBrowserDialog
        {
            get
            {
                return this.folderBrowserDialog1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (this.folderBrowserDialog1 == null)
                    ;
                this.folderBrowserDialog1 = value;
                if (this.folderBrowserDialog1 != null)
                    ;
            }
        }

        #region formsetup
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;
            int WM_PARENTNOTIFY = 0x0210;

            if (!this.Focused && m.Msg == WM_PARENTNOTIFY)
            {
                // Make this form auto-grab the focus when menu/controls are clicked
                this.Activate();
            }

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
                if (!chkSettings_disableAnimations.Value)
                    FadeIn(this, vars.mainFormAnimSpeed);

            base.WndProc(ref m);
        }

        private async void FadeIn(Form o, [Optional] int interval)
        {
            int newIntervall = Convert.ToInt16(Convert.ToDouble(interval) / vars.mainFormOpacity);
            while (o.Opacity < vars.mainFormOpacity)
            {
                await Task.Delay(newIntervall);
                o.Opacity += 0.05;
            }

            o.Opacity = vars.mainFormOpacity;
        }

        private async void FadeOut(Form o, [Optional] int interval, [Optional] int action)
        {
            int newIntervall = Convert.ToInt16(Convert.ToDouble(interval) / vars.mainFormOpacity);
            while (o.Opacity > 0.0)
            {
                await Task.Delay(newIntervall);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0;

            if (action == 1)
                this.WindowState = FormWindowState.Minimized;

            else
                this.Close();
        }
        #endregion

        public dlg_bspTools()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.CustGrey, Primary.Green400, Primary.Green400, Accent.CustGreen, TextShade.WHITE);
            this.Text = "COD4 Compile Tools";
            this.Size = new System.Drawing.Size(755, 695);

            this.Opacity = 0;
            this.FocusMe();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (!chkSettings_disableAnimations.Value)
                FadeOut(this, vars.mainFormAnimSpeed, 1);

            else
                this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            bool msgboxResult = false;

            if (execBatch != null)
            {
                string msgbox_msg = "It seems like a compiler is still running! \n" +
                                    "Are you sure you want to exit? \n" +
                                    "Press OK to quit";

                using (var form = new msgbox(msgbox_msg))
                {
                    var result = form.ShowDialog();

                    if (result == DialogResult.Cancel)
                        msgboxResult = true;
                }
            }

            if(!msgboxResult)
            {
                if (!chkSettings_disableAnimations.Value)
                    FadeOut(this, vars.mainFormAnimSpeed, 0);
                else
                    this.Close();
            }
        }

        public void updateLocalPaths()
        {
            vars.strBinPath = vars.strTreePath + "bin\\";
            vars.strMainPath = vars.strTreePath + "main\\";
            vars.strPCMapsPath = vars.strTreePath + "raw\\maps\\";
            vars.strPCMapsPath_MP = vars.strTreePath + "raw\\maps\\mp\\";
            vars.strMapSourcePath = vars.strTreePath + "map_source\\";
            vars.strPCGamePath = vars.strTreePath;
            vars.strZoneSourcePath = vars.strTreePath + "zone_source\\";
            this.txtTreePath.Text = vars.strTreePath;
            vars.strWorkingDir = Application.StartupPath + "\\";
        }

        private void dlg_bspTools_Load(object sender, EventArgs e)
        {
            vars.allowSaveLastMap = false;
            if (!this.loadUserSettings())
            {
                this.LoadDefaultUserSettings();
                this.fillMapList();
            }
            this.setLanguage();
            vars.allowSaveLastMap = true;
            this.gridOption.SelectedIndex = 0;

            if (!chkSettings_disableAnimations.Value)
            {
                FadeIn(this, vars.mainFormAnimSpeed);
            }
                
            else
                this.Opacity = vars.mainFormOpacity;

            if (chkSettings_consoleOnStart.Value)
                openConsole();

        }

        private bool loadUserSettings()
        {
            bool flag = false;
            string str1 = Application.StartupPath + "\\CoD4CompileTools.settings";
            if (StringType.StrCmp(FileSystem.Dir(str1, FileAttribute.Normal), "", false) != 0)
                flag = true;

            if (!flag)
                return flag;

            File.SetAttributes(Application.StartupPath + "\\CoD4CompileTools.settings", FileAttributes.Normal);
            StreamReader streamReader = new StreamReader((Stream)File.OpenRead(str1));
            while (streamReader.Peek() != -1)
            {
                string String1 = streamReader.ReadLine();
                int count = Strings.InStr(String1, ",", CompareMethod.Binary);
                if (count == 0)
                    return false;

                string sLeft = String1.Remove(checked(count - 1), checked(String1.Length - count + 1));
                string str2 = String1.Remove(0, count);
                if (StringType.StrCmp(sLeft, "tree", false) == 0)
                {
                    vars.strTreePath = str2;
                    this.updateLocalPaths();
                    this.fillMapList();
                }

                else if (StringType.StrCmp(sLeft, "mapname", false) == 0)
                {
                    int num1 = -1;
                    int num2 = 0;
                    int num3 = checked(this.lstMapFiles.Items.Count - 1);
                    int index = num2;
                    while (index <= num3)
                    {
                        if (ObjectType.ObjTst(this.lstMapFiles.Items[index], (object)str2, false) == 0)
                            num1 = index;
                        checked { ++index; }
                    }

                    if (num1 >= 0)
                        this.lstMapFiles.SelectedIndex = num1;
                }

                else if (StringType.StrCmp(sLeft, "developer", false) == 0)
                    this.chkDeveloper.Checked = BooleanType.FromString(str2);
                else if (StringType.StrCmp(sLeft, "developerscript", false) == 0)
                    this.chkDeveloperScript.Checked = BooleanType.FromString(str2);
                else if (StringType.StrCmp(sLeft, "cheats", false) == 0)
                    this.chkCheats.Checked = BooleanType.FromString(str2);
                else if (StringType.StrCmp(sLeft, "tab", false) == 0)
                    this.materialTabControl2.SelectedIndex = IntegerType.FromString(str2);
                else if (StringType.StrCmp(sLeft, "chkCustomCommandLine", false) == 0)
                    this.chkCustomCommandLine.Checked = BooleanType.FromString(str2);

                else if (StringType.StrCmp(sLeft, "maincolor", false) == 0)
                {
                    int savedColor = Convert.ToInt32(str2);

                    if (savedColor <= 0 || savedColor > 14 )
                        savedColor = 5;

                    else
                        vars.mainFormColor = savedColor;
                    
                    doMainColor();
                }
                    
                else if (StringType.StrCmp(sLeft, "mainopacity", false) == 0)
                {
                    double tempOpacity = Convert.ToDouble(str2);
                    if(tempOpacity >= (this.opacity_trackbar.Minimum * 100) && tempOpacity <= (this.opacity_trackbar.Maximum * 100))
                        vars.mainFormOpacity = tempOpacity;

                    opacity_textbox.Text = vars.mainFormOpacity.ToString();
                    opacity_trackbar.Value = Convert.ToInt32(vars.mainFormOpacity * 100);

                    if (chkSettings_disableAnimations.Value)
                        doMainOpacity();
                }

                else if (StringType.StrCmp(sLeft, "moveToUsermaps", false) == 0)
                    this.copy_mapToUsermaps.Value = BooleanType.FromString(str2);

                else if (StringType.StrCmp(sLeft, "txtCustomCommandLine", false) == 0)
                    this.txtCustomCommandLine.Text = str2;

                else if (StringType.StrCmp(sLeft, "consoleOnStart", false) == 0)
                    this.chkSettings_consoleOnStart.Value = BooleanType.FromString(str2);

                else if (StringType.StrCmp(sLeft, "disableAnimations", false) == 0)
                {
                    this.chkSettings_disableAnimations.Value = BooleanType.FromString(str2);
                    vars.mainFormAnimEnabled = this.chkSettings_disableAnimations.Value;
                }

                else if (StringType.StrCmp(sLeft, "animationSpeed", false) == 0)
                {
                    int tempAnimSpeed = Convert.ToInt16(str2);
                    if (tempAnimSpeed >= this.animSpeed_trackbar.Minimum && tempAnimSpeed <= this.animSpeed_trackbar.Maximum)
                        vars.mainFormAnimSpeed = tempAnimSpeed;

                    animSpeed_textbox.Text = vars.mainFormAnimSpeed.ToString();
                    animSpeed_trackbar.Value = vars.mainFormAnimSpeed;

                    if (chkSettings_disableAnimations.Value)
                        doMainOpacity();
                }
            }

            streamReader.Close();
            return true;
        }

        private void LoadDefaultUserSettings()
        {
            vars.strTreePath = Application.StartupPath.Remove(checked(Application.StartupPath.Length - 20), 20);
            this.updateLocalPaths();
            this.fillMapList();
            this.resetCompileOptions();
        }

        public void resetCompileOptions()
        {
            if (!this.CheckSavedMap("default"))
                return;

            this.LoadSavedMap("default");
        }

        private void fillMapList()
        {
            
            DirectoryInfo directoryInfo = new DirectoryInfo(vars.strMapSourcePath + "\\");
            if (!directoryInfo.Exists)
            {
                this.lstMapFiles.Items.Clear();
                this.txtTreePath.Text = "";
            }

            else
            {
                FileInfo[] files = directoryInfo.GetFiles();
                this.lstMapFiles.Items.Clear();
                vars.selectedMap_Name = "";
                vars.selectedMap_Index = -1;
                FileInfo[] fileInfoArray = files;
                int index = 0;

                while (index < fileInfoArray.Length)
                {
                    FileInfo fileInfo = fileInfoArray[index];
                    if (fileInfo.Name.EndsWith(".map"))
                    {
                        this.lstMapFiles.Items.Add((object)fileInfo.Name.Remove(checked(fileInfo.Name.Length - 4), 4));
                    }
                        
                    checked { ++index; }
                }
            }
            
        }

        private void LoadSavedMap(string strMapName)
        {
            StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Application.StartupPath + "\\" + strMapName + ".settings"));
            while (streamReader.Peek() != -1)
                this.LoadSavedSetting(streamReader.ReadLine());

            streamReader.Close();
        }

        private void LoadSavedSetting(string strLine)
        {
            int count = Strings.InStr(strLine, ",", CompareMethod.Binary);
            if (count == 0)
                return;

            string sLeft = strLine.Remove(checked(count - 1), checked(strLine.Length - count + 1));
            string str = strLine.Remove(0, count);

            if (StringType.StrCmp(sLeft, "bsp", false) == 0)
                this.chkBSP.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "light", false) == 0)
                this.chkLight.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "paths", false) == 0)
                this.chkPaths.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "onlyents", false) == 0)
                this.chkOnlyEnts.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "blocksize", false) == 0)
                this.chkBlockSize.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "blocksize_value", false) == 0)
                this.txtBlockSize.Text = str;
            else if (StringType.StrCmp(sLeft, "samplescale", false) == 0)
                this.chkSampleScale.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "samplescale_value", false) == 0)
                this.txtSampleScale.Text = str;
            else if (StringType.StrCmp(sLeft, "debugLightmaps", false) == 0)
                this.chkDebugLightMaps.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "bspoptions", false) == 0)
                this.txtBSPOptions.Text = str;
            else if (StringType.StrCmp(sLeft, "fast", false) == 0)
                this.chkLightFast.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "extra", false) == 0)
                this.chkLightExtra.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "verbose", false) == 0)
                this.chkLightVerbose.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "modelshadow", false) == 0)
                this.chkModelShadow.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "nomodelshadow", false) == 0)
                this.chkNoModelShadow.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "dumpoptions", false) == 0)
                this.chkDumpOptions.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "traces", false) == 0)
                this.chkTraces.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "traces_value", false) == 0)
                this.txtTraces.Text = str;
            else if (StringType.StrCmp(sLeft, "bouncefraction", false) == 0)
                this.chkBounceFraction.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "bouncefraction_value", false) == 0)
                this.txtBounceFraction.Text = str;
            else if (StringType.StrCmp(sLeft, "jitter", false) == 0)
                this.chkJitter.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "jitter_value", false) == 0)
                this.txtJitter.Text = str;
            else if (StringType.StrCmp(sLeft, "chkCustomCommandLineBSP", false) == 0)
                this.chkCustomCommandLineBSP.Checked = BooleanType.FromString(str);
            else if (StringType.StrCmp(sLeft, "chkCustomCommandLineLight", false) == 0)
                this.chkCustomCommandLineLight.Checked = BooleanType.FromString(str);

            else
            {
                if (StringType.StrCmp(sLeft, "lightoptions", false) != 0)
                    return;

                this.txtLightOptions.Text = str;
            }
        }

        private void saveUserSettings()
        {
            if (!vars.allowSaveLastMap)
                return;

            StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(vars.strWorkingDir + "CoD4CompileTools.settings", FileMode.Create));
            streamWriter.WriteLine("tree," + vars.strTreePath);
            streamWriter.WriteLine("mapname," + vars.selectedMap_Name);
            streamWriter.WriteLine("developer," + StringType.FromBoolean(this.chkDeveloper.Checked));
            streamWriter.WriteLine("developerscript," + StringType.FromBoolean(this.chkDeveloperScript.Checked));
            streamWriter.WriteLine("cheats," + StringType.FromBoolean(this.chkCheats.Checked));
            streamWriter.WriteLine("tab," + StringType.FromInteger(this.materialTabControl2.SelectedIndex));
            streamWriter.WriteLine("chkCustomCommandLine," + StringType.FromBoolean(this.chkCustomCommandLine.Checked));
            streamWriter.WriteLine("txtCustomCommandLine," + this.txtCustomCommandLine.Text);
            streamWriter.WriteLine("maincolor," + vars.mainFormColor);
            streamWriter.WriteLine("mainopacity," + vars.mainFormOpacity);
            streamWriter.WriteLine("moveToUsermaps," + copy_mapToUsermaps.Value);
            streamWriter.WriteLine("consoleOnStart," + chkSettings_consoleOnStart.Value);
            streamWriter.WriteLine("disableAnimations," + chkSettings_disableAnimations.Value);
            streamWriter.WriteLine("animationSpeed," + vars.mainFormAnimSpeed);

            streamWriter.Close();
        }

        private void SaveCompileSettings()
        {
            if (vars.selectedMap_Index == -1)
                return;

            this.SaveNewCompileSettings();
        }

        private void SaveNewCompileSettings()
        {
            if (this.CheckSavedMap(vars.selectedMap_Name))
                File.SetAttributes(Application.StartupPath + "\\" + vars.selectedMap_Name + ".settings", FileAttributes.Normal);

            StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(Application.StartupPath + "\\" + vars.selectedMap_Name + ".settings", FileMode.Create));
            streamWriter.WriteLine("bsp," + StringType.FromBoolean(this.chkBSP.Checked));
            streamWriter.WriteLine("light," + StringType.FromBoolean(this.chkLight.Checked));
            streamWriter.WriteLine("paths," + StringType.FromBoolean(this.chkPaths.Checked));
            streamWriter.WriteLine("onlyents," + StringType.FromBoolean(this.chkOnlyEnts.Checked));
            streamWriter.WriteLine("blocksize," + StringType.FromBoolean(this.chkBlockSize.Checked));
            streamWriter.WriteLine("blocksize_value," + this.txtBlockSize.Text);
            streamWriter.WriteLine("samplescale," + StringType.FromBoolean(this.chkSampleScale.Checked));
            streamWriter.WriteLine("samplescale_value," + this.txtSampleScale.Text);
            streamWriter.WriteLine("debugLightmaps," + StringType.FromBoolean(this.chkDebugLightMaps.Checked));
            streamWriter.WriteLine("chkCustomCommandLineBSP," + StringType.FromBoolean(this.chkCustomCommandLineBSP.Checked));
            streamWriter.WriteLine("bspoptions," + this.txtBSPOptions.Text);
            streamWriter.WriteLine("fast," + StringType.FromBoolean(this.chkLightFast.Checked));
            streamWriter.WriteLine("extra," + StringType.FromBoolean(this.chkLightExtra.Checked));
            streamWriter.WriteLine("verbose," + StringType.FromBoolean(this.chkLightVerbose.Checked));
            streamWriter.WriteLine("modelshadow," + StringType.FromBoolean(this.chkModelShadow.Checked));
            streamWriter.WriteLine("nomodelshadow," + StringType.FromBoolean(this.chkNoModelShadow.Checked));
            streamWriter.WriteLine("dumpoptions," + StringType.FromBoolean(this.chkDumpOptions.Checked));
            streamWriter.WriteLine("traces," + StringType.FromBoolean(this.chkTraces.Checked));
            streamWriter.WriteLine("traces_value," + this.txtTraces.Text);
            streamWriter.WriteLine("bouncefraction," + StringType.FromBoolean(this.chkBounceFraction.Checked));
            streamWriter.WriteLine("bouncefraction_value," + this.txtBounceFraction.Text);
            streamWriter.WriteLine("jitter," + StringType.FromBoolean(this.chkJitter.Checked));
            streamWriter.WriteLine("jitter_value," + this.txtJitter.Text);
            streamWriter.WriteLine("chkCustomCommandLineLight," + StringType.FromBoolean(this.chkCustomCommandLineLight.Checked));
            streamWriter.WriteLine("lightoptions," + this.txtLightOptions.Text);
            streamWriter.Close();
        }

        private void dlg_bspTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveUserSettings();
        }

        private void refreshMapList_Click(object sender, EventArgs e)
        {
            this.fillMapList();
        }

        public bool checkFileExists(string strFileName)
        {
            return StringType.StrCmp(strFileName, "", false) != 0 && StringType.StrCmp(FileSystem.Dir(strFileName, FileAttribute.Normal), "", false) != 0;
        }

        private bool CheckSavedMap(string strMapName)
        {
            return StringType.StrCmp(FileSystem.Dir(Application.StartupPath + "\\" + strMapName + ".settings", FileAttribute.Normal), "", false) != 0;
        }

        
        public bool runProcess(string strFilename, string strWorkingDirectory, string strArguments = "")
        {
            if (this.checkFileExists(strFilename))
                return this.runProcess_NoFileCheck(strFilename, strWorkingDirectory, strArguments);

            bool okOnly = true;
            string msgbox_msg = "Error: could not find the specified file " + strFilename + "\n";

            using (var form = new msgbox(msgbox_msg, okOnly)) {
                var result = form.ShowDialog();
            }

            return false;
        }

        public bool runProcess_NoFileCheck(string strFilename, string strWorkingDirectory, string strArguments = "")
        {
            new Process()
            {
                StartInfo = new ProcessStartInfo(strFilename)
                {
                    WorkingDirectory = strWorkingDirectory,
                    Arguments = strArguments,
                    UseShellExecute = false
                }
            }.Start();

            return true;
        }

        private void startConverter_Click(object sender, EventArgs e)
        {
            this.runProcess(vars.strWorkingDir + "cod4compiletools_convert.bat", vars.strWorkingDir, "\"" + vars.strBinPath + "\"");
        }

        private void startModBuilder_Click(object sender, EventArgs e)
        {
            this.runProcess(vars.strBinPath + "MoDBuilder.exe", vars.strBinPath, "");
        }

        private void startAssetMan_Click(object sender, EventArgs e)
        {
            this.runProcess(vars.strBinPath + "asset_manager.exe", vars.strBinPath, "");
        }

        private void startFxEditor_Click(object sender, EventArgs e)
        {
            this.runProcess(vars.strBinPath + "CoD4EffectsEd.exe", vars.strBinPath, "");
        }

        private void startRadiant_Click(object sender, EventArgs e)
        {
            this.runProcess(vars.strBinPath + "CoD4Radiant.exe", vars.strBinPath, "");
        }

        private void browse_cod4dir_Click(object sender, EventArgs e)
        {
            int num1 = (int)this.FolderBrowserDialog.ShowDialog();
            if (this.FolderBrowserDialog.SelectedPath.Length == 0)
                return;

            if (this.FolderBrowserDialog.SelectedPath.Length < 4)
            {
                bool okOnly = true;
                string msgbox_msg = "Error: That path is not a valid CoD4 path.";

                using (var form = new msgbox(msgbox_msg, okOnly))
                {
                    var result = form.ShowDialog();
                }
            }
            else if (StringType.StrCmp(FileSystem.Dir(this.FolderBrowserDialog.SelectedPath + "\\iw3sp.exe", FileAttribute.Normal), "", false) == 0)
            {
                bool okOnly = true;
                string msgbox_msg = "Error: That path is not a valid CoD4 path.";

                using (var form = new msgbox(msgbox_msg, okOnly))
                {
                    var result = form.ShowDialog();
                }
            }

            else
            {
                vars.strTreePath = this.folderBrowserDialog1.SelectedPath + "\\";
                this.updateLocalPaths();
                this.fillMapList();
                this.saveUserSettings();
            }
        }

        private void dlg_bspTools_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.saveUserSettings();
        }

        private void lstMapFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstMapFiles.SelectedIndex != -1)
            {
                vars.selectedMap_Index = this.lstMapFiles.SelectedIndex;
                vars.selectedMap_Name = StringType.FromObject(this.lstMapFiles.Items[vars.selectedMap_Index]);
                this.saveUserSettings();
                this.btnRunMap.Enabled = true;
                this.btnGrid.Enabled = true;

                if (this.CheckSavedMap(vars.selectedMap_Name))
                {
                    this.LoadSavedMap(vars.selectedMap_Name);
                    return;
                }
            }
            else
            {
                this.btnRunMap.Enabled = false;
                this.btnGrid.Enabled = false;
            }

            this.resetCompileOptions();
        }

        public bool isMPMap(string strMapName)
        {
            return strMapName.Length > 3 && BooleanType.FromString(Strings.LCase(StringType.FromBoolean(strMapName.StartsWith("mp_"))));
        }

        #region console

        private long consoleTicksWhenLastFocus = DateTime.Now.Ticks;
        private DateTime consoleProcessStartTime;

        private void LauncherTimer_Tick(object sender, EventArgs e)
        {
            if (this.execBatch != null)
                this.LauncherProcessTimeElapsedTextBox.Text = (DateTime.Now - this.consoleProcessStartTime).ToString().Substring(0, 8);
        }

        private void openConsole()
        {
            if (this.Size.Width == 755)
            {
                this.Size = new System.Drawing.Size(1280, 695);

                this.tool_logo.Location = new Point(this.tool_logo.Location.X + 525, this.tool_logo.Location.Y);
                this.btn_minimize.Location = new Point(this.btn_minimize.Location.X + 525, this.btn_minimize.Location.Y);
                this.btn_close.Location = new Point(this.btn_close.Location.X + 525, this.btn_close.Location.Y);
            }
        }

        private void closeConsole()
        {
            if (this.Size.Width == 1280)
            {
                this.Size = new System.Drawing.Size(755, 695);
                this.tool_logo.Location = new Point(this.tool_logo.Location.X - 525, this.tool_logo.Location.Y);
                this.btn_minimize.Location = new Point(this.btn_minimize.Location.X - 525, this.btn_minimize.Location.Y);
                this.btn_close.Location = new Point(this.btn_close.Location.X - 525, this.btn_close.Location.Y);
            }   
        }

        private void LauncherButtonCloseConsole_Click(object sender, EventArgs e)
        {
            closeConsole();
        }

        private string[] stringErrorArray =
        {
            "ERROR:",
            "******* leaked *******",
            "WROTE BSP LEAKFILE",
            "MAX_MAP_LIGHTBYTES"
        };

        private string[] stringWarningArray =
        {
            "WARNING:",
            "is missing",
            "Could not load file"
        };

        private string[] stringGreenArray =
        {
            "SUCCESS:"
        };

        private string[] stringStatusArray =
        {
            "CONSOLESTATUS:"
        };

        private string[] fatalError =
        {
            "Linker will now terminate",
            "UNRECOVERABLE",
            "(!)"
        };

        private void WriteConsole(string s, bool isStdError)
        {
            if (s == null)
                return;

            long ticks = DateTime.Now.Ticks;
            bool doFocus = ticks - this.consoleTicksWhenLastFocus > 10000000L;

            if (doFocus)
                this.consoleTicksWhenLastFocus = ticks;

            this.LauncherConsole.Invoke((Action)(() =>
            {
                Color selectionColor = this.LauncherConsole.SelectionColor;
                Font selectionFont = this.LauncherConsole.SelectionFont;

                if(stringErrorArray.Any(s.Contains))
                {
                    this.LauncherConsole.SelectionFont = new Font(this.LauncherConsole.SelectionFont, FontStyle.Bold);
                    this.LauncherConsole.SelectionColor = Color.Red;
                }

                else if (stringErrorArray.Any(s.Contains))
                {
                    this.LauncherConsole.SelectionFont = new Font(this.LauncherConsole.SelectionFont, FontStyle.Bold);
                    this.LauncherConsole.SelectionColor = Color.Red;
                }

                else if (stringWarningArray.Any(s.Contains))
                {
                    this.LauncherConsole.SelectionFont = new Font(this.LauncherConsole.SelectionFont, FontStyle.Bold);
                    this.LauncherConsole.SelectionColor = Color.Yellow;
                }

                else if (stringGreenArray.Any(s.Contains))
                {
                    this.LauncherConsole.SelectionFont = new Font(this.LauncherConsole.SelectionFont, FontStyle.Bold);
                    this.LauncherConsole.SelectionColor = Color.Green;
                }

                else if (stringStatusArray.Any(s.Contains))
                {
                    s = s.Replace("CONSOLESTATUS:", "");
                    this.label_cmpStatus.Text = "++ " + s + " ++";
                    s = "";
                }

                this.LauncherConsole.AppendText(s + "\n");
                if (doFocus)
                    this.LauncherConsole.Focus();

                this.LauncherConsole.SelectionColor = selectionColor;
                this.LauncherConsole.SelectionFont = selectionFont;
            }));
        }

        #endregion

        #region compiler

        private void toggleCompileButtons( bool onOff )
        {
            if( !onOff )
            {
                this.btnCompile.Enabled = false;
                this.btnReflections.Enabled = false;
                this.btnBuildFastFile.Enabled = false;
                this.btnMissingAssets.Enabled = false;
                this.btnGrid.Enabled = false;
                this.btnRunMap.Enabled = false;
                this.copy_iwiToIWD.Enabled = false;
            }

            else
            {
                this.btnCompile.Enabled = true;
                this.btnReflections.Enabled = true;
                this.btnBuildFastFile.Enabled = true;
                this.btnMissingAssets.Enabled = true;
                this.btnGrid.Enabled = true;
                this.btnRunMap.Enabled = true;
                this.copy_iwiToIWD.Enabled = true;
            }
        }

        private Process execBatch;

        private void createCompileProcess( string compileOptions, string fileName, [Optional] string otherPath, [Optional] string workingDirPath )
        {
            string customFilePath = vars.strWorkingDir;
            string customWorkDir = vars.strWorkingDir;
            
            // If other path defined:
            if (otherPath != null)
                customFilePath = otherPath;
            if (workingDirPath != null)
                customWorkDir = workingDirPath;

            string command = "/c ";

            System.Diagnostics.ProcessStartInfo openBatch = new System.Diagnostics.ProcessStartInfo(customFilePath + "\\" + fileName + " ", command);
            openBatch.WorkingDirectory = customWorkDir;

            // Batch Arguments
            openBatch.Arguments = compileOptions;
            // redirect to the Process.StandardOutput StreamReader.
            openBatch.RedirectStandardOutput = true;
            // redirect to the Process.StandardError StreamReader. 
            openBatch.RedirectStandardError = true;

            openBatch.UseShellExecute = false;
            openBatch.CreateNoWindow = true;

            // create process, assign ProcessStartInfo
            //System.Diagnostics.Process execBatch = new System.Diagnostics.Process();
            execBatch = new System.Diagnostics.Process();

            // Events
            execBatch.EnableRaisingEvents = true;

            // Startinfo
            execBatch.StartInfo = openBatch;

            openConsole();

            try
            {
                this.consoleProcessStartTime = DateTime.Now;

                execBatch.Start();
                execBatch.BeginOutputReadLine();

                this.LauncherConsole.Clear();
                this.LauncherButtonCancel.Text = "Cancel Current Compiler";
                this.LauncherButtonCancel.Enabled = true;
                this.LauncherButtonCloseConsole.Enabled = false;

                toggleCompileButtons(false);

                // Event output to console                    
                execBatch.OutputDataReceived += printProcessOutputToConsole;
                // Event error
                execBatch.ErrorDataReceived += printProcessErrorToConsole;

                execBatch.Exited += printProcessExitToConsole;
            }
            catch
            {
                execBatch = null;
                this.WriteConsole("ERROR: Can not find " + fileName + " in " + vars.strWorkingDir, true);

                this.LauncherButtonCancel.Text = "Cancel Current Compiler";
                this.LauncherButtonCancel.Enabled = false;
            }
        }

        void printProcessOutputToConsole(object sendingProcess, DataReceivedEventArgs e)
        {
            if (execBatch != null)
                this.WriteConsole(e.Data, true);
        }

        void printProcessErrorToConsole(object sendingProcess, DataReceivedEventArgs e)
        {
            if (execBatch != null)
                this.WriteConsole("ERROR: " + e.Data, true);
        }

        void printProcessExitToConsole(object sendingProcess, EventArgs e)
        {
            if (execBatch != null)
            {
                try
                {
                    this.LauncherButtonCancel.Text = "Compiler Finished";
                }

                catch { }

                toggleCompileButtons(true);
                this.LauncherButtonCancel.Enabled = false;
                this.LauncherButtonCloseConsole.Enabled = true;

                if (!execBatch.HasExited)
                    execBatch.Kill();

                execBatch = null;
            }
        }

        private void LauncherButtonCancel_Click(object sender, EventArgs e)
        {
            this.LauncherButtonCloseConsole.Enabled = true;

            if (!execBatch.HasExited)
            {
                execBatch.Kill();
                execBatch = null;

                LauncherConsole.Clear();
            }

            bool killedAny = false;
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == "cod4map" || theprocess.ProcessName == "cod4rad" || theprocess.ProcessName == "converter" || theprocess.ProcessName == "linker_pc")
                {
                    theprocess.Kill();
                    killedAny = true;
                }
            }

            if (killedAny)
            {
                LauncherConsole.Clear();

                this.WriteConsole("ERROR: Canceled compilation!", true);
                this.LauncherButtonCancel.Enabled = false;
            }
            else
                this.WriteConsole("WARNING: Nothing To Cancel!", true);

            toggleCompileButtons(true);
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(vars.selectedMap_Name, "", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: You must select a map first!", true);
            }

            else
            {
                this.SaveCompileSettings();
                string strMapSourcePath = vars.strMapSourcePath;
                string str1 = vars.strPCMapsPath;
                string str2 = "0";

                if (this.isMPMap(vars.selectedMap_Name))
                {
                    str2 = "1";
                    str1 = vars.strPCMapsPath_MP;
                }

                string str3 = "0";
                string str4 = "";
                if (this.chkBSP.Checked)
                {
                    str3 = "1";
                    if (this.chkOnlyEnts.Checked)
                    {
                        string str5 = "";
                        str4 = str5 + "-onlyents ";
                    }

                    if (this.chkBlockSize.Checked && StringType.StrCmp(this.txtBlockSize.Text, "", false) != 0)
                        str4 = str4 + "-blocksize " + this.txtBlockSize.Text + " ";
                    if (this.chkSampleScale.Checked && StringType.StrCmp(this.txtSampleScale.Text, "", false) != 0)
                        str4 = str4 + "-samplescale " + this.txtSampleScale.Text + " ";
                    if (this.chkDebugLightMaps.Checked)
                        str4 += "-debugLightmaps ";

                    if (this.chkCustomCommandLineBSP.Checked)
                    {
                        if (StringType.StrCmp(this.txtBSPOptions.Text, "", false) != 0)
                            str4 = str4 + this.txtBSPOptions.Text + " ";
                    }
                }
                string str6 = "0";
                string str7 = "";
                if (this.chkLight.Checked)
                {
                    str6 = "1";
                    if (this.chkLightFast.Checked)
                    {
                        string str5 = "";
                        str7 = str5 + "-fast ";
                    }

                    if (this.chkLightExtra.Checked)
                        str7 += "-extra ";
                    if (this.chkLightVerbose.Checked)
                        str7 += "-verbose ";
                    if (this.chkModelShadow.Checked)
                        str7 += "-modelshadow ";
                    if (this.chkNoModelShadow.Checked)
                        str7 += "-nomodelshadow ";
                    if (this.chkDumpOptions.Checked)
                        str7 += "-dumpoptions ";
                    if (this.chkTraces.Checked && StringType.StrCmp(this.txtTraces.Text, "", false) != 0)
                        str7 = str7 + "-traces " + this.txtTraces.Text + " ";
                    if (this.chkBounceFraction.Checked && StringType.StrCmp(this.txtBounceFraction.Text, "", false) != 0)
                        str7 = str7 + "-bouncefraction " + this.txtBounceFraction.Text + " ";
                    if (this.chkJitter.Checked && StringType.StrCmp(this.txtJitter.Text, "", false) != 0)
                        str7 = str7 + "-jitter " + this.txtJitter.Text + " ";

                    if (this.chkCustomCommandLineLight.Checked)
                    {
                        if (StringType.StrCmp(this.txtLightOptions.Text, "", false) != 0)
                            str7 = str7 + this.txtLightOptions.Text + " ";
                    }
                }
                string str8 = "0";
                if (this.chkPaths.Checked)
                    str8 = "1";

                string str9 = StringType.StrCmp(str4, "", false) != 0 ? "\"" + Strings.Trim(str4) + "\"" : "-";
                string str10 = StringType.StrCmp(str7, "", false) != 0 ? "\"" + Strings.Trim(str7) + "\"" : "-";
                string fileName = "cod4compiletools_compilebsp_custom.bat";
                string str11 = "";
                string str12 = " \"" + str1 + "\"" + " " + "\"" + strMapSourcePath + "\"" + " " + "\"" + vars.strTreePath + "\"" + " " + vars.selectedMap_Name + " " + str9 + " " + str10 + " " + str3 + " " + str6 + " " + str8 + " " + str11 + " " + str2;

                createCompileProcess(str12, fileName);
            }
        }

        private void btnReflections_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(vars.selectedMap_Name, "", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: You must select a map first!", true);
            }

            else
            {
                string str1 = "0";
                if (this.isMPMap(vars.selectedMap_Name))
                    str1 = "1";

                string fileName = "cod4compiletools_reflections_custom.bat";
                string str2 = "\"" + vars.strTreePath + "\"" + " " + vars.selectedMap_Name + " " + str1;

                createCompileProcess(str2, fileName);
            }
        }

        private void btnBuildFastFile_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(vars.selectedMap_Name, "", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: You must select a map first!", true);
            }

            else if (StringType.StrCmp(vars.selectedMap_Name, "mp_backlot", false) == 0 | StringType.StrCmp(vars.selectedMap_Name, "mp_backlot_geo", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: This sample map cannot be built using it's original name. Doing so would cause errors when trying to join a multiplayer match on another server.", true);
            }

            else
            {
                if (!this.CheckForZoneFiles(vars.selectedMap_Name))
                    return;

                string fileName = "build_custom.bat";
                if (copy_mapToUsermaps.Value)
                    fileName = "build_custom_move.bat"; ;

                string filePath = vars.strZoneSourcePath + "english\\";
                string workingDirPath = vars.strBinPath;

                string str = this.language + " " + vars.selectedMap_Name;

                createCompileProcess(str, fileName, filePath, workingDirPath );
            }
        }

        private void btnMissingAssets_Click(object sender, EventArgs e)
        {
            using (var form = new assets())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string val = form.ReturnValue_Asset;

                }
            }
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(vars.selectedMap_Name, "", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: You must select a map first!", true);
            }

            else
            {
                string str1 = this.gridOption.SelectedIndex != 0 ? "1" : "2";
                string str2 = "1";
                if (this.gridCullModels.Value)
                    str2 = "0";

                string str3 = "0";
                if (this.isMPMap(vars.selectedMap_Name))
                    str3 = "1";

                string fileName = "cod4compiletools_grid_custom.bat";
                string str4 = "\"" + vars.strTreePath + "\"" + " " + "\"" + vars.strMapSourcePath + "\"" + " " + str1 + " " + str2 + " " + vars.selectedMap_Name + " " + str3;

                createCompileProcess(str4, fileName);
            }
        }

        private void btnRunMap_Click(object sender, EventArgs e)
        {
            if (StringType.StrCmp(vars.selectedMap_Name, "", false) == 0)
            {
                openConsole();
                this.WriteConsole("ERROR: You must select a map first!", true);
            }

            else
            {
                string str1 = "0";
                if (this.isMPMap(vars.selectedMap_Name))
                    str1 = "1";

                string str2 = "";
                if (this.chkDeveloper.Checked)
                {
                    string str3 = "";
                    str2 = str3 + "+set developer 1 ";
                }

                if (this.chkDeveloperScript.Checked)
                    str2 += "+set developer_script 1 ";
                if (this.chkCheats.Checked)
                    str2 += "+set thereisacow 1337 ";
                if (this.chkCustomCommandLine.Checked & this.txtCustomCommandLine.Text.Length > 0)
                    str2 += this.txtCustomCommandLine.Text;
                string fileName = "cod4compiletools_runmap_custom.bat";
                string str4 = "\"" + vars.strTreePath + "\"" + " " + vars.selectedMap_Name + " " + str1 + " " + "\"" + str2 + "\"";

                createCompileProcess(str4, fileName);
            }
        }
        #endregion

        public static Color accentColor( Color colorValue )
        {
            int R = colorValue.R; int G = colorValue.G; int B = colorValue.B;
            if (R >= 20) R -= 20; if (G >= 20) G -= 20; if (B >= 20) B -= 20;

            Color newColor = Color.FromArgb(R, G, B);
            return newColor;
        }

        private void doMainColor()
        {
            this.Style = (MetroFramework.MetroColorStyle)vars.mainFormColor;

            int decColor = 0;
            Color color = Color.FromArgb(0, 0, 0);
            Color defChkBackColor = Color.FromArgb(30, 30, 30);
            Color btnActive;

            switch (vars.mainFormColor)
            {
                case 0: decColor = 0;               color = Color.FromArgb(0, 0, 0); break; // Default = 0
                case 1: decColor = 0;               color = Color.FromArgb(0, 0, 0); break; // Black = 1
                case 2: decColor = 16777215;        color = Color.FromArgb(255, 255, 255); break; // White = 2
                case 3: decColor = 10066329;        color = Color.FromArgb(90, 90, 90); break; // Silver = 3
                case 4: decColor = 1668818;         color = Color.FromArgb(95, 158, 200); break; // Blue = 4
                case 5: decColor = 45401;           color = Color.FromArgb(0, 177, 89); break; // Green = 5
                case 6: decColor = 13951319;        color = Color.FromArgb(134, 185, 30); break; // Lime = 6
                case 7: decColor = 1960374;         color = Color.FromArgb(0, 128, 128); break; // Teal = 7
                case 8: decColor = 16727296;        color = Color.FromArgb(235, 127, 70); break; // Orange = 8
                case 9: decColor = 13789470;        color = Color.FromArgb(210, 105, 30); break; // Brown = 9
                case 10: decColor = 16728193;       color = Color.FromArgb(190, 95, 215); break; // Pink = 10
                case 11: decColor = 12915042;       color = Color.FromArgb(255, 20, 147); break; // Magenta = 11
                case 12: decColor = 13959417;       color = Color.FromArgb(128, 0, 128); break; // Purple = 12
                case 13: decColor = 13959168;       color = Color.FromArgb(220, 20, 60); break; // Red = 13
                case 14: decColor = 16766464;       color = Color.FromArgb(208, 155, 32); break; // Yellow = 14

                // fuckin hex values in materialSkin
            }

            btnActive = accentColor(color);

            // Settings
            this.bunifuCustomLabel26.BackColor = color;
            this.bunifuSeparator3.LineColor = color;
            this.browse_cod4dir.Normalcolor = color; this.browse_cod4dir.Activecolor = btnActive; this.browse_cod4dir.OnHovercolor = btnActive;
            this.copy_mapToUsermaps.Oncolor = color;
            this.chkSettings_consoleOnStart.Oncolor = color;
            this.chkSettings_disableAnimations.Oncolor = color;
            this.bouncepatch_link.ForeColor = color;

            // Apps
            this.bunifuCustomLabel32.BackColor = color;
            this.bunifuSeparator6.LineColor = color;
            this.startModBuilder.Normalcolor = color; this.startModBuilder.Activecolor = btnActive; this.startModBuilder.OnHovercolor = btnActive;
            this.startRadiant.Normalcolor = color; this.startRadiant.Activecolor = btnActive; this.startRadiant.OnHovercolor = btnActive;
            this.startFxEditor.Normalcolor = color; this.startFxEditor.Activecolor = btnActive; this.startFxEditor.OnHovercolor = btnActive;
            this.startAssetMan.Normalcolor = color; this.startAssetMan.Activecolor = btnActive; this.startAssetMan.OnHovercolor = btnActive;
            this.startConverter.Normalcolor = color; this.startConverter.Activecolor = btnActive; this.startConverter.OnHovercolor = btnActive;

            // Level
            this.bunifuCustomLabel9.BackColor = color;
            this.bunifuSeparator5.LineColor = color;
            this.bunifuSeparator2.LineColor = color;
            this.bunifuCustomLabel21.BackColor = color;
            this.bunifuSeparator1.LineColor = color;

            // Main
            this.refreshMapList.Normalcolor = color; this.refreshMapList.Activecolor = btnActive; this.refreshMapList.OnHovercolor = btnActive;
            this.btnCompile.Normalcolor = color; this.btnCompile.Activecolor = btnActive; this.btnCompile.OnHovercolor = btnActive;
            this.btnReflections.Normalcolor = color; this.btnReflections.Activecolor = btnActive; this.btnReflections.OnHovercolor = btnActive;
            this.btnBuildFastFile.Normalcolor = color; this.btnBuildFastFile.Activecolor = btnActive; this.btnBuildFastFile.OnHovercolor = btnActive;
            this.btnMissingAssets.Normalcolor = color; this.btnMissingAssets.Activecolor = btnActive; this.btnMissingAssets.OnHovercolor = btnActive;
            this.btnGrid.Normalcolor = color; this.btnGrid.Activecolor = btnActive; this.btnGrid.OnHovercolor = btnActive;
            this.btnRunMap.Normalcolor = color; this.btnRunMap.Activecolor = btnActive; this.btnRunMap.OnHovercolor = btnActive;
            this.copy_iwiToIWD.Normalcolor = color; this.copy_iwiToIWD.Activecolor = btnActive; this.copy_iwiToIWD.OnHovercolor = btnActive;
            this.gridCullModels.Oncolor = color;

            // Console
            this.label_cmpStatus.BackColor = color;
            this.bunifuSeparator4.LineColor = color;

            materialSkinManager.ColorScheme = new ColorScheme(Primary.CustGrey, (Primary)decColor, (Primary)decColor, (Accent)decColor, TextShade.WHITE);

            this.Refresh();
            materialTabControl2.Refresh();
            materialTabControl1.Refresh();
        }

        private void doMainOpacity()
        {
            this.Opacity = vars.mainFormOpacity;
        }

        private void main_orange_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 8;
            doMainColor();
        }

        private void main_green_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 5;
            doMainColor();
        }

        private void main_silver_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 3;
            doMainColor();
        }

        private void main_white_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 2;
            doMainColor();
        }

        private void main_blue_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 4;
            doMainColor();
        }

        private void main_lime_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 6;
            doMainColor();
        }

        private void main_teal_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 7;
            doMainColor();
        }

        private void main_brown_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 9;
            doMainColor();
        }

        private void main_pink_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 10;
            doMainColor();
        }

        private void main_magenta_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 11;
            doMainColor();
        }

        private void main_purple_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 12;
            doMainColor();
        }

        private void main_red_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 13;
            doMainColor();
        }

        private void main_yellow_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 14;
            doMainColor();
        }

        private void main_black_Click(object sender, EventArgs e)
        {
            vars.mainFormColor = 1;
            doMainColor();
        }

        public void setLanguage()
        {
            string str1 = vars.strTreePath + "localization.txt";
            if (StringType.StrCmp(vars.strTreePath, "", false) == 0)
            {
                this.language = "english";
                vars.language = "english";
            }

            else
            {
                if (this.checkFileExists(str1))
                {
                    StreamReader streamReader = new StreamReader((Stream)File.OpenRead(str1));
                    string str2 = streamReader.ReadLine();
                    streamReader.Close();
                    if (str2.Length > 0)
                    {
                        this.language = str2;
                        vars.language = str2;
                    }
                        
                }

                else
                {
                    this.language = "english";
                    vars.language = "english";

                    string msgbox_msg = "Couldn't find " + str1 + "\n" + 
                                        "to see which language you are using. \n" +
                                        "Defaulting to English.";

                    using (var form = new msgbox(msgbox_msg, true))
                    {
                        var result = form.ShowDialog();
                    }
                }
                this.txtLanguage.Text = "Language: " + this.language;
            }
        }

        public bool createZoneFiles(string strMapName)
        {
            StreamWriter streamWriter1 = new StreamWriter((Stream)new FileStream(vars.strZoneSourcePath + "\\" + strMapName + ".csv", FileMode.Create));
            if (this.isMPMap(strMapName))
            {
                streamWriter1.WriteLine("ignore,code_post_gfx_mp");
                streamWriter1.WriteLine("ignore,common_mp");
                streamWriter1.WriteLine("ignore,localized_code_post_gfx_mp");
                streamWriter1.WriteLine("ignore,localized_common_mp");
                streamWriter1.WriteLine("col_map_mp,maps/mp/" + strMapName + ".d3dbsp");
                streamWriter1.WriteLine("rawfile,maps/mp/" + strMapName + ".gsc");
                streamWriter1.WriteLine("impactfx," + strMapName);
                streamWriter1.WriteLine("sound,common," + strMapName + ",!all_mp");
                streamWriter1.WriteLine("sound,generic," + strMapName + ",!all_mp");
                streamWriter1.WriteLine("sound,voiceovers," + strMapName + ",!all_mp");
                streamWriter1.WriteLine("sound,multiplayer," + strMapName + ",!all_mp");
                streamWriter1.Close();

                StreamWriter streamWriter2 = new StreamWriter((Stream)new FileStream(vars.strZoneSourcePath + "\\" + strMapName + "_load.csv", FileMode.Create));
                streamWriter2.WriteLine("ignore,code_post_gfx_mp");
                streamWriter2.WriteLine("ignore,common_mp");
                streamWriter2.WriteLine("ignore,localized_code_post_gfx_mp");
                streamWriter2.WriteLine("ignore,localized_common_mp");
                streamWriter2.WriteLine("ui_map,maps/" + strMapName + ".csv");
                streamWriter2.Close();
            }

            else
            {
                streamWriter1.WriteLine("ignore,code_post_gfx");
                streamWriter1.WriteLine("ignore,common");
                streamWriter1.WriteLine("col_map_sp,maps/" + strMapName + ".d3dbsp");
                streamWriter1.WriteLine("rawfile,maps/" + strMapName + ".gsc");
                streamWriter1.WriteLine("localize," + strMapName);
                streamWriter1.WriteLine("sound,common," + strMapName + ",!all_sp");
                streamWriter1.WriteLine("sound,generic," + strMapName + ",!all_sp");
                streamWriter1.WriteLine("sound,voiceovers," + strMapName + ",!all_sp");
                streamWriter1.WriteLine("sound,requests," + strMapName + ",!all_sp");
                streamWriter1.Close();
            }
            return true;
        }

        public bool CheckForZoneFiles(string strMapName)
        {
            bool fileExists;
            fileExists = this.checkFileExists(vars.strZoneSourcePath + "\\" + strMapName + ".csv");

            if(!fileExists)
            {
                string msgbox_msg = "There are no zone files for " + strMapName + ".\n" +
                                    "Would you like to create them?";

                using (var form = new msgbox(msgbox_msg))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        bool createdZoneFile = false;
                        createdZoneFile = this.createZoneFiles(strMapName);

                        if(createdZoneFile)
                            fileExists = true;
                    }
                }
            }

            return fileExists;
        }

        private void opacity_trackbar_Scroll(object sender, ScrollEventArgs e)
        {
            vars.mainFormOpacity = opacity_trackbar.Value * 0.01;
            this.Opacity = vars.mainFormOpacity;
            opacity_textbox.Text = vars.mainFormOpacity.ToString();
        }

        private void animSpeed_trackbar_Scroll(object sender, ScrollEventArgs e)
        {
            vars.mainFormAnimSpeed = animSpeed_trackbar.Value;
            animSpeed_textbox.Text = vars.mainFormAnimSpeed.ToString();
        }

        private void copy_iwiToIWD_Click(object sender, EventArgs e)
        {
            openConsole();
            this.label_cmpStatus.Text = "++ Copy Images to IWD ++";

            string text = vars.selectedMap_Name;
            if (text == null || vars.selectedMap_Name == null)
                this.WriteConsole("ERROR: Selected Map Invalid!", true);

            else
            {
                string usermap = Launcher.GetUsermapDirectory(text);
                string usermap_images = usermap + "images";
                string raw_images = vars.strTreePath + "raw\\images\\";

                string[] imagesToCopy = Launcher.GetImagesToCopy(text);
                if (imagesToCopy == null)
                {
                    this.WriteConsole("ERROR: No zone_source/assetlist/" + text + ".csv! Compile the map first!\nABORTING COPYING IMAGES!", true);
                    this.WriteConsole("" + imagesToCopy, true);
                }

                else if (!Directory.Exists(raw_images))
                    this.WriteConsole("ERROR: You didn't have a raw/images folder, make sure to go through Asset Manager/converter first!\nABORTING COPYING IMAGES!", true);

                else
                {
                    if (!Directory.Exists(usermap_images))
                        Directory.CreateDirectory(usermap_images);

                    foreach (string path2 in imagesToCopy)
                    {
                        if (File.Exists(Path.Combine(raw_images, path2)))
                        {
                            File.Copy(Path.Combine(raw_images, path2), Path.Combine(usermap_images, path2), true);
                            //this.WriteConsole("INFO: Copying image " + path2 + " to " + text + "/images!", false);
                        }

                        else
                            this.WriteConsole("ERROR: " + path2 + " does not exist in raw/images!", true);
                    }

                    string iwd = usermap + text + ".iwd";

                    if (!File.Exists(iwd))
                    {
                        this.WriteConsole("WARNING: IWD " + text + ".iwd not found; Creating ...", true);

                        using (FileStream zipToCreate = new FileStream(iwd, FileMode.Create))
                        {
                            using (ZipArchive newArchive = new ZipArchive(zipToCreate, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry readmeEntry;
                                DirectoryInfo d = new DirectoryInfo(usermap_images);
                                FileInfo[] Files = d.GetFiles("*");
                                foreach (FileInfo file in Files)
                                {
                                    readmeEntry = newArchive.CreateEntryFromFile(usermap_images + "\\" + file.Name, "images" + "/" + file.Name);
                                }
                            }
                        }
                    }

                    else
                    {
                        try
                        {
                            using (FileStream zipToOpen = new FileStream(iwd, FileMode.Open))
                            {
                                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                                {
                                    ZipArchiveEntry readmeEntry;
                                    DirectoryInfo d = new DirectoryInfo(usermap_images);
                                    FileInfo[] Files = d.GetFiles("*");
                                    foreach (FileInfo file in Files)
                                    {
                                        foreach (var item in archive.Entries)
                                        {
                                            if (item.Name.Equals(file.ToString()))
                                            {
                                                item.Delete();
                                                break;
                                            }
                                        }

                                        this.WriteConsole("INFO: Copying Image:     " + file.ToString(), true);
                                        readmeEntry = archive.CreateEntryFromFile(usermap_images + "\\" + file.Name, "images" + "/" + file.Name);
                                    }

                                    d.Delete(true);
                                }
                            }

                            this.WriteConsole("SUCCESS: Added Images to: " + text + ".iwd", true);
                        }

                        catch
                        {
                            this.WriteConsole("ERROR: Can not open: " + iwd + "!", true);
                            this.WriteConsole("ERROR: Be sure that no other process is using " + text + ".iwd", true);
                        }
                    }
                }
            }
        }

        private void bouncepatch_link_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.bouncepatch.com/cod4compiletools");
        }

        private void chkSettings_disableAnimations_Click(object sender, EventArgs e)
        {
            vars.mainFormAnimEnabled = this.chkSettings_disableAnimations.Value;
        }
    }
}
