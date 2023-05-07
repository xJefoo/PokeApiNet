using System;
using PokeApiNet;

namespace PokemonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new PokeApiClient();
            var result = client.GetResourceAsync<Pokemon>("pikachu").Result;
            Console.WriteLine($"Pikachu's ID is {result.Id}.");
        }
    }
}
