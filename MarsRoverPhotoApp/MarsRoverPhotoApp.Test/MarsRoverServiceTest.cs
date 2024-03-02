using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoverPhotoApp.Test
{
    public class MarsRoverServiceTest
    {
        private IConfiguration configuration = null!;

        public MarsRoverServiceTest()
        {
            configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json")
         .Build();
        }
        [Fact]
        public async Task GetPhotosByDateAsync_CallShouldSucceed_ForValidDate()
        {
            MarsRoverService marsRoverService = new MarsRoverService(configuration);
            var images = await marsRoverService.GetPhotosByDateAsync(Convert.ToDateTime("02/27/17"));
            Assert.NotNull(images);
        }

        [Fact]
        public async Task GetPhotosByDateAsync_CallShouldFail_ForInValidDate()
        {
            MarsRoverService marsRoverService = new MarsRoverService(configuration);
            var exception = Assert.ThrowsAsync<Exception>(async () => await marsRoverService.GetPhotosByDateAsync(Convert.ToDateTime("02/30/17")));
            Assert.NotNull(exception);
        }
    }
}