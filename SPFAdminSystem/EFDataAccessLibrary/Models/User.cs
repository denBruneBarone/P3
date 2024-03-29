﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models
{
    public class User
    {
        public int Id { get; set; }

        public DateTime RegisterDate { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public DateTime DeleteDate { get; set; }

        public string Role { get; set; }
    }
}
