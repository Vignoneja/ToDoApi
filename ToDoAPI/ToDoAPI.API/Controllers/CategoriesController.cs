using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoAPI.API.Models;
using ToDoAPI.DATA.EF;
using System.Web.Http.Cors;

namespace ToDoAPI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        ToDoEntities db = new ToDoEntities();

        public IHttpActionResult GetCategories()
        {
            List<CategoryViewModel> cats = db.Categories.Select(c => new CategoryViewModel()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.Name,
                CategoryDescription = c.Description
            }).ToList();
            if (cats.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(cats);
            }
        }//END GETCATEGORIES()

        public IHttpActionResult GetCategory(int id)
        {
            CategoryViewModel cat = db.Categories.Where(c => c.CategoryId == id).Select(c => new CategoryViewModel()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.Name,
                CategoryDescription = c.Description
            }).FirstOrDefault();
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cat);
            }
        }//END GETCATEGORY()

        public IHttpActionResult PostCategory(CategoryViewModel cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            db.Categories.Add(new Category()
            {
                Name = cat.CategoryName,
                Description = cat.CategoryDescription
            });
            db.SaveChanges();
            return Ok();
        }//END POSTCATEGORY()

        public IHttpActionResult PutCategory(CategoryViewModel cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            Category existingCategory = db.Categories.Where(c => c.CategoryId == cat.CategoryId).FirstOrDefault();
            if (existingCategory != null)
            {
                existingCategory.Name = cat.CategoryName;
                existingCategory.Description = cat.CategoryDescription;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//END PUTCATEGORY()

        public IHttpActionResult DeleteCategory(int id)
        {
            Category cat = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//END DELETECATEGORY

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }//END CLASS
}//END NAMESPACE
