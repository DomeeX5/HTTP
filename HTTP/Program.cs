using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Channels;
using static System.Net.WebRequestMethods;

namespace HTTP
{
	public class Program
	{
		static HttpClient client = new HttpClient();
		public static void Main(string[] args)
		{
			OsszesAdatKiirasa();
			Console.WriteLine();
			List<Person> list = GetPeople();
			foreach (var item in list)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine();

			Console.WriteLine($"Házasok száma: {CountMarried(list)}");

			Console.WriteLine();
			List<string> creditScoreHigherThan700 = list.Where(person => person.Credit_Score > 700)
				.Select(person => person.Name).ToList();
			Console.WriteLine("Credit Score > 700: ");
			creditScoreHigherThan700.ForEach(name => Console.WriteLine($"\t{name}"));

			Console.WriteLine("\n");


			Console.WriteLine("Credit Score < 600 és házas: ");
			list.Where(person => person.Credit_Score < 600 && person.Married)
				.ToList().ForEach(name => Console.WriteLine($"\t{name}"));

			Console.WriteLine("\n");

			Console.WriteLine("Nem házas emberek sorrendben credit score alapján: ");
			list.Where(person => !person.Married).OrderBy(person => person.Credit_Score)
				.ToList()
				.ForEach(name => Console.WriteLine($"\t{name}"));

			Console.WriteLine("\n");

			Console.WriteLine($"Átlag credit score: {list.Average(person => person.Credit_Score):0.00}");

			Console.WriteLine("\n");

			Console.WriteLine(list.MaxBy(person => person.Credit_Score)!.Name);

			Console.WriteLine("\n");

			list.GroupBy(person => person.Married)
				.Select(group => (group.Key ? "Házas: " : "Nem házas: ") + group
				.Count())
				.ToList()
				.ForEach(Console.WriteLine);

			Console.WriteLine("\n");
		}

		public static int CountMarried(List<Person> people)
		{
			//return people.Where(person => person.Married).Count();
			return people.Count(person => person.Married);
		}

		public static void OsszesAdatKiirasa()
		{
			Task<string> adatok = OsszesAdatLekereseStringbe();
			Console.WriteLine(adatok.Result);
		}

		public static List<Person> GetPeople()
		{
			//client.GetFromJsonAsync<List<Person>>("https://retoolapi.dev/ZehjxA/people");
			string adatok = client.GetStringAsync("https://retoolapi.dev/ZehjxA/people").Result;
			List<Person> people = JsonConvert.DeserializeObject<List<Person>>(adatok)!;
			return people;
		}

		public static async Task<string> OsszesAdatLekereseStringbe()
		{
			string adatok = await client.GetStringAsync("https://retoolapi.dev/ZehjxA/people");
			return adatok;
		}
	}
}