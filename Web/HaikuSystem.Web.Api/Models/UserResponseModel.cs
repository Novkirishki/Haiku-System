namespace HaikuSystem.Web.Api.Models
{
    using HaikuSystem.Data.Models;
    using HaikuSystem.Web.Api.Infrastructure.Mappings;
    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;

    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public double Rating { get; set; }

        public List<HaikuResponseModel> Haikus { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserResponseModel>()
                .ForMember(u => u.Rating, opts => opts.MapFrom(u => u.Haikus.Count == 0 ? 0 : ((double)u.Haikus.Sum(h => h.Ratings.Count == 0 ? 0 : h.Ratings.Sum(r => r.Value)) / (u.Haikus.Sum(h => h.Ratings.Count) == 0 ? 1 : u.Haikus.Sum(h => h.Ratings.Count)))));
        }
    }
}