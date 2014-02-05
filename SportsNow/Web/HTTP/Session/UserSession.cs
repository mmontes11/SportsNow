using System;

namespace Es.Udc.DotNet.SportsNow.Web.HTTP.Session
{
    public class UserSession
    {

        private long userProfileId;
        private String firstName;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }
        
        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
    }
}


