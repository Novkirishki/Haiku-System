namespace HaikuSystem.Web.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserRequestModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string PublishCode { get; set; }
    }
}