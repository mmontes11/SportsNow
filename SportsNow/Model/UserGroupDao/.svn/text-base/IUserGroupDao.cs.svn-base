using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupDao
{
    public interface IUserGroupDao : IGenericDao<UserGroup, Int64>
    {      
        List<UserGroup> FindAllUserGroups(int startIndex, int count);

        List<UserProfile> GetMembers(long userGroupId);

        List<UserGroup> GetGroups(long userProfileId, int startIndex, int count);

        int GetNumberOfGroups(long userProfileId);

        int GetNumberOfAllGroups();

        UserGroup FindGroupByName(string groupName);
    }
}
