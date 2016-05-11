using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingExports.DataExports
{
    class DataExporter
    {


        //private static string _sqlQuery = "SELECT *  FROM [dbo].[TweetData]   where RetweetCount = 0 order by TweetDate";

        //private static string _destinationFolder = @"C:\Users\Alivelu\Dropbox\PhD-work\Twitter-data-analysis\Chennai-Floods\original-tweets";

        //private static int _lineslimit = 100000;

        private static int _fileIndex = 1;

        private static int _recordCount = 0;

        private static StreamWriter _writer;




        public static void ExportData(string sqlQuery, string destinationFolder, int linesLimit)
        {

            Console.WriteLine($"{DateTime.Now}: Starting exporting records.");

            InitialiseWriter(destinationFolder);


            try
            {
                using (var connection = new SqlConnection(Utilities.connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = sqlQuery,
                        CommandType = CommandType.Text,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    var dataReader = command.ExecuteReader();

                    var columnList = string.Join(",", dataReader.ColumnList());

                    _writer.WriteLine(columnList);


                    while (dataReader.Read())
                    {
                        var values = dataReader.GetValuesList().Select(data => Utilities.ProcessText(data)).ToList();


                        //var valuesStr = string.Join(",", dataReader.GetValuesList());

                        var valuesStr = string.Join(",", values);

                        _writer.WriteLine(valuesStr);

                        _recordCount++;

                        if (_recordCount % linesLimit == 0)
                        {
                            InitialiseWriter(destinationFolder);

                            _writer.WriteLine(columnList);

                            Console.WriteLine($"{DateTime.Now}: Exported record number: {_recordCount}");
                        }


                    }


                    Console.WriteLine($"{DateTime.Now}: Done exporting records.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to export data from database. Error: {ex.Message}");
            }








        }

        private static void InitialiseWriter(string destinationFolder)
        {
            // first claose the writer is it is laredy open.
            _writer?.Close();

            var filePath = $@"{destinationFolder}\dataexport_{_fileIndex}.csv";

            _writer = new StreamWriter(filePath) { AutoFlush = true };

            _fileIndex++;






        }
    }
}
