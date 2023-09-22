 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TODOList.DAL;
using TODOList.Models;

namespace TODOList.Controllers
{
    public class todoController : Controller
    {
        GlobalClass global = new GlobalClass();
        // GET: todo
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getque()
        {
            try
            {
                DataSet dataSet = global.GetQuestions();
                List<QuestionModel> que = FillQues(dataSet);
                return Json(que);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<QuestionModel> FillQues(DataSet dataSet)
        {
            List<QuestionModel> que = new List<QuestionModel>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                que.Add(new QuestionModel()
                {
                    QueId = Convert.ToInt32(dr[0].ToString()),
                    Questions = dr[1].ToString(),
                });
            }
            return que;
        }

        [HttpPost]
        public ActionResult PutData(RegisterModel register)
        {
            bool chk = global.CheckDuplicate(register);
            if (chk)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                global.InsertData(register);
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PutTask(int CatId, string Task)
        {
            int u = int.Parse(Session["uid"].ToString());
            //task.UserId = u;
            global.InsertTasks(CatId, Task, u);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogAccount(RegisterModel register)
        {
            bool chk = global.CheckDuplicate(register);
            if (chk)
            {
                IdModel id = new IdModel();
                Session["uname"] = register.Username;
                id.Username = Session["uname"].ToString();
                id = global.Getid(id);
                Session["uid"] = id.UserId;
                if (register.Username.ToString() == "Admin23" && register.Pass.ToString() == "Admin2023")
                {
                    return Json(11, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserMain()
        {
            if(Session["uname"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult getCat()
        {
            try
            {
                DataSet dataSet = global.GetCategories();
                List<CategoryModel> cat = FillCat(dataSet);
                return Json(cat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<CategoryModel> FillCat(DataSet dataSet)
        {
            List<CategoryModel> cat = new List<CategoryModel>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                cat.Add(new CategoryModel()
                {
                    CatId = Convert.ToInt32(dr[0].ToString()),
                    Category = dr[1].ToString(),
                });
            }
            return cat;
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPass(RegisterModel register)
        {
            global.InsertData(register);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindUser(IdModel id)
        {
            IdModel id1 = new IdModel();
            if(id.Answers == null)
            {
                id1 = global.Getid(id);
                string que = id1.Questions.ToString();
                return Json(que);
            }
            else if(id.Answers != null)
            {
                id1 = global.Getid(id);
                string pass = id1.passwords.ToString();
                return Json(pass);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTask(TaskModel task)
        {
            int uid = int.Parse(Session["uid"].ToString());
            DataSet set = global.GetTask(task, uid);
            List<TaskModel> tasks = FillTask(set);
            return Json(tasks);
        }

        private List<TaskModel> FillTask(DataSet dataSet)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                tasks.Add(new TaskModel()
                {
                    Task = dr[1].ToString(),
                });
            }
            return tasks;
        }
    }
}