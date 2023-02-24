﻿using System.ComponentModel.DataAnnotations;

namespace IntroIdentityJWT.Models
{
    public class UserDetails
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
