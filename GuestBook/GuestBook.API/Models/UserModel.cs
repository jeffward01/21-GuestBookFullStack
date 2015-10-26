using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.API.Models
{
    class UserModel
    {
        public int UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string AllPostsURL
        {
            get
            {
                return "api/posts/" + UserId + "/posts";
 
            }
        }


    }
}
