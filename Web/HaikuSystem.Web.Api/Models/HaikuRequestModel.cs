namespace HaikuSystem.Web.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class HaikuRequestModel
    {
        [Required]
        public string Text { get; set; }
    }
}