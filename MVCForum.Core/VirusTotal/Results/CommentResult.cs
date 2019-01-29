using System.Collections.Generic;
using Newtonsoft.Json;
using MvcForum.Core.VirusTotal.Objects;

namespace MvcForum.Core.VirusTotal.Results
{
    public class CommentResult
    {
        /// <summary>
        /// A list of comments on the resource
        /// </summary>
        public List<UserComment> Comments { get; set; }

        /// <summary>
        /// Contains the message that corresponds to the response code.
        /// </summary>
        [JsonProperty("verbose_msg")]
        public string VerboseMsg { get; set; }
    }
}