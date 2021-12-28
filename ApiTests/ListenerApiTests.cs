using ApiModels.Requests;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TeamServer.Models;
using Xunit;

namespace ApiTests
{
    public class ListenerApiTests : ApiTest
    {

        [Fact]
        public async void CreateListenerTest()
        {
            var createListenerRequest = new StartHttpListenerRequest
            {
                Name = "TestListener",
                BindPort = 8080
            };

            var json = JsonConvert.SerializeObject(createListenerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/Listeners", content);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseListener = JsonConvert.DeserializeObject<HttpListener>(responseJson);

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(responseListener);
            Assert.Equal(createListenerRequest.Name, responseListener.Name);
            Assert.Equal(createListenerRequest.BindPort, responseListener.BindPort);
        }
    }
}
