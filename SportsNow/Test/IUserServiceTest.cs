using System.Collections.Generic;
using Es.Udc.DotNet.SportsNow.Model.UserService;
using Es.Udc.DotNet.SportsNow.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using System;
using System.Transactions;
using Es.Udc.DotNet.SportsNow.Model.UserService.Util;

namespace Es.Udc.DotNet.SportsNow.Test
{

    /// <summary>
    ///This is a test class for IUserServiceTest and is intended
    ///to contain all IUserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IUserServiceTest
    {

        private static IUnityContainer container;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        TransactionScope transaction;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            container = TestManager.ConfigureUnityContainer("unity");
            userService = container.Resolve<IUserService>();
            userProfileDao = container.Resolve<IUserProfileDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            // Register user and find profile
            long userId =
                userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            UserProfile userProfile = userProfileDao.Find(userId);

            // Check data
            Assert.AreEqual(userId, userProfile.id);
            Assert.AreEqual(loginName, userProfile.loginName);
            Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
            Assert.AreEqual(firstName, userProfile.firstName);
            Assert.AreEqual(lastName, userProfile.lastName);
            Assert.AreEqual(email, userProfile.email);
            Assert.AreEqual(language, userProfile.language);
            Assert.AreEqual(country, userProfile.country);

        }

        /// <summary>
        ///A test for registering a user that already exists in the database
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            // Register user
            userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));
            // Register the same user
            userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod()]
        public void LoginClearPasswordTest()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            LoginResult expected = new LoginResult(userId, firstName,
                PasswordEncrypter.Crypt(clearPassword), language, country);

            // Login with clear password
            LoginResult actual =
                   userService.Login(loginName,
                   clearPassword, false);

            // Check data
            Assert.AreEqual(expected, actual);

        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod()]
        public void LoginEncryptedPasswordTest()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            LoginResult expected = new LoginResult(userId, firstName,
                PasswordEncrypter.Crypt(clearPassword), language, country);

            // Login with encrypted password
            LoginResult obtained =
                   userService.Login(loginName,
                   PasswordEncrypter.Crypt(clearPassword), true);

            // Check data
            Assert.AreEqual(expected, obtained);
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Login with incorrect (clear) password
            LoginResult actual =
                   userService.Login(loginName, clearPassword + "X", false);
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            LoginResult actual =
                   userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        ///A test for FindUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void FindUserProfileDetailsTest()
        {
            UserProfileDetails expected =
                new UserProfileDetails(firstName, lastName, email, language, country);

            long userId =
                userService.RegisterUser(loginName, clearPassword, expected);

            UserProfileDetails obtained =
                   userService.FindUserProfileDetails(userId);

            // Check data
            Assert.AreEqual(expected, obtained);
        }

        /// <summary>
        ///A test for FindUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void UpdateUserProfileDetailsTest()
        {
            // Register user and update profile details
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            UserProfileDetails expected =
                    new UserProfileDetails(firstName + "X", lastName + "X",
                        email + "X", "XX", "XX");

            userService.UpdateUserProfileDetails(userId, expected);

            UserProfileDetails obtained =
                userService.FindUserProfileDetails(userId);

            // Check changes
            Assert.AreEqual(expected, obtained);
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                new UserProfileDetails(firstName, lastName, email, language, country));
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Change password
            String newClearPassword = clearPassword + "X";
            userService.ChangePassword(userId, clearPassword, newClearPassword);

            // Try to login with the new password. If the login is correct, then
            // the password was successfully changed.
            userService.Login(loginName, newClearPassword, false);
        }

        /// <summary>
        ///A test for ChangePassword entering a wrong old password
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Change password
            String newClearPassword = clearPassword + "X";
            userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);
        }

        /// <summary>
        ///A test for ChangePassword when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }
    }
}
