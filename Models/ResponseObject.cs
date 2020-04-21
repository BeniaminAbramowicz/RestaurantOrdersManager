using System.Collections.Generic;

namespace ASPNETapp2.Models
{
    public class ResponseObject<T>
    {
        public T ResponseData { get; set; }
        public IEnumerable<T> ResponseList { get; set; }
        public string Message { get; set; }
    }
}