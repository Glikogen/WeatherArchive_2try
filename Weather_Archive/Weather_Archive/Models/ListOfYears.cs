using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather_Archive.Models
{
    public static class ListOfYears
    {
        public static List<Year> Years { get; set; } = new List<Year>();

        public static int GetAmountOfSkippedRecords(int year)
        {
            int result = 0;
            for (int i = 0; i < year; i++)
            {
                result += Years[i].GetSkippedRecordsAmount(12);
                result += Years[i].Months[11].RecordsAmount;
            }

            return result;
        }
    }

    public class Year
    {
        public int year { get; set; }
        public List<Month> Months { get; set; }

        public int GetSkippedRecordsAmount(int monthNumber)
        {
            int count = 0;
            for (int i = 1; i < monthNumber; i++)
            {
                count += Months[i - 1].RecordsAmount;
            }
            return count;
        }
    }

    public class Month
    {
        public int MonthNumber { get; set; }
        public int RecordsAmount { get; set; }
    }
}