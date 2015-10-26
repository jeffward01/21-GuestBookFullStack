using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.API.Models
{
    class PostModel
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public System.DateTime PostDate { get; set; }

        public string UserURL
        {
            get
            {
                return "api/Users/"+UserId;
            }
        }
    }
}
