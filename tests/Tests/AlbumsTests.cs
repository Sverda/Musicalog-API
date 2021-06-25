using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Musicalog.WebUI.Dto;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class AlbumsTests
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        private static IHost _host;
        private static HttpClient _client;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTestsAsync()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseConfiguration(configurationBuilder.Build());
                    webHost.UseTestServer();
                    webHost.UseStartup<Musicalog.WebUI.Startup>();
                });

            _host = await hostBuilder.StartAsync();
            _client = _host.GetTestClient();
        }

        [Test]
        public async Task GetAlbums_Success()
        {
            var response = await _client.GetAsync("/api/albums");

            var responseContent = await response.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<AlbumDto[]>(responseContent);
            TestContext.WriteLine(JsonConvert.SerializeObject(albums, Formatting.Indented));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task AddAlbum_Success()
        {
            var album = new CreateAlbumDto
            {
                Title = "New album 1",
                AlbumType = "CD",
                ArtistId = 1,
                Stock = 200
            };
            var body = JsonConvert.SerializeObject(album);
            var response = await _client.PostAsync("/api/albums", 
                new StringContent(
                    body, 
                    Encoding.UTF8, 
                    "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();
            TestContext.WriteLine(responseContent);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task UpdateAlbum_Success()
        {
            var dbAlbum = (await GetAlbums()).LastOrDefault();
            if (dbAlbum is null)
            {
                Assert.Fail("No albums in db");
            }

            var album = new CreateAlbumDto
            {
                Title = "Updated album 1",
                AlbumType = "CD",
                ArtistId = 1,
                Stock = 2400
            };
            var body = JsonConvert.SerializeObject(album);
            var response = await _client.PutAsync($"/api/albums/{dbAlbum.Id}",
                new StringContent(
                    body,
                    Encoding.UTF8,
                    "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();
            TestContext.WriteLine(responseContent);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteAlbum_Success()
        {
            var dbAlbum = (await GetAlbums()).LastOrDefault();
            if (dbAlbum is null)
            {
                Assert.Fail("No albums in db");
            }

            var response = await _client.DeleteAsync($"/api/albums/{dbAlbum.Id}");

            var responseContent = await response.Content.ReadAsStringAsync();
            TestContext.WriteLine(responseContent);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private static async Task<List<AlbumDto>> GetAlbums()
        {
            var response = await _client.GetAsync("/api/albums");
            var responseContent = await response.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<AlbumDto[]>(responseContent).ToList();
            return albums;
        }
    }
}