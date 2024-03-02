using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MarsRoverService
{
    private readonly IConfiguration _configuration;

    public MarsRoverService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<string>> GetPhotosByDateAsync(DateTime date)
    {
        var apiKey = _configuration["NASA:ApiKey"];
        var baseUrl = _configuration["NASA:BaseUrl"];

        using (var httpClient = new HttpClient())
        {
            var url = $"{baseUrl}?earth_date={date.ToString("yyyy-MM-dd")}&api_key={apiKey}";
            var response = await httpClient.GetStringAsync(url);
            var photoData = JsonConvert.DeserializeObject<MarsRoverApiResponse>(response);

            var imageUrls = new List<string>();
            foreach (var photo in photoData.Photos)
            {
                imageUrls.Add(photo.ImgSrc);
            }

            return imageUrls;
        }
    }

    public async Task DownloadAndSaveImagesAsync(IEnumerable<string> imageUrls, string saveDirectory)
    {
        using (var httpClient = new HttpClient())
        {
            foreach (var imageUrl in imageUrls)
            {
                Console.WriteLine($"Downloading image: {imageUrl}");

                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                // Extract the filename from the URL
                var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);

                // Combine the save directory with the filename
                var filePath = Path.Combine(saveDirectory, fileName);

                // Save the image to the local file system
                File.WriteAllBytes(filePath, imageBytes);

                Console.WriteLine($"Image saved to: {filePath}");
            }
        }
    }
}

public class MarsRoverApiResponse
{
    [JsonProperty("photos")]
    public List<MarsRoverPhoto> Photos { get; set; }
}

public class MarsRoverPhoto
{
    [JsonProperty("img_src")]
    public string ImgSrc { get; set; }
}
