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
    public class ToDoController : ApiController
    {
        ToDoEntities db = new ToDoEntities();

        public IHttpActionResult GetToDoItems()
        {
            List<ToDoViewModel> toDo = db.ToDoItems.Include("Category").Select(tdi => new ToDoViewModel()
            {
                ToDoId = tdi.ToDoId,
                Action = tdi.Action,
                Done = tdi.Done,
                CategoryId = tdi.CategoryId,
                Category = new CategoryViewModel()
                {
                    CategoryId = tdi.Category.CategoryId,
                    CategoryName = tdi.Category.Name,
                    CategoryDescription = tdi.Category.Description
                }
            }).ToList();
            if (toDo.Count == 0)
            {
                return NotFound();
            }
            return Ok(toDo);
        }//END GETTODOITEMS()

        public IHttpActionResult GetToDoItem(int id)
        {
            ToDoViewModel todo = db.ToDoItems.Include("Category").Where(tdi => tdi.ToDoId == id).Select(tdi => new ToDoViewModel()
            {
                ToDoId = tdi.ToDoId,
                Action = tdi.Action,
                Done = tdi.Done,
                CategoryId = tdi.CategoryId,
                Category = new CategoryViewModel()
                {
                    CategoryId = tdi.Category.CategoryId,
                    CategoryName = tdi.Category.Name,
                    CategoryDescription = tdi.Category.Description
                }
            }).FirstOrDefault();
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }//END GETTODOITEM()

        public IHttpActionResult PostToDoItem(ToDoViewModel toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            ToDoItem newToDoItem = new ToDoItem()
            {
                Action = toDo.Action,

                Done = toDo.Done,

                CategoryId = toDo.CategoryId

            };
            db.ToDoItems.Add(newToDoItem);

            db.SaveChanges();

            return Ok(newToDoItem);

        }//END POSTTODOITEM()

        public IHttpActionResult PutToDoItem(ToDoViewModel toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            ToDoItem existingToDoItem = db.ToDoItems.Where(td => td.ToDoId == toDo.ToDoId).FirstOrDefault();
            if (existingToDoItem != null)
            {
                existingToDoItem.ToDoId = toDo.ToDoId;
                existingToDoItem.Action = toDo.Action;
                existingToDoItem.Done = toDo.Done;
                existingToDoItem.CategoryId = toDo.CategoryId;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//END PUTTODOITEM()

        public IHttpActionResult DeleteToDoItem(int id)
        {
            ToDoItem toDo = db.ToDoItems.Where(td => td.ToDoId == id).FirstOrDefault();
            if (toDo != null)
            {
                db.ToDoItems.Remove(toDo);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//END DELETETODOITEM()

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }//END DISPOSE()
    }
}
