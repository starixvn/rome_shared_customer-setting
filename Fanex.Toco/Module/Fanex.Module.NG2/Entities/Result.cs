using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.Module.NG2.Entities
{
    public class Result
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess
        {
            get { return ErrorCode == 0; }
        }
    }
}
