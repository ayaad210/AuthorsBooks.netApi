using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Models
{
    public class ApiResponse<T>
    {
        public int MesasageId { get; set; }
        public string Mesasage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
