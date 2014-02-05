using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.CommentDao;
using Es.Udc.DotNet.SportsNow.Model.RecommendationDao;
using Es.Udc.DotNet.SportsNow.Model.SportEventDao;
using Es.Udc.DotNet.SportsNow.Model.SportEventService.Exceptions;
using Es.Udc.DotNet.SportsNow.Model.TagDao;
using Es.Udc.DotNet.SportsNow.Model.UserGroupDao;
using Es.Udc.DotNet.SportsNow.Model.UserProfileDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.SportsNow.Model.CategoryDao;

namespace Es.Udc.DotNet.SportsNow.Model.SportEventService
{
    public class SportEventService : ISportEventService
    {
        [Dependency]
        public ISportEventDao SportEventDao { set; private get; }

        [Dependency]
        public ICommentDao CommentDao { set; private get; }

        [Dependency]
        public IUserProfileDao UserProfileDao { set; private get; }

        [Dependency]
        public IUserGroupDao UserGroupDao { set; private get; }

        [Dependency]
        public IRecommendationDao RecommendationDao { set; private get; }

        [Dependency]
        public ICategoryDao CategoryDao { set; private get; }

        [Dependency]
        public ITagDao TagDao { set; private get; }

        public SportEvent FindSportEvent(long eventId)
        {
            return SportEventDao.Find(eventId);
        }

        public List<EventCategoryDTO> FindByKeywordsCategory(string keywords, long categoryId, int startIndex, int count)
        {
            List<SportEvent> sportEvents = SportEventDao.FindByKeywordsCategory(keywords, categoryId, startIndex, count);
            List<EventCategoryDTO> eventCategories = new List<EventCategoryDTO>();
            foreach (SportEvent sportEvent in sportEvents)
            {
                eventCategories.Add(new EventCategoryDTO(sportEvent, sportEvent.Category));
            }
            return eventCategories;
        }

        public List<EventCategoryDTO> FindByKeywordsCategory(string keywords, int startIndex, int count)
        {
            List<SportEvent> sportEvents = SportEventDao.FindByKeywordsCategory(keywords, startIndex, count);
            List<EventCategoryDTO> eventCategories = new List<EventCategoryDTO>();
            foreach (SportEvent sportEvent in sportEvents)
            {
                eventCategories.Add(new EventCategoryDTO(sportEvent, sportEvent.Category));
            }
            return eventCategories;
        }

        public int GetNumFindByKc(string keywords, long categoryId)
        {
            return SportEventDao.GetNumFindByKC(keywords, categoryId);
        }

        public int GetNumFindByK(string keywords)
        {
            return SportEventDao.GetNumFindByKC(keywords, null);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="SportEventAlreadyStartedException"/>
        /// <exception cref="DuplicateInstanceException"/>
        public Comment CreateComment(string comment, long eventId, long userId, List<long> tagsId)
        {
            if (tagsId.Count() != tagsId.Distinct().Count())
                throw new DuplicateInstanceException(0, "Tag");

            SportEvent se = SportEventDao.Find(eventId);
            if (DateTime.Now.CompareTo(se.dateEvent) > 0)
            {
                throw new SportEventAlreadyStartedException(se.id);
            }
            UserProfile up = UserProfileDao.Find(userId);
            Comment c = Comment.CreateComment(-1, comment, DateTime.Now);
            c.SportEvent = se;
            c.UserProfile = up;

            if (tagsId != null)
            {
                foreach (long tagId in tagsId)
                {
                    Tag t = TagDao.Find(tagId);
                    c.Tags.Add(t);
                }
            }
            CommentDao.Create(c);
            return c;
        }

        public List<Comment> GetEventComments(long sportEventId)
        {
            return CommentDao.GetEventComments(sportEventId);
        }

        /// <exception cref="DuplicateInstanceException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UserProfileNotSubscribedException"/>
        public Recommendation CreateRecommendation(string why, List<long> listUserGroupIds, long userProfileId, long sportEventId)
        {
            if (listUserGroupIds.Count() != listUserGroupIds.Distinct().Count())
                throw new DuplicateInstanceException(0, "UserGroup");

            UserProfile user = UserProfileDao.Find(userProfileId);
            List<UserGroup> nuevosUserGroups = new List<UserGroup>();
            foreach (long userGroupId in listUserGroupIds)
            {
                UserGroup ug = UserGroupDao.Find(userGroupId);
                nuevosUserGroups.Add(ug);
                if (!user.UserGroups.Contains(ug)) throw new UserProfileNotSubscribedException(userProfileId, userGroupId);
            }

            Recommendation r = Recommendation.CreateRecommendation(-1, DateTime.Now, why);
            foreach (long userGroupId in listUserGroupIds)
            {
                UserGroup ug = UserGroupDao.Find(userGroupId);
                if (RecommendationDao.FindRecommendations(why, sportEventId, userProfileId, ug.id).Count == 0)
                {
                    r.UserGroups.Add(ug);
                }
            }
            if (r.UserGroups.Count == 0)
            {
                throw new DuplicateInstanceException(r.id, typeof(Recommendation).FullName);
            }
            r.UserProfile = user;
            r.SportEvent = SportEventDao.Find(sportEventId);
            try
            {
                RecommendationDao.Create(r);
            }
            catch (Exception)
            {
                throw new DuplicateInstanceException(r.id, typeof(Recommendation).FullName);
            }
            return r;
        }

        public List<EventRecommendationDto> FindRecommendationsByUserProfile(long userProfileId, int startIndex, int count)
        {
            return RecommendationDao.FindRecommendationsByUserProfile(userProfileId, startIndex, count);
        }

        public int NumberOfRecommendationsByUserProfile(long userProfileId)
        {
            return RecommendationDao.NumberOfRecommendationsByUserProfile(userProfileId);
        }

        public List<Category> FindAllCategories(int startIndex, int count)
        {
            return CategoryDao.FindAllCategories(startIndex, count);
        }

        public int GetNumCategories()
        {
            return CategoryDao.GetNumCategories();
        }

        public List<Tag> CreateTag(List<string> tagsName)
        {
            List<Tag> tags = new List<Tag>();
            foreach (string tagName in tagsName)
            {
                Tag newTag = Tag.CreateTag(-1, tagName);
                Tag t = TagDao.FindTagByName(tagName);
                if (t != null)
                {
                    tags.Add(t);
                }
                else
                {
                    TagDao.Create(newTag);
                    tags.Add(newTag);
                }
            }
            return tags;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void TagComment(List<long> tagsId, long commentId)
        {
            Comment c = CommentDao.Find(commentId);
            c.Tags.Clear();
            foreach (long tagId in tagsId)
            {
                Tag t = TagDao.Find(tagId);
                if (!c.Tags.Contains(t))
                {
                    c.Tags.Add(t);
                }
            }
            CommentDao.Update(c);
        }

        /// <exception cref="DuplicateInstanceException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteTag(List<long> tagsId, long commentId)
        {
            if (tagsId.Count() != tagsId.Distinct().Count())
                throw new DuplicateInstanceException(0, "Tag");

            Comment c = CommentDao.Find(commentId);
            foreach (long tagId in tagsId)
            {
                Tag t = TagDao.Find(tagId);
                c.Tags.Remove(t);
            }
            CommentDao.Update(c);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Comment FindComment(long commentId)
        {
            return CommentDao.Find(commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public List<CommentEventDTO> GetCommentsByTag(long tagId)
        {
            TagDao.Find(tagId);
            List<Comment> comments = TagDao.GetCommentsByTag(tagId);
            List<CommentEventDTO> dtos = new List<CommentEventDTO>();
            SportEvent se;
            UserProfile up;
            foreach (Comment c in comments)
            {
                up = CommentDao.FindUserProfileByCommentId(c.id);
                se = CommentDao.FinEventByCommentId(c.id);
                dtos.Add(new CommentEventDTO(c.id,up.loginName,c.dateComment,c.value,se.id));
            }
            return dtos;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public int GetNumberOfTaggedComments(long tagId)
        {
            Tag t = TagDao.Find(tagId);
            return t.Comments.Count;
        }

        public List<Tag> GetTags(long commentId)
        {
            return CommentDao.GetTagsByComment(commentId);
        }

        public List<Tag> FindAllTags()
        {
            return TagDao.FindAllTags();
        }

        public List<Tag> FindPopularTags(int numTags)
        {
            return TagDao.FindPopularTags(numTags);
        }

        public Tag GetTagByName(string name)
        {
            return TagDao.GetTagByName(name);
        }
    }
}
