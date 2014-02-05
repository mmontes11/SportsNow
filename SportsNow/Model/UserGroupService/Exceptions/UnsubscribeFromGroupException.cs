using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupService.Exceptions
{
    public class UnsubscribeFromGroupException : Exception
    {
        public String LoginName { get; private set; }
        public String UserGroupName { get; private set; }

        public UnsubscribeFromGroupException(String LoginName, String UserGroupName)
            : base(LoginName + " couldn't be unsubscribed from " + UserGroupName)
        {
            this.LoginName = LoginName;
            this.UserGroupName = UserGroupName;
        }
    }


}
