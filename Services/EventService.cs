using net_core_bootcamp_b1.DTOs;
using net_core_bootcamp_b1.Models;
using System;
using System.Collections.Generic;

namespace net_core_bootcamp_b1.Services
{
    public interface IEventService 
    {
        string Add(EventAddDto model);
    }

    public class EventService : IEventService
    {
        private static readonly IList<Event> data = new List<Event>();

        public string Add(EventAddDto model)
        {
            Event entity = new Event
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            entity.Name = model.Name;
            entity.StartDate = model.StartDate;
            entity.FinishDate = model.FinishDate;
            entity.Address = model.Address;
            entity.IsFree = model.IsFree;
            entity.Price = model.Price;
            entity.Subject = model.Subject;
            entity.Desc = model.Desc;

            data.Add(entity);

            return "eklendi.";
        }
    }
}
