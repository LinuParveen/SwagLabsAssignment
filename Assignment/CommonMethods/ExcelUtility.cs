using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace Assignment.CommonMethods
{
    public static class ExcelUtility
    {
        static readonly List<DataCollection> dataCollections = new List<DataCollection>();

        public static DataTable ExcelToDataTable(string fileName)
        {
            var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            var result = excelDataReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            var table = result.Tables;
            var resultTable = table["sheet1"];

            return resultTable;
        }

        public static void PopulateInCollection(string fileName)
        {
            var dataTable = ExcelToDataTable(fileName);

            for (int row = 1; row <= dataTable.Rows.Count; row++)
            {
                for (int column = 0; column < dataTable.Columns.Count; column++)
                {
                    var dataCollection = new DataCollection()
                    {
                        Rownumber = row,
                        ColumnName = dataTable.Columns[column].ColumnName,
                        ColumnValue = dataTable.Rows[row - 1][column].ToString(),
                    };

                    dataCollections.Add(dataCollection);
                }
            }
        }

        public static string ReadData(int rownumber, string columnName)
        {
            try
            {
                var data = (from coldata in dataCollections
                               where coldata.ColumnName == columnName && coldata.Rownumber == rownumber
                               select coldata.ColumnValue).SingleOrDefault();

                return Convert.ToString(data);
            }
            catch (Exception ex)
            {
                //Not loging the exception as it is not part of the test reqirement.
                return null;
            }
        }

    }
    public class DataCollection
    {
        public int Rownumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
