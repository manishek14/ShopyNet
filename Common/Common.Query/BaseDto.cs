using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Query
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
