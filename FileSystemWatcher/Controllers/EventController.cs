using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using FileSystemWatcherDAL.Entities;
using FileSystemWatcherDAL.Repositories;
using System.Data.Entity.Core.Objects;

namespace FileSystemWatcher.Controllers
{
    public class EventController : ApiController
    {
        private readonly IRepository<Event> db = new Repository<Event>();

        // GET: api/Events
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return db.GetList();
        }

        //GET: api/Events/5
        [HttpGet]
        public IEnumerable<Event> GetEventForDate(DateTime id)
        {
            return db.GetWithFilter(x => EntityFunctions.TruncateTime(x.DateEvent) == EntityFunctions.TruncateTime(id.Date));
        }

        // POST: api/Events
        [HttpPost]
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Add(@event);
            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

    }
}