using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Statistics
{
	public class Poll
	{
		private readonly string _projectId;
		private readonly FirestoreDb _fireStoreDb;

		public Poll()
		{
			string filepath = "key.json";
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
			_projectId = "csharp10-6f640";
			_fireStoreDb = FirestoreDb.Create(_projectId);
		}

		public async Task<IEnumerable<Result>> GetResultsAsync()
		{
			var query = _fireStoreDb.Collection("poll");
			var snapshot = await query.GetSnapshotAsync();
			return snapshot.Documents
						   .Where(x => x.Exists)
						   .Select(x =>
						   {
							   try
							   {
								   return new Vote
								   {
									   Question = x.TryGetValue<int>("question", out var i) ? i - 2 : -1,
									   Liked = x.TryGetValue<bool>("like", out var b) && b
								   };

							   }
							   catch
							   {
								   return new Vote
								   {
									   Question = int.Parse(x.TryGetValue<string>("question", out var i) ? i : "1") - 2,
									   Liked = x.TryGetValue<bool>("like", out var b) && b
								   };

								}
							})
                           .Where(x => x.Question >= 0)
						   .GroupBy(x => x.Question)
						   .Select(g => new Result
						   {
							   Question = g.Key,
							   Likes = g.Count(x => x.Liked),
							   Dislikes = g.Count(x => !x.Liked)
						   })
						   .ToList();
        }
    }
}
