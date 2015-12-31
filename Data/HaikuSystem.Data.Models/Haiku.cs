namespace HaikuSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Haiku
    {
        private ICollection<Rating> ratings;

        public Haiku()
        {
            this.Ratings = new HashSet<Rating>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public DateTime? DatePublished { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public ICollection<Rating> Ratings
        {
            get
            {
                return this.ratings;
            }

            set
            {
                this.ratings = value;
            }
        }
    }
}
