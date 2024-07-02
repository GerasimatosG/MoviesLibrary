using System.ComponentModel.DataAnnotations;

namespace MoviesLibrary.Models
{
    public partial class CategoryMetadata
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "The Category Name is required.")]
        public string Categoryname { get; set; } = null!;
    }

    public partial class PlatformMetadata
    {
        [Display(Name = "Platform Name")]
        [Required(ErrorMessage = "The Platform Name is required.")]
        public string Platformname { get; set; } = null!;
    }
    public partial class ActorMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Actors First Name is required.")]
        public string Firstname { get; set; } = null!;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Actors Last Name is required.")]
        public string Lastname { get; set; } = null!;

        [Display(Name = "Brith Date")]
        [Required(ErrorMessage = "The date of birth is required.")]
        public string Birthdate { get; set; } = null!;
    }

}

