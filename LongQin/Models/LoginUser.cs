using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LongQin.Models
{
    public class LoginUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string LogoPath { get; set; }

        public string SystemName { get; set; }
    }
}