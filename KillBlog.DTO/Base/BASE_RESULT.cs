using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillBlog.DTO.Base
{
    public class BASE_RESULT<T>
    {
        public BASE_RESULT() { }
        public BASE_RESULT(T result) { }

        public bool IS_SUCCESS { get; set; }
        public string? TRACKING_CODE { get; set; }
        public string? MESSAGE { get; set; }
        public T? RESULT { get; set; } = default;
    }
}
