using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Public
{
    public class ResponseTokenDTO
    {
        public String access_token { get; set; } = null!;
        public String? token_type { get; set; }
    }
}
