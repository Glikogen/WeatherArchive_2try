using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather_Archive.Models;
using PagedList.Mvc;
using PagedList;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Weather_Archive.Controllers
{
    public class HomeController : Controller
    {
        WeatherDataContext weatherDC = new WeatherDataContext();

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult GetInfo(int page = 0, int year = 0)
        {
            IEnumerable<WeatherData> dataPerPages = weatherDC.weatherDatas
                .OrderBy(x => x.Id)
                .Skip(ListOfYears.GetAmountOfSkippedRecords(year) + ListOfYears.Years[year].GetSkippedRecordsAmount(page))
                .Take(ListOfYears.Years[year].Months[page].RecordsAmount);
            PageInfoMonth pageInfoMonth = new PageInfoMonth
            {
                PageNumber = page,
                PageSize = ListOfYears.Years[year].Months[page].RecordsAmount,
                TotalItems = weatherDC.weatherDatas.Count()
            };
            PageInfoYear.chosenYear = year;
            PageInfoYear pageInfoYear = new PageInfoYear() { currentYear = year };

            InfoViewModel infoViewModel = new InfoViewModel { PageInfoMonth = pageInfoMonth, weather = dataPerPages, PageInfoYear = pageInfoYear };

            return View(infoViewModel);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> uploads)
        {
            foreach (var upload in uploads)
            {
                if (upload != null)
                {
                    try
                    {
                        UploadingDatas uploading = new UploadingDatas();
                        string fileName = Path.GetFileName(upload.FileName);
                        string path = Server.MapPath("~/ExcelArchive/" + fileName);
                        upload.SaveAs(path);

                        uploading.AddingDatas(path, weatherDC);
                    }
                    catch (Exception ex)
                    {
                        string fileName = Path.GetFileName(upload.FileName);
                        string path = Server.MapPath("~/ExcelArchive/" + fileName);
                        FileInfo fileInf = new FileInfo(path);
                        fileInf.Delete();
                        ViewBag.Message = fileName + " - " + ex.Message;
                        return View("UploadError");
                    }
                }
            }

            return RedirectToAction("Upload");
        }

        protected override void Dispose(bool disposing)
        {
            weatherDC.Dispose();
            base.Dispose(disposing);
        }
    }
}