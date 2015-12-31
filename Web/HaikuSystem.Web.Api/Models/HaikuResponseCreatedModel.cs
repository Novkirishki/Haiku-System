namespace HaikuSystem.Web.Api.Models
{
    using HaikuSystem.Data.Models;
    using HaikuSystem.Web.Api.Infrastructure.Mappings;
    using System;

    public class HaikuResponseCreatedModel : IMapFrom<Haiku>
    {
        public int Id { get; set; }

        public DateTime DatePublished { get; set; }
    }
}