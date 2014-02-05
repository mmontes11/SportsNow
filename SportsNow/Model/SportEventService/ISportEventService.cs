using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
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
    public interface ISportEventService
    {
        [Dependency]
        ISportEventDao SportEventDao { set; }

        [Dependency]
        ICommentDao CommentDao { set; }

        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        [Dependency]
        IUserGroupDao UserGroupDao { set; }

        [Dependency]
        IRecommendationDao RecommendationDao { set; }

        [Dependency]
        ICategoryDao CategoryDao { set; }

        [Dependency]
        ITagDao TagDao { set; }

        [Transactional]
        SportEvent FindSportEvent(long eventId);

        [Transactional]
        List<EventCategoryDTO> FindByKeywordsCategory(string keywords, long categoryId, int startIndex, int count);

        [Transactional]
        List<EventCategoryDTO> FindByKeywordsCategory(string keywords, int startIndex, int count);

        [Transactional]
        int GetNumFindByKc(string keywords, long categoryId);

        [Transactional]
        int GetNumFindByK(string keywords);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="SportEventAlreadyStartedException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        Comment CreateComment(string comment, long eventId, long userId, List<long> tagsId);

        [Transactional]
        List<Comment> GetEventComments(long sportEventId);

        /// <exception cref="DuplicateInstanceException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UserProfileNotSubscribedException"/>
        [Transactional]
        Recommendation CreateRecommendation(string why, List<long> listUserGroupIds, long userProfileid, long sportEventId);

        [Transactional]
        List<EventRecommendationDto> FindRecommendationsByUserProfile(long userProfileId, int startIndex, int count);

        [Transactional]
        int NumberOfRecommendationsByUserProfile(long userProfileId);
        
        [Transactional]
        List<Category> FindAllCategories(int startIndex, int count);

        [Transactional]
        int GetNumCategories();

        [Transactional]
        List<Tag> CreateTag(List<string> tagsName);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void TagComment(List<long> tagsId, long commentId);

        /// <exception cref="DuplicateInstanceException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void DeleteTag(List<long> tagsId, long commentId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comment FindComment(long commentId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<CommentEventDTO> GetCommentsByTag(long tagId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        int GetNumberOfTaggedComments(long tagId);

        [Transactional]
        List<Tag> GetTags(long commentId);

        [Transactional]
        List<Tag> FindAllTags();

        [Transactional]
        List<Tag> FindPopularTags(int numTags);

        [Transactional]
        Tag GetTagByName(string name);
    }
}
