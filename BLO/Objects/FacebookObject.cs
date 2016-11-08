﻿using System;

namespace BLO.Objects
{
    public class FacebookObject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string link { get; set; }
        public string locale { get; set; }
        public string timezone { get; set; }
        public DateTime updated_time { get; set; }
        public bool verified { get; set; }
    }
}
