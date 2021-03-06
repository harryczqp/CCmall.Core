﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Model.Models
{
    public class RouterData
    {
        public RouterData()
        {
            if (children == null)
            {
                children = new List<RouterData>();
            }
        }
        public int id { get; set; }
        public string path { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string component { get; set; } = string.Empty;
        public RouterDataMeta meta { get; set; } = new RouterDataMeta() { icon = string.Empty, title = string.Empty };
        public List<RouterData> children { get; set; }
    }
    public class RouterDataMeta
    {
        /// <summary>
        /// name
        /// </summary>
        public string title { get; set; }
        public string icon { get; set; }
    }
}
