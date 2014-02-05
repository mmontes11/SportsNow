using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.SportsNow.Model.UserGroupDao;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Model.UserGroupService
{
    public interface IUserGroupService
    {
        [Dependency]
        IUserGroupDao UserGroupDao { set; }

        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        [Transactional]
        UserGroup CreateUserGroup(long userId, string name, string description);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>Todos los grupos existentes.</returns>
        [Transactional]
        List<UserGroup> FindAllUserGroups(int startIndex, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>La información de todos los grupos existentes.</returns>
        [Transactional]
        List<GroupDTO> GetAllGroupsInfo(int startIndex, int count);

        [Transactional]
        int GetNumberOfMembers(long userGroupId);

        [Transactional]
        int GetNumberOfGroups(long userProfileId);

        /// <summary>
        /// Añade un usuario a un grupo existente.
        /// </summary>
        /// <param name="userProfileId">Id del usuario a añadir.</param>
        /// <param name="userGroupsId">Ids de los grupos a los que se quiere añadir.</param>
        /// <exception cref="SubscribeFromGroupException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        void SubscribeUserProfile(long userProfileId, List<long> userGroupsId);

        /// <summary>
        /// Elimina un usuario de un grupo.
        /// </summary>
        /// <param name="userProfileId">Id del usuario a eliminar.</param>
        /// <param name="userGroupsId">Ids de los grupos a los que pertenece.</param>
        /// <exception cref="UnsubscribeFromGroupException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        void UnsubscribeUserProfile(long userProfileId, List<long> userGroupsId);

        [Transactional]
        List<UserProfile> GetMembers(long userGroupId);

        [Transactional]
        List<UserGroup> GetGroups(long userProfileId, int startIndex, int count);

        [Transactional]
        int GetNumberOfAllGroups();

    }
}
