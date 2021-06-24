using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATeamFinalProject.Models
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string CoachName { get; set; }
        public string CoachNumber { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
