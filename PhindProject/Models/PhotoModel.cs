using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace PhindProject.Models
{
    [DataContract]
    public class PhotoModel
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "LargeUrl")]
        public string LargeUrl { get; set; }

        [DataMember(Name = "Small320Url")]
        public string Small320Url { get; set; }

        [DataMember(Name = "LargeSquareThumbnailUrl")]
        public string LargeSquareThumbnailUrl { get; set; }
    }
}
