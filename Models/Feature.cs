using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("Features")]
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public IList<VehicleFeature> Vehicle { get; set; }

        public Feature()
        {
            Vehicle = new List<VehicleFeature>();
        }
    }
}
