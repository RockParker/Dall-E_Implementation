using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dall_E_Implementation
{
    public class ChatBot
    {
        private OpenAIService _myAIService;

        private bool _SUCCESS = false;
        public bool SUCCESS { get { return _SUCCESS; } }

        public ChatBot()
        {
            InitAIService();
        }

        /// <summary>
        /// setsup the api connection for use later
        /// </summary>
        public void InitAIService()
        {
            string key = Properties.Settings.Default.API_KEY;
            if (key == null || key == "")
            {
                MessageBox.Show("There is no OpenAI Key to start a connection.\nGet a key from OpenAI and enter it into the application settings.");
                return;
            }
            _myAIService = new(new OpenAiOptions() { ApiKey = Properties.Settings.Default.API_KEY });
            _myAIService.SetDefaultEngineId("text-davinci-003");
            _SUCCESS = true;
        }

        /// <summary>
        /// awaits a response from the api with the given a prompt
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<String> getCompletionResult(string prompt)
        {

            var completionResult = await _myAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = prompt,
                N = 1,
                MaxTokens = 1000
            });


            if (completionResult.Successful)
            {
                return completionResult.Choices.FirstOrDefault().Text;
            }


            if (completionResult.Error == null)
            {
                throw new Exception("Unknown Error");
            }
            return "Failed Call\n" + completionResult.Error.ToString();

        }

        public async Task<string> getImageResult(string prompt)
        {
            var imageResult = await _myAIService.CreateImage(new ImageCreateRequest()
            {
                Prompt = prompt,
                N = 1,
                Size = "512x512",
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url
            });

            if (imageResult.Successful)
            {
                return imageResult.Results.Select(r => r.Url).FirstOrDefault();
            }


            MessageBox.Show(imageResult.Error.ToString());
            return "Failed Call";
        }
    }
}
