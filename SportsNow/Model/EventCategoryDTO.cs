using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public class EventCategoryDTO
    {
        public long EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public Category Category { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public EventCategoryDTO(SportEvent sportEvent, Category category)
        {
            EventId = sportEvent.id;
            EventName = sportEvent.name;
            EventDate = sportEvent.dateEvent;
            this.Category = category;
            CategoryId = category.id;
            CategoryName = category.name;
        }
    }
}
