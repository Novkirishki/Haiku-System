namespace HaikuSystem.Web.Api.Models
{
    using System;
    using AutoMapper;
    using Data.Models;
    using HaikuSystem.Web.Api.Infrastructure.Mappings;
    using System.Linq;

    public class HaikuResponseModel : IMapFrom<Haiku>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public double? Rating { get; set; }

        public string AuthorName { get; set; }

        public DateTime? DatePublished { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Haiku, HaikuResponseModel>()
                .ForMember(h => h.Rating, opts => opts.MapFrom(h => h.Ratings.Count == 0 ? 0 : ((double)h.Ratings.Sum(r => r.Value) / h.Ratings.Count)))
                .ForMember(h => h.AuthorName, opts => opts.MapFrom(h => h.User.Username));
        }
    }
}