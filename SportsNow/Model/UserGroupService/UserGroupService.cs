
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.RecommendationDao;
using Es.Udc.DotNet.SportsNow.Model.UserGroupDao;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupService
{
    public class UserGroupService : IUserGroupService
    {
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        [Dependency]
        public IUserGroupDao UserGroupDao { set; private get; }
        [Dependency]
        public IUserProfileDao UserProfileDao { set; private get; }
        [Dependency]
        public IRecommendationDao RecommendationDao { set; private get; }


        public UserGroup CreateUserGroup(long userId, string name, string description)
        {
            UserGroup ug = UserGroup.CreateUserGroup(-1, name, description);
            UserGroup existingGroup = UserGroupDao.FindGroupByName(name);

            if (existingGroup == null)
            {
                UserGroupDao.Create(ug);
                List<long> groupIds = new List<long> { ug.id };
                SubscribeUserProfile(userId, groupIds);
                return ug;
            }
            return null;
        }

        public List<GroupDTO> GetAllGroupsInfo(int startIndex, int count)
        {
            List<UserGroup> userGroups = UserGroupDao.FindAllUserGroups(startIndex, count);
            List<GroupDTO> groupDtos = new List<GroupDTO>();

            foreach (UserGroup userGroup in userGroups)
            {
                groupDtos.Add(new GroupDTO(userGroup, RecommendationDao.GetNumberOfRecommendations(userGroup.id), GetNumberOfMembers(userGroup.id)));
            }

            return groupDtos;
        }

        public List<UserGroup> FindAllUserGroups(int startIndex, int count)
        {
            return UserGroupDao.FindAllUserGroups(startIndex, count);
        }

        public int GetNumberOfMembers(long userGroupId)
        {
            return UserGroupDao.GetMembers(userGroupId).Count;
        }

        public int GetNumberOfGroups(long userProfileId)
        {
            return UserGroupDao.GetNumberOfGroups(userProfileId);
        }

        /// <exception cref="SubscribeFromGroupException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        public void SubscribeUserProfile(long userProfileId, List<long> userGroupsId)
        {
            UserProfile up = UserProfileDao.Find(userProfileId);
            if (userGroupsId.Count() != userGroupsId.Distinct().Count()) 
                throw new DuplicateInstanceException(0, "UserGroup");            
            List<UserGroup> userGroups = new List<UserGroup>();
            foreach (long userGroupId in userGroupsId)
            {
                UserGroup ug = UserGroupDao.Find(userGroupId);
                if (ug.UserProfiles.Contains(up))
                {
                    throw new SubscribeFromGroupException(up.loginName, ug.name);
                }
                userGroups.Add(ug);
            }
            foreach (UserGroup ug in userGroups)
            {
                ug.UserProfiles.Add(up);
                UserGroupDao.Update(ug);
            }
        }

        /// <exception cref="UnsubscribeFromGroupException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        public void UnsubscribeUserProfile(long userProfileId, List<long> userGroupsId)
        {
            UserProfile up = UserProfileDao.Find(userProfileId);
            if (userGroupsId.Count() != userGroupsId.Distinct().Count())
                throw new DuplicateInstanceException(0, "UserGroup");
            List<UserGroup> userGroups = new List<UserGroup>();
            foreach (long userGroupId in userGroupsId)
            {
                UserGroup ug = UserGroupDao.Find(userGroupId);
                if (!ug.UserProfiles.Contains(up))
                {
                    throw new UnsubscribeFromGroupException(up.loginName, ug.name);
                }
                userGroups.Add(ug);
            }
            foreach (UserGroup ug in userGroups)
            {
                ug.UserProfiles.Remove(up);
                UserGroupDao.Update(ug);
            }
        }

        public List<UserProfile> GetMembers(long userGroupId)
        {
            return UserGroupDao.GetMembers(userGroupId);
        }

        public List<UserGroup> GetGroups(long userProfileId, int startIndex, int count)
        {
            return UserGroupDao.GetGroups(userProfileId, startIndex, count);
        }

        public int GetNumberOfAllGroups()
        {
            return UserGroupDao.GetNumberOfAllGroups();
        }
    }
}
