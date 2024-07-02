using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MoviesLibrary.Models
{
    [ModelMetadataType(typeof(PlatformMetadata))]
    public partial class Platform
    {
        public int Id { get; set; }

        public string Platformname { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}