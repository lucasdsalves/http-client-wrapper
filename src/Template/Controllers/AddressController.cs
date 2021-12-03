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

        [HttpGet("cep-details/{cep:int}")]
        public async Task<IActionResult> GetAddressDetailsByCep(int cep)
        {
            return Ok(await HttpClientWrapper<AddressViewModel>.Get(_urlBrasilApi + $"cep/v1/{cep}"));
        }
    }
}
