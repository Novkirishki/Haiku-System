namespace HaikuSystem.Web.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AbusementRequestModel
    {
        [Required]
        public string Text { get; set; }
    }
}