using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

using Quran.Data;

namespace Quran
{
    public partial class MainWindow : Form
    {
        QuranData QuranData = new QuranData();

        int i = 0;
        //int k;
        bool isPlaying = false;
        //int curAyaIndex = 0;

        string strSelAyahId;

        string kuranBgColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBgColor", "#D3E9D3").ToString();
        string kuranBorderColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBorderColor", "#628F62").ToString();
        string kuranSelColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranSelColor", "darkgreen").ToString();
        string kuranForeColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranForeColor", "Black").ToString();

        //string strLanguages = "True_True_True_False";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = 0; i <= QuranData.Metadata.Sura.Length - 1; i++)
            {
                //comboBoxSura.Items.Add((i + 1) + ". " + QuranData.Metadata.Sura[i].TName);
                SureListBox.Items.Add((i + 1) + "." + QuranData.Metadata.Sura[i].TName + " - " + QuranData.Metadata.Sura[i].Name);
            }

            //kuranBgColor = "#D3E9D3";
            //kuranBorderColor = "#628F62";
            //kuranSelColor = "darkgreen";
            ApplyTheme();

            //Make control transparent.
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.TransparencyKey = Color.FromKnownColor(KnownColor.Control);
            //this.Update();

            groupBox1_Resize(sender, e);

            //checkedListBox1.SetItemChecked(0, true);
            //checkedListBox1.SetItemChecked(3, true);

            try
            {
                //comboBoxSura.SelectedIndex = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "CurrentSura", 0).GetHashCode();
                SureListBox.SelectedIndex = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "CurrentSura", 0).GetHashCode();
            }
            catch
            {
                //comboBoxSura.SelectedIndex = 0;
                SureListBox.SelectedIndex = 0;
            }
        }

        private void Load_Document()
        {
            if (webBrowser1.DocumentText != "")
                webBrowser1.Document.InvokeScript("goWaitState");

            //strSelAyahId = "tdAyahNo" + (comboBoxSura.SelectedIndex + 1).ToString() + "_1";
            strSelAyahId = "tdAyahNo" + (SureListBox.SelectedIndex + 1).ToString() + "_1";
            timerLoadDocument.Enabled = true;
        }

        private void comboBoxSura_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (webBrowser1.DocumentText != "")
            //    webBrowser1.Document.InvokeScript("goWaitState");

            //strSelAyahId = "tdAyahNo" + (comboBoxSura.SelectedIndex+1).ToString() + "_1";
            //timerLoadDocument.Enabled = true;
            Load_Document();

            //comboBoxAya.Items.Clear();

            //for (k = 1; k <= QuranData.Metadata.Sura[comboBoxSura.SelectedIndex].AyatCount; k++)
            //    comboBoxAya.Items.Add(k);

            //comboBoxRuku.Items.Clear();

            //for (k = 1; k <= QuranData.Metadata.Sura[comboBoxSura.SelectedIndex].Rukus; k++)
            //    comboBoxRuku.Items.Add(k);


        }
        string ConverToArDigit(int EnDigit)
        {
            //۰۱۲۳۴۵۶۷۸۹
            string temp;

            temp = EnDigit.ToString();

            //٠١٢٣٤٥٦٧٨٩
            temp = temp.Replace('0', '٠');
            temp = temp.Replace('1', '١');
            temp = temp.Replace('2', '٢');
            temp = temp.Replace('3', '٣');
            temp = temp.Replace('4', '٤');
            temp = temp.Replace('5', '٥');
            temp = temp.Replace('6', '٦');
            temp = temp.Replace('7', '٧');
            temp = temp.Replace('8', '٨');
            temp = temp.Replace('9', '٩');

            return temp;

        }

        private void comboBoxAya_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnNextAya.Enabled = comboBoxAya.SelectedIndex < comboBoxAya.Items.Count - 1;
            //btnPrevAya.Enabled = comboBoxAya.SelectedIndex > 0;

            //setSelectedAyah(comboBoxAya.SelectedIndex + 1);
        }
        private void setSelectedAyah(int AyahNo)
        {
            if (AyahNo <= 0) AyahNo = 1;
            if (!webBrowser1.IsBusy)
            {
                webBrowser1.Document.GetElementById(strSelAyahId).Style = "background-color:" + kuranBorderColor + @";color:" + kuranForeColor; //restore old selected ayah in browser

                strSelAyahId = "tdAyahNo" + (SureListBox.SelectedIndex + 1).ToString() + "_" + (AyahNo).ToString();
                webBrowser1.Document.GetElementById(strSelAyahId).Style = "background-color:" + kuranSelColor + @"; color:" + kuranForeColor + @"; font-weight:bold;"; //select ayah in browser
                if (AyahNo != 1)
                    webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.GetElementById(strSelAyahId).OffsetRectangle.Top - 50); //scroll to selected ayah
                else
                    webBrowser1.Document.Window.ScrollTo(0, 0);
            }
        }



        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //if (isPlaying == true && axWMP.playState == WMPLib.WMPPlayState.wmppsStopped)
            //    timerPlayback.Enabled = true;
        }


        private void timerPlayback_Tick(object sender, EventArgs e)
        {
            timerPlayback.Enabled = false;

            //if (comboBoxAya.SelectedIndex >= comboBoxAya.Items.Count - 1)
            //    return;

            //comboBoxAya.SelectedIndex += 1;

            //int intSuraNo = comboBoxSura.SelectedIndex + 1;
            //string strSuraNo = intSuraNo < 10 ?
            //            strSuraNo = "00" + intSuraNo.ToString() : intSuraNo < 100 ?
            //                strSuraNo = "0" + intSuraNo.ToString() : strSuraNo = intSuraNo.ToString();

            //int intAyaNo = comboBoxAya.SelectedIndex + 1;
            //string strAyaNo = intAyaNo < 10 ?
            //            strAyaNo = "00" + intAyaNo.ToString() : intAyaNo < 100 ?
            //                strAyaNo = "0" + intAyaNo.ToString() : strAyaNo = intAyaNo.ToString();

            ////axWMP.URL = @"G:\Quran Recitation\afasy-64kbps-offline.recit\afasy-64kbps-offline\" + strSuraNo + "\\" + strSuraNo + strAyaNo + ".mp3";
            //if (comboBoxReciter.Text == "Mishari Rashid Bin Alafasy")
            //    axWMP.URL = @"G:\Quran Recitation\afasy-64kbps-offline.recit\afasy-64kbps-offline\" + strSuraNo + "\\" + strSuraNo + strAyaNo + ".mp3";
            //else if (comboBoxReciter.Text == "Abdul Basit")
            //    axWMP.URL = Application.StartupPath + @"\Recitations\abdulbasit-mujawwad-64kbps-offline.recit\abdulbasit-mujawwad-64kbps-offline\" + strSuraNo + "\\" + strSuraNo + strAyaNo + ".mp3";

            //axWMP.Ctlcontrols.play();

        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (curAyaIndex == 0)
            //{
            //    curAyaIndex = Registry.GetValue(@"HKEY_CURRENT_USER\Software\DFA Tech\Quran", "CurrentAyah", 0).GetHashCode();
            //    comboBoxAya.SelectedIndex = curAyaIndex;
            //}
            //else comboBoxAya.SelectedIndex = 0;

        }

        private void btnNextSura_Click(object sender, EventArgs e)
        {
            //if (comboBoxSura.SelectedIndex < comboBoxSura.Items.Count - 1) comboBoxSura.SelectedIndex += 1;
            if (SureListBox.SelectedIndex < SureListBox.Items.Count - 1) SureListBox.SelectedIndex += 1;

        }

        private void btnPrevSura_Click(object sender, EventArgs e)
        {
            //if (comboBoxSura.SelectedIndex > 0) comboBoxSura.SelectedIndex -= 1;
            if (SureListBox.SelectedIndex > 0) SureListBox.SelectedIndex -= 1;

        }

        private void btnNextAya_Click(object sender, EventArgs e)
        {
            //if (comboBoxAya.SelectedIndex < comboBoxAya.Items.Count - 1) comboBoxAya.SelectedIndex += 1;

        }

        private void btnPrevAya_Click(object sender, EventArgs e)
        {
            //if (comboBoxAya.SelectedIndex > 0) comboBoxAya.SelectedIndex -= 1;

        }

        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {

            //switch (comboBoxColor.Text)
            //{
            //    case "Green":
            //        kuranBgColor = "#D3E9D3";
            //        kuranBorderColor = "#628F62";
            //        kuranSelColor = "darkgreen";
            //        break;
            //    case "Rose":
            //        kuranBgColor = "#FFE4E1";
            //        kuranBorderColor = "#BC8F8F";
            //        kuranSelColor = "maroon";
            //        break;
            //    case "Blue":
            //        kuranBgColor = "#CCCCFF";
            //        kuranBorderColor = "#336699";
            //        kuranSelColor = "#0000CC";
            //        break;
            //    case "Gray":
            //        kuranBgColor = "#CCCCCC";
            //        kuranBorderColor = "#777777";
            //        kuranSelColor = "#555555";
            //        break;

            //}
            //Load_Document();

            //setSelectedAyah(comboBoxAya.Text.GetHashCode());

        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            //panel1.Width = (groupBox1.Width / 2) - panel1.Left;
            //panel2.Width = panel1.Width - panel1.Left;
            //panel2.Left = panel1.Width + panel1.Left * 2;
            groupBox1.Refresh();
            //groupBox2.Refresh();
            SureListBox.Left = groupBox1.Left + 3;
            SureListBox.Width = groupBox1.Width - 23;
            //groupBox3.Refresh();
            //groupBox4.Refresh();

        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (strLanguages != checkedListBox1.GetItemChecked(0).ToString() + "_" + checkedListBox1.GetItemChecked(1).ToString() + "_" + checkedListBox1.GetItemChecked(2).ToString())
            //{
            //    CreateDocument(comboBoxSura.SelectedIndex + 1);
            //    strLanguages = checkedListBox1.GetItemChecked(0).ToString() + "_" + checkedListBox1.GetItemChecked(1).ToString() + "_" + checkedListBox1.GetItemChecked(2).ToString();

            //}

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //axWMP.settings.volume = trackBar1.Value;

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //timerPlayback.Enabled = true;

            //isPlaying = true;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //isPlaying = false;
            //timerPlayback.Enabled = false;
            //axWMP.Ctlcontrols.stop();

        }

        private void timerLoadDocument_Tick(object sender, EventArgs e)
        {
            timerLoadDocument.Enabled = false;
            //CreateDocument(comboBoxSura.SelectedIndex + 1);
            CreateDocument(SureListBox.SelectedIndex + 1);

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //            //SELECT * FROM quran WHERE (AyahText LIKE '%الْحَمْدُ %')
            //            string searchin = "";
            //            if (comboBoxSearchIn.SelectedIndex == 1)
            //                searchin = "AND SuraID=" + (comboBoxSura.SelectedIndex + 1).ToString();

            //            dt = quranDB.Search(textBoxSearch.Text, searchin);

            //            string strAppURL = "file:///" + Application.StartupPath;
            //            strAppURL = strAppURL.Replace(@"\", "/");


            //            richTextBox1.Clear();
            //            richTextBox1.AppendText(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            //<html xmlns=""http://www.w3.org/1999/xhtml"" >
            //<head>
            //    <title>Untitled Page</title>
            //    <style type=""text/css"">
            //        .tdAyah
            //        {
            //            border:solid 2px " + quranBorderColor + @";
            //            padding:5px 10px 5px 10px;

            //        }
            //        .divArabic
            //        {
            //        	direction:rtl;
            //            text-align:justify;
            //            font-size:30px;
            //            font-family:me_quran;
            //        }
            //        .divTrans
            //        {
            //            text-align:justify;
            //            font-family:bangla;
            //        }
            //        .tdAyahNo
            //        {
            //        	direction:rtl;
            //            color:" + quranBgColor + @";
            //            background-color:" + quranBorderColor + @";
            //            text-align:center;
            //            font-family:me_quran;
            //            width:60px;


            //        }
            //    </style>

            //    <script type=""text/javascript"">
            //    function goWaitState(){
            //    divWait.style.display=""block"";
            //    }

            //    </script>
            //</head>
            //<body style='background-color:" + quranBgColor + @"; margin:10px'>
            //<div id='divWait' 
            //style='position:fixed; display:none; color:White; font-size:35px; text-align:center;'>
            //Please Wait...</div>

            //<table width='100%'
            //style='background-position: center; 
            //margin: 0px; font-size:25px; 
            //border:solid 5px " + quranBorderColor + @"; 
            //border-collapse:collapse; 
            //background-image: url(""" + strAppURL + @"/zekr-bg.png""); 
            //background-repeat: no-repeat; 
            //background-attachment: fixed;'>

            //<tr><td colspan='2'
            //style='color:white;
            //background-color:" + quranBorderColor + @";
            //text-align:center;
            //padding-bottom:10px; height:49;'>
            //Search Result<br/>
            //" + dt.Rows.Count + @" Ayats contains """ + textBoxSearch.Text + @"""
            //</td></tr>


            //");
            //            string strArDiv;
            //            string strBnDiv;
            //            string strEnDiv;

            //            string strTrAya;

            //            for (i = 0; i < dt.Rows.Count; i++)
            //            {
            //                strArDiv = checkedListBox1.GetItemChecked(0) ?
            //                    "<div class='divArabic'>" + dt.Rows[i]["AyahText"].ToString()
            //                    + @" <span style='font-size:20px'>۝</span></div>" //adding ۝ sign at every arabic ayah
            //                    : "";
            //                strArDiv = strArDiv.Replace(textBoxSearch.Text, "<b style='color:" + quranBgColor + "; background-color:" + quranBorderColor + "'>" + textBoxSearch.Text + "</b>");

            //                strBnDiv = checkedListBox1.GetItemChecked(1) ?
            //                    "<div class='divTrans'>" + dt.Rows[i]["easy_bn_trans"].ToString() + @"</div>"
            //                    : "";
            //                strBnDiv = strBnDiv.Replace(textBoxSearch.Text, "<b style='color:" + quranBgColor + "; background-color:" + quranBorderColor + "'>" + textBoxSearch.Text + "</b>");

            //                strEnDiv = checkedListBox1.GetItemChecked(2) ?
            //                    "<div class='divTrans'>" + dt.Rows[i]["English"].ToString() + @"</div>"
            //                    : "";
            //                strEnDiv = strEnDiv.Replace(textBoxSearch.Text, "<b style='color:" + quranBgColor + "; background-color:" + quranBorderColor + "'>" + textBoxSearch.Text + "</b>");


            //                //ArAyaNo = dt.Rows[i]["suraID"].GetHashCode() + ":" + dt.Rows[i]["verseID"].GetHashCode();

            //                strTrAya = @"<tr><td class='tdAyah'>" + strArDiv + strBnDiv + strEnDiv + @"</td>
            //<td title="
            //                    + dt_SuraInfo.Rows[dt.Rows[i]["suraID"].GetHashCode() - 1]["tName_en"].ToString() + "-"
            //                    + dt.Rows[i]["verseID"].ToString()
            //                    + " id='tdAyahNo" + dt.Rows[i]["suraID"].ToString() + "_" + dt.Rows[i]["verseID"].ToString()
            //                    + "' class='tdAyahNo'>"
            //                    + dt.Rows[i]["suraID"].GetHashCode() + ":" + dt.Rows[i]["verseID"].GetHashCode() + @"</td>                    
            //";



            //                richTextBox1.AppendText(strTrAya);

            //            }

            //            richTextBox1.AppendText(@"
            //</table></body></html>");

            //            webBrowser1.DocumentText = richTextBox1.Text;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Registry.SetValue(@"HKEY_CURRENT_USER\Software\DFA Tech\Quran", "jk", "9");
            this.Text = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "jk", "5").ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "CurrentSura", SureListBox.SelectedIndex);
            //Registry.SetValue(@"HKEY_CURRENT_USER\Software\DFA Tech\Quran", "CurrentSura", comboBoxSura.SelectedIndex);
            //Registry.SetValue(@"HKEY_CURRENT_USER\Software\DFA Tech\Quran", "CurrentAyah", comboBoxAya.SelectedIndex);

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 abbox = new AboutBox1();
            abbox.Show();

        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
            ApplyTheme();
            Load_Document();
            //setSelectedAyah(comboBoxAya.Text.GetHashCode());
        }

        private void ApplyTheme()
        {
            string kuranBgColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBgColor", "#D3E9D3").ToString();
            //string kuranBorderColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBorderColor", "#628F62").ToString();
            //string kuranSelColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranSelColor", "darkgreen").ToString();
            //string kuranForeColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranForeColor", "Black").ToString();
            string currentTheme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "currentTheme", "Light").ToString();
            //BackColor = this.splitContainer1.Panel1.BackColor = this.menuStrip1.BackColor = ColorTranslator.FromHtml(kuranBgColor);
            switch (currentTheme)
            {
                case "Green": this.BackgroundImage = Image.FromFile(@"Images\bggreen.png"); break;
                case "Rose": this.BackgroundImage = Image.FromFile(@"Images\bgpurple.png"); break;
                case "Blue": this.BackgroundImage = Image.FromFile(@"Images\bgblue.jpg"); break;
                case "Gray": this.BackgroundImage = Image.FromFile(@"Images\bggray.jpg"); break;
                case "Material Blue": this.BackgroundImage = Image.FromFile(@"Images\bgmaterialblue.png"); break;
                default:this.BackgroundImage = null;
                    break;
            }
            if (this.BackgroundImage==null)
            {
                this.BackColor = splitContainer1.Panel1.BackColor = menuStrip1.BackColor = ColorTranslator.FromHtml(kuranBgColor);
            }
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Find().ShowDialog();
        }

        private void SureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Document();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DBFixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBFixer window = new DBFixer();
            window.Show();
        }
    }
}
