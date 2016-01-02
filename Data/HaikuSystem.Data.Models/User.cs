namespace HaikuSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Haiku> haikus;

        public User()
        {
            this.Haikus = new HashSet<Haiku>();
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
        public string PublishCode { get; set; }
        
        public bool IsAdmin { get; set; }

        public bool IsVIP { get; set; }

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
    }
}
