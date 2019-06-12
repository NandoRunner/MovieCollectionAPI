using System.Collections.Generic;
using System.Linq;
using WebApi.Data.Converter;
using WebApi.Data.VO;
using WebApi.Model;

namespace WebApi.Data.Converters
{
    public class ActorConverter : IParser<ActorVO, Actor>, IParser<Actor, ActorVO>
    {
        public Actor Parse(ActorVO origin)
        {
            if (origin == null) return new Actor();
            return new Actor
            {
                id = origin.Id,
                name = origin.Name
            };
        }

        public ActorVO Parse(Actor origin)
        {
            if (origin == null) return new ActorVO();
            return new ActorVO
            {
                Id = origin.id,
                Name = origin.name
            };
        }

        public List<Actor> ParseList(List<ActorVO> origin)
        {
            if (origin == null) return new List<Actor>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<ActorVO> ParseList(List<Actor> origin)
        {
            if (origin == null) return new List<ActorVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
