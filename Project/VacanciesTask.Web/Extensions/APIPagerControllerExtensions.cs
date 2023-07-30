using Microsoft.AspNetCore.Mvc;

namespace NewProject.API.Extensions
{
    public static class APIPagerControllerExtensions
    {
        public static string GetPagedData(this ControllerBase controller, string Key)
        {
            var dir = new Dictionary<string, string>();
            dir.Add("start", controller.Request.Form["start"].FirstOrDefault());
            dir.Add("pageSize", controller.Request.Form["length"].FirstOrDefault());
            dir.Add("searchValue", controller.Request.Form["search[value]"].FirstOrDefault().Trim().ToLower());
            dir.Add("sortColumnDir", controller.Request.Form["order[0][dir]"].FirstOrDefault().Trim().ToUpper());
            return string.IsNullOrEmpty(dir[Key])? String.Empty: dir[Key];
        }
    }
}
