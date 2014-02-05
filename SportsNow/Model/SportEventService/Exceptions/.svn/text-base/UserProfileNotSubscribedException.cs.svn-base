using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.SportEventService.Exceptions
{
    public class UserProfileNotSubscribedException : Exception
    {
        public long UserProfileId { get; private set; }

        public long UserGroupId { get; private set; }

        public UserProfileNotSubscribedException(long UserProfileId, long UserGroupId)
            : base("User " + UserProfileId + " is not subscribed to group " + UserGroupId + ".")
        {
            this.UserProfileId = UserProfileId;
            this.UserGroupId = UserGroupId;
        }
    }
}
