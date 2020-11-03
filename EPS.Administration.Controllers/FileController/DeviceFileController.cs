﻿using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.Models.Device;
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
    public class DeviceFileController
    {
        private Stream _stream;
        private Dictionary<string, List<Device>> _devices;
        private readonly IDetailedStatusService _statusService;
        private readonly IClassificationService _groupService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IDeviceLocationService _deviceLocationService;
        public DeviceFileController(Stream stream, 
                                    IDetailedStatusService statusService, 
                                    IClassificationService classificationService,
                                    IDeviceModelService deviceModelService,
                                    IDeviceLocationService deviceLocationService)
        {
            _stream = stream;
            _devices = new Dictionary<string, List<Device>>();
            _statusService = statusService;
            _groupService = classificationService;
            _deviceModelService = deviceModelService;
            _deviceLocationService = deviceLocationService;
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
                        var values = Enum.GetValues(typeof(DeviceDataFlag))
                                            .OfType<DeviceDataFlag>()
                                            .Where(x=> x != DeviceDataFlag.EOF && x != DeviceDataFlag.Registras)
                                            .Select(x => x.ToString().ToUpper())
                                            .ToList();

                        var selector = values.FirstOrDefault(x => excel.GetString(1) != null && excel.GetString(1).ToUpper().Contains(x));
                        if (selector != null)
                        {
                            var currnetFlag = Enum.GetValues(typeof(DeviceDataFlag))
                                            .OfType<DeviceDataFlag>()
                                            .FirstOrDefault(x => x.ToString().ToUpper() == selector);

                            var porpertyList = ReadProperty(excel);
                            AddProperties(currnetFlag, porpertyList);
                        }else if (excel.GetString(1) != null && excel.GetString(1).ToUpper().Contains(DeviceDataFlag.Registras.ToString().ToUpper()))
                        {
                            Skip(excel, 5);
                            ReadRevisions(excel);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new AdministrationException(ex.Message);
                //TODO: MEDIUM Add logging.
                throw new AdministrationException("Failed to process file due to an internal error.");
            }
            return Task.CompletedTask;
        }

        private List<Tuple<string,string>> ReadProperty(IExcelDataReader excel)
        {
            Skip(excel, 2);
            var propertyList = new List<Tuple<string, string>>();
            while(excel.Read() && excel.GetString(1)?.ToUpper() != DeviceDataFlag.EOF.ToString())
            {
                string Key = excel.GetString(1);
                string Value = excel.GetString(2);

                //TODO: might need a change, since it seems like an error in excel, this is in locations table
                if (string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(excel.GetString(4)))
                {
                    Value = excel.GetString(4);
                }

                if(string.IsNullOrEmpty(Key))
                {
                    continue;
                }

                var property = new Tuple<string, string>(Key, Value);
                propertyList.Add(property);
            }

            return propertyList;
        }

        private void ReadRevisions (IExcelDataReader excel)
        {
            while(excel.Read() && excel.GetString(3) != null)
            {
                var device = new Device();

                try
                {

                }catch
                {
                    //TODO: HIGH add logging
                    throw new AdministrationException($"Failed to parse the device {excel.GetString(3)}");
                }

            }
        }

        private void Skip(IExcelDataReader excel, int count)
        {
            for(int i = 0; i < count; i++)
            {
                excel.Read();
            }
        }

        private void AddProperties(DeviceDataFlag flag, List<Tuple<string,string>> properties)
        {
            //TODO: HIGH AddOrUpdate property
            switch (flag)
            {
                case DeviceDataFlag.Statusai:
                    var statuses = new List<DetailedStatus>();
                    foreach(var statusItem in properties)
                    {
                        var stat = new DetailedStatus()
                        {
                            Status = statusItem.Item1,
                            Description = statusItem.Item2,
                        };
                        statuses.Add(stat);
                    }
                    _statusService.AddOrUpdate(statuses);
                    break;
                case DeviceDataFlag.klasifikatorius:
                    var models = new List<DeviceModel>();
                    foreach (var statusItem in properties)
                    {
                        var deviceModel = new DeviceModel()
                        {
                            Name = statusItem.Item1,
                            Description = statusItem.Item2,
                        };
                        models.Add(deviceModel);
                    }
                    _deviceModelService.AddOrUpdate(models);
                    break;
                case DeviceDataFlag.komplektacijos:
                    var groups = new List<Classification>();
                    foreach (var groupItem in properties)
                    {
                        var group = new Classification()
                        {
                            Code = groupItem.Item1,
                            Model = groupItem.Item2,
                        };
                        groups.Add(group);
                    }
                    _groupService.AddOrUpdate(groups);
                    break;
                case DeviceDataFlag.Vietos:
                    var locations = new List<DeviceLocation>();
                    foreach (var locationItem in properties)
                    {
                        var location = new DeviceLocation()
                        {
                            Name = locationItem.Item1,
                            Details = locationItem.Item2,
                        };
                        locations.Add(location);
                    }
                    _deviceLocationService.AddOrUpdate(locations);
                    break;
            }
        }
    }
}
