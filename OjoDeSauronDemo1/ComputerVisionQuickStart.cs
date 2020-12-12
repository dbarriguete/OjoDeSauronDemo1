using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;

namespace OjoDeSauronDemo1
{
    class ComputerVisionQuickStart
    {
        // Add your Computer Vision subscription key and endpoint
        static string subscriptionKey = "";
        static string endpoint = "";

        // URL image used for analyzing an image (image of puppy)
        private const string ANALYZE_URL_IMAGE = "https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png";
        private const string LOCAL_IMAGE = "\\img\\Ejemplo01.png";
//        private const string ANALYZE_URL_IMAGE = "https://github.com/dbarriguete/OjoDeSauronDemo1/blob/main/OjoDeSauronDemo1/img/Ejemplo01.png";

        public int ReviewPersons()
        {
            int answer = 0;
            //create a client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

            string imagelocation = Path.Combine(Path.GetPathRoot( Directory.GetCurrentDirectory() ),  "temp", "img", "Ejemplo03.png");

            // Analyze an image to get features and other properties.
            AnalyzeImageUrl(client, imagelocation, false).Wait();


            return answer;
        }

        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            { Endpoint = endpoint };
            return client;
        }

        /* 
         * ANALYZE IMAGE - URL IMAGE
         * Analyze URL image. Extracts captions, categories, tags, objects, faces, racy/adult content,
         * brands, celebrities, landmarks, color scheme, and image types.
         */
        public static async Task<int> AnalyzeImageUrl(ComputerVisionClient client, string imageUrl, bool isImageUrl)
        {
            int answer = 0;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("ANALYZE IMAGE - URL");
            Console.WriteLine();

            // Creating a list that defines the features to be extracted from the image. 

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

            Console.WriteLine($"Analyzing the image {Path.GetFileName(imageUrl)}...");
            Console.WriteLine();

            ImageAnalysis results = null;
            
            // Analyze the URL image 
            if (isImageUrl)
                results = await client.AnalyzeImageAsync(imageUrl, features);
            else
                results = await client.AnalyzeImageInStreamAsync(new FileStream(imageUrl, FileMode.Open), features);

            // Sunmarizes the image content.
            Console.WriteLine("Summary:");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();


            // Objects
            Console.WriteLine("Objects:");
            foreach (var obj in results.Objects)
            {
                Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
                  $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
                if ( obj.ObjectProperty.Equals("person") )
                {
                    answer++;
                }
            }
            Console.WriteLine();

            System.Console.WriteLine($"The image has {answer} persons.");
            return answer;
        }
    }
}
