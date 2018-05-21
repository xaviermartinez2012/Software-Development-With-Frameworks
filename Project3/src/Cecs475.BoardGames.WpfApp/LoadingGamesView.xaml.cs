using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.WpfApp
{
    /// <summary>
    /// Interaction logic for LoadingGamesView.xaml
    /// </summary>
    public partial class LoadingGamesView : Window
    {
        public LoadingGamesView()
        {
            InitializeComponent();
        }
        
        private List<(string, string, string)> Files { get; set; }
        private int FileCount { get; set; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Files = new List<(string, string, string)>();
            
            LoadLocalGames();
            RestClient client = new RestClient("https://cecs475-boardamges.herokuapp.com/api/games");
            RestRequest request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            var content = response.Content;
            JArray jArray = JArray.Parse(content);
            IEnumerable<JToken> jTokens = jArray.Select(t => t["Files"]);
            ComputeFileCount(jTokens);
            DownloadFiles(jTokens);
        }

        private void LoadLocalGames()
        {
            var files = Directory.EnumerateFiles(@"games", "*.dll");
            foreach (var file in files)
            {
                Assembly.LoadFrom(file);
            }
        }

        private void LoadDownloadedGames()
        {
            foreach (var file in Files)
            {
                string assembly = file.Item1.Replace(".dll", "");
                Assembly.Load($"{assembly}, Version={file.Item3}, Culture=neutral, PublicKeyToken={file.Item2}");
            }
        }

        private void DownloadFiles(IEnumerable<JToken> tokens)
        {
            foreach (var file in tokens.Children())
            {
                WebClient webClient = new WebClient();
                var fileName = (string)file["FileName"];
                var publicKey = (string)file["PublicKey"];
                var version = (string)file["Version"];
                var fileTuple = (fileName, publicKey, version);
                Files.Add(fileTuple);
                var url = new Uri((string)file["Url"]);
                
                webClient.DownloadFileAsync(url, @"games/" + fileName);
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            }
        }

        private void ComputeFileCount(IEnumerable<JToken> tokens)
        {
            foreach (var token in tokens)
            {
                FileCount += token.Children().Count();
            }
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            FileCount -= 1;
            if (FileCount == 0)
            {
                LoadDownloadedGames();
                var window = GetWindow(this);
                var window2 = new GameChoiceWindow();
                window2.Show();
                window.Close();
            }
        }
    }
}
