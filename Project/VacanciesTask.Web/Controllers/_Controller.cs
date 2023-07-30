using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Localization;
using NewProject.DBL;
using NToastNotify;


namespace VacanciesTask.Controllers
{
    
    public class _Controller : Controller
    {
        public _Controller(){}
        
        private readonly IToastNotification _toastNotification;
        
        public _Controller(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        
        public  void TableColumns(bool createUrl = true, params string[] columns)
        {
            ViewBag.Columns = columns.Length > 0?  columns: new[] { "name", "activate"};
            ViewBag.CreateBtn = createUrl;
        }
        
        public ActionResult DataTable(IEnumerable<object>? _data, int pageSize)
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            //total
            var recordsTotal = _data.Count();
            //Returning
            return Json(new 
            {   draw = draw, 
                recordsFiltered = recordsTotal, 
                recordsTotal = recordsTotal, 
                data = _data,
                pageSize = pageSize,
            });
        }
    }
}
