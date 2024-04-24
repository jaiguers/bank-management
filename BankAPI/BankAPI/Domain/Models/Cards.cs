﻿using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using MongoDbGenericRepository.Attributes;

namespace BankAPI.Domain.Models
{
    [CollectionName("cards")]
    public class Cards
    {
        public ObjectId Id { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string AccountType { get; set; }
        public string NameOnCard { get; set; }
        public string Provider { get; set; }
        public string CVV { get; set; }
        public string ExpirationDate { get; set; }
        public string Balance { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public List<Amount> amounts { get; set; }
    }
}
