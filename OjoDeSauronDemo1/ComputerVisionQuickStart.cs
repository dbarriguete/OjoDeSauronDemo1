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

        public int ReviewPersons()
        {
            int answer = 0;
            //create a client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

            // Analyze an image to get features and other properties.
            //AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();


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
    }
}
