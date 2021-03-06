﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NetworkApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        //don't map the token
        [NotMapped]
        public string Token { get; set; }

    }
}
