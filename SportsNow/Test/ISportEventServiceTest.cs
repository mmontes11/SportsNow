using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.CategoryDao;
using Es.Udc.DotNet.SportsNow.Model.SportEventDao;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Model.SportEventService.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Es.Udc.DotNet.SportsNow.Test
{
    /// <summary>
    /// Descripción resumida de ISportEventServiceTest
    /// </summary>
    [TestClass]
    public class SportEventServiceTest
    {

        private static IUnityContainer _container;
        private static ISportEventDao _sportEventDao;
        private static ISportEventService _sportEventService;
        private static IUserProfileDao _userProfileDao;
        private static IUserGroupService _userGroupService;
        public static ICategoryDao CategoryDao;

        TransactionScope _transaction;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            _container = TestManager.ConfigureUnityContainer("unity");
            _sportEventService = _container.Resolve<ISportEventService>();
            _sportEventDao = _container.Resolve<ISportEventDao>();
            _userProfileDao = _container.Resolve<IUserProfileDao>();
            _userGroupService = _container.Resolve<IUserGroupService>();
            CategoryDao = _container.Resolve<ICategoryDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(_container);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            _transaction = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            _transaction.Dispose();
        }

        #endregion


        [TestMethod]
        public void CreateGetCommentTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            List<long> tags = new List<long>();
            _sportEventService.CreateComment("nice", se.id, u1.id, tags);
            _sportEventService.CreateComment("ugly", se.id, u1.id, tags);
            _sportEventService.CreateComment("amazing", se.id, u1.id, tags);

            List<Comment> comments = _sportEventService.GetEventComments(se.id);
            Assert.AreEqual(comments.Count, 3);
        }

        [TestMethod]
        public void GetNumberOfTaggedCommentsTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            List<string> tagsName = new List<string>();
            tagsName.Add("tag");
            List<Tag> tags = _sportEventService.CreateTag(tagsName);
            List<long> tagsId = new List<long>();
            tagsId.Add(tags[0].id);
            _sportEventService.CreateComment("nice", se.id, u1.id, tagsId);
            _sportEventService.CreateComment("nice", se.id, u1.id, tagsId);
            _sportEventService.CreateComment("nice", se.id, u1.id, tagsId);

            Assert.AreEqual(_sportEventService.GetNumberOfTaggedComments(tags[0].id), 3L);
        }

        [TestMethod]
        public void FindAllCategoriesTest()
        {
            Category c1 = Category.CreateCategory(-1, "c1");
            CategoryDao.Create(c1);
            Category c2 = Category.CreateCategory(-1, "c2");
            CategoryDao.Create(c2);
            Category c3 = Category.CreateCategory(-1, "c3");
            CategoryDao.Create(c3);
            Category c4 = Category.CreateCategory(-1, "c4");
            CategoryDao.Create(c4);

            List<Category> categories = CategoryDao.FindAllCategories(0, 10);
            Assert.IsTrue(categories.Contains(c1));
            Assert.IsTrue(categories.Contains(c2));
            Assert.IsTrue(categories.Contains(c3));
            Assert.IsTrue(categories.Contains(c4));

            categories = CategoryDao.FindAllCategories(0, 2);
            Assert.IsTrue(!categories.Contains(c1));
            Assert.IsTrue(!categories.Contains(c2));
            Assert.IsTrue(!categories.Contains(c3));
            Assert.IsTrue(!categories.Contains(c4));

            //4 categorias creadas y 3 en el script del SQL
            Assert.AreEqual(7, CategoryDao.GetNumCategories());
        }

        [TestMethod]
        public void CreateDeleteTagCommentTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            List<string> tagsName = new List<string>();
            tagsName.Add("tag");
            tagsName.Add("tag2");
            tagsName.Add("tag3");
            tagsName.Add("tag4");
            tagsName.Add("tag5");
            List<Tag> createdTags = _sportEventService.CreateTag(tagsName);

            //Crear un comentario al mismo tiempo que se etiqueta
            List<long> tagsId = new List<long>();
            tagsId.Add(createdTags[0].id);
            tagsId.Add(createdTags[1].id);
            tagsId.Add(createdTags[2].id);
            Comment c = _sportEventService.CreateComment("nice", se.id, u1.id, tagsId);

            List<Tag> tags = _sportEventService.GetTags(c.id);
            Assert.IsTrue(tags.Contains(createdTags[0]));
            Assert.IsTrue(tags.Contains(createdTags[1]));
            Assert.IsTrue(tags.Contains(createdTags[2]));

            //Eliminamos una etiqueta de un comentario
            List<long> tagsToDelete = new List<long>();
            tagsToDelete.Add(createdTags[1].id);
            _sportEventService.DeleteTag(tagsToDelete, c.id);
            tags = _sportEventService.GetTags(c.id);
            Assert.IsTrue(tags.Contains(createdTags[0]));
            Assert.IsFalse(tags.Contains(createdTags[1]));
            Assert.IsTrue(tags.Contains(createdTags[2]));

            //Modificamos las etiquetas de un comentario
            tagsId.Remove(createdTags[1].id);
            tagsId.Remove(createdTags[2].id);
            tagsId.Add(createdTags[3].id);
            tagsId.Add(createdTags[4].id);
            _sportEventService.TagComment(tagsId, c.id);
            tags = _sportEventService.GetTags(c.id);
            Assert.IsTrue(tags.Contains(createdTags[0]));
            Assert.IsTrue(tags.Contains(createdTags[3]));
            Assert.IsTrue(tags.Contains(createdTags[4]));
        }

        [TestMethod]
        public void CreateFindRecommendations()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            UserProfile u2 = UserProfile.CreateUserProfile(-1, "es.tanislao", "12345", "Estanislao", "Florez", "estanis@gmail.com", "ES", "ES");
            _userProfileDao.Create(u2);

            UserGroup ug1 = _userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");
            UserGroup ug2 = _userGroupService.CreateUserGroup(u1.id, "grupo2", "descripcion2");
            UserGroup ug3 = _userGroupService.CreateUserGroup(u1.id, "grupo3", "descripcion3");
            UserGroup ug4 = _userGroupService.CreateUserGroup(u1.id, "grupo4", "descripcion3");

            List<long> groupsList = new List<long>();
            groupsList.Add(ug1.id);
            groupsList.Add(ug2.id);
            groupsList.Add(ug3.id);
            groupsList.Add(ug4.id);

            List<long> recommendationGroups1 = new List<long>();
            recommendationGroups1.Add(ug2.id);
            recommendationGroups1.Add(ug4.id);
            _userGroupService.SubscribeUserProfile(u2.id, recommendationGroups1);

            List<long> recommendationGroups2 = new List<long>();
            recommendationGroups2.Add(ug1.id);
            recommendationGroups2.Add(ug3.id);
            _userGroupService.SubscribeUserProfile(u2.id, recommendationGroups2);

            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            _sportEventService.CreateRecommendation("reason", recommendationGroups1, u2.id, se.id);

            List<EventRecommendationDto> recommendations = _sportEventService.FindRecommendationsByUserProfile(u1.id, 0, 100);
            Assert.AreEqual(recommendations.Count, 2);
            Assert.AreEqual(recommendations[0].UserName, u2.loginName);
            Assert.AreEqual(recommendations[0].EventId, se.id);

            _sportEventService.CreateRecommendation("reason2", recommendationGroups2, u2.id, se.id);
            recommendationGroups2.Remove(1);

            recommendations = _sportEventService.FindRecommendationsByUserProfile(u1.id, 0, 100);
            Assert.AreEqual(recommendations.Count, 4);
            Assert.AreEqual(recommendations[1].UserName, u2.loginName);
            Assert.AreEqual(recommendations[1].EventId, se.id);
        }

        [TestMethod]
        [ExpectedException(typeof(UserProfileNotSubscribedException))]
        public void RecommendToAnotherGroupTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);

            UserGroup ug1 = _userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");
            UserGroup ug2 = _userGroupService.CreateUserGroup(u1.id, "grupo2", "descripcion2");

            List<long> recommendationGroups1 = new List<long>();
            recommendationGroups1.Add(ug1.id);
            recommendationGroups1.Add(ug2.id);

            _userGroupService.UnsubscribeUserProfile(u1.id, recommendationGroups1);

            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            _sportEventService.CreateRecommendation("reason", recommendationGroups1, u1.id, se.id);
        }

        [TestMethod]
        public void GetNumberOfRecommendations()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "hortensio", "12345", "hortensio", "Perez", "hort@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            UserProfile u2 = UserProfile.CreateUserProfile(-1, "eustaquio", "12345", "eustaquio", "Lopez", "eustaqui@gmail.com", "ES", "ES");
            _userProfileDao.Create(u2);

            UserGroup ug1 = _userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");
            UserGroup ug2 = _userGroupService.CreateUserGroup(u1.id, "grupo2", "descripcion2");
            UserGroup ug3 = _userGroupService.CreateUserGroup(u1.id, "grupo3", "descripcion3");
            UserGroup ug4 = _userGroupService.CreateUserGroup(u1.id, "grupo4", "descripcion3");

            List<long> recommendationGroups1 = new List<long>();
            recommendationGroups1.Add(ug2.id);
            recommendationGroups1.Add(ug4.id);
            _userGroupService.SubscribeUserProfile(u2.id, recommendationGroups1);

            List<long> recommendationGroups2 = new List<long>();
            recommendationGroups2.Add(ug1.id);
            recommendationGroups2.Add(ug2.id);
            recommendationGroups2.Add(ug3.id);

            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            _sportEventService.CreateRecommendation("reason", recommendationGroups1, u2.id, se.id);
            _sportEventService.CreateRecommendation("reason", recommendationGroups2, u1.id, se.id);

            Assert.AreEqual(5, _sportEventService.NumberOfRecommendationsByUserProfile(u1.id));
        }

        [TestMethod]
        public void FindAllTagsTest()
        {
            List<string> tagsName = new List<string>();
            tagsName.Add("tag1");
            tagsName.Add("tag2");
            tagsName.Add("tag3");
            List<Tag> createdTags = _sportEventService.CreateTag(tagsName);

            List<Tag> tags = _sportEventService.FindAllTags();

            Assert.IsTrue(tags.Contains(createdTags[0]));
            Assert.IsTrue(tags.Contains(createdTags[1]));
            Assert.IsTrue(tags.Contains(createdTags[2]));
        }

        [TestMethod]
        public void FindPopularTagsTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);
            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            List<string> tagsName = new List<string> { "tag1", "tag2", "tag3" };
            List<Tag> createdTags = _sportEventService.CreateTag(tagsName);
            List<long> tagsId = new List<long> { createdTags[0].id, createdTags[2].id };

            _sportEventService.CreateComment("nice0. tag0 y tag2", se.id, u1.id, tagsId);
            tagsId.Add(createdTags[1].id);
            _sportEventService.CreateComment("nice1. tag0, tag1 y tag2", se.id, u1.id, tagsId);
            tagsId.Remove(1);
            tagsId.Remove(2);

            _sportEventService.CreateComment("nice2. tag0", se.id, u1.id, tagsId);

            List<Tag> popularTags = _sportEventService.FindPopularTags(2);

            Assert.AreEqual(popularTags.Count, 2);
            Assert.IsTrue(popularTags.Contains(createdTags[0]));
            Assert.IsTrue(popularTags.Contains(createdTags[2]));

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void DuplicateRecommendationsTest()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);

            UserGroup ug = _userGroupService.CreateUserGroup(u1.id, "grupo", "descripcion");

            List<long> recommendationGroups = new List<long>();
            recommendationGroups.Add(ug.id);

            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2020, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            _sportEventService.CreateRecommendation("reason", recommendationGroups, u1.id, se.id);
            List<EventRecommendationDto> recs = _sportEventService.FindRecommendationsByUserProfile(u1.id, 0, 100);
            Assert.AreEqual(recs.Count, 1L);
            _sportEventService.CreateRecommendation("reason", recommendationGroups, u1.id, se.id);
        }

        [TestMethod]
        [ExpectedException(typeof(SportEventAlreadyStartedException))]
        public void AddCommentToAlreadyStartedException()
        {
            UserProfile u1 = UserProfile.CreateUserProfile(-1, "o.video", "12345", "Ovidio", "del Río", "ovidio@gmail.com", "GL", "GZ");
            _userProfileDao.Create(u1);

            SportEvent se = SportEvent.CreateSportEvent(-1, "sportEvent", new DateTime(2010, 1, 1), "here", "good");
            _sportEventDao.Create(se);

            List<long> tags = new List<long>();
            _sportEventService.CreateComment("nice", se.id, u1.id, tags);
        }


        [TestMethod]
        public void FindSportEventsTest()
        {
            Category c1 = Category.CreateCategory(-1, "c1");
            CategoryDao.Create(c1);
            Category c2 = Category.CreateCategory(-1, "c2");
            CategoryDao.Create(c2);

            int i;
            for (i = 0; i < 4; i++)
            {
                SportEvent se = SportEvent.CreateSportEvent(-1, "SportEvent" + i, new DateTime(2020, 1, 1), "here", "good");
                _sportEventDao.Create(se);
            }

            //Filtrar solo por KeyWords

            List<EventCategoryDTO> sportEvents = _sportEventService.FindByKeywordsCategory("POrTe", 0, 10);
            Assert.AreEqual(sportEvents.Count, 10);
            i = 3;
            foreach (EventCategoryDTO dto in sportEvents)
            {
                Assert.AreEqual("SportEvent" + i, dto.EventName);
                i++;
            }

            sportEvents = _sportEventService.FindByKeywordsCategory("&%$€", 0, 10);
            Assert.AreEqual(sportEvents.Count, 0);

            sportEvents = _sportEventService.FindByKeywordsCategory("POrTe", 0, 2);
            Assert.AreEqual(sportEvents.Count, 2);

            //Filtrar por KeyWords y categoría

            for (i = 0; i < 6; i++)
            {
                SportEvent se = SportEvent.CreateSportEvent(-1, "SportEvent" + i, new DateTime(2020, 1, 1), "here", "good");
                _sportEventDao.Create(se);

                if (i < 3)
                {
                    se.Category = c1;
                }
                else
                {
                    se.Category = c2;
                }
            }

            sportEvents = _sportEventService.FindByKeywordsCategory("VeNt", c1.id, 0, 10);
            Assert.AreEqual(sportEvents.Count, 3);
            i = 0;
            foreach (EventCategoryDTO dto in sportEvents)
            {
                Assert.AreEqual("SportEvent" + i, dto.EventName);
                i++;
            }

            sportEvents = _sportEventService.FindByKeywordsCategory("VeNt", c2.id, 0, 10);
            Assert.AreEqual(sportEvents.Count, 2);
            i = 3;
            foreach (EventCategoryDTO dto in sportEvents)
            {
                Assert.AreEqual("SportEvent" + i, dto.EventName);
                i++;
            }

            sportEvents = _sportEventService.FindByKeywordsCategory("ZZZ 1", c1.id, 0, 10);
            Assert.AreEqual(sportEvents.Count, 0);


            sportEvents = _sportEventService.FindByKeywordsCategory("VeNt POrTe s 1", c1.id, 0, 10);
            Assert.AreEqual(sportEvents.Count, 1);
            Assert.AreEqual("SportEvent1", sportEvents[0].EventName);
        }
    }
}
