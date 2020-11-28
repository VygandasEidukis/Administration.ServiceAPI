using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.ClassificationService
{
    public class ClassificationService : IClassificationService
    {
        private readonly IBaseService<ClassificationData> _classificationService;
        private readonly IMapper _mapper;

        public ClassificationService(IBaseService<ClassificationData> baseService, IMapper mapper)
        {
            _classificationService = baseService;
            _mapper = mapper;
        }

        public void AddOrUpdate(Classification classification)
        {
            _classificationService.AddOrUpdate(_mapper.Map<ClassificationData>(classification));
            _classificationService.Save();
        }

        public void AddOrUpdate(IEnumerable<Classification> classifications)
        {
            var dtos = classifications.Select(x => _mapper.Map<ClassificationData>(x));
            foreach (var dto in dtos)
            {
                var item = _classificationService.GetSingle(x => x.Code == dto.Code);
                dto.Id = item == null ? 0 : item.Id;
                _classificationService.AddOrUpdate(dto);
            }
            _classificationService.Save();
        }

        public Classification Get(string code)
        {
            var classification = _classificationService.GetSingle(x => x.Code == code);
            return _mapper.Map<Classification>(classification);
        }

        public Classification Get(int id)
        {
            var classification = _classificationService.GetSingle(x => x.Id == id);
            return _mapper.Map<Classification>(classification);
        }

        public List<Classification> Get()
        {
            var classificationsDto = _classificationService.GetLatest();

            List<Classification> classifications = classificationsDto.Select(x => _mapper.Map<Classification>(x)).ToList();
            return classifications;
        }
    }
}
