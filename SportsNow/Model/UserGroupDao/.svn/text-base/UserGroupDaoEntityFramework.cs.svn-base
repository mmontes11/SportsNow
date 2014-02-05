using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupDao
{
    public class UserGroupDaoEntityFramework : GenericDaoEntityFramework<UserGroup, Int64>, IUserGroupDao
    {
        public List<UserGroup> FindAllUserGroups(int startIndex, int count)
        {
            ObjectSet<UserGroup> userGroups =
                Context.CreateObjectSet<UserGroup>();

            List<UserGroup> result =
                (from ug in userGroups
                 orderby ug.id
                 select ug).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int GetNumberOfGroups(long userProfileId)
        {
            ObjectSet<UserProfile> userProfiles =
                Context.CreateObjectSet<UserProfile>();

            var result =
                (from up in userProfiles
                 where up.id == userProfileId
                 select up.UserGroups).First().Count();

            return result;
        }

        public List<UserProfile> GetMembers(long userGroupId)
        {
            ObjectSet<UserGroup> userGroups =
                Context.CreateObjectSet<UserGroup>();

            var result =
                (from ug in userGroups
                 where ug.id == userGroupId
                 select ug.UserProfiles).First().ToList();

            return result;
        }

        public List<UserGroup> GetGroups(long userProfileId, int startIndex, int count)
        {
            ObjectSet<UserProfile> userProfiles =
                Context.CreateObjectSet<UserProfile>();

            var result =
                (from up in userProfiles
                 where up.id == userProfileId
                 select up.UserGroups).First().Skip(startIndex).Take(count).ToList();

            return result;
        }


        public int GetNumberOfAllGroups()
        {
            ObjectSet<UserGroup> userGroups =
                Context.CreateObjectSet<UserGroup>();

            int result =
                (from ug in userGroups
                 select ug).Count();

            return result;
        }

        public UserGroup FindGroupByName(string groupName)
        {
            ObjectSet<UserGroup> userGroups =
                Context.CreateObjectSet<UserGroup>();

            List<UserGroup> result =
                (from ug in userGroups
                 where ug.name.Equals(groupName)
                 select ug).ToList();

            if (result.Count != 1)
            {
                return null;
            }

            return result.First();
        }
    }
}
