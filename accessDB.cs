using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VoiceAuthomatedEmail
{
    class accessDB
    {
        private const String SERVER = "127.0.0.1";
        private const String DATABASE = "voice";
        private const String UID = "root";
        private const String PASSWORD = "bereket";
        private static MySqlConnection dbConn;

        public String email { get;  set; }
        public String cntctEmail { get; set; }
        public String password { get;  set; }
        public String cntctName { get; set; }
        public String srecipient { get; set; }
        public String ssubject { get; set; }
        public String sbody { get; set; }
        public String sattachement { get; set; }
        public String bdykey { get; set; }
        public String bdybody { get; set; }
        public String bdysender { get; set; }
        private accessDB(String n, String e)
        {
            cntctEmail = e;
            cntctName = n;
        }
        private accessDB(String k, String b, String o)
        {
            bdykey = k;
            bdybody = b;
            bdysender = o;
        }
        private accessDB(String r, String s,String b, String a) {
            srecipient = r;
            ssubject = s;
            sbody = b;
            sattachement = a;
        }
        public accessDB()
        {

        }
        public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.Database = DATABASE;
            builder.UserID = UID;
            builder.Password = PASSWORD;

            String connString = builder.ToString();
            builder = null;
            Console.Write(connString);
            dbConn = new MySqlConnection(connString);
            Application.ApplicationExit += (sender, args) => {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };
        }
        public void selectuser(string username,int pin)
        {

            try { 
            String query = string.Format("SELECT * FROM voice.userinfo WHERE uname='{0}' AND pin={1};", username,pin);

            //String query= string.Format("SELECT aes_decrypt(password,'set') as password, uemail FROM voice.userinfo WHERE uname='{0}' AND pin={1};", username, pin);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);
            dbConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                email = reader["uemail"].ToString();
                password = reader["password"].ToString();
            }
            dbConn.Close();
            reader.Close();
            }
            catch(Exception)
            {

            }
        }
        public void insertuser(string email,string password,string username, int pin)
        {
            try
            {
                //String query = string.Format("INSERT INTO  voice.userinfo (uname,uemail,password,pin)VALUES('{0}','{1}',aes_encrypt('{2}','set'),{3})", username,email,password, pin);
                String query = string.Format("INSERT INTO  voice.userinfo (uname,uemail,password,pin)VALUES" +
                    "('{0}','{1}','{2}',{3});", username, email, password, pin);
            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
            }
            catch (Exception)
            {
            }
        }
        public void insertsent(string recipient, string subject, string body, string attachement, string sender, string date)
        {
            try
            {
                
                String query = string.Format("INSERT INTO  voice.sent (recipient,subject,body,attachement,sender,date,status)" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','sent');", recipient, subject, body, attachement, sender, date);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
            catch(Exception)
            {
                
            }
        }
        public void insertInbox(string sender, string subject, string body, string attachement, string date, string  recipient)
        {
            try
            {
                String query = string.Format("INSERT INTO  voice.inbox (sender,subject,body,attachment,date,to)" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}');", sender, subject, body, attachement, date, recipient);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
           catch (Exception)
            {
                
            }
        }
        public void insertContact(string name, string email, string owner)
        {
            try
            {
                String query = string.Format("INSERT INTO  voice.contact (name, email, owner) VALUES('{0}','{1}','{2}');", name, email, owner);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
            catch (Exception)
            {

            }
        }
        public static List <accessDB> SelectContact(string owner, string letter)
        {

           // try
            {
                List<accessDB> contacts = new List<accessDB>();
                String query = string.Format("SELECT * FROM voice.contact WHERE owner='{0}' AND name LIKE '{1}%' ORDER BY name;", owner, letter);

                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String cName = reader["name"].ToString();
                    String cEmail = reader["email"].ToString();
                    accessDB c = new accessDB(cName,cEmail);
                    contacts.Add(c);
                }
                dbConn.Close();
                reader.Close();
                return contacts;
            }
           // catch (Exception)
            {

            }
        }
        public static List<accessDB> SelectSent(string sender, string status)
        {

            // try
            {
                List<accessDB> sentb = new List<accessDB>();
                String query = string.Format("SELECT * FROM voice.sent WHERE sender='{0}' AND status='{1}';", sender,status);

                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                //dbConn.Close();
                dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String recipient = reader["recipient"].ToString();
                    String subject = reader["subject"].ToString();
                    String body = reader["body"].ToString();
                    String attachement = reader["attachement"].ToString();
                    accessDB c = new accessDB(recipient,subject,body,attachement);
                    sentb.Add(c);
                }
                dbConn.Close();
                reader.Close();
                return sentb;
            }
            // catch (Exception)
            {

            }
        }
        public void UpdateTrash(string sender, string recipient, string subject)
        {
            try
            {
                String query = string.Format("UPDATE voice.sent SET status = 'trash' WHERE " +
                    "sender='{0}' AND recipient='{1}' AND subject='{2}'; ", sender, recipient, subject);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
            catch (Exception)
            {

            }
        }
        public void UpdateTrashP(string sender, string recipient, string subject)
        {
            try
            {
                String query = string.Format("UPDATE voice.sent SET status = 'Deleted' WHERE " +
                    "sender='{0}' AND recipient='{1}' AND subject='{2}'; ", sender, recipient, subject);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
            catch (Exception)
            {

            }
        }
        public void inserTemplate(string k, string b, string s)
        {
            try
            {
                String query = string.Format("INSERT INTO  voice.body (key, email, sender) VALUES('{0}','{1}','{2}');", k, b, s);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);

                dbConn.Open();

                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
            catch (Exception)
            {

            }
        }
        public static List<accessDB> SelecTemplate(string owner)
        {

            // try
            {
                List<accessDB> temp = new List<accessDB>();
                String query = string.Format("SELECT * FROM voice.body WHERE sender='{0}';", owner);

                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String bkey = reader["key"].ToString();
                    String bbody = reader["body"].ToString();
                    String bsender = reader["body"].ToString();
                    accessDB c = new accessDB(bkey, bbody,bsender);
                    temp.Add(c);
                }
                dbConn.Close();
                reader.Close();
                return temp;
            }
            // catch (Exception)
            {

            }
        }
        public string GetEmail()
        {
            return email;
        }
        public string GetPassword()
        {
            return password;
        }

    }
}
