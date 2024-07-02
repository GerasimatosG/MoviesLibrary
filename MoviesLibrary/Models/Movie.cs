using System;
using System.Collections.Generic;

namespace MoviesLibrary.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly Premierdate { get; set; }

    public string? Imageurl { get; set; }

    public bool? Seenstatus { get; set; }

    public DateOnly? Seendate { get; set; }

    public int? Platformid { get; set; }

    public int? Rating { get; set; }

    public virtual Platform? Platform { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
