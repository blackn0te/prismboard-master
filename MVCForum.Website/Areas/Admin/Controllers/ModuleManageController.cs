namespace MvcForum.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Core.Constants;
    using Core.Interfaces;
    using Core.Interfaces.Services;
    using Core.Models.General;
    using MvcForum.Core.Models;
    using Web.ViewModels;
    using Web.ViewModels.Admin;

    [Authorize(Roles = Constants.AdminRoleName)]
    public class ModuleManageController : BaseAdminController
    {

        public ModuleManageController(ILoggingService loggingService,
            IMembershipService membershipService,
            ILocalizationService localizationService,
            ISettingsService settingsService, IMvcForumContext context)
            : base(loggingService, membershipService, localizationService, settingsService, context)
        {
        }

        public ActionResult DetailModule(string id)
        {
            dynamic model = new ExpandoObject();

            Module specificMod = getModuleById(id);

            return View(model);
        }

        [HttpGet]
        public ActionResult NewModule()
        {
            dynamic model = new ExpandoObject();
            model.Module = getModules();
            model.Lecture = getLecturers();
            model.Student = getStudent();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewModule(SY_ModuleViewModel module)
        {

            if (module.ModuleName != null && module.ModStart != null && module.ModEnd != null && module.ModId != null && module.lectList != null && module.studList != null)
            {
                Module modAdd = new Module
                {
                    ModuleID = module.ModId,
                    ModuleName = module.ModuleName,
                    ModuleStart = module.ModStart,
                    ModuleEnd = module.ModEnd
                };

                Boolean checker = addModule(modAdd);
                if (checker)
                {
                    addPersonToModule(module.studList, module.lectList, modAdd.ModuleID);
                    var redirectUrl = new UrlHelper(Request.RequestContext).Action("Module", "AdminDashBoard");
                    return Json(new { Url = redirectUrl });
                }
                else
                {
                    return JavaScript("location.reload(true)");
                }

            }
            else
            {
                return JavaScript("location.reload(true)");
            }
        }

        // GET: AdminDashBoard/Module  
        public ActionResult Module()
        {
            return View(getModules());
        }

        [HttpGet]
        public ActionResult DeleteModule(string id)
        {
            Console.WriteLine(id);

            if (id != null)
            {
                Module mod = getModuleById(id);
                if (mod != null)
                {
                    return View(mod);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModule(string id, string confirm)
        {
            try
            {
                Boolean checker = deleteModule(id);
                if (checker)
                {
                    return Redirect("/AdminDashBoard/Module");
                }
                else
                {
                    return Redirect("/AdminDashBoard/Module");
                }
            }
            catch (Exception e)
            {
                return Redirect("/AdminDashBoard/Module");
            }
        }

        //Data Access Objects

        //retrive Module from DB
        private static Module getModuleById(string id)
        {
            string query = "SELECT * FROM Module WHERE ModuleID LIKE @id";
            string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
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
                        conn.Close();

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

        //add Module to DB
        private static Boolean addModule(Module mod)
        {
            Boolean checker = moduleExist(mod.ModuleName);
            if (checker)
            {
                return false;
            }
            else
            {
                string query = "INSERT INTO module VALUES (@ModID, @ModName, @ModStart, @ModEnd)";
                string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(conStr))
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

        //Delete Module
        private static Boolean deleteModule(string id)
        {
            Module checker = getModuleById(id);
            if (checker != null)
            {
                delModSql(id);
                delModDelSql(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void delModSql(string id)
        {
            string query = "DELETE FROM Module WHERE ModuleID = @id";
            string connStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void delModDelSql(string id)
        {
            string query = "DELETE FROM moduleDetails WHERE moduleID = @id";
            string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        //Check if Module Exist
        private static Boolean moduleExist(string modTitle)
        {
            string query = "SELECT * from module where ModuleName LIKE @modName";
            string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conStr))
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

        //add Student and Lecturer to Module
        private static void addPersonToModule(string[] studList, string[] lectList, string ModId)
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

        private static void personSqlConn(string name, string type, string modId)
        {
            string query = "INSERT INTO moduleDetails (personName, personType, moduleID) VALUES (@name, @type, @id)";
            string connStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
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

        private static string getStudID(string name)
        {
            string query = "SELECT * from student where StudName = @name";
            string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
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

        private static string getLectID(string name)
        {
            string query = "SELECT * from lecturer where LectName = @name";
            string conStr = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
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

        private static List<Module> getModules()
        {
            List<Module> mList = new List<Module>();
            string query = "SELECT * FROM module";
            string connection = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
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
            }
            return mList;
        }

        private static List<Lecturer> getLecturers()
        {
            List<Lecturer> lectList = new List<Lecturer>();
            string query = "SELECT * FROM Lecturer";
            string connection = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
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

        private static List<Student> getStudent()
        {
            List<Student> studList = new List<Student>();
            string query = "SELECT * FROM Student";
            string connection = ConfigurationManager.ConnectionStrings["MVCForumContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
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
                                StudName = reader["StudName"].ToString(),
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
}
