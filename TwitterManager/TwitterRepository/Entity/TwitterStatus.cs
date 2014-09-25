using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterHelper.Entity
{
    public class TwitterStatus
    {
        public bool favorited { get; set; }
        public bool truncated { get; set; }

        //public string coordinates { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string created_at { get; set; }
        public string contributors { get; set; }
        public Int64 id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public Int64 retweet_count { get; set; }
        public string in_reply_to_status_id_str { get; set; }

        //public string geo { get; set; }
        public bool retweeted { get; set; }
        public Int64? in_reply_to_user_id { get; set; }
        //public string place { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public Int64? in_reply_to_status_id { get; set; }
        public TwitterStatus retweeted_status { get; set; }

    }
}