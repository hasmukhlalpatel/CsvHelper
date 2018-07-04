using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CsvHelper
{
    public static class CsvSerializer{

        public static IList<T> Deserialize<T>(string filePath, string separator = ",")
        where T:class, new()
        {
            var returnList = new List<T>();

            using (var fileSR= new StreamReader(filePath))
            {
                var line =string.Empty;
                List<CSVColumnProperty> typeProperties =null;

                while ((line = fileSR.ReadLine()) != null)
                {
                    if( typeProperties == null){
                        typeProperties = GetProperties<T>(line,separator);
                    }else{
                        var obj = Deserialize<T>(typeProperties,line, separator);
                        returnList.Add(obj);
                    }
                }
            } 

            return returnList;

        }

        private static List<CSVColumnProperty> GetProperties<T>(string headerLine, string separator){

            var columnNames = Regex.Split(headerLine, separator);

            var type = typeof(T);
            var properties = type.GetProperties();
            
            var index=0;
            var typeProperties = properties
            .Select(x=> {
                var columnProperty = new CSVColumnProperty{ Name = x.Name, Property = x, Index = index++ };
                var csvColumnAtrrib = x.GetCustomAttribute(typeof(CSVColumnAttribute)) as CSVColumnAttribute;
                if(csvColumnAtrrib != null){
                    columnProperty.CSVColumnName = csvColumnAtrrib.Name;
                    columnProperty.Index = csvColumnAtrrib.Index;
                    columnProperty.HasMapped = true;
                }
                var indexOf = Array.IndexOf(columnNames, x.Name);
                if(indexOf >=0){
                    columnProperty.Index = indexOf;
                    columnProperty.HasMapped = true;
                }
                return columnProperty;

            } )
            .ToList();
            return typeProperties;
        }

        private static T Deserialize<T>(List<CSVColumnProperty> typeProperties, string dataLine, string separator)
        where T:class, new()
        {
            var columnData = Regex.Split(dataLine, separator);
            var obj = new T();
            foreach (var typeProperty in typeProperties.Where(x=>x.HasMapped))
            {
                var data = columnData[typeProperty.Index];
                typeProperty.Property.SetValue(obj,data);
            }
            return obj;
        }

    }
}
