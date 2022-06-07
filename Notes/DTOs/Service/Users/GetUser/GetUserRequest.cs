﻿namespace Notes.DTOs.Service.Users.GetUser
{
    public class GetUserRequest
    {
        public GetUserRequest(int id)
        {
            Id = id;
        }

        public GetUserRequest() { }

        public int Id { get; set; }
    }
}
