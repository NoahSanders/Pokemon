using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokemon_Informatica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient client = new RestClient("http://pokeapi.co/api/v1");

            string pokemonName = PokemonNameBox.Text;

            RestRequest request = new RestRequest("pokemon/{name}", Method.GET);
            request.AddUrlSegment("name", pokemonName);
            var response = client.Execute(request);

            if (response.Content == "")
            {
                ResultBox.AppendText(String.Format("Pokemon {0} not found{1}",pokemonName,Environment.NewLine));
            }

            dynamic pokemon = JsonConvert.DeserializeObject(response.Content);

            bool hasEvolution = (pokemon.evolutions.Count > 0) ? true : false;

            if (hasEvolution)
            {
                ResultBox.AppendText(String.Format("{0} has an evolution!{1}", pokemonName,Environment.NewLine));
                ResultBox.AppendText(String.Format("Method of evolution: {0}{1}", pokemon.evolutions[0].method, Environment.NewLine));
            }
            else
            {
                ResultBox.AppendText(String.Format("{0} has no evolution.{1}", pokemonName,Environment.NewLine));
            }

            ResultBox.AppendText(Environment.NewLine);
        }
    }
}
