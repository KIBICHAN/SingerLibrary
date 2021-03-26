using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SingerLibrary
{
    public class SingerData
    {
        string strConnection;
        public SingerData()
        {
            strConnection = getConnectionString();
        }
        //Lay chuoi ket noi
        public string getConnectionString()
        {
            string strConnection = "server=.;database=SingerManager;uid=sa;pwd=01678789381el";
            return strConnection;
        }
        //Lay danh sach ca si
        public DataTable getSinger()
        {
            string SQL = "select * from Singer";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtSinger = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtSinger);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtSinger;
        }

        //Them ca si
        public bool addSinger(Singer singer)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Insert Singer values(@SingerID,@SingerName,@SingerAge,@SingerEmail)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@SingerID", singer.SingerID);
            cmd.Parameters.AddWithValue("@SingerName", singer.SingerName);
            cmd.Parameters.AddWithValue("@SingerAge", singer.SingerAge);
            cmd.Parameters.AddWithValue("@SingerEmail", singer.SingerEmail);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        //xoa ca si
        public bool deleteSinger(int SingerID)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Singer where SingerID=@SingerID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@SingerID", SingerID);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        //tim ca si
        public Singer findSinger(int SingerID)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Select SingerID, SingerName, SingerAge, SingerEmail from Singer where SingerID=@SingerID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@SingerID", SingerID);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            Singer s = null;
            SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", reader[0], reader[1], reader[2], reader[3]));
                    var ID = reader[0];
                    var Name = reader[1];
                    var Age = reader[2];
                    var Email = reader[3];
                    s = new Singer
                    {
                        SingerID = (int)ID,
                        SingerName = (string)Name,
                        SingerAge = (int)Age,
                        SingerEmail = (string)Email
                    };
                    break;
                }
            }
            return s;
        }
    }
}//end
