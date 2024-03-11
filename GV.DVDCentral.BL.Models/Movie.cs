using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.DVDCentral.BL.Models
{
    public class Movie
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double Cost { get; set; } 
        public int  RatingId { get; set; }

        public int FormatId { get; set; }

        public int DirectorId { get; set; }

        

        [DisplayName("Quantity")]
        
        public int InStkQty { get; set; }

        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [DisplayName("Director")]
        public string? DirectorFullName { get; set; }
        public int Quantity { get; set; }
        public string Rating { get; set; }
        public string Format { get; set; }


        public string GenreId { get; set; }


        [DisplayName("Genre Description")]
        public string GenreDescription { get; set; }

        [DisplayName("Rating Description")]
        public string RatingDescription { get; set; }
        

        [DisplayName("Format Description")]

        public string FormatDescription { get; set; }


        [DisplayName("Director Description")]
        public string DirectorDescription { get; set; }


        public List<Genre> Genres { get; set; } = new List<Genre>(); 
    }
}
