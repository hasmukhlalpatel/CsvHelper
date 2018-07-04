using System;

namespace CsvHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "c:\\temp\\test.csv";
           var data = CsvSerializer.Deserialize<SampleData>(filePath);
           foreach (var item in data)
           {
            Console.WriteLine($"{item.Name}- {item.DOB} - {item.Address}");   
           }

            
        }
    }

    public class SampleData{

        [CSVColumn("Name", Index=1)]
        public string Name { get; set; }

        [CSVColumn(2)]
        public string DOB { get; set; }
        public string Address { get; set; }   
    }
}
