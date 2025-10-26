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
                var connectionString = "mongodb+srv://ClusterGroup20:Groupmyror2025@cluster0.ovxrc4r.mongodb.net/?appName=Cluster0";
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("Myrforetag");

                // 2️ Skapa och lägg till myra (utan dubbletter)
                var nyMyra = new Myra
                {
                    Namn = "Myran Sara",
                    Skift = "Dag",
                    FodelseDatum = DateTime.Now.AddDays(-5),
                    SkapadTid = DateTime.Now
                };

                var collection = database.GetCollection<Myra>("Personal");
                var alreadyExists = collection.Find(m => m.Namn == nyMyra.Namn).FirstOrDefault();

                if (alreadyExists == null)
                {
                    collection.InsertOne(nyMyra);
                    Console.WriteLine($"Ny myra '{nyMyra.Namn}' har lagts till i databasen!");
                }
                else
                {
                    Console.WriteLine($"Myran '{nyMyra.Namn}' finns redan i databasen, hoppar över inläggning.");
                }

                // 4️ Läs ut alla dokument
                Console.WriteLine("\nAlla myror i databasen:");
                var resultat = collection.Find(_ => true).ToList();
                foreach (var doc in resultat)
                    Console.WriteLine(doc.ToJson());

                client.ListDatabaseNames();
                Console.WriteLine("\nAnslutningen till MongoDB fungerar!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid anslutning eller databasoperation: {ex.Message}");
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey();
        }
    }

    // 🐜 Klass för att representera en myra
    public class Myra
    {
        public ObjectId Id { get; set; }
        public string Namn { get; set; }
        public string Skift { get; set; }
        public DateTime FodelseDatum { get; set; }
        public DateTime SkapadTid { get; set; }
    }
}
