using System.Collections.Generic;

namespace PowerFrontTestApplication.Models
{
    public class ObjectCompleteDescriptionViewModel
    {
        public ObjectCompleteDescriptionViewModel()
        {
            ListOfObjects = new List<ObjectCompleteDescription>();
        }

        public List<ObjectCompleteDescription> ListOfObjects
        { set; get; }
    }
}
