using Microsoft.AspNetCore.Mvc;

namespace NewProject.API.Extensions
{
    public static class APIPagerControllerExtensions
    {
        public static Dictionary<string, string> GetPagedData(this ControllerBase controller)
        {
            var dir = new Dictionary<string, string>();
            dir.Add("start", controller.Request.Form["start"].FirstOrDefault());
            dir.Add("pageSize", controller.Request.Form["length"].FirstOrDefault());
            dir.Add("searchValue", controller.Request.Form["search[value]"].FirstOrDefault().Trim().ToLower());
            dir.Add("sortColumnDir", controller.Request.Form["order[0][dir]"].FirstOrDefault().Trim().ToUpper());
            return dir;
        }
    }
}
