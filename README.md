# HTTP generic client wrapper
Generic HTTP wrapper to consume RESTful API.

This webapi project is cosuming Brasil API - https://brasilapi.com.br/api/ - to get CEP code details.

<b><i>AddressController.cs </b></i>

```csharp
        [HttpGet("cep-details/{cep:int}")]
        public async Task<IActionResult> GetAddressDetailsByCep(int cep)
        {
            return Ok(await HttpClientWrapper<AddressViewModel>.Get(_urlBrasilApi + $"cep/v1/{cep}"));
        }
```

<i> Result </i>
```json
{
"cep": "89010025",
"state": "SC",
"city": "Blumenau",
"neighborhood": "Centro",
"street": "Rua Doutor Luiz de Freitas Melro",
"service": "viacep"
}
```

<b><i>HttpClientWrapper.cs </b></i>

```csharp
        public static async Task<T> Get(string url)
        {
            T result = null;
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }
```
		
* This HTTP Client Wrapper was built based on this [Medium article](https://medium.com/@srikanth.gunnala/generic-wrapper-to-consume-asp-net-web-api-rest-service-641b50462c0 )



