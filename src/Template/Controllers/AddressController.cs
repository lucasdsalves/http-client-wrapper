using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Template.Application.ViewModels;
using Template.Infra.Data.HttpRequests;

namespace Template.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly string _urlBrasilApi;

        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlBrasilApi = _configuration.GetSection("UrlBrasilApi").Value;
        }

        [HttpGet("cep-details/http-client-wrapper/{cep:int}")]
        public async Task<IActionResult> GetAddressDetailsByCepUsingHttpClientWrapper(int cep)
        {
            return Ok(await HttpClientWrapper<AddressViewModel>.Get(_urlBrasilApi + $"cep/v1/{cep}"));
        }

        [HttpGet("cep-details/flur/{cep:int}")]
        public async Task<IActionResult> GetAddressDetailsByCepUsingFlur(int cep)
        {
            return Ok(await _urlBrasilApi.AppendPathSegment("cep/v1/" + cep).GetJsonAsync<AddressViewModel>());
        }
    }
}
