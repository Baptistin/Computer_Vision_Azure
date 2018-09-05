// ####################################
//  Class : Program ComputerVision
//  Author : Baptistin
//  Date : 08 / 2018
// ####################################

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ConsoleApp1
{
    /// <summary>
    ///     Computer Vision Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     The subscription key
        /// </summary>
        private const string SubscriptionKey = "<KEY>";

        /// <summary>
        ///     The remote image URL
        /// </summary>
        private const string RemoteImageUrl =
            "http://baptistin.com/Images/landscape.jpg";

        // Specify the features to return
        /// <summary>
        ///     The features
        /// </summary>
        private static readonly List<VisualFeatureTypes> Features =
            new List<VisualFeatureTypes>
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags
            };

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            var computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(SubscriptionKey))
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com" // For Europe location
            };


            Console.WriteLine("Images being analyzed ...");
            var t1 = AnalyzeRemoteAsync(computerVision, RemoteImageUrl);

            Task.WhenAny(t1).Wait(5000);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        /// <summary>
        ///     Analyzes the remote asynchronous.
        /// </summary>
        /// <param name="computerVision">The computer vision.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns></returns>
        private static async Task AnalyzeRemoteAsync(
            IComputerVisionClient computerVision, string imageUrl)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Console.WriteLine(
                    "\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                return;
            }

            var analysis =
                await computerVision.AnalyzeImageAsync(imageUrl, Features);
            DisplayResults(analysis, imageUrl);
        }

        // Display the most relevant caption for the image
        /// <summary>
        ///     Displays the results.
        /// </summary>
        /// <param name="analysis">The analysis.</param>
        /// <param name="imageUri">The image URI.</param>
        private static void DisplayResults(ImageAnalysis analysis, string imageUri)
        {
            Console.WriteLine(imageUri);
            Console.WriteLine(analysis.Description.Captions[0].Text + "\n");
        }
    }
}