﻿namespace MyStore.Database.Models.User
{
    public class CreateUserResponse
    {
        public string Token { get; set; }
        public ApplicationUser User { get; set; }
    }
}
