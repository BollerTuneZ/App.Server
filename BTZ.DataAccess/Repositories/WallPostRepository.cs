using System;
using BTZ.Data;
using System.Collections.Generic;
using BTZ.Infrastructure;
using ServiceStack.OrmLite;
using log4net;
namespace BTZ.DataAccess
{
	/// <summary>
	/// Jonas Ahlf 19.04.2015 23:48:12
	/// </summary>
	public class WallPostRepository : IWallPostRepository
	{
		readonly DatabaseHandler handler;
		readonly static ILog s_log = LogManager.GetLogger (typeof(WallPostRepository));
		private List<WallPost> _wallPosts;
		public WallPostRepository (DatabaseHandler handler)
		{
			this.handler = handler;
			RefreshWallPosts ();
		}

		#region IWallPostRepository implementation
		public void AddWallPost (WallPost post)
		{
			handler.Database.Insert<WallPost> (post);
			RefreshWallPosts ();
			s_log.Info (String.Format ("Added WallPost id={0}", post.Id));
		}
		public List<WallPost> GetAllWallPosts ()
		{
			return _wallPosts;
		}
		public void DeleteWallPost (WallPost post)
		{
			handler.Database.Delete<WallPost> (post);
			RefreshWallPosts ();
			s_log.Info (String.Format ("Deleted WallPost id={0}", post.Id));
		}
		#endregion

		void RefreshWallPosts()
		{
			_wallPosts = handler.Database.LoadSelect<WallPost> ();
		}
	
	}
}

