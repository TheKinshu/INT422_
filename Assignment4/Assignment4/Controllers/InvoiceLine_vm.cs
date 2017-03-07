using Assignment4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InvoiceLineBase
    {
        [Key]
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        public int TrackId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

    }

    public class InvoiceLineWithDetail : InvoiceLineBase
    {
        public Invoice Invoice { get; set; }
        public String TrackName { get; set; }
        public String TrackComposer { get; set; }
        public String TrackAlbumArtistName { get; set; }
        public String TrackAlbumTitle { get; set; }
        public String TrackMediaTypeName { get; set; }
    }
}