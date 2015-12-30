namespace HaikuSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rating
    {
        [Key, Column(Order = 0)]
        public int HaikuId { get; set; }

        public virtual Haiku Haiku { get; set; }

        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }
    }
}
