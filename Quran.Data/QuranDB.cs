using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Data
{
    [Obsolete]
    public class QuranDB
    {
        OleDbConnection cnn;
        OleDbCommand cmd;
        OleDbDataAdapter da;

        public QuranDB(string accdbFilePath)
        {
            cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + accdbFilePath);
        }

        public DataTable GetSuraInfo()
        {
            DataTable dt_SuraInfo;
            cnn.Open();
            cmd = new OleDbCommand("select * from SuraInfo order by Number;", cnn);
            da = new OleDbDataAdapter(cmd);
            dt_SuraInfo = new DataTable();
            da.Fill(dt_SuraInfo);
            cnn.Close();

            return dt_SuraInfo;
        }

        public DataTable GetSuraById(int suraId)
        {
            cnn.Open();
            da = new OleDbDataAdapter("select * from Quran where suraID=" + (suraId) + " ORDER BY ID;", cnn);
            var dt = new DataTable();
            da.Fill(dt);
            cnn.Close();

            return dt;
        }

        public DataTable Search(string searchTerm, string searchIn = null)
        {
            cnn.Open();
            da = new OleDbDataAdapter("SELECT * FROM quran WHERE (AyahText LIKE '%" + searchTerm + "%' " + searchIn + " ) ORDER BY ID;", cnn);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                da = new OleDbDataAdapter("SELECT * FROM quran WHERE (English LIKE '%" + searchTerm + "%' " + searchIn + " ) ORDER BY ID;", cnn);
                da.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                da = new OleDbDataAdapter("SELECT * FROM quran WHERE (easy_bn_trans LIKE '%" + searchTerm + "%' " + searchIn + " ) ORDER BY ID;", cnn);
                da.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                da = new OleDbDataAdapter("SELECT * FROM quran WHERE (easy_ug_trans LIKE '%" + searchTerm + "%' " + searchIn + " ) ORDER BY ID;", cnn);
                da.Fill(dt);
            }
            cnn.Close();

            return dt;
        }
    }
}
