using System.Collections.Generic;
using System.Linq;
using WebApi.Data.Converter;
using WebApi.Data.VO;
using WebApi.Model;

namespace WebApi.Data.Converters
{
    public class DirectorConverter : IParser<DirectorVO, Director>, IParser<Director, DirectorVO>
    {
        public Director Parse(DirectorVO origin)
        {
            if (origin == null) return new Director();
            return new Director
            {
                id = origin.Id,
                name = origin.Name
            };
        }

        public DirectorVO Parse(Director origin)
        {
            if (origin == null) return new DirectorVO();
            return new DirectorVO
            {
                Id = origin.id,
                Name = origin.name
            };
        }

        public List<Director> ParseList(List<DirectorVO> origin)
        {
            if (origin == null) return new List<Director>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DirectorVO> ParseList(List<Director> origin)
        {
            if (origin == null) return new List<DirectorVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
