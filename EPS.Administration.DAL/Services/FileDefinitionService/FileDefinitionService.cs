using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;

namespace EPS.Administration.DAL.Services.FileDefinitionService
{
    public class FileDefinitionService : IFileDefinitionService
    {
        private readonly IBaseService<FileDefinitionData> _fileDefinition;
        private readonly IMapper _mapper;

        public FileDefinitionService(IBaseService<FileDefinitionData> fileDefinition, IMapper mapper)
        {
            _fileDefinition = fileDefinition;
            _mapper = mapper;
        }

        public FileDefinition Get(int id)
        {
            return _mapper.Map<FileDefinition>(_fileDefinition.Get(x => x.Id == id));
        }
    }
}
