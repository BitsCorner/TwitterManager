using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterRepository.Entity
{
    public class TwitterUserIDCursor
    {
        public Int64 previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }
        public Int64 next_cursor { get; set; }
        public string next_cursor_str { get; set; }
        public List<string> ids { get; set; }
    }
}
