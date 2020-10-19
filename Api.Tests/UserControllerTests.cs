using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OneIdentityApi.Models;
using Xunit;

namespace Api.Tests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<OneIdentityApi.Startup>>
    {
        public HttpClient _client { get; }

        public UserControllerTests(WebApplicationFactory<OneIdentityApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Users()
        {
            var response = await _client.GetAsync("api/user");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = JsonConvert.DeserializeObject<User[]>(await response.Content.ReadAsStringAsync());
            users.Should().HaveCount(9);
        }

        [Fact]
        public async Task Get_Should_Retrieve_UserId1()
        {
            var response = await _client.GetAsync("api/user/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            user.id.Should().Be(1);
        }

        [Fact]
        public async Task Put_Should_Update_UserId7()
        {
            string jsonBody = "{\"id\":7,\"name\":\"XXX Weissnat\",\"username\":\"Elwyn.Skiles\",\"email\":\"Telly.Hoeger@billy.biz\",\"address\":{\"street\":\"Rex Trail\",\"suite\":\"Suite 280\",\"city\":\"Howemouth\",\"zipcode\":\"58804-1099\",\"geo\":{\"lat\":\"24.8918\",\"lng\":\"21.8984\"}},\"phone\":\"210.067.6132\",\"website\":\"elvis.io\",\"company\":{\"name\":\"Johns Group\",\"catchPhrase\":\"Configurable multimedia task-force\",\"bs\":\"generate enterprise e-tailers\"}}";
            HttpContent hp = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/user/7", hp);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
          

            var response2 = await _client.GetAsync("api/user/7");
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            var user = JsonConvert.DeserializeObject<User>(await response2.Content.ReadAsStringAsync());
            user.name.Should().Be("XXX Weissnat");
        }
        [Fact]
        public async Task Delete_Should_Delete_UserId7()
        {
            var response = await _client.DeleteAsync("api/user/7");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Thread.Sleep(100);

            var response2 = await _client.GetAsync("api/user/7");
            response2.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task Post_Should_Create_UserId7()
        {

            string jsonBody = "{\"id\":7,\"name\":\"Kurtis Weissnat\",\"username\":\"Elwyn.Skiles\",\"email\":\"Telly.Hoeger@billy.biz\",\"address\":{\"street\":\"Rex Trail\",\"suite\":\"Suite 280\",\"city\":\"Howemouth\",\"zipcode\":\"58804-1099\",\"geo\":{\"lat\":\"24.8918\",\"lng\":\"21.8984\"}},\"phone\":\"210.067.6132\",\"website\":\"elvis.io\",\"company\":{\"name\":\"Johns Group\",\"catchPhrase\":\"Configurable multimedia task-force\",\"bs\":\"generate enterprise e-tailers\"}}";
            HttpContent hp = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/user/", hp);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            

            var response2 = await _client.GetAsync("api/user/7");
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            var user2 = JsonConvert.DeserializeObject<User>(await response2.Content.ReadAsStringAsync());
            user2.name.Should().Be("Kurtis Weissnat");
        }
    }
}

