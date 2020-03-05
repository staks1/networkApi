using System.ComponentModel.DataAnnotations;

namespace NetworkApi.Models
{
    public class NationalNetwork
    { //domain model //
        /* primary key */
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        public string Created { get; set; }

        public string Email { get; set; }
    }
}
