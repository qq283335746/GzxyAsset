using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Intelligencia.UrlRewriter.TygaSoft
{
    public class TygaSoftRuntime
    {
        public static bool ValidateRuntime()
        {
            var result = string.Empty;
            var statusCode = -1;
            var content = "{\"item\":\"Hnztc\"}";
            DoHttpPost("http://my.tygaweb.com/Services/TygaSoftRunService.svc/GetRuntime", content, "application/json", out statusCode, out result);
            return int.Parse(result) == 1000;
        }

        public static void DoHttpPost(string url, string content, string contentType, out int statusCode, out string result)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            statusCode = -1;

            try
            {
                if (string.IsNullOrWhiteSpace(contentType)) contentType = "application/x-www-form-urlencoded";

                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] data = encoding.GetBytes(content);

                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = contentType;
                req.ContentLength = data.Length;

                Stream reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                res = (HttpWebResponse)req.GetResponse();
                Stream responseStream = res.GetResponseStream();

                var streamReader = new StreamReader(responseStream);
                result = streamReader.ReadToEnd();

                //获取响应结果的http状态码，200-请求成功
                statusCode = (int)res.StatusCode;
                res.Close();
                responseStream.Close();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }
    }
}
