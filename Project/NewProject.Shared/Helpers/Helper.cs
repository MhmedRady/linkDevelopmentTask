using System.Collections.Generic;
using System.Globalization;

namespace VacanciesTask.Shared.Helpers
{
    public static partial class Helper
    {

        public static string getLnag()
        {
            return CultureInfo.CurrentCulture.Name.StartsWith("ar") ? "ar" : "en";
        }
    
        public static Dictionary<string, object>? jsonResult(object? obj, int? status = 200)
        {
            var result = new Dictionary<string, object>();
            result["status"] = status;
            result["data"] = obj;
            return result;
        }

    }
}