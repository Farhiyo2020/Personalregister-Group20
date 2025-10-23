using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Personalregister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej, Personalregister-systemet startar...");

            try
            {
                // 1️⃣ Anslutning till MongoDB
                var connectionString = "mongodb+srv://ClusterGroup20:Groupmyror2025@cluster0.ovxrc4r.mongodb.net/?appName=Cluster0";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("Myrforetag");
                var collection = database.GetCollection<BsonDocument>("Personal");

                // 2️⃣ Skapa ett nytt dokument
                var dokument = new BsonDocument
                {
                    { "Namn", "Myran Sara" },
                    { "Skift", "Dag" },
                    { "FodelseDatum", DateTime.Now.AddDays(-5) },
                    { "SkapadTid", DateTime.Now }
                };

                // 3️⃣ Lägg till dokumentet
                collection.InsertOne(dokument);
                Console.WriteLine("✅ En ny myra har lagts till i databasen!");

                // 4️⃣ Läs ut alla dokument
                Console.WriteLine("\n📋 Alla myror i databasen:");
                var resultat = collection.Find(new BsonDocument()).ToList();
                foreach (var doc in resultat)
                    Console.WriteLine(doc.ToJson());

                // 5️⃣ Testa anslutning
                client.ListDatabaseNames();
                Console.WriteLine("\n✅ Anslutningen till MongoDB fungerar!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Fel vid anslutning eller databasoperation: {ex.Message}");
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey();
        }
    }
}



