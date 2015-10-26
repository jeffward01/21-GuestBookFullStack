using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.API.Models
{
    public class PostModel
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public System.DateTime PostDate { get; set; }
       // public int PostCount { get; set; }
        

        public string UserURL
        {
            get
            {
                return "api/Users/"+UserId;
            }
        }
        /*
        public void CountPosts()
        {
            var db = new GuestBookEntities();
            var users = db.Users.Select(u => new PostModel
            {
                PostCount = u.Posts.Count()
            });
        }
        */
    }
}
