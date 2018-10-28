using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.IO;
using TygaSoft.Converters;

namespace TygaSoft.WebHelper
{
    public class HttpContextHelper
    {
        public static void Export(HttpContext context, DataTable dt)
        {
            using (var stream = new MemoryStream())
            {
                ExcelHelper.Export(stream, dt);
                context.Response.Buffer = true;
                context.Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
                context.Response.ContentType = "application/ms-excel";
                context.Response.BinaryWrite(stream.ToArray());
                context.Response.Flush();
            }
        }
    }
}
