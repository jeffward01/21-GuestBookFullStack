using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GuestBook.API;
using GuestBook.API.Models;

namespace GuestBook.API.Controllers
{
    public class PostsController : ApiController
    {
        private GuestBookEntities db = new GuestBookEntities();

        // GET: api/Posts
        public IQueryable<PostModel> GetPosts()
        {
          
            return db.Posts.Select(p => new PostModel
            {
            
                UserId =p.UserId,
                PostId = p.PostId,
                PostTitle = p.PostTitle,
                PostContent = p.PostContent,
                PostDate = p.PostDate
            });
        }

        // GET: api/Posts/5
        [ResponseType(typeof(PostModel)), Route("api/posts/{id}/posts")]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var posts = db.Posts.Where(p => p.UserId == id);

            if(posts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(posts.Select(p => new PostModel
            {
                UserId = p.UserId,
                PostId = p.PostId,
                PostTitle = p.PostTitle,
                PostContent= p.PostContent,
                PostDate = p.PostDate
            }));
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(int id, PostModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostId)
            {
                return BadRequest();
            }

            //Update Customer in the Database
            var dbPost = db.Posts.Find(id);

            //Update the Database
            dbPost.Update(post);

            //Modified State
            db.Entry(post).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Posts
        [ResponseType(typeof(PostModel))]
        public async Task<IHttpActionResult> PostPost(PostModel post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Build New Post
            var dbPost = new Post();

            //Update User with new Post model
            dbPost.Update(post);

            //Add Post Model to DB
            db.Posts.Add(dbPost);

            post.PostId = dbPost.PostId;

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [ResponseType(typeof(PostModel))]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            //Locate Post from Database 
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }


            db.Posts.Remove(post);
            await db.SaveChangesAsync();

            var postModel = new PostModel
            {
                UserId = post.UserId,
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostContent = post.PostContent,
                PostDate = post.PostDate
            };

            return Ok(postModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }
    }
}