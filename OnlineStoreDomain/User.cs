﻿namespace OnlineStore.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
