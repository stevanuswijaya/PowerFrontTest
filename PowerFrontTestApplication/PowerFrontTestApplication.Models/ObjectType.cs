using System.ComponentModel.DataAnnotations;

namespace PowerFrontTestApplication.Models
{
    public class ObjectType
    {
        [Required]
        public int ObjectTypeId { set; get; }

        [Required]
        public string TypeName { set; get; }
    }
}
