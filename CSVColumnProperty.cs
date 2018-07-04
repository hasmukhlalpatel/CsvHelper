using System.Reflection;

namespace CsvHelper
{
    public class CSVColumnProperty{
        public string Name { get; set;}
        public string CSVColumnName { get; set; }       
        public int Index { get; set;}   
        public bool HasMapped { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
