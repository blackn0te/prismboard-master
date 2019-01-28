using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Logging;
using System.Data.SqlClient;
using System.Text;
using System.Web.WebPages;

namespace mvcforum.web
{

    public class ChatHub : Hub
    {
        private static Boolean isIntervalOn = false;

        private static ArrayList ConnectedUsers = new ArrayList();

        public void Send(string name, string message, string adminno, string classgrp)
        {
            //call the addNewMessageToPage method to update clients
            System.Diagnostics.Debug.WriteLine(name + " " + message + " " + adminno + " " + classgrp);

            //message.Replace("" , "");
            //call sanitize
            message = sanitize(message);

            Clients.All.addNewMessageToPage(name, message, adminno, classgrp);
        }

        public string sanitize(string uncleaninput)
        {
            StringBuilder clean = new StringBuilder(uncleaninput);
            string s = uncleaninput;
            //get length 
            int inputlength = uncleaninput.Length;
            //for loop through per char if char.at(i)
            for (int i = 0; i < inputlength; i++)
            {

                System.Diagnostics.Debug.WriteLine("Looped once");
                if (uncleaninput[i] == '<' || uncleaninput[i] == '>' || uncleaninput[i] == '@' || uncleaninput[i] == '$' || uncleaninput[i] == '(' || uncleaninput[i] == ')')
                {
                    clean[i] = ' ';
                }
                else
                {
                    clean[i] = uncleaninput[i];
                }

            }//for loop
            return clean.ToString();
        }

        public void UpdateInterval()
        {
            //var timer = new System.Threading.Timer(
            //    e => Updateusers(),
            //    null,
            //    TimeSpan.Zero,
            //    TimeSpan.FromSeconds(10000));

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);//originally 10

            var timer = new System.Threading.Timer((e) =>
            {
                Updateusers();
            }, null, startTimeSpan, periodTimeSpan);
        }



        public void Clientonline(string username)
        {
            if (!isIntervalOn)
            {
                isIntervalOn = true;
                UpdateInterval();

            }

            ConnectedUsers.Add(username);//get all connected users and add to array

            System.Threading.Thread.Sleep(1000);//wait 1 second

            Clients.All.updateConnectedClients(ConnectedUsers);//call client method  
        }

        public void Clientonline2(string username)//reHelloClient calls this
        {
            ConnectedUsers.Add(username);

            System.Threading.Thread.Sleep(1000);//wait 1 second

            Clients.All.updateConnectedClients(ConnectedUsers);//call client method  
        }
        //call this function every 5-10 seconds 
        public void Updateusers()
        {
            ConnectedUsers.Clear();//clear arraylist 
            Clients.All.clearOnlineUserList();//Client-side : clear online users list
            Clients.All.reHelloClient();//ask all users who online

        }

        public static void test_localdb()
        {
            string one = "Marcus";
            string two = "69";
            string query = "INSERT INTO TestTable(name,age) values(@one, @two)";
            string connectionString = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\Marcus Low\\Desktop\\signalR chat backups\\SignalRChat (Final working copy)\\SignalR\\App_Data\\ChatDB.mdf\"; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@one", one);//for parameterized queries
                cmd.Parameters.AddWithValue("@two", two);//for parameterized queries
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }








    }//Chathub : Hub 

    public class BackendDB
    {



        public static void test_localdb()
        {
            string one = "Marcus";
            string two = "69";
            string query = "INSERT INTO TestTable(name,age) values(@one, @two)";
            string connectionString = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\Marcus Low\\Desktop\\signalR chat backups\\SignalRChat (Final working copy)\\SignalR\\App_Data\\ChatDB.mdf\"; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@one", one);//for parameterized queries
                cmd.Parameters.AddWithValue("@two", two);//for parameterized queries
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        //connection template from syahiran. check phone for more templates.
        public static void habis_db_method(string unsanitized_variable)
        {
            string query = "INSERT INTO ..... values (@one, @two, @three)";
            string connectionString = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\Marcus Low\\Desktop\\signalR chat backups\\SignalRChat (Final working copy)\\SignalR\\App_Data\\ChatDB.mdf\"; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@one", unsanitized_variable);//for parameterized queries
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }


        }

    }




}