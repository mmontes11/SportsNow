using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.RecommendationDao
{
    public class RecommendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, Int64>, IRecommendationDao
    {
        public List<EventRecommendationDto> FindRecommendationsByUserProfile(long userProfileId, int startIndex, int count)
        {
            ObjectSet<Recommendation> recommendations =
                Context.CreateObjectSet<Recommendation>();
           
            List<EventRecommendationDto> result =
                (from r in recommendations
                 from ug in r.UserGroups
                 from up in ug.UserProfiles
                 where up.id == userProfileId
                 orderby r.dateRecommendation descending 
                 select new EventRecommendationDto
                 {
                     EventId = r.SportEvent.id,
                     EventName = r.SportEvent.name,
                     RecommendationDate = r.dateRecommendation,
                     RecommendationWhy = r.why,
                     RecommendationId = r.id,
                     UserName = r.UserProfile.loginName,
                     Groups = ug.name
                 }).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Recommendation> FindRecommendations(string why, long sportEventId, long userProfileId, long userGroupId)
        {
            ObjectSet<Recommendation> recommendations =
                Context.CreateObjectSet<Recommendation>();

            List<Recommendation> result =
                (from r in recommendations
                 from ug in r.UserGroups
                 where r.why == why && r.SportEvent.id == sportEventId && r.UserProfile.id == userProfileId && ug.id == userGroupId
                 orderby r.dateRecommendation
                 select r).ToList();

            return result;
        }

        public int NumberOfRecommendationsByUserProfile(long userProfileId)
        {
            ObjectSet<Recommendation> recommendations =
                Context.CreateObjectSet<Recommendation>();

            int result =
                (from r in recommendations
                 from ug in r.UserGroups
                 from up in ug.UserProfiles
                 where up.id == userProfileId
                 orderby r.dateRecommendation descending
                 select r).Count();

            return result;
        }

        public int GetNumberOfRecommendations(long userGroupId)
        {
            ObjectSet<Recommendation> recommendations =
                Context.CreateObjectSet<Recommendation>();

            int result =
                (from r in recommendations
                 from ug in r.UserGroups
                 where ug.id == userGroupId
                 select r).Count();
            return result;
        }

        public SportEvent FindSportEventByRecommendation(long recommendationId)
        {
            ObjectSet<Recommendation> recommendations = Context.CreateObjectSet<Recommendation>();

            SportEvent result =
                (from r in recommendations
                 where r.id == recommendationId
                 select r.SportEvent).First();
            return result;
        }

        public UserProfile FindUserProfileByRecommendation(long recommendationId)
        {
            ObjectSet<Recommendation> recommendations = Context.CreateObjectSet<Recommendation>();

            UserProfile result =
                (from r in recommendations
                 where r.id == recommendationId
                 select r.UserProfile).First();
            return result;
        }

        public List<UserGroup> FindUserGroupsByRecommendation(long recommendationId)
        {
            ObjectSet<Recommendation> recommendations = Context.CreateObjectSet<Recommendation>();

            List<UserGroup> result =
                (from r in recommendations
                 where r.id == recommendationId
                 select r.UserGroups).First().ToList();
            return result;
        }
    }
}
