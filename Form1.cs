using FirebirdSql.Data.FirebirdClient;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pl2n
{
    public partial class Form1 : Form
    {
        private FbConnection fb = new FbConnection();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Form1()
        {
            InitializeComponent();
        }

        private Boolean ConnectionToDataBase(string username, string password, string database, string charset= "WIN1257")
        {
            Boolean result = false;
            try
            {
                if (fb.State == ConnectionState.Open)
                {
                    fb.Close();
                }

                FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
                //fb_con.Charset = "WIN1251"; //используемая кодировка
                fb_con.Charset = charset;
                fb_con.Database = database; //путь к файлу базы данных
                fb_con.UserID = username; //логин
                fb_con.Password = password; //пароль
                fb_con.ServerType = 0; //указываем тип сервера (0 - "полноценный Firebird" (classic или super server), 1 - встроенный (embedded))

                //обновляем подключение
                fb.ConnectionString = fb_con.ToString(); //передаем нашу строку подключения объекту класса FbConnection                
                fb.Open(); //открываем БД
            }
            catch (Exception err)
            {

            }
            finally
            {
                if (fb.State == ConnectionState.Open)
                {
                    result = true;
                }
                
            }
            return result;
        }

        private string DataBaseInfo()
        {
            FbDatabaseInfo fb_inf = new FbDatabaseInfo(fb); //информация о БД
            //пока у объекта БД не был вызван метод Open() - никакой информации о БД не получить, будет только ошибка
            return "Info: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion; //выводим тип и версию используемого сервера Firebird
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
