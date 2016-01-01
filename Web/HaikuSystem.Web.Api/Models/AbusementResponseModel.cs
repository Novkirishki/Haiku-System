namespace HaikuSystem.Web.Api.Models
{
    using System;
    using AutoMapper;
    using HaikuSystem.Data.Models;
    using HaikuSystem.Web.Api.Infrastructure.Mappings;

    public class AbusementResponseModel : IMapFrom<Abusement>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public int HaikuId { get; set; }
    }
}