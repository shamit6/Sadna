using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.DTOs
{
    public class LoginResponse
    {
        public string Role { get; set; }

        public string AuthorizationKey { get; set; }
    }
}