﻿namespace ServerAPI.Domain
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public List<Bid> Bids { get; } = new List<Bid>();
    }
}
