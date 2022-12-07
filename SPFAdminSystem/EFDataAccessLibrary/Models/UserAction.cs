using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models
{
    public class UserAction
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string ActionType { get; set; }

        public string Value { get; set; }

        public int AffectedProductId { get; set; }

        public int AffectedUser { get; set; }
    }
}
