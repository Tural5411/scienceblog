using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SonsuzBlog.Models;

namespace SonsuzBlog.ViewModel
{
    public class PostVm
    {
        public tbl_post  postsingle { get; set; }
        public List<tbl_post> posts { get; set; }
        public List<tbl_category> categories { get; set; }
        public tbl_category categorysingle { get; set; }
        public tbl_users usersingle { get; set; }
        public List<tbl_users> users { get; set; }
    }
}