# Countries-REST-API

C# Developer Challenge
Descrição do Projeto

Este projeto é um cliente REST em C# que consome a API https://restcountries.com
e permite obter informações detalhadas de países, incluindo:

-> Nome comum e oficial

-> Capital

-> Região e sub-região

-> População e área

-> Fusos horários

-> Nomes nativos

-> para bandeiras (PNG/SVG)

-> Além de visualizar os dados via JSON, o sistema permite exportar informações dos países em diferentes formatos: CSV, Excel (XLSX) e XML.

========================================================================================================================================

Estrutura do Projecto (Arquitetura limpa)

-> DTOs (CountryDto, NameInfo, Flags): mapeiam os dados recebidos da API.

-> Serviço (ICountryService / CountryService): responsável por consumir a API e fornecer os dados.

-> Controller (CountryController): fornece endpoints REST para:

-> Obter países em JSON

-> Exportar CSV, XLSX e XML

-> Dependências externas:

-> ClosedXML para geração de arquivos Excel.

======================================================================================================================================

Principais Funcionalidades

-> Consumir API REST

-> Utiliza HttpClient com GetFromJsonAsync para obter dados da API de forma assíncrona.

-> Exportação de Dados

CSV: gera um arquivo CSV com todos os campos. Valores de lista em aspas, para evitar quebrar o arquivo em em caso de encontrar vírgula ou ponto e vírgula.

Excel (XLSX): gera planilha com cabeçalhos claros e ajuste automático de colunas.

XML: serializa objetos para XML, respeitando UTF-8.

Boas práticas

-> Uso de interface (ICountryService) para permitir injeção de dependência e facilitar testes unitários.

Tratamento de valores nulos (null) e listas vazias.

Estrutura organizada seguindo princípios SOLID.

===================================================================================================================================

Tecnologias Utilizadas

C# / .NET 9

ASP.NET Core Web API

ClosedXML (Excel)

System.Xml.Serialization (XML)

==================================================================================================================================

Como Executar

1. Clone o projecto
   git close <link-deste-repo>

2. Instale dependências NuGet
   -> dotnet restore

3. Corra a aplicação
   -> dotnet run

=================================================================================================================================

Observações Técnicas

Injeção de dependência: o controller depende de ICountryService ao invés de CountryService directamente. Isso permite trocar a implementação ou usar mocks para testes.

Validação de valores nulos: usamos operador ?? e verificações para evitar erros durante exportação.
