using System.ComponentModel.DataAnnotations;

namespace NetworkApi.Models
{
    public class Line
    { //domain model //
        /* primary key */
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string Details { get; set; }
    }
}
