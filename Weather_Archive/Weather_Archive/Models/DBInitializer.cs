using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Web.Hosting;
using System.Text.RegularExpressions;

namespace Weather_Archive.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<WeatherDataContext>
    {
        protected override void Seed(WeatherDataContext db)
        {
            UploadingDatas uploading = new UploadingDatas();
            string FullPath = HostingEnvironment.MapPath("~/ExcelArchive");
            foreach (string filePath in Directory.GetFiles(FullPath))
            {
                uploading.AddingDatas(filePath, db);
            }

            base.Seed(db);
        }
    }
}