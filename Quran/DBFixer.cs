using Quran.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quran
{
    public partial class DBFixer : Form
    {
        private string connectionString= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb;Persist Security Info=True";
        readonly QuranData QuranData = new QuranData();

        public DBFixer()
        {
            InitializeComponent();
        }

        private void DBFixer_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            connection.Open();
            string sql,izahat;
            for (int i = 1; i < 114; i++)
            {
                for (int k = 1; k <= QuranData.Metadata.Sura[i].AyatCount; k++)
                {
                    label1.Text = "Processing " + i + "- Sura " + k + "- Ayat";
                    sql = "SELECT izahat FROM Quran_Ayat WHERE ayat_sura_num=" + i + " and Ayat_num=" + k + ";";
                    richTextBox1.AppendText(sql + "\n");
                    command.CommandText = sql;
                    var izah = command.ExecuteScalar();
                    izahat = izah != null ? izah.ToString() : "";
                    if (!string.IsNullOrEmpty(izahat))
                    {
                        if (izahat.Contains(','))
                        {
                            var izahlar = izahat.Split(',');
                            foreach (var item in izahlar)
                            {
                                sql = "INSERT INTO Quran_Ayat_Izahat(Ayat_Id, Izahat_Id) Values('" + i + ":" + k + "'," + item + ");";
                                richTextBox1.AppendText(sql + "\n");
                                command.CommandText = sql;
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            sql = "INSERT INTO Quran_Ayat_Izahat(Ayat_Id, Izahat_Id) Values('" + i + ":" + k + "'," + izahat + ");";
                            richTextBox1.AppendText(sql + "\n");
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand();
            OleDbCommand izahCommand = new OleDbCommand();
            command.Connection = izahCommand.Connection = connection;
            connection.Open();
            string sql, izahSql, ayat, izahat, izahText;
            for (int i = 1; i <= 114; i++)
            {
                for (int k = 1; k <= QuranData.Metadata.Sura[1].AyatCount; k++)
                {
                    label1.Text = "Processing " + i + "- Sura " + k + "- Ayat";
                    sql = "SELECT Ayat_ayat_yeni, izahat FROM Quran_Ayat WHERE ayat_sura_num=" + i + " and Ayat_num=" + k + ";";
                    richTextBox1.AppendText(sql + "\n");
                    command.CommandText = sql;
                    var izah = command.ExecuteReader();
                    while (izah.Read())
                    {
                        ayat = izah[0].ToString();
                        izahat = izah[1].ToString();
                        if (!string.IsNullOrEmpty(izahat))
                        {
                            if (izahat.Contains(','))
                            {
                                var izahlar = izahat.Split(',');
                                foreach (var item in izahlar)
                                {
                                    izahSql = "SELECT Izahat From Quran_Izahat WHERE sure_id=" + i + " AND id=" + item + ";";
                                    richTextBox1.AppendText(izahSql + "\n");
                                    izahCommand.CommandText = izahSql;
                                    izahText=izahCommand.ExecuteScalar().ToString();
                                    ayat += "<br />[" + item + "] " + izahText;
                                }
                            }
                            else
                            {
                                izahSql = "SELECT Izahat From Quran_Izahat WHERE sure_id=" + i + " AND id=" + izahat + ";";
                                richTextBox1.AppendText(izahSql + "\n");
                                izahCommand.CommandText = izahSql;
                                izahText = izahCommand.ExecuteScalar().ToString();
                                ayat += "<br />[" + izahat + "] " + izahText;
                            }
                        }
                        ayat += "\n";
                        richTextBox2.AppendText(ayat);
                    }
                    izah.Close();
                }
                progressBar1.Value++;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox2.SaveFile("ug.enes.txt", RichTextBoxStreamType.UnicodePlainText);
            MessageBox.Show("File Saved As ug.enes.txt");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            connectionString = "Data Source=dellwinserver;Initial Catalog=SVMeal;User ID=dba;Password=123;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            string sql;
            sql = "SELECT [Id],[Sura],[Aya],[Text],[Comment],[DetailComment]FROM[SVMeal].[dbo].[QrTurkish]";
            richTextBox1.AppendText("İyiliği sonsuz, ikramı bol Allah’ın adıyla," + "\n");
            command.CommandText = sql;
            var ayahs = command.ExecuteReader();
            while (ayahs.Read())
            {
                richTextBox1.AppendText(ayahs["Text"].ToString().Replace("\r\n", ""));
                richTextBox1.AppendText(ayahs["Comment"].ToString().Replace("\r\n", ""));
                richTextBox1.AppendText("\n");
            }
        }
    }
}
