using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupService.Exceptions
{
    public class SubscribeFromGroupException : Exception
    {
        public String LoginName { get; private set; }
        public String UserGroupName { get; private set; }

        public SubscribeFromGroupException(String LoginName, String UserGroupName)
            : base(LoginName + " couldn't be subscribed from " + UserGroupName)
        {
            this.LoginName = LoginName;
            this.UserGroupName = UserGroupName;
        }
    }
}
