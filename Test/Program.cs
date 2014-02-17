using Newtonsoft.Json;
using RestSharp;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient("http://pokeapi.co/api/v1");
            string pokemonName = "";

            while(true)
            {
                Console.Write("Enter Pokemon Name: ");
                pokemonName = Console.ReadLine();
                pokemonName = pokemonName.ToLower();

                if (pokemonName == "q" || pokemonName == "Q") { break; }

                RestRequest request = new RestRequest("pokemon/{name}",Method.GET);
                request.AddUrlSegment("name",pokemonName);
                var response = client.Execute(request);

                if (response.Content == "")
                {
                    Console.WriteLine("Pokemon {0} not found", pokemonName);
                    continue;
                }

                dynamic pokemon = JsonConvert.DeserializeObject(response.Content);

                bool hasEvolution = (pokemon.evolutions.Count > 0) ? true : false;

                if (hasEvolution)
                {
                    Console.WriteLine("{0} has an evolution!", pokemonName);
                    Console.WriteLine("Method of evolution: {0}",pokemon.evolutions[0].method);
                }
                else
                {
                    Console.WriteLine("{0} has no evolution.", pokemonName);
                }

                Console.WriteLine();
            }
        }
    }
}
