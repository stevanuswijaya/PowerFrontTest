using System.ComponentModel.DataAnnotations;

namespace PowerFrontTestApplication.Models
{
    public class ObjectProperty
    {
        [Required]
        public int ObjectTypeId { set; get; }

        [Required]
        public int ObjectPropertyId { set; get; }

        [Required]
        public string ObjectPropertyName { set; get; }
    }
}
