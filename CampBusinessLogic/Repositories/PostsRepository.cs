using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;

namespace CampDataAccess.Repositories
{
    class PostsRepository
    {
        private AppContext db;

        public PostsRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Post> GetPostList()
        {
            return db.Posts;
        }

        public Post GetPost(int id)
        {
            return db.Posts.Find(id);
        }

        public void Create(Post post)
        {
            db.Posts.Add(post);
        }

        public void Update(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Post post = db.Posts.Find(id);
            if (post != null)
                db.Posts.Remove(post);
        }
    }
}