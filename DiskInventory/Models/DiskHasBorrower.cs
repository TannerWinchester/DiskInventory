using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasBorrower
    {
        public int DiskBorrower { get; set; }
        [Required(ErrorMessage = "Please select a borrower Name")]
        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Please select a borrowed Date")]
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        [Required(ErrorMessage = "Please select a Disk Name")]
        public int CdId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Disk Cd { get; set; }
    }
}
