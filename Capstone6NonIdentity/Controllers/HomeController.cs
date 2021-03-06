﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone6NonIdentity.Models;

namespace Capstone6NonIdentity.Controllers
{
    
    public class HomeController : Controller
    {
        public static CurrentUser currentUser = new CurrentUser();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            if (currentUser.LogIn == true)
            {
                currentUser.LogIn = false;
                currentUser.ID = 0;
                ViewBag.Logout = "You've been Logged out!";
                return View("Login");
            }
            ViewBag.LoggedOut = "You're not logged in";
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult LoginButton(User logUser)
        {

            TaskListDBEntities ORM = new TaskListDBEntities();
            List<User> foundID = ORM.Users.ToList<User>();
            foreach (User user in foundID)
            {
                if (logUser.Email == user.Email)
                {
                    if (currentUser.LogIn == false)
                    {
                        currentUser.LogIn = true;
                        currentUser.ID = user.UserID;
                        TempData["LoggedIn"] = "You've successfully logged in!";
                        return RedirectToAction("TaskList");//, logUser
                    }
                    ViewBag.Error = "You are already logged in!";
                    return View("Login");

                }
            }
            ViewBag.Error = "This is not a valid email address";
                return View("Login");

            
        }
        public ActionResult RegisterUser(User newUser)
        {
            if (ModelState.IsValid)
            {
                TaskListDBEntities ORM = new TaskListDBEntities();
                User added = ORM.Users.Add(newUser);
                ORM.SaveChanges();
                currentUser.ID = added.UserID;
                currentUser.LogIn = true;
                return RedirectToAction("TaskList");//, added
            }
            else
            {
                ViewBag.Error = "Error with adding user."; 
                return View("Login");
            }

        }

        public ActionResult TaskList()//User CurrentUser
        {
            
            TaskListDBEntities ORM = new TaskListDBEntities();
            ViewBag.CurrentUserUserID = currentUser.ID;
            ViewBag.Tasks = ORM.Tasks.ToList<Task>();

            return View();
        }

        public ActionResult UpdateComplete(int taskID)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            Task oldTask = ORM.Tasks.Find(taskID);
            if (oldTask.Complete == false)
            {
                oldTask.Complete = true;
                ORM.Entry(oldTask).State = System.Data.Entity.EntityState.Modified;
                ORM.SaveChanges();
                return RedirectToAction("TaskList");
            }
            else
            {
                return RedirectToAction("TaskList");
            }
        }

        public ActionResult DeleteTask(int taskID)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            Task found = ORM.Tasks.Find(taskID);
            ORM.Tasks.Remove(found);
            ORM.SaveChanges();
            return RedirectToAction("TaskList");

        }

        public ActionResult AddTask()
        {
            return View();
        }

        public ActionResult AddNewTask(Task newTask)
        {
            if (ModelState.IsValid)
            {
                TaskListDBEntities ORM = new TaskListDBEntities();
                newTask.UserID = currentUser.ID;
                ORM.Tasks.Add(newTask);
                ORM.SaveChanges();
                return RedirectToAction("TaskList");

            }
            else
            {
                ViewBag.Error = "Error with adding task.";
                return View("AddTask");
            }
        }

    }
}