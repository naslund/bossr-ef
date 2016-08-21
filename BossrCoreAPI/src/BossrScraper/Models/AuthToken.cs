using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrScraper.Models
{
    public class AuthToken
    {
        public string Token_Type { get; set; }
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
    }
}
