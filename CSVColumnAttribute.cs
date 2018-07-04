namespace CsvHelper
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class CSVColumnAttribute : System.Attribute
    {
        public CSVColumnAttribute(string name)
        {
            Name = name;
        }
        public CSVColumnAttribute(int index)
        {
            Index = index;
        }

        public string Name { get; set;}
                
        public int Index { get; set;}
    }
}
