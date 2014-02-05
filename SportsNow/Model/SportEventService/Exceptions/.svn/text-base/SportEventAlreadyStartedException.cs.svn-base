using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.SportEventService.Exceptions
{
    public class SportEventAlreadyStartedException : Exception
    {
        public long SportEventId { get; private set; }

        public SportEventAlreadyStartedException(long SportEventId)
            : base(SportEventId + " has already started, so you can't add a comment")
        {
            this.SportEventId = SportEventId;
        }
    
    }
}
