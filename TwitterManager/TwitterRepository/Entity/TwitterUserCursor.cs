using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterHelper.Entity
{
    public class TwitterUserCursor
    {
        public List<TwitterUser> users { get; set; }
        public Int64 previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }
        public Int64 next_cursor { get; set; }
    }
}
 
