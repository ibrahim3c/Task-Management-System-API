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

        public static ResultDTO<T> Failure(List<string>? messages, T? Data=default)
        {
            return new ResultDTO<T> { Success = false, Messages = messages, Data = Data };
        }

        public static ResultDTO<T> SuccessFully(List<string>? messages, T? Data)
        {
            return new ResultDTO<T> { Success = true, Messages = messages, Data = Data };
        }

    }
}
