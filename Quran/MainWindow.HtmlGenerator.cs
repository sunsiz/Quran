using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quran
{
    partial class MainWindow
    {
        private void CreateDocument(int intSuraNo)
        {
            string strAppURL = "file:///" + Application.StartupPath;
            strAppURL = strAppURL.Replace(@"\", "/");

            var selectedSura = QuranData.Metadata.Sura[intSuraNo - 1];

            string strLanguages = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Suleymaniye Vakfi Meali\Kuran", "strLanguage", "True_True_True_False").ToString();
            var langs = strLanguages.Split('_');

            string SuraName_Arabic = bool.Parse(langs[0]) ?  selectedSura.Name : "";

            string SuraName_English = bool.Parse(langs[1]) ?  selectedSura.EName + " | " : "";

            string SuraName_Uyghur = bool.Parse(langs[2]) ?  selectedSura.EName + " | " : "";

            string SuraName_Bangla = bool.Parse(langs[3]) ?  selectedSura.TName + " | " : "";

            //string SuraName_Arabic = checkedListBox1.GetItemChecked(0) ?
            //    selectedSura.Name : "";

            //string SuraName_Bangla = checkedListBox1.GetItemChecked(1) ?
            //    selectedSura.TName + " | " : "";

            //string SuraName_English = checkedListBox1.GetItemChecked(2) ?
            //    selectedSura.EName + " | " : "";

            //string SuraName_Uyghur = checkedListBox1.GetItemChecked(3) ?
            //    selectedSura.EName + " | " : "";

            string SuraType = selectedSura.Type;
            int AyaCount = selectedSura.AyatCount;
            int RevelationOrder = selectedSura.Order;

            var sb = new StringBuilder();

            sb.AppendLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" >
<head>
    <title>Untitled Page</title>
    <style type=""text/css"">
        .tdAyah
        {
            border:solid 2px " + kuranBorderColor + @";
            padding:10px 15px 10px 15px;

        }
        .divArabic
        {
        	direction:rtl;
            text-align:justify;
            font-size:30px;
            font-family:me_quran;
        }
        .divTrans
        {
            text-align:justify;
            font-family:bangla;
        }
        .divUgTrans
        {
            direction:rtl;
            text-align:justify;
            font-size:25px;
            font-family:'Calibri';
        }
        .divRuku
        {
            text-align:center;
            font-family:bangla;
            padding: 20px;

        }
        .tdAyahNo
        {
        	direction:rtl;
            color:" + kuranBgColor + @";
            background-color:" + kuranBorderColor + @";
            text-align:center;
            font-family:me_quran;
            width:60px;


        }
    </style>

    <script type=""text/javascript"">
    function goWaitState(){
    divWait.style.display=""block"";
    }

    </script>
</head>
<body style='background-color:" + kuranBgColor + @"; margin:10px'>
<div id='divWait' 
style='position:fixed; display:none; color:White; font-size:35px; text-align:center;'>
Please Wait...</div>

<table width='100%'
style='background-position: center; 
margin: 0px; font-size:25px; 
border:solid 5px " + kuranBorderColor + @"; 
border-collapse:collapse; 
background-image: url(""" + strAppURL + @"/zekr-bg.png""); 
background-repeat: no-repeat; 
background-attachment: fixed;'>

<tr><td colspan='2'
style='color:white;
background-color:" + kuranBorderColor + @";
text-align:center;
padding-bottom:10px; height:49;'>
<span style='font-family:bangla; font-size:35px;'>"
                   + (intSuraNo).ToString() + ". " + SuraName_Bangla + SuraName_English + SuraName_Uyghur + @"</span>
<span style='font-family:me_quran;  font-size:35px;'>" + SuraName_Arabic + @"</span><br />
<span style='font-family:bangla; color:" + kuranBgColor + @"; font-size:20px;'>
Descent: " + SuraType + @", 
Ayah Count: " + AyaCount + @", 
Relevation Order: " + RevelationOrder + @" </span></td></tr>


");
            string strArDiv = bool.Parse(langs[0]) ?
                "<div class='divArabic'style='text-align:center'>بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ</div>"
                : "";
            string strEnDiv = bool.Parse(langs[1]) ?
                "<div class='divTrans' style='text-align:center'>In the name of Allah, the Entirely Merciful, the Especially Merciful.</div>"
                : "";
            string strUgDiv = bool.Parse(langs[2]) ?
                "<div class='divUgTrans' style='text-align:center'>ياخشىلىقى چەكسىز، ئىنئامى كۆپ ئاللاھنىڭ ئىسمى بىلەن باشلايمەن.</div>"
                : "";
            string strBnDiv = bool.Parse(langs[3]) ?
                "<div class='divTrans' style='text-align:center'>শুরু করছি আল্লাহর নামে যিনি পরম করুণাময়, অতি দয়ালু।</div>"
                : "";

            if (intSuraNo - 1 != 0 && intSuraNo - 1 != 8) //for not sura 1 and 9
                if (bool.Parse(langs[0]))
                    sb.AppendLine(@"<tr><td class='tdAyah'>"
                        + strArDiv + strBnDiv + strEnDiv + strUgDiv + @"</td>
<td id='tdAyahNo0_0' class='tdAyahNo'></td></tr>");
                else
                    sb.AppendLine(@"<tr><td id='tdAyahNo0_0' class='tdAyahNo'></td>
<td class='tdAyah'>" + strArDiv + strUgDiv + @"</td></tr>");


            string strTrAya;
            string ArAyaNo;
            string[] quranArabicText = QuranData.GetTexts("quran-simple-enhanced");
            string[] quranBanglaText = QuranData.GetTexts("bn.bengali");
            string[] quranEnglishText = QuranData.GetTexts("en.sahih");
            string[] quranUyghurText = QuranData.GetTexts("ug.enes");

            for (i = 0; i < selectedSura.AyatCount; i++)
            {

                strArDiv = bool.Parse(langs[0]) ?
                    "<div class='divArabic'>" + quranArabicText[selectedSura.StartIndex + i]
                    + @" <span style='font-size:20px'>۝</span></div>" //adding ۝ sign at every arabic ayah
                    : "";
                strEnDiv = bool.Parse(langs[1]) ?
                    "<div class='divTrans'>" + quranEnglishText[selectedSura.StartIndex + i] + @"</div>"
                    : "";
                strUgDiv = bool.Parse(langs[2]) ?
                    "<div class='divUgTrans'>" + quranUyghurText[selectedSura.StartIndex + i] + @"</div>"
                    : "";
                strBnDiv = bool.Parse(langs[3]) ?
                    "<div class='divTrans'>" + quranBanglaText[selectedSura.StartIndex + i] + @"</div>"
                    : "";

                if (QuranData.Metadata.IsRukuStart(intSuraNo, i + 1))
                {
                    strArDiv = "<div class='divRuku'>Ruku " + QuranData.Metadata.GetRukuNumber(intSuraNo, i + 1) + @"</div>" + strArDiv;
                }

                if (bool.Parse(langs[0]))
                {
                    ArAyaNo = ConverToArDigit(i + 1);

                    strTrAya = @"<tr><td class='tdAyah'>" + strArDiv + strBnDiv + strEnDiv + strUgDiv + @"</td>
<td title='"
                        + selectedSura.EName + "-"
                        + (i + 1)
                        + "' id='tdAyahNo" + intSuraNo + "_" + (i + 1)
                        + "' class='tdAyahNo'>﴿"
                        + ArAyaNo + @"﴾</td>                    
";
                }
                else
                    strTrAya = @"<tr><td title='"
                        + selectedSura.EName + "-"
                        + (intSuraNo - 1)
                        + "' id='tdAyahNo" + intSuraNo + "_" + (i + 1)
                        + "' class='tdAyahNo'>&nbsp"
                        + i + 1 + @"&nbsp</td>
                    <td class='tdAyah'>" + strArDiv + strBnDiv + strEnDiv + strUgDiv + @"</td></tr>
";

                sb.AppendLine(strTrAya);

            }

            sb.AppendLine(@"
</table></body></html>");

            webBrowser1.DocumentText = sb.ToString();


        }

    }
}
