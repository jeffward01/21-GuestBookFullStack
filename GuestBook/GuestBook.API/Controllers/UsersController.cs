using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GuestBook.API;
using System.Web.Mvc;
using System.Web;
using GuestBook.API.Models;

namespace GuestBook.API.Controllers
{
    public class UsersController : ApiController
    {
        private GuestBookEntities db = new GuestBookEntities();


        // GET: api/Users
        public IQueryable<UserModel> GetUsers()
        {


            return db.Users.Select(u => new UserModel
            {
                UserId = u.UserId,
                CreatedDate = u.CreatedDate,
                Username = u.Username,
                Password = u.Password,
                EmailAddress = u.EmailAddress,
                PhoneNumber = u.PhoneNumber,
                PostCount = u.Posts.Count
            });
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            UserModel modelUser = new UserModel
            {
                UserId = user.UserId,
                CreatedDate = user.CreatedDate,
                Username = user.Username,
                Password = user.Password,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber
            };



            return Ok(modelUser);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, UserModel user)
        {
            //If not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //If user is not found
            if (id != user.UserId)
            {
                return BadRequest();
            }

            //Update User In Database
            var dbUser = db.Users.Find(id);

            //Updates Entry State in DB
            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult PostUser(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Build new User
            var dbUser = new User();

            //Update User with new Model
            dbUser.Update(user);

            //Add user to Database
            db.Users.Add(dbUser);

            db.SaveChanges();

            user.UserId = dbUser.UserId;

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            //Locate All posts by user
            var posts = db.Posts.Where(p => p.UserId == user.UserId);

            //Remove all posts by user
            db.Posts.RemoveRange(posts);

            //Save Changes
            db.SaveChanges();

            //remove Customer
            db.Users.Remove(user);

            
            db.SaveChanges();

            //Return model to USer
            var userModel = new UserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                EmailAddress = user.EmailAddress,
                TwitterHandle = user.TwitterHandle,
                CreatedDate = user.CreatedDate,
                PhoneNumber = user.PhoneNumber

            };

            return Ok(userModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}