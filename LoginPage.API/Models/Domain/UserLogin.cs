﻿namespace LoginPage.API.Models.Domain
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
