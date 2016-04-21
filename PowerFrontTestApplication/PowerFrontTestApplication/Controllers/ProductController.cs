using PowerFrontTestApplication.Interface;
using PowerFrontTestApplication.Models;
using System.Web.Mvc;

namespace PowerFrontTestApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IObjectOperation _objectOperation;

        public ProductController(IObjectOperation objectOperation)
        {
            _objectOperation = objectOperation;
        }

        // GET: api/Product/Get
        public ActionResult Get()
        {
            return View(new ObjectCompleteDescriptionViewModel
            { ListOfObjects = _objectOperation.GetAllObjectData() });
        }

        // GET: api/Product/Get
        public ActionResult GetSpecific(int objectTypeId, int objectPropertyId, int objectId)
        {
            return View(_objectOperation.GetSpecificObjectData(objectTypeId, objectPropertyId, objectId));
        }

        // GET: api/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: api/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ObjectCompleteDescription value)
        {
            if (ModelState.IsValid)
            {
                _objectOperation.AddNewObjectData(value);
                return RedirectToAction("Get");
            }
            return View(value);
        }

        // GET: api/Product/Edit
        public ActionResult Edit(int objectTypeId, int objectPropertyId, int objectId)
        {
            var existedData = _objectOperation.GetSpecificObjectData(objectTypeId, objectPropertyId, objectId);
            if (existedData == null)
            {
                return HttpNotFound();
            }
            return View(existedData);
        }

        // POST: api/Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ObjectCompleteDescription value)
        {
            if (ModelState.IsValid)
            {
                _objectOperation.UpdateExistingObjectData(value);
                return RedirectToAction("Get");
            }
            return View(value);
        }

        // GET: api/Product/Delete
        public ActionResult Delete(int objectTypeId, int objectPropertyId, int objectId)
        {
            var existedData = _objectOperation.GetSpecificObjectData(objectTypeId, objectPropertyId, objectId);
            if (existedData == null)
            {
                return HttpNotFound();
            }
            return View(existedData);
        }

        // POST: api/Product/Delete  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ObjectCompleteDescription value)
        {
            if (ModelState.IsValid)
            {
                _objectOperation.DeleteExistingObjectData(value);
                return RedirectToAction("Get");
            }
            return View(value);
        }
    }
}
