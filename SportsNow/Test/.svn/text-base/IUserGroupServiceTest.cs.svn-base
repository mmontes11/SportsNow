using System;
using System.Data.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.UserGroupDao;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Es.Udc.DotNet.SportsNow.Model.UserService;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Es.Udc.DotNet.SportsNow.Test
{
    /// <summary>
    /// Descripción resumida de IUserGroupServiceTest
    /// </summary>
    [TestClass]
    public class IUserGroupServiceTest
    {
        private static IUnityContainer container;
        private static IUserGroupService userGroupService;
        private static IUserGroupDao userGroupDao;
        private static IUserProfileDao userProfileDao;
        private static IUserService userService;

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
            userGroupService = container.Resolve<IUserGroupService>();
            userGroupDao = container.Resolve<IUserGroupDao>();
            userProfileDao = container.Resolve<IUserProfileDao>();
            userService = container.Resolve<IUserService>();
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

        [TestMethod()]
        public void FindAllGroupsTest()
        {
            long userId = userService.RegisterUser(loginName + "1", clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            long userId2 =
                userService.RegisterUser(loginName + "2", clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));
            long userId3 =
                userService.RegisterUser(loginName + "3", clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            UserGroup ug1 = userGroupService.CreateUserGroup(userId, "grupo", "descripcion");

            UserGroup ug2 = userGroupService.CreateUserGroup(userId2, "grupo2", "descripcion");

            UserGroup ug3 = userGroupService.CreateUserGroup(userId3, "grupo3", "descripcion");

            List<UserGroup> userGroups = userGroupService.FindAllUserGroups(0, 60);

            Assert.IsTrue(userGroups.Contains(ug1));
            Assert.IsTrue(userGroups.Contains(ug2));
            Assert.IsTrue(userGroups.Contains(ug3));
        }


        [TestMethod()]
        public void GetMembersAndGroupsTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            userProfileDao.Create(u1);

            UserGroup ug = userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");

            UserProfile u2 = UserProfile.CreateUserProfile(-1, "es.tanislao", "12345", "Estanislao", "Florez", "estanis@gmail.com", "ES", "ES");
            userProfileDao.Create(u2);

            List<long> groupsList = new List<long>();
            groupsList.Add(ug.id);
            userGroupService.SubscribeUserProfile(u2.id, groupsList);

            List<UserProfile> users = userGroupService.GetMembers(ug.id);
            Assert.IsTrue(users.Contains(u1));
            Assert.IsTrue(users.Contains(u2));

            List<UserGroup> groups = userGroupService.GetGroups(u1.id, 0, 100);
            Assert.IsTrue(groups.Contains(ug));
            groups = userGroupService.GetGroups(u2.id, 0, 100);
            Assert.IsTrue(groups.Contains(ug));

        }


        [TestMethod()]
        public void SuscribeUnsuscribeUserGroupTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            userProfileDao.Create(u1);

            UserProfile u2 = UserProfile.CreateUserProfile(-1, "es.tanislao", "12345", "Estanislao", "Florez", "estanis@gmail.com", "ES", "ES");
            userProfileDao.Create(u2);

            UserGroup ug = userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");
            UserGroup ug2 = userGroupService.CreateUserGroup(u2.id, "grupo2", "descripcion2");

            List<long> groupsList = new List<long>();
            groupsList.Add(ug.id);
            List<long> groupsList2 = new List<long>();
            groupsList2.Add(ug2.id);

            Assert.AreEqual(1L, userGroupService.GetNumberOfGroups(u1.id));
            Assert.AreEqual(1L, userGroupService.GetNumberOfGroups(u2.id));
            Assert.AreEqual(1L, userGroupService.GetNumberOfMembers(ug.id));
            Assert.AreEqual(1L, userGroupService.GetNumberOfMembers(ug2.id));

            userGroupService.UnsubscribeUserProfile(u1.id, groupsList);
            userGroupService.UnsubscribeUserProfile(u2.id, groupsList2);

            Assert.AreEqual(0L, userGroupService.GetNumberOfMembers(ug.id));
            Assert.AreEqual(0L, userGroupService.GetNumberOfMembers(ug2.id));
        }


        [TestMethod()]
        [ExpectedException(typeof(SubscribeFromGroupException))]
        public void SubscribeExceptionTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            userProfileDao.Create(u1);

            UserGroup ug = userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");

            List<long> groupsList = new List<long>();
            groupsList.Add(ug.id);

            userGroupService.SubscribeUserProfile(u1.id, groupsList);
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void SubscribeDuplicatedListExceptionTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            userProfileDao.Create(u1);

            UserGroup ug1 = userGroupService.CreateUserGroup(u1.id, "grupo1", "descripcion1");
            UserGroup ug2 = userGroupService.CreateUserGroup(u1.id, "grupo2", "descripcion2");
            UserGroup ug3 = userGroupService.CreateUserGroup(u1.id, "grupo3", "descripcion3");

            List<long> groupsList = new List<long>();
            groupsList.Add(ug1.id);
            groupsList.Add(ug2.id);
            groupsList.Add(ug1.id);
            groupsList.Add(ug3.id);

            userGroupService.SubscribeUserProfile(u1.id, groupsList);
        }

        [TestMethod()]
        [ExpectedException(typeof(UnsubscribeFromGroupException))]
        public void UnsubscribeExceptionTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            userProfileDao.Create(u1);

            UserGroup ug = userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");

            UserProfile u2 = UserProfile.CreateUserProfile(-1, "o.video2", "12345", "Ovidio", "del Río", "ovidi2o@gmail.com", "GL", "GZ");
            userProfileDao.Create(u2);

            List<long> groupsList = new List<long>();
            groupsList.Add(ug.id);
            userGroupService.UnsubscribeUserProfile(u2.id, groupsList);
        }

    }
}
