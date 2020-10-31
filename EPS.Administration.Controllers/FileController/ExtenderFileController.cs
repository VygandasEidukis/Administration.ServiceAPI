using EPS.Administration.Models.Exceptions;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Administration.Controllers.FileController
{
    public class ExtenderFileController
    {
        private Stream _stream;

        public ExtenderFileController(Stream stream)
        {
            this._stream = stream;
        }

        public Task ProcessFile()
        {
            if (_stream == null || _stream.Length == 0)
            {
                //TODO: MEDIUM Add logging.
                throw new AdministrationException("File is empty or does not exists.");
            }

            try
            {
                //TODO: LOW Consider using key values being set from interface instead of hard coding enumeration of fields. 
                //Since this approach is fine until there's more properties or property names are changed.
                //If it does come down of using 
                using (var excel = ExcelReaderFactory.CreateReader(_stream))
                {
                    while(excel.Read())
                    {
                        var values = Enum.GetValues(typeof(FileDataFlag))
                                            .OfType<FileDataFlag>()
                                            .Where(x=> x != FileDataFlag.EOF && x != FileDataFlag.Registras)
                                            .Select(x => x.ToString().ToUpper())
                                            .ToList();

                        var selector = values.FirstOrDefault(x => excel.GetString(1) != null && excel.GetString(1).ToUpper().Contains(x));
                        if (selector != null)
                        {
                            var currnetFlag = Enum.GetValues(typeof(FileDataFlag))
                                            .OfType<FileDataFlag>()
                                            .FirstOrDefault(x => x.ToString().ToUpper() == selector);

                            var porpertyList = ReadProperty(excel);
                            AddProperties(currnetFlag, porpertyList);
                        }
                    }
                }
            }
            catch
            {
                //TODO: MEDIUM Add logging.
                throw new AdministrationException("Failed to process file due to an internal error.");
            }
            return Task.CompletedTask;
        }

        private List<Tuple<string,string>> ReadProperty(IExcelDataReader excel)
        {
            Skip(excel, 2);
            var propertyList = new List<Tuple<string, string>>();
            while(excel.Read() && excel.GetString(1)?.ToUpper() != FileDataFlag.EOF.ToString())
            {
                string Key = excel.GetString(1);
                string Value = excel.GetString(2);

                if(string.IsNullOrEmpty(Key))
                {
                    continue;
                }

                var property = new Tuple<string, string>(Key, Value);
                propertyList.Add(property);
            }

            return propertyList;
        }

        private void Skip(IExcelDataReader excel, int count)
        {
            for(int i = 0; i < count; i++)
            {
                excel.Read();
            }
        }

        private void AddProperties(FileDataFlag flag, List<Tuple<string,string>> properties)
        {
            //TODO: HIGH AddOrUpdate property
            switch (flag)
            {
                case FileDataFlag.Statusai:
                    break;
                case FileDataFlag.klasifikatorius:
                    break;
                case FileDataFlag.komplektacijos:
                    break;
                case FileDataFlag.Vietos:
                    break;
            }
        }
    }
}
