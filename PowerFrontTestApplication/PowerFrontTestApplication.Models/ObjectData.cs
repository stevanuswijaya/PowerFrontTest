using System.ComponentModel.DataAnnotations;

namespace PowerFrontTestApplication.Models
{
    public class ObjectData
    {
        [Required]
        public int ObjectTypeId { set; get; }

        [Required]
        public int ObjectPropertyId { set; get; }

        [Required]
        public int ObjectId { set; get; }

        [Required]
        public string ObjectName { set; get; }
    }
}
