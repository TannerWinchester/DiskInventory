﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class IndividualArtist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}
