using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.DVDCentral.BL.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string? Description { get; set; }


        public List<Movie> Movies { get; set; } = new List<Movie>();


    }
}
