using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS
{
    public class ResultDTO<T>
    {
        public bool Success { get; set; }
        public List<string>? Messages { get; set; }
        public T? Data { get; set; }

    }
}
