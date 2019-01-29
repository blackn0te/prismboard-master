﻿using System.Collections.Generic;
using Newtonsoft.Json;
using MvcForum.Core.VirusTotal.Objects;
using MvcForum.Core.VirusTotal.ResponseCodes;

namespace MvcForum.Core.VirusTotal.Results
{
    public class IPReport
    {
        [JsonProperty("as_owner")]
        public string AsOwner { get; set; }

        public int ASN { get; set; }

        public string Country { get; set; }

        [JsonProperty("detected_communicating_samples")]
        public List<SampleWithDate> DetectedCommunicatingSamples { get; set; }

        [JsonProperty("detected_downloaded_samples")]
        public List<SampleWithDate> DetectedDownloadedSamples { get; set; }

        [JsonProperty("detected_referrer_samples")]
        public List<SampleWithDate> DetectedReferrerSamples { get; set; }

        [JsonProperty("detected_urls")]
        public List<DetectedUrl> DetectedUrls { get; set; }

        public List<IPResolution> Resolutions { get; set; }

        [JsonProperty("undetected_communicating_samples")]
        public List<SampleWithDate> UndetectedCommunicatingSamples { get; set; }

        [JsonProperty("undetected_downloaded_samples")]
        public List<SampleWithDate> UndetectedDownloadedSamples { get; set; }

        [JsonProperty("undetected_referrer_samples")]
        public List<Sample> UndetectedReferrerSamples { get; set; }

        [JsonProperty("undetected_urls")]
        public List<List<string>> UndetectedUrls { get; set; }

        /// <summary>
        /// The response code. Use this to determine the status of the report.
        /// </summary>
        [JsonProperty("response_code")]
        public IPReportResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Contains the message that corresponds to the response code.
        /// </summary>
        [JsonProperty("verbose_msg")]
        public string VerboseMsg { get; set; }
    }
}