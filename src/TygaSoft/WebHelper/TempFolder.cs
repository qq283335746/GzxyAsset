using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;

namespace TygaSoft.WebHelper
{
    public class TempFolder
    {
        string fileRoot = ConfigurationManager.AppSettings["FilesRoot"];
        DateTime currTime = DateTime.Now;

        public string GetTempFolderUrl()
        {
            return string.Format("{0}/{1}", fileRoot.TrimEnd('/'), "Temp");
        }

        public string GetTempFolderPath()
        {
            var path = HttpContext.Current.Server.MapPath(GetTempFolderUrl());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        public void DoTempFile()
        {
            var tempPath = GetTempFolderPath();
            var lastDir = currTime.ToString("yyyyMMdd");
            var prevDir = currTime.AddDays(-1).ToString("yyyyMMdd");
            var lastDirPath = string.Format("{0}\\{1}", tempPath, lastDir);
            var prevDirPath = string.Format("{0}\\{1}", tempPath, prevDir);
            var dirs = Directory.GetDirectories(tempPath);
            foreach (var item in dirs)
            {
                var dirName = item.Substring(item.LastIndexOf('\\')+1);
                if (dirName != prevDir && dirName != lastDir)
                {
                    var subDirs = Directory.GetDirectories(item);
                    foreach (var subItem in subDirs)
                    {
                        var subFiles = Directory.GetFiles(subItem);
                        foreach (var f in subFiles)
                        {
                            File.Delete(f);
                        }
                    }
                    var files = Directory.GetFiles(item);
                    foreach (var f in files)
                    {
                        File.Delete(f);
                    }
                    Directory.Delete(item);
                }
            }
            if (!Directory.Exists(lastDirPath)) Directory.CreateDirectory(lastDirPath);
        }

        public void CopyToTemp(string excelFileName,out string tempUrl)
        {
            var sourceUrl = HttpContext.Current.Server.MapPath("~/App_Data/Template/" + excelFileName + "");
            var rndDir = string.Format(@"{0}\{1}\{2}", GetTempFolderPath(), currTime.ToString("yyyyMMdd"), Path.GetRandomFileName());
            if (!Directory.Exists(rndDir)) Directory.CreateDirectory(rndDir);
            tempUrl = string.Format(@"{0}\{1}", rndDir, excelFileName);
            File.Copy(sourceUrl, tempUrl);
        }

        public string GetExportTempUrl(string excelFileName)
        {
            return string.Format(@"{0}/{1}/{2}/{3}", GetTempFolderUrl(), currTime.ToString("yyyyMMdd"), Path.GetRandomFileName(), excelFileName);
        }

        public string GetExportSourceUrl(string excelFileName)
        {
            return string.Format(@"{0}/{1}", "~/App_Data/Template", excelFileName);
        }
    }
}
