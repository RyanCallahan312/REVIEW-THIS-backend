﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models
{
    public class ModificationsObject
    {
        public DateTime Time { get; set; }
        public string Headline { get; set; }
        public List<SectionsObject> Sections { get; set; }
        public float Rating { get; set; }
    }
}