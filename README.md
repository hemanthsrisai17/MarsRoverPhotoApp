Mars Rover Photo Explorer

A collection of tools to explore and enjoy NASA's Mars rover imagery.

Overview

Mars Rover Photo Explorer connects to NASA's API to download and display photographs taken by the Curiosity rover on Mars. The application handles different date formats, downloads high-resolution images, and provides a gallery to browse the Martian landscape.

What's Included:

The project consists of three main components:

Image Downloader (Console Application)

Connects to NASA's Mars Rover Photos API using your API key
Processes multiple date formats from an input file
Downloads and saves images to your specified destination folder
Cross-platform compatibility with path handling


Image Gallery (Angular Web Application)

Browse downloaded Mars rover photos in a user-friendly interface
Responsive design for desktop and mobile viewing


Testing Framework (xUnit)

Ensures reliability of the core functionality



Configuration

appsettings.json
The downloader requires configuration in the appsettings.json file:
json{
  "NASA": {
    "ApiKey": "YOUR_API_KEY",
    "BaseUrl": "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos",
    "DestinationFolder": "c:\\temp"
  }
}

dates.txt

Create a text file with dates in various supported formats:

02/27/17
June 2, 2018
Jul-13-2016
April 31, 2018

The application supports multiple date formats:

MM/dd/yy
MMMM d, yyyy
MMM-dd-yyyy
MMMM dd, yyyy

How It Works

The application reads dates from the dates.txt file, parses them into a standardized format, and then:

Queries the NASA API for photos taken on each date
Downloads each photo using HttpClient
Saves images to the configured destination folder
Provides status updates in the console during download

Core functionality is handled by the MarsRoverService class, which:

Connects to NASA's API with your provided key
Parses the JSON response to extract image URLs
Downloads and saves each image with its original filename

Getting Started:

Prerequisites:

.NET 6.0 or higher
NASA API key (get one at https://api.nasa.gov/)
Node.js and npm (for the Angular application)
Docker (optional, for containerized deployment)

Running the Image Downloader:

Configure your NASA API key in appsettings.json
Add desired dates to download in dates.txt
Run the application:
dotnet run --project MarsRoverPhotoApp.Fetcher


Running the Image Gallery:

Navigate to the Angular project directory
cd MarsRoverPhotoApp.Gallery

Install dependencies and start the server
npm install
ng serve

Open your browser to http://localhost:4200

Docker Support
Both applications can be run in containers using the provided Dockerfiles:

bash# Build and run the Image Downloader

docker build -t mars-rover-downloader .

docker run -v /your/image/folder:/app/images mars-rover-downloader

Below are the screen shots of the Apps while developing them in my local

1) Image gallery
   ![image](https://github.com/hemanthsrisai17/MarsRoverPhotoApp/assets/71496909/36dc0752-da5f-41b7-a9db-2c8403dc28ab)
2) Dowloaded files
    ![image](https://github.com/hemanthsrisai17/MarsRoverPhotoApp/assets/71496909/1805a677-8b66-4641-b72a-696a2d2bd260)
3) Tests from test explorer
    ![image](https://github.com/hemanthsrisai17/MarsRoverPhotoApp/assets/71496909/0aaee45a-8316-4c90-ae1c-e07bba76d6e8)


