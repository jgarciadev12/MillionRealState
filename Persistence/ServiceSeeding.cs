using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Context;

namespace Persistence
{
    public class ServiceSeeding
    {
        public async Task SeedAsync(IMongoDatabase context)
        {
            var ownersCollection = context.GetCollection<Owner>("Owners");
            var propertiesCollection = context.GetCollection<Property>("Properties");

            var ownerCount = await ownersCollection.CountDocumentsAsync(FilterDefinition<Owner>.Empty);
            if (ownerCount > 0)
                return;

            var now = DateTime.UtcNow;
            var random = new Random();

            var owners = Enumerable.Range(1, 10).Select(i => new Owner
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = $"Owner {i}",
                Address = $"Street {i}, City {i % 10}",
                Photo = $"https://example.com/photos/owner{i}.jpg",
                Birthday = now.AddYears(-30).AddDays(i)
            }).ToList();

            await ownersCollection.InsertManyAsync(owners);

            var properties = Enumerable.Range(1, 100).Select(i =>
            {
                var owner = owners[random.Next(owners.Count)];
                var propertyId = ObjectId.GenerateNewId().ToString();

                return new Property
                {
                    Id = propertyId,
                    Name = $"Property {i}",
                    Address = $"Property Address {i}, Zone {i % 50}",
                    Price = 100000 + i * 500,
                    CodeInternal = $"PROP{i:D5}",
                    Year = 2000 + (i % 25),
                    OwnerId = owner.Id,
                    Images = new List<PropertyImage>
                    {
                        new PropertyImage { File = GetRandomString(), Enabled = true },
                    },
                    Traces = new List<PropertyTrace>
                    {
                        new PropertyTrace
                        {
                            DateSale = now.AddDays(-i),
                            Name = $"Sale {i}-A",
                            Value = 100000 + i * 500,
                            Tax = (100000 + i * 500) * 0.1m
                        },
                        new PropertyTrace
                        {
                            DateSale = now.AddDays(-i - 30),
                            Name = $"Sale {i}-B",
                            Value = 90000 + i * 400,
                            Tax = (90000 + i * 400) * 0.1m
                        }
                    }
                };
            }).ToList();

            await propertiesCollection.InsertManyAsync(properties);
        }

        public string GetRandomString()
        {
            string[] options = { "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8Y2FzYXxlbnwwfHwwfHx8MA%3D%3D",
                "https://media.istockphoto.com/id/1447708518/es/foto/villa-moderna-exterior-en-verano.jpg?s=612x612&w=0&k=20&c=ilV-_pf5lyMf_TvciYXwqSweGXvYzXstQAqABd5bFuo=",
                "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8Y2FzYXxlbnwwfHwwfHx8MA%3D%3D",
                "https://images.adsttc.com/media/images/5a4d/a50c/f197/cc1d/4d00/0082/newsletter/Joa%CC%83o_Morgado.jpg?1515037958",
                "https://planner5d.com/blog/content/images/2024/05/diseno.de.casas.22.jpg" };
            Random random = new Random();
            int index = random.Next(options.Length);
            return options[index];
        }
    }
}
