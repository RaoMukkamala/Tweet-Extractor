using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetDataExtractor.Json
{
   public class UserFollowersRootObject
    {

        public List<long> ids { get; set; }
        public long next_cursor { get; set; }
        public string next_cursor_str { get; set; }
        public int previous_cursor { get; set; }
        public string previous_cursor_str { get; set; }
    }
}
