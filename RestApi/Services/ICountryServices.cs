using System.Collections.Generic;
using System.Threading.Tasks;
public interface ICountryService
{
    public Task<List<CountryDto>> GetAllCountriesAsync();
}