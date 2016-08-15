﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser LastUpdatedBy { get; set; }
    }
}
