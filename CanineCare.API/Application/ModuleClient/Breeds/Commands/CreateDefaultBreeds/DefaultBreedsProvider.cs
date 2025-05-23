using Domain.ModuleClient.Pets.Builders;
using Domain.ModuleClient.Pets.Models;

namespace Application.ModuleClient.Breeds.Commands.CreateDefaultBreeds
{
    public static class DefaultBreedsProvider
    {
        public static List<Breed> GetDefaultBreeds()
        {
            var names = new List<string>
            {
                "Labrador",
                "Pastor Alemán",
                "Bulldog",
                "Golden Retriever",
                "Chihuahua",
                "Poodle",
                "Beagle",
                "Rottweiler",
                "Dálmata",
                "Boxer"
            };

            return names
                .Select(name => new BreedBuilder()
                    .WithName(name)
                    .Build())
                .ToList();
        }
    }
}
