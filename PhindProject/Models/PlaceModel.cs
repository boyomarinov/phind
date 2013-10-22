using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace PhindProject.Models
{
    [DataContract]
    public class PlaceModel
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "Longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "PhotoCount")]
        public int? PhotoCount { get; set; }

        [DataMember(Name = "Timezone")]
        public string Timezone { get; set; }
    }
}
