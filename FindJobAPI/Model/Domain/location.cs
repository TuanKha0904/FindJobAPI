using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class location
    {
        [Key]
        public int location_id { get; set; }
        public string? location_name { get; set; }

        //navigation properties: one industry has many job
        public List<job>? job { get; set; }

    }
}
