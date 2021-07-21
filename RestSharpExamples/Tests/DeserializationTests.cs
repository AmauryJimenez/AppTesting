using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharpExamples.DataEntities;

namespace RestSharpExamples.Tests
{
    [TestFixture]
    public class DeserializationTests
    {
        [Test]
        public void BizagiApis()
        {
            // arrange
            RestClient client = new RestClient("https://qa-amauryj:444/AmauryUI/oauth2/server/token");
            client.Authenticator = new HttpBasicAuthenticator("8a91f56b21c53c19e777698cc8cba62fa6280603c8acf9f414373ec48fe164cc", "17446bf3520f6e2a788d50979a21322bf02b12b820b4daa4a173b6adaa1b85b8");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=api", ParameterType.RequestBody);

            // act
            IRestResponse response = client.Execute(request);

            dynamic resp = JObject.Parse(response.Content);
            string token = resp.access_token;


            client = new RestClient("https://qa-amauryj:444/AmauryUI/odata/data");
            request = new RestRequest("/cases", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("cache-control", "no-cache");
            response = client.Execute(request);
            dynamic jsonToCopy = JObject.Parse(response.Content);


            //var odata = JsonConvert.DeserializeObject<OData>(json);
            var customerDto = JsonConvert.DeserializeObject<CasesResponse>(response.Content);
            //CasesResponse locationResponse = new JsonDeserializer().Deserialize<CasesResponse>(response);


            // assert
            // Assert.That(locationResponse.CountryAbbreviation, Is.EqualTo("US"));
        }

        [Test]
        public void CountryAbbreviationSerializationTest()
        {

            // arrange
            RestClient client = new RestClient("http://api.zippopotam.us");
            RestRequest request = new RestRequest("us/90210", Method.GET);

            // act
            IRestResponse response = client.Execute(request);
            dynamic resp2 = JObject.Parse(response.Content);

            LocationResponse locationResponse =
                new JsonDeserializer().
                Deserialize<LocationResponse>(response);

            // assert
            Assert.That(locationResponse.CountryAbbreviation, Is.EqualTo("US"));
        }

        [Test]
        public void StateSerializationTest()
        {
            // arrange
            RestClient client = new RestClient("http://api.zippopotam.us");
            RestRequest request = new RestRequest("us/12345", Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            LocationResponse locationResponse =
                new JsonDeserializer().
                Deserialize<LocationResponse>(response);

            // assert
            Assert.That(locationResponse.Places[0].State, Is.EqualTo("New York"));
        }
    }
}