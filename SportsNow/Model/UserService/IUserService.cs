using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Es.Udc.DotNet.SportsNow.Model.UserService.Exceptions;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Model.UserService
{
    public interface IUserService
    {
        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is
        /// encrypted].</param>
        /// <returns>LoginResult</returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>        
        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <returns>The user profile details</returns> 
        /// <exception cref="InstanceNotFoundException"/>  
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="oldClearPassword">The old clear password.</param>
        /// <param name="newClearPassword">The new clear password.</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

    }
}
