namespace PowerFrontTestApplication.Models
{
    public class ObjectCompleteDescription
    {
        public int ObjectId { set; get; }
        public int ObjectPropertyId { set; get; }
        public int ObjectTypeId { set; get; }

        public string ObjectName { set; get; }
        public string ObjectPropertyName { set; get; }
        public string TypeName { set; get; }
    }
}
