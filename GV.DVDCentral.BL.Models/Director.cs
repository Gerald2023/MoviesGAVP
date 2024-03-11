using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.DVDCentral.BL.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }   

        public string? DirectorFullName
        {
            get
            {
                return $"{FirstName} {LastName}"; 
            }
        }

    }
}
