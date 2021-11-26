using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Address
    {
        public Address(string title,string addresscontent,string floor,string type,string addressdirection,float lat,float lon) 
        {
            ID = new Guid().ToString();
            Title = title;
            AddressContent = addresscontent;
            Floor = floor;
            Type = type;
            AddressDirection = addressdirection;
            AddressLocation = new Coordinates2D(lon, lat);
            EnableStatus = true;
        }
        public string ID { get; private set; }
        public string Title { get; private set; }
        public string AddressContent { get; private set; }
        public string Floor { get; private set; }
        public string Type { get; private set; }
        public string AddressDirection { get; private set; }
        public Coordinates2D AddressLocation { get; private set; }
        public bool EnableStatus { get; private set; }
    }
}
