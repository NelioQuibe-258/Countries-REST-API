using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.Xml.Serialization;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    //Buscar dados dos países
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries);
    }

    //exportar em csv
    [HttpGet("export/csv")]
    public async Task<IActionResult> ExportCsv()
    {
        var countries = await _countryService.GetAllCountriesAsync();

        var sb = new StringBuilder();
        sb.AppendLine("Name,Capital,Region,Subregion,Population,Area,Timezones,NativeName,Flag");

        foreach (var c in countries)
        {
            var name = c.Name?.Common ?? "";
            var capital = c.Capital != null ? string.Join(" | ", c.Capital) : "";
            var timezones = c.Timezones != null ? string.Join(" | ", c.Timezones) : "";
            var nativeNames = c.NativeName != null ? string.Join(" | ", c.NativeName.Values) : "";
            var flag = c.Flags?.Png ?? "";

            sb.AppendLine($"\"{name}\",\"{capital}\",\"{c.Region}\",\"{c.Subregion}\",\"{c.Population}\",\"{c.Area}\",\"{timezones}\",\"{nativeNames}\",\"{flag}\"");

        }

        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "countries.csv");
    }

    //exportar em XLS

    [HttpGet("export/xls")]
    public async Task<IActionResult> ExportExcel()
    {
        var countries = await _countryService.GetAllCountriesAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Countries");

            // Cabeçalhos
            worksheet.Cell(1, 1).Value = "Common Name";
            worksheet.Cell(1, 2).Value = "Official Name";
            worksheet.Cell(1, 3).Value = "Capital";
            worksheet.Cell(1, 4).Value = "Region";
            worksheet.Cell(1, 5).Value = "Subregion";
            worksheet.Cell(1, 6).Value = "Population";
            worksheet.Cell(1, 7).Value = "Area";
            worksheet.Cell(1, 8).Value = "Timezones";
            worksheet.Cell(1, 9).Value = "Native Names";
            worksheet.Cell(1, 10).Value = "Flag";

            int row = 2;
            foreach (var c in countries)
            {
                var commonName = c.Name?.Common ?? "";
                var officialName = c.Name?.Official ?? "";
                var capital = c.Capital != null ? string.Join(";", c.Capital) : "";
                var timezones = c.Timezones != null ? string.Join(";", c.Timezones) : "";
                var nativeNames = c.NativeName != null
                    ? string.Join(";", c.NativeName.Select(n => $"{n.Key}:{n.Value}"))
                    : "";
                var flag = c.Flags?.Png ?? "";

                worksheet.Cell(row, 1).Value = commonName;
                worksheet.Cell(row, 2).Value = officialName;
                worksheet.Cell(row, 3).Value = capital;
                worksheet.Cell(row, 4).Value = c.Region;
                worksheet.Cell(row, 5).Value = c.Subregion;
                worksheet.Cell(row, 6).Value = c.Population;
                worksheet.Cell(row, 7).Value = c.Area;
                worksheet.Cell(row, 8).Value = timezones;
                worksheet.Cell(row, 9).Value = nativeNames;
                worksheet.Cell(row, 10).Value = flag;

                row++;
            }

            // Ajustar largura automática
            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "countries.xlsx");
            }
        }
    }


    //export xml
    [HttpGet("export/xml")]
    public async Task<IActionResult> ExportXml()
    {
        var countries = await _countryService.GetAllCountriesAsync();

        // Serializador XML
        var serializer = new XmlSerializer(typeof(List<CountryDto>));

        await using var memoryStream = new MemoryStream();
        using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
        {
            serializer.Serialize(writer, countries);
        }

        memoryStream.Position = 0; // voltar ao início do stream

        return File(memoryStream.ToArray(), "application/xml", "countries.xml");
    }

}
