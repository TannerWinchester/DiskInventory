using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
            Disks = new HashSet<Disk>();
        }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual ICollection<Disk> Disks { get; set; }
    }
}
