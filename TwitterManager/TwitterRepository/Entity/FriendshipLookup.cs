using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterHelper.Entity
{
    public class FriendshipLookup
    {
      public string name { get; set; }
      public string id_str { get; set; }
      public Int64 id { get; set; }
      public List<string> connections { get; set; }
      public string screen_name { get; set; }
    }
}