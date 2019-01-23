using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace MvcForum.Core.Models
{
    public class SY_ModuleModel
    {
        public List<Module> ModuleList { get; set; }
        public List<Lecturer> LecturerList { get; set; }
        public List<Student> StudentList { get; set; }

        public SY_ModuleModel(List<Module> ModuleList, List<Lecturer> LecturerList, List<Student> StudentList) {
            this.ModuleList = ModuleList;
            this.LecturerList = LecturerList;
            this.StudentList = StudentList;
        }
    }

    public class ModuleDetails
    {
        public int ModuleDetailID { get; set; }
        public string PersonName { get; set; }
        public string PersonType { get; set; }
        public int ModuleID { get; set; }

        public ModuleDetails(int ModuleDetailID, string PersonName, string PersonType, int ModuleID) {
            this.ModuleDetailID = ModuleDetailID;
            this.PersonName = PersonName;
            this.PersonType = PersonType;
            this.ModuleID = ModuleID;
        }
    }

    public class Module
    {
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public DateTime ModuleStart { get; set; }
        public DateTime ModuleEnd { get; set; }

        public Module() {

        }

    }

    public class Student
    {
        public string StudName { get; set; }
        public string StudAdminNo { get; set; }
        public string StudEmail { get; set; }
        public string StudCourse { get; set; }
        public string StudSecondEmail { get; set; }
        public string StudModuleGrp { get; set; }

        public Student() {

        }
    }


    public class Lecturer
    {
        public int LecturerID { get; set; }
        public string LectName { get; set; }
        public string LectEmail { get; set; }
        public string ModuleGrp { get; set; }
        public int ModuleID { get; set; }
        
        public Lecturer(int LecturerID, string LectName, string LectEmail)
        {
            this.LecturerID = LecturerID;
            this.LectName = LectName;
            this.LectEmail = LectEmail;
        }

        public Lecturer(int LecturerID, string LectName, string LectEmail, int ModuleID ,string ModuleGrp)
        {
            this.LecturerID = LecturerID;
            this.LectName = LectName;
            this.LectEmail = LectEmail;
            this.ModuleID = ModuleID;
            this.ModuleGrp = ModuleGrp;
        }

        public Lecturer()
        {

        }

    }

    //DAO Objects
    public class ModuleDAO
    {
        public static string ConnString { get; set; }

        public ModuleDAO() {
            ConnString = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
        }

        public static List<Module> getModules() {
            List<Module> mList = new List<Module>();
            string query = "SELECT * FROM module";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mList.Add(new Module
                        {
                            ModuleID = reader["ModuleID"].ToString(),
                            ModuleName = reader["ModuleName"].ToString(),
                            ModuleStart = DateTime.Parse(reader["ModuleStart"].ToString()),
                            ModuleEnd = DateTime.Parse(reader["ModuleEnd"].ToString())
                        });
                    }
                    con.Close();
                }
            }
            return mList;
        }

        public static Module getModuleById(string id)
        {
            string query = "SELECT * FROM Module WHERE ModuleID like @id";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        Module pootis = new Module();
                        while (sdr.Read())
                        {
                            Module test = new Module
                            {
                                ModuleID = sdr["ModuleID"].ToString(),
                                ModuleName = sdr["ModuleName"].ToString(),
                                ModuleStart = Convert.ToDateTime(sdr["ModuleStart"]),
                                ModuleEnd = Convert.ToDateTime(sdr["ModuleEnd"])
                            };
                            pootis = test;
                        }
                        con.Close();

                        return pootis;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static Boolean moduleExist(string modTitle)
        {
            string query = "SELECT * from module where ModuleName LIKE @modName";
            
            using (SqlConnection connection = new SqlConnection(ConnString)
)
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                Console.WriteLine(modTitle);
                command.Parameters.AddWithValue("@modName", modTitle);
                command.Connection = connection;
                connection.Open();
                int UserExist = Convert.ToInt16(command.ExecuteScalar());
                connection.Close();
                if (UserExist > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public static Boolean addModule(Module mod)
        {
            Boolean checker = moduleExist(mod.ModuleName);
            if (checker)
            {
                return false;
            }
            else
            {
                string query = "INSERT INTO module VALUES (@ModID, @ModName, @ModStart, @ModEnd)";

                using (SqlConnection conn = new SqlConnection(ConnString
))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ModID", mod.ModuleID);
                    cmd.Parameters.AddWithValue("@ModName", mod.ModuleName);
                    cmd.Parameters.AddWithValue("@ModStart", mod.ModuleStart);
                    cmd.Parameters.AddWithValue("@ModEnd", mod.ModuleEnd);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
        }

        public static void delModSql(string id)
        {
            string query = "DELETE FROM Module WHERE ModuleID = @id";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void delModDetSql(string id)
        {
            string query = "DELETE FROM moduleDetails WHERE moduleID = @id";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Boolean deleteModule(string id)
        {
            Module checker = getModuleById(id);
            if (checker != null)
            {
                delModSql(id);
                delModDetSql(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void personSqlConn(string name, string type, string modId)
        {
            string query = "INSERT INTO moduleDetails (personName, personType, moduleID) VALUES (@name, @type, @id)";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@id", modId);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public static void addPersonToModule(string[] studList, string[] lectList, string ModId)
        {

            //Student Loop
            string type = "Student";
            for (int i = 0; i < studList.Length; i++)
            {
                personSqlConn(studList[i], type, ModId);
            }

            //Lecture Loop
            type = "Lecturer";
            for (int i = 0; i < studList.Length; i++)
            {
                personSqlConn(lectList[i], type, ModId);
            }
        }

        public static void getModuleCodePerName(string name)
        {
            string query = "SELECT * FROM moduleDetails WHERE personName = @name";


            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Connection = con;
                con.Open();
                
            }
        }
    }

    public class StudentDAO
    {
        public static string ConnString { get; set; }

        public StudentDAO()
        {
            ConnString = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
        }

        public static string getStudID(string name)
        {
            string query = "SELECT * from student where StudName = @name";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Connection = con;
                con.Open();
                Student student = (Student)cmd.ExecuteScalar();
                con.Close();
                return student.StudName;
            }
        }

        public static List<Student> getStudent()
        {
            List<Student> studList = new List<Student>();
            string query = "SELECT * FROM Student";
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studList.Add(new Student
                            {
                                StudAdminNo = reader["StudentAdminNo"].ToString(),
                                StudName =  reader["StudName"].ToString(),
                                StudEmail = reader["StudEmail"].ToString(),
                                StudCourse = reader["StudCourse"].ToString(),
                                StudModuleGrp = reader["StudModuleGrp"].ToString()
                            });
                        }
                        con.Close();
                    }
                }
            }

            return studList;

        }
    }

    public class LecturerDAO
    {
        public static string ConnString { get; set;}

        public LecturerDAO()
        {
            ConnString = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
        }

        public static string getLectID(string name)
        {
            string query = "SELECT * from lecturer where LectName = @name";
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Connection = con;
                con.Open();
                Lecturer lecture = (Lecturer)cmd.ExecuteScalar();
                con.Close();
                return lecture.LectName;
            }
        }

        public static List<Lecturer> getLecturers()
        {
            List<Lecturer> lectList = new List<Lecturer>();
            string query = "SELECT * FROM Lecturer";
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lectList.Add(new Lecturer
                            {
                                LecturerID = int.Parse(reader["LecturerID"].ToString()),
                                LectName = reader["LectName"].ToString(),
                                LectEmail = reader["LectEmail"].ToString()
                            });
                        }
                        con.Close();
                    }
                }
            }

            return lectList;
        }
    }


}