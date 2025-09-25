using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class CountryService : ICountryService
{
    private readonly HttpClient _httpClient;

    //Constructor
    public CountryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    /*Retorna todos os dados da api*/
    public async Task<List<CountryDto>> GetAllCountriesAsync()
    {
        var url = "https://restcountries.com/v3.1/all?fields=name,capital,region,subregion,population,area,timezones,flags";

        var countries = await _httpClient.GetFromJsonAsync<List<CountryDto>>(url);

        return countries ?? new List<CountryDto>();
    }
}