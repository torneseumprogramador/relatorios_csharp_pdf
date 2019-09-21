using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Paginacao.Models
{
    public class Helpers
    {
        public static string QueryStringToParams(int pagina, System.Web.HttpRequestBase request)
        {
            string query = request.Params.ToString();
            Regex regex = new Regex("pagina.*?&");
            query = regex.Replace(query, "");

            regex = new Regex("ALL_HTTP.*");
            query = regex.Replace(query, "");

            regex = new Regex("&&.*");
            query = regex.Replace(query, "");

            return "?pagina=" + pagina + "&" + query;
        }
    }
}