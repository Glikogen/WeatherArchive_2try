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
using System.Collections;

namespace Weather_Archive.Models
{
    public class UploadingDatas
    {
        public void AddingDatas(string filePath, WeatherDataContext db)
        {

            XSSFWorkbook xssfwb;

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                xssfwb = new XSSFWorkbook(file);
            }

            string A1 = xssfwb.GetSheetAt(0).GetRow(0).GetCell(0).StringCellValue;
            Regex regex = new Regex(@"\d{4}");
            if (regex.Matches(A1).Count == 0) throw new Exception("В данном файле не удалось установить год");
            int result = int.Parse(regex.Match(A1).Value);
            int index = ListOfYears.Years.FindIndex(x => x.year == result);
            if (index != -1)
            {
                throw new Exception("В архиве уже есть данные за этот период");

            }
            Year year = new Year() { year = result, Months = new List<Month>() };

            try
            {
                for (int i = 0; i < xssfwb.NumberOfSheets; i++)
                {
                    ISheet sheet = xssfwb.GetSheetAt(i);

                    Month month = new Month() { MonthNumber = i + 1, RecordsAmount = sheet.LastRowNum - 3 };
                    year.Months.Add(month);

                    List<WeatherData> dataForDB = new List<WeatherData>();

                    for (int j = 4; j <= sheet.LastRowNum; j++)
                    {
                        string date = sheet.GetRow(j).GetCell(0).StringCellValue;
                        string time = sheet.GetRow(j).GetCell(1).StringCellValue;
                        double temperature = sheet.GetRow(j).GetCell(2).NumericCellValue;
                        double airHumidity = sheet.GetRow(j).GetCell(3).NumericCellValue;
                        double dewPoint = sheet.GetRow(j).GetCell(4).NumericCellValue;
                        double pressure = sheet.GetRow(j).GetCell(5).NumericCellValue;
                        string directionOfTheWind = sheet.GetRow(j).GetCell(6).StringCellValue;
                        string windSpeed;
                        if (sheet.GetRow(j).GetCell(7).CellType.ToString().Equals("Numeric"))
                        {
                            windSpeed = sheet.GetRow(j).GetCell(7).NumericCellValue.ToString();
                        }
                        else
                        {
                            windSpeed = sheet.GetRow(j).GetCell(7).StringCellValue;
                        }
                        string cloudiness;
                        if (sheet.GetRow(j).GetCell(8).CellType.ToString().Equals("Numeric"))
                        {
                            cloudiness = sheet.GetRow(j).GetCell(8).NumericCellValue.ToString();
                        }
                        else
                        {
                            cloudiness = sheet.GetRow(j).GetCell(8).StringCellValue;
                        }
                        string cloudBase;
                        if (sheet.GetRow(j).GetCell(9).CellType.ToString().Equals("Numeric"))
                        {
                            cloudBase = sheet.GetRow(j).GetCell(9).NumericCellValue.ToString();
                        }
                        else
                        {
                            cloudBase = sheet.GetRow(j).GetCell(9).StringCellValue;
                        }
                        string horizontalVisibility;
                        if (sheet.GetRow(j).GetCell(10).CellType.ToString().Equals("Numeric"))
                        {
                            horizontalVisibility = sheet.GetRow(j).GetCell(10).NumericCellValue.ToString();
                        }
                        else
                        {
                            horizontalVisibility = sheet.GetRow(j).GetCell(10).StringCellValue;
                        }
                        string weatherConditions;
                        try { weatherConditions = sheet.GetRow(j).GetCell(11).StringCellValue; }
                        catch (Exception ex)
                        {
                            weatherConditions = "";
                        }

                        WeatherData weather = new WeatherData()
                        {
                            Date = date,
                            Time = time,
                            Temperature = temperature,
                            AirHumidity = airHumidity,
                            DewPoint = dewPoint,
                            Pressure = pressure,
                            DirectionOfTheWind = directionOfTheWind,
                            WindSpeed = windSpeed,
                            CloudBase = cloudBase,
                            Cloudiness = cloudiness,
                            HorizontalVisibility = horizontalVisibility,
                            WeatherConditions = weatherConditions
                        };

                        dataForDB.Add(weather);
                    }

                    db.weatherDatas.AddRange(dataForDB);
                    db.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw new Exception("Этот архив не подходит для считывания данных");
            }

            ListOfYears.Years.Add(year);
        }

    }

}