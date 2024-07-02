using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MoviesLibrary.Models
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {

        public int Id { get; set; }

        public string Categoryname { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }  
}
