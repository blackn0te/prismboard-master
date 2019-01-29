namespace MvcForum.Core.VirusTotal.ResponseCodes
{
    public enum DomainResponseCode
    {
        /// <summary>
        /// The item you searched for was not present in VirusTotal's dataset.
        /// </summary>
        NotPresent = 0,

        /// <summary>
        /// The item was present and it could be retrieved.
        /// </summary>
        Present = 1
    }
}