﻿using System.ComponentModel.DataAnnotations;

namespace BaseAuthApp_DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
