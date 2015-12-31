namespace HaikuSystem.Web.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RatingRequestModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}