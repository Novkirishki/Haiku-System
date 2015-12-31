namespace HaikuSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rating
    {
        public int Id { get; set; }

        public int HaikuId { get; set; }

        public virtual Haiku Haiku { get; set; }

        [Range(1, 5)]
        [Required]
        public int Value { get; set; }
    }
}
