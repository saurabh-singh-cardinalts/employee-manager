using System;

namespace EM.Data.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string OtherId { get; set; }
        public string DLNumber { get; set; }
        public DateTime DLExpiryDate { get; set; }
        public string AdharCard { get; set; }
        public string PanCard { get; set; }
        public string BankAccount { get; set; }
        public string VoterId { get; set; }
    }
}