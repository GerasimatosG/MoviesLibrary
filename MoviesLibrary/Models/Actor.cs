using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MoviesLibrary.Models;
[ModelMetadataType(typeof(ActorMetadata))]
public partial class Actor
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;
   
    public Gender Gender { get; set; }
    
    public DateTime? Birthdate { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
