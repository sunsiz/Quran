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
        string strLanguages = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "strLanguage", "True_True_True_False").ToString();

        private void Okbutton_Click(object sender, EventArgs e)
        {
            strLanguages = checkedListBox1.GetItemChecked(0).ToString() + "_" + checkedListBox1.GetItemChecked(1).ToString() + "_" + checkedListBox1.GetItemChecked(2).ToString() + "_" + checkedListBox1.GetItemChecked(3).ToString();
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "strLanguage", strLanguages);
            switch (comboBoxColor.SelectedItem.ToString())
            {
                case "Green":
                    kuranBgColor = "#D3E9D3";
                    kuranBorderColor = "#628F62";
                    kuranSelColor = "darkgreen";
                    break;
                case "Rose":
                    kuranBgColor = "#FFE4E1";
                    kuranBorderColor = "#BC8F8F";
                    kuranSelColor = "maroon";
                    break;
                case "Blue":
                    kuranBgColor = "#CCCCFF";
                    kuranBorderColor = "#336699";
                    kuranSelColor = "#0000CC";
                    break;
                case "Gray":
                    kuranBgColor = "#CCCCCC";
                    kuranBorderColor = "#777777";
                    kuranSelColor = "#555555";
                    break;
            }
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBgColor", kuranBgColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranBorderColor", kuranBorderColor);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "kuranSelColor", kuranSelColor);

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
            switch (kuranSelColor)
            {
                case "darkgreen":comboBoxColor.SelectedItem = "Green";break;
                case "maroon": comboBoxColor.SelectedItem = "Rose"; break;
                case "#0000CC": comboBoxColor.SelectedItem = "Blue"; break;
                case "#555555": comboBoxColor.SelectedItem = "Gray"; break;
                default:
                    break;
            }
        }
    }
}
