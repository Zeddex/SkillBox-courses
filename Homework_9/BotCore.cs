using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Homework_9
{
    class BotCore
    {
        static TelegramBotClient botClient;

        /// <summary>
        /// Starting .NET client Telegram.Bot
        /// </summary>
        public static void BotInit()
        {
            // Add HTTP proxy
            var httpProxy = new WebProxy("8.8.8.8", 40000)                     // Address and port
            {
                Credentials = new NetworkCredential("user", "password")        // Login and password (if needed)
            };

            string token = "";        // Enter your telegram token
            botClient = new TelegramBotClient(token, httpProxy);

            // Get bot info
            var botInfo = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot info: id={botInfo.Id}, name '{botInfo.FirstName}'\n");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} from user: {e.Message.Chat.FirstName}, " +
                $"in chat: {e.Message.Chat.Id}, message: {e.Message.Text}, message type: {e.Message.Type.ToString()}");

            if (e.Message.Text == "/start")                       // Check /start
            {
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: $"Hi! I'm bot id'{botClient.BotId}'");
            }

            if (e.Message.Type == MessageType.Text && e.Message.Text != "/start")             // If text
            {
                Console.WriteLine($"Received new text message '{e.Message.Text}' in chat {e.Message.Chat.Id}");
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "I got some text");
            }
            
            if (e.Message.Type == MessageType.Document)         // If document
            {
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "I got a document, saving file...");
                Console.WriteLine($"FileId: {e.Message.Document.FileId}");
                Console.WriteLine($"FileName: {e.Message.Document.FileName}");
                Console.WriteLine($"FileSize: {e.Message.Document.FileSize}");

                Download(e.Message.Document.FileId, e.Message.Document.FileName);
            }

            if (e.Message.Type == MessageType.Photo)            // If picture
            {
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "I got a photo, saving file...");
                foreach (var pic in e.Message.Photo)
                {
                    Console.WriteLine($"File id: {pic.FileId}");
                    Console.WriteLine($"File size: {pic.FileSize}");
                    Console.WriteLine($"Width: {pic.Width}");
                    Console.WriteLine($"Height: {pic.Height}\n");
                }
                Download(e.Message.Photo[2].FileId, $"{e.Message.Photo[2].FileId}.jpg");        // There are 3 elements of array for each picture. Saving the last element
            }

            if (e.Message.Type == MessageType.Sticker)          // If sticker
            {
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "I got a sticker. Here is a sticker for you");
                botClient.SendStickerAsync(chatId: e.Message.Chat, sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp");
            }
        }

        /// <summary>
        /// Download and save file
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="filePath">Filename</param>
        static async void Download(string fileId, string filePath)
        {
            var file = await botClient.GetFileAsync(fileId);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await botClient.DownloadFileAsync(file.FilePath, fs);
            }
        }
    }
}
