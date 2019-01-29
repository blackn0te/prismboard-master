﻿using System;
using Newtonsoft.Json;

namespace MvcForum.Core.VirusTotal.Objects
{
    public class DetectedUrl
    {
        public string Url { get; set; }

        public int Positives { get; set; }

        public int Total { get; set; }

        [JsonProperty("scan_date")]
        public DateTime ScanDate { get; set; }
    }
}