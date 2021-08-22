using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShirtNo { get; set; }
        public int Goals { get; set; }

    }
}
