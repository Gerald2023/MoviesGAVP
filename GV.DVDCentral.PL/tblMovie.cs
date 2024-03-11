using System;
using System.Collections.Generic;

namespace GV.DVDCentral.PL;

public partial class tblMovie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Cost { get; set; }

    public int RatingId { get; set; }

    public int FormatId { get; set; }

    public int DirectorId { get; set; }

    public int InStkQty { get; set; }

    public string ImagePath { get; set; } = null!;
}
