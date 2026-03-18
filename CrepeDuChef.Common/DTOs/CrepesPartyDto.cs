using System;
using System.Collections.Generic;
using System.Text;

namespace CrepeDuChef.Common.DTOs
{
    public class CrepesPartyDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SessionNumber { get; set; }

        public int UserId { get; set; }
        //public User User { get; set; } = new();
    }
}
