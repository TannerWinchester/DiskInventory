using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasArtist
    {
        public int DiskArtist { get; set; }
        public int ArtistId { get; set; }
        public int CdId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
