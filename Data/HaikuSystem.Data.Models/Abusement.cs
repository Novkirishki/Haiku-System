namespace HaikuSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Abusement
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int HaikuId { get; set; }

        public Haiku Haiku { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
