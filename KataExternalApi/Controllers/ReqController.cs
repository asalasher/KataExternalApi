using KataExternalApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KataExternalApi.Controllers
{
    public class ReqController : ApiController
    {
        // GET: api/Req
        [HttpGet]
        [Route("api/req")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://fakestoreapi.com/products");

                response.EnsureSuccessStatusCode();
                string responseAsString = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseAsString);

                IEnumerable<Product> bestProducts = products.Where(x => x.Rating.Rate > 3);

                return Ok(bestProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/req/v2")]
        public async Task<IHttpActionResult> GetV2()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://fakestoreapi.com/products");
            var client = new HttpClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseAsString);
                return Ok(products);
            }
            else
            {
                return BadRequest($"There was an error getting out forecast {response.ReasonPhrase}");
            }
        }

        /// <summary>
        /// Requesting information to an api with post method and content in the body
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/req/v3")]
        public async Task<IHttpActionResult> GetV3()
        {
            var client = new HttpClient();

            // option 1
            string jsonBody = JsonConvert.SerializeObject(new
            {
                userId = 1,
                id = 1,
                title = "new title",
            });

            //StringContent requestBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // option 2
            StringContent requestBody = new StringContent(
                JsonConvert.SerializeObject(
                    new
                    {
                        userId = 1,
                        id = 1,
                        title = "new title",
                    }),
                Encoding.UTF8,
                "application/json"
            );


            HttpResponseMessage response = await client.PostAsync("https://fakestoreapi.com/products", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Succesful request");
            }
            else
            {
                return BadRequest($"There was an error getting out forecast {response.ReasonPhrase}");
            }

        }

        /// <summary>
        /// Reading query params
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/req/v4")]
        public IHttpActionResult GetV4()
        {
            string queryParam1 = Request.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "param1").Value;
            string queryParam2 = Request.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "param2").Value;

            return Ok(new { queryParam1, queryParam2, page = 2 });
        }

        // GET: api/Req/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Req
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Req/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Req/5
        public void Delete(int id)
        {
        }
    }
}
