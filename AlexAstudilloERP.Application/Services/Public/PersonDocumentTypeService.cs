using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class PersonDocumentTypeService : IPersonDocumentTypeService
{
    private readonly IPersonDocumentTypeRepository _repository;

    public PersonDocumentTypeService(IPersonDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PersonDocumentType>> GetAll(bool? onlyActive = null)
    {
        IEnumerable<PersonDocumentType> data = await _repository.FindAllAsync();
        if (onlyActive == null) return data.ToList();
        if (onlyActive.Value) return data.Where(pdt => pdt.Active).ToList();
        return data.Where(pdt => !pdt.Active).ToList();
    }

    public async Task<List<PersonDocumentType>> GetByCountryCodeAsync(string countryCode, bool? onlyActive = null)
    {
        List<PersonDocumentType> data = await _repository.FindByCountryCodeAsync(countryCode);
        if (onlyActive == null) return data;
        if (onlyActive.Value) return data.Where(pdt => pdt.Active).ToList();
        return data.Where(pdt => !pdt.Active).ToList();
    }
}
