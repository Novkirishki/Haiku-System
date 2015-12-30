namespace HaikuSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Rating> ratings;
        private ICollection<Haiku> haikus;

        public User()
        {
            this.Haikus = new HashSet<Haiku>();
            this.Ratings = new HashSet<Rating>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string Username { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string PublishCode { get; set; }

        public virtual ICollection<Haiku> Haikus
        {
            get
            {
                return this.haikus;
            }

            set
            {
                this.haikus = value;
            }
        }

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
