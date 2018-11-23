using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using MetroFramework.Forms;


namespace CoD4CompileTools
{
    public partial class assets : MetroForm
    {
        public string strLevelCSVFileName;
        public string ReturnValue_Asset { get; set; }

        public assets()
        {
            InitializeComponent();
            this.Opacity = 0;
        }

        private void assets_Load(object sender, EventArgs e)
        {
            this.txtMissingAssets.Clear();
            this.txtLevelCSV.Clear();
            this.readMissingAssets();

            string strLevelCSV = vars.strZoneSourcePath + vars.selectedMap_Name + ".csv";
            this.loadLevelCSV(strLevelCSV);

            if (!vars.mainFormAnimEnabled)
                FadeIn(this, vars.mainFormAnimSpeed);
            else
                this.Opacity = vars.mainFormOpacity;

            doMainColor();
        }

        private void readMissingAssets()
        {
            string str = vars.strTreePath + "main\\" + "missingasset.csv";
            if (!vars.checkFileExists(str))
            {
                string msgbox_msg = "Can't find " + str + "\n" +
                                    "Try building the level fast file again. \n" +
                                    "It's possible that the zone file is up to date.";

                using (var form = new msgbox(msgbox_msg, true))
                {
                    var result = form.ShowDialog();
                }

                this.Close();
            }
            else
            {
                StreamReader streamReader = new StreamReader((Stream)File.OpenRead(str));
                while (streamReader.Peek() != -1)
                    this.txtMissingAssets.AppendText(streamReader.ReadLine() + "\r");
                streamReader.Close();
            }
        }

        public void loadLevelCSV(string strLevelCSV)
        {
            this.strLevelCSVFileName = strLevelCSV;
            this.lblLevelCSV.Text = this.strLevelCSVFileName;
            if (!vars.checkFileExists(this.strLevelCSVFileName))
            {
                string msgbox_msg = "Can't find " + this.strLevelCSVFileName + "\n" +
                                    "You may need to try building the fast file first so a zone source file will be created.";

                using (var form = new msgbox(msgbox_msg, true))
                {
                    var result = form.ShowDialog();
                }

                this.Close();
            }
            else
            {
                StreamReader streamReader = new StreamReader((Stream)File.OpenRead(this.strLevelCSVFileName));
                while (streamReader.Peek() != -1)
                    this.txtLevelCSV.AppendText(streamReader.ReadLine() + "\r");
                streamReader.Close();
            }
        }

        private void saveNewLevelCSV()
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(this.strLevelCSVFileName, FileMode.Create));
                streamWriter.WriteLine(this.txtLevelCSV.Text);
                streamWriter.Close();

                if (!vars.mainFormAnimEnabled)
                    FadeOut(this, vars.mainFormAnimSpeed, 2);
                else
                    this.Close();
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);

                string msgbox_msg = this.strLevelCSVFileName + " could not be successfully updated." + "\n" +
                                    "Make sure file wasn't removed and is not being used by something else.";

                using (var form = new msgbox(msgbox_msg, true))
                {
                    var result = form.ShowDialog();
                }

                ProjectData.ClearProjectError();

                if (!vars.mainFormAnimEnabled)
                    FadeOut(this, vars.mainFormAnimSpeed, 0);
                else
                    this.Close();
            }
        }

        private void btnSaveLevelCSV_Click(object sender, EventArgs e)
        {
            this.saveNewLevelCSV();
        }

        private async void FadeIn(Form o, int interval = 80)
        {
            int newIntervall = Convert.ToInt16(Convert.ToDouble(interval) / vars.mainFormOpacity);
            while (o.Opacity < vars.mainFormOpacity)
            {
                await Task.Delay(newIntervall);
                o.Opacity += 0.05;
            }
            o.Opacity = vars.mainFormOpacity;
        }

        private async void FadeOut(Form o, int interval = 80, int action = 1)
        {
            int newIntervall = Convert.ToInt16(Convert.ToDouble(interval) / vars.mainFormOpacity);
            while (o.Opacity > 0.0)
            {
                await Task.Delay(newIntervall);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0;

            if (action == 1)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            else if (action == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            else if (action == 2)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (!vars.mainFormAnimEnabled)
                FadeOut(this, vars.mainFormAnimSpeed, 0);
            else
                this.Close();
        }

        public static Color accentColor(Color colorValue)
        {
            int R = colorValue.R; int G = colorValue.G; int B = colorValue.B;
            if (R >= 20) R -= 20; if (G >= 20) G -= 20; if (B >= 20) B -= 20;

            Color newColor = Color.FromArgb(R, G, B);
            return newColor;
        }

        private void doMainColor()
        {
            this.Style = (MetroFramework.MetroColorStyle)vars.mainFormColor;
            Color color = Color.FromArgb(0, 0, 0);
            Color btnActive;

            switch (vars.mainFormColor)
            {
                case 0: color = Color.FromArgb(0, 0, 0); break; // Default = 0
                case 1: color = Color.FromArgb(0, 0, 0); break; // Black = 1
                case 2: color = Color.FromArgb(255, 255, 255); break; // White = 2
                case 3: color = Color.FromArgb(90, 90, 90); break; // Silver = 3
                case 4: color = Color.FromArgb(95, 158, 200); break; // Blue = 4
                case 5: color = Color.FromArgb(0, 177, 89); break; // Green = 5
                case 6: color = Color.FromArgb(134, 185, 30); break; // Lime = 6
                case 7: color = Color.FromArgb(0, 128, 128); break; // Teal = 7
                case 8: color = Color.FromArgb(235, 127, 70); break; // Orange = 8
                case 9: color = Color.FromArgb(210, 105, 30); break; // Brown = 9
                case 10: color = Color.FromArgb(190, 95, 215); break; // Pink = 10
                case 11: color = Color.FromArgb(255, 20, 147); break; // Magenta = 11
                case 12: color = Color.FromArgb(128, 0, 128); break; // Purple = 12
                case 13: color = Color.FromArgb(220, 20, 60); break; // Red = 13
                case 14: color = Color.FromArgb(208, 155, 32); break; // Yellow = 14

                // fuckin hex values in materialSkin
            }

            btnActive = accentColor(color);
            // Assets
            this.bunifuCustomLabel9.BackColor = color;
            this.bunifuSeparator1.LineColor = color;

            this.bunifuCustomLabel1.BackColor = color;
            this.bunifuSeparator5.LineColor = color;

            this.btnSaveLevelCSV.Normalcolor = color; this.btnSaveLevelCSV.Activecolor = btnActive; this.btnSaveLevelCSV.OnHovercolor = btnActive;

            this.Refresh();
        }

        private void doMainOpacity()
        {
            this.Opacity = vars.mainFormOpacity;
        }
    }
}


