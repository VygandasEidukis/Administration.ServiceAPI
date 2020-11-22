using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.DetailedStatusService
{
    public class DetailedStatusService : IDetailedStatusService
    {
        private readonly IBaseService<DetailedStatusData> _detailedStatusService;
        private readonly IMapper _mapper;

        public DetailedStatusService(IBaseService<DetailedStatusData> baseService, IMapper mapper)
        {
            _detailedStatusService = baseService;
            _mapper = mapper;
        }

        public void AddOrUpdate(DetailedStatus status)
        {
            _detailedStatusService.AddOrUpdate(_mapper.Map<DetailedStatusData>(status));
            _detailedStatusService.Save();
        }

        public void AddOrUpdate(IEnumerable<DetailedStatus> statuses)
        {
            var dtos = statuses.Select(x => _mapper.Map<DetailedStatusData>(x));
            foreach(var dto in dtos)
            {
                var item = _detailedStatusService.GetSingle(x=>x.Status == dto.Status);
                dto.Id = item == null ? 0 : item.Id;
                _detailedStatusService.AddOrUpdate(dto);
            }
            _detailedStatusService.Save();
        }

        public List<DetailedStatus> Get()
        {
            var statusDTO = _detailedStatusService.GetLatest();

            List<DetailedStatus> statuses = statusDTO.Select(x => _mapper.Map<DetailedStatus>(x)).ToList();
            return statuses;
        }

        public DetailedStatus GetStatus(string status)
        {
            var item = _detailedStatusService.GetSingle(x => x.Status == status);
            return _mapper.Map<DetailedStatus>(item);
        }

        public DetailedStatus GetStatus(int id)
        {
            var item = _detailedStatusService.GetSingle(x => x.Id == id);
            return _mapper.Map<DetailedStatus>(item);
        }
    }
}
