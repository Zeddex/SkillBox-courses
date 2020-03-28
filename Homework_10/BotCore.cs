using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.Win32;

namespace Homework_10
{
    class BotCore
    {
        private TelegramBotClient botClient;
        private MainWindow window;
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        public BotCore(MainWindow W)
        {
            BotMessageLog = new ObservableCollection<MessageLog>();
            window = W;

            // Add HTTP proxy
            //var httpProxy = new WebProxy("8.8.8.8", 50000)                     // Address and port
            //{
            //    Credentials = new NetworkCredential("user", "password")        // Login and password (if needed)
            //};

            string token = "";     // Enter your telegram token
            botClient = new TelegramBotClient(token);
            //botClient = new TelegramBotClient(token, httpProxy);               // With proxy

            // Get bot info
            var botInfo = botClient.GetMeAsync().Result;
            Debug.WriteLine($"Bot info: id={botInfo.Id}, name '{botInfo.FirstName}'\n");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string botMessage = String.Empty;

            if (e.Message.Text == "/start")         // Check "/start" message
            {
                botMessage = $"Hi! I'm bot id'{botClient.BotId}'";
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, botMessage);
            }

            if (e.Message.Type == MessageType.Text && e.Message.Text != "/start")           // If any text
            {
                botMessage = "I got some text";
                Console.WriteLine($"Received new text message '{e.Message.Text}' in chat {e.Message.Chat.Id}");
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, botMessage);
            }

            if (e.Message.Type == MessageType.Document)                                     // If document
            {
                botMessage = "I got a document, saving file...";
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, botMessage);
                Console.WriteLine($"FileId: {e.Message.Document.FileId}");
                Console.WriteLine($"FileName: {e.Message.Document.FileName}");
                Console.WriteLine($"FileSize: {e.Message.Document.FileSize}");

                Download(e.Message.Document.FileId, e.Message.Document.FileName);
            }

            if (e.Message.Type == MessageType.Photo)            // If picture
            {
                botMessage = "I got a photo, saving file...";
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, botMessage);
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
                botMessage = "I got a sticker. Here is a sticker for you";
                botClient.SendTextMessageAsync(chatId: e.Message.Chat, botMessage);
                botClient.SendStickerAsync(chatId: e.Message.Chat, sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp");
            }

            var messageText = e.Message.Text;

            SendMessage(e.Message.Chat.FirstName, messageText, e.Message.Chat.Id);         // Show our message in form
            SendMessage("Bot", botMessage, 0);                                             // Show bot's message in form
        }

        /// <summary>
        /// Show our message to bot in form
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="text">Message</param>
        /// <param name="id">Chat id</param>
        void SendMessage (string name, string text, long id)
        {
            window.Dispatcher.Invoke(() =>
            {
                BotMessageLog.Add(
                    new MessageLog(
                        DateTime.Now.ToLongTimeString(), name, text, id));
            });
        }

        /// <summary>
        /// Download and save file
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="filePath">Filename</param>
        async void Download(string fileId, string filePath)
        {
            var file = await botClient.GetFileAsync(fileId);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await botClient.DownloadFileAsync(file.FilePath, fs);
            }
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        public void SaveFile()
        {
            // Serialization
            //string data = string.Empty;
            string json = JsonConvert.SerializeObject(BotMessageLog);

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "bot_data";                  // Default file name
            dlg.DefaultExt = ".json";                   // Default file extension
            dlg.Filter = "JSON file (.json)|*.json";    // Filter files by extension

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine(json);
                }
            }
            MessageBox.Show("Data successfully saved", "Save data", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ClearHistory()
        {
            BotMessageLog.Clear();
        }
    }
}
