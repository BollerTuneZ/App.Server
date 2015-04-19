using System;
using BTZ.Data;
using System.Collections.Generic;

namespace BTZ.Infrastructure
{
	/// <summary>
	/// Repository für das Newsfeed, um WallPosts zu beziehen
	/// Jonas Ahlf 19.04.2015 23:46:31
	/// </summary>
	public interface IWallPostRepository
	{
		/// <summary>
		/// Adds the wall post.
		/// </summary>
		/// <param name="post">Post.</param>
		void AddWallPost(WallPost post);

		/// <summary>
		/// Gets all wall posts.
		/// </summary>
		/// <returns>The all wall posts.</returns>
		List<WallPost> GetAllWallPosts();

		/// <summary>
		/// Deletes the wall post.
		/// </summary>
		/// <param name="post">Post.</param>
		void DeleteWallPost(WallPost post);
	}
}

