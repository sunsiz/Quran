using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quran
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        string kuranBgColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBgColor", "#D3E9D3").ToString();
        string kuranBorderColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBorderColor", "#628F62").ToString();
        string kuranSelColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranSelColor", "darkgreen").ToString();
        string kuranForeColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranForeColor", "Black").ToString();
        string kuranHeadColor = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranHeadColor", "Black").ToString();
        string strLanguages = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "strLanguage", "True_True_True_False").ToString();
        string currentTheme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "currentTheme", "Light").ToString();

        private void Okbutton_Click(object sender, EventArgs e)
        {
            strLanguages = checkedListBox1.GetItemChecked(0).ToString() + "_" + checkedListBox1.GetItemChecked(1).ToString() + "_" + checkedListBox1.GetItemChecked(2).ToString() + "_" + checkedListBox1.GetItemChecked(3).ToString();
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "strLanguage", strLanguages);
            //switch (comboBoxColor.SelectedItem.ToString())
            //{
            //    case "Green":
            //        kuranBgColor = "#66BB6A";
            //        kuranBorderColor = "#43A047";
            //        kuranSelColor = "darkgreen";
            //        kuranForeColor = "white";
            //        break;
            //    case "Rose":
            //        kuranBgColor = "#F48FB1";
            //        kuranBorderColor = "#E57373";
            //        kuranSelColor = "maroon";
            //        kuranForeColor = "black";
            //        break;
            //    case "Blue":
            //        kuranBgColor = "#81D4FA";
            //        kuranBorderColor = "#40C4FF";
            //        kuranSelColor = "#0000CC";
            //        kuranForeColor = "Black";
            //        break;
            //    case "Gray":
            //        kuranBgColor = "#9E9E9E";
            //        kuranBorderColor = "#BDBDBD";
            //        kuranSelColor = "#555555";
            //        kuranForeColor = "Black";
            //        break;
            //    case "Light":
            //        kuranBgColor = Color.FromKnownColor(KnownColor.Control).ToArgb().ToString();
            //        kuranBorderColor = "#FAFAFA"/*Color.FromKnownColor(KnownColor.WindowText).ToArgb().ToString()*/;
            //        kuranSelColor = Color.FromKnownColor(KnownColor.ControlDarkDark).ToArgb().ToString();
            //        kuranForeColor = Color.FromKnownColor(KnownColor.ControlText).ToArgb().ToString();
            //        break;
            //    case "Material Blue":
            //        kuranBgColor = "#4278F4";
            //        kuranBorderColor = "#3872F0";
            //        kuranSelColor = "#4989FF";
            //        kuranForeColor = "White";
            //        break;
            //    case "Dark":
            //        kuranBgColor = "#424242";
            //        kuranBorderColor = "#212121";
            //        kuranSelColor = "#263238";
            //        kuranForeColor = "White";
            //        break;
            //}
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBgColor", kuranBgColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBorderColor", kuranBorderColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranSelColor", kuranSelColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranForeColor", kuranForeColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranHeadColor", kuranHeadColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "currentTheme", currentTheme);

        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            var langs = strLanguages.Split('_');
            for (int i = 0; i < langs.Length; i++)
            {
                checkedListBox1.SetItemChecked(i, bool.Parse(langs[i]));
            }
            switch (currentTheme)
            {
                case "Green": comboBoxColor.SelectedItem = "Green"; this.BackgroundImage = Image.FromFile(@"Images\bggreen.png"); break;
                case "Rose": comboBoxColor.SelectedItem = "Rose"; this.BackgroundImage = Image.FromFile(@"Images\bgpurple.png"); break;
                case "Blue": comboBoxColor.SelectedItem = "Blue"; this.BackgroundImage = Image.FromFile(@"Images\bgblue.jpg"); break;
                case "Gray": comboBoxColor.SelectedItem = "Gray"; this.BackgroundImage = Image.FromFile(@"Images\bggray.jpg"); break;
                case "Material Blue": comboBoxColor.SelectedItem = "Material Blue"; this.BackgroundImage = Image.FromFile(@"Images\bgmaterialblue.png"); break;
                case "Dark": comboBoxColor.SelectedItem = "Dark"; this.BackgroundImage = null; break;
                default: comboBoxColor.SelectedItem = "Light"; this.BackgroundImage = null; break;
            }
            if (BackgroundImage == null)
            {
                BackColor = groupBox1.BackColor = groupBox4.BackColor = ColorTranslator.FromHtml(kuranBgColor);
            }
        }

        private void ComboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxColor.SelectedItem.ToString())
            {
                case "Green": kuranBgColor = "#66BB6A"; kuranBorderColor = "#43A047"; kuranSelColor = "darkgreen"; kuranForeColor = "white"; kuranHeadColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bggreen.png"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; currentTheme = "Green"; break;
                case "Rose": kuranBgColor = "#F48FB1"; kuranBorderColor = "#E57373"; kuranSelColor = "maroon"; kuranForeColor = "black"; kuranHeadColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bgpurple.png"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; currentTheme = "Rose"; break;
                case "Blue": kuranBgColor = "#81D4FA"; kuranBorderColor = "#40C4FF"; kuranSelColor = "#0000CC"; kuranForeColor = "Black"; kuranHeadColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bgblue.jpg"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; currentTheme = "Blue"; break;
                case "Gray": kuranBgColor = "#9E9E9E"; kuranBorderColor = "#BDBDBD"; kuranSelColor = "#555555"; kuranForeColor = "Black"; kuranHeadColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bggray.jpg"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; currentTheme = "Gray"; break;
                //case "Material Blue": kuranBgColor = "#4278F4"; kuranBorderColor = "#3872F0"; kuranSelColor = "#4989FF"; kuranForeColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bgmaterialblue.png"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; break;
                case "Material Blue": kuranBgColor = "#f8f9fa"; kuranBorderColor = "#4285F4"; kuranSelColor = "#4989FF"; kuranForeColor = "Black"; kuranHeadColor = "White"; this.BackgroundImage = Image.FromFile(@"Images\bgmaterialblue.png"); groupBox1.BackColor = groupBox4.BackColor = Color.Transparent; currentTheme = "Material Blue"; break;
                case "Light":
                    kuranBgColor = Color.FromKnownColor(KnownColor.Control).ToArgb().ToString();
                    kuranBorderColor = "#FAFAFA"/*Color.FromKnownColor(KnownColor.WindowText).ToArgb().ToString()*/;
                    kuranSelColor = Color.FromKnownColor(KnownColor.ControlDarkDark).ToArgb().ToString();
                    kuranForeColor = Color.FromKnownColor(KnownColor.ControlText).ToArgb().ToString();
                    kuranHeadColor = Color.FromKnownColor(KnownColor.Black).ToArgb().ToString();
                    this.BackgroundImage = null; BackColor = groupBox1.BackColor = groupBox4.BackColor = ColorTranslator.FromHtml(kuranBgColor); currentTheme = "Light";
                    break;
                case "Dark": kuranBgColor = "#424242"; kuranBorderColor = "#212121"; kuranSelColor = "#263238"; kuranForeColor = "White"; kuranHeadColor = "White"; this.BackgroundImage = null; BackColor = groupBox1.BackColor = groupBox4.BackColor = ColorTranslator.FromHtml(kuranBgColor); currentTheme = "Dark"; break;
                default:
                    break;
            }
        }
    }
}
