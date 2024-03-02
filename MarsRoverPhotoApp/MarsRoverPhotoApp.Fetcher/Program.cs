using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var service = new MarsRoverService(configuration);
        var dates = await File.ReadAllLinesAsync("dates.txt");

        foreach (var date in dates)
        {
            DateTime parsedDate;
            if (!DateTime.TryParseExact(date, new[] { "MM/dd/yy", "MMMM d, yyyy", "MMM-dd-yyyy", "MMMM dd, yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                Console.WriteLine($"Invalid date format: {date}");
                return;
            }
            var imageUrls = await service.GetPhotosByDateAsync(Convert.ToDateTime(parsedDate));


            await service.DownloadAndSaveImagesAsync(imageUrls, configuration["NASA:DestinationFolder"]!);
        }
    }
}
