using System.Collections.Generic;

namespace Vega.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public IList<int> Features { get; set; }

        public VehicleResource()
        {
            Features = new List<int>();
        }
    }
}
