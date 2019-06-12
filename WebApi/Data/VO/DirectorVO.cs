using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace WebApi.Data.VO
{
    public class DirectorVO : ISupportsHyperMedia
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
