using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class PagedResult<T> : PagedResultBase where T : class, IEntity,new()
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
