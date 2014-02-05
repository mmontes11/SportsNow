using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.RecommendationDao
{
    public interface IRecommendationDao : IGenericDao<Recommendation,Int64>
    {
        List<EventRecommendationDto> FindRecommendationsByUserProfile(long userProfileId, int startIndex, int count);

        List<Recommendation> FindRecommendations(string why, long sportEventId, long userProfileId, long userGroupId);

        int NumberOfRecommendationsByUserProfile(long userProfileId);

        int GetNumberOfRecommendations(long userGroupId);

        SportEvent FindSportEventByRecommendation(long recommendationId);

        UserProfile FindUserProfileByRecommendation(long recommendationId);

        List<UserGroup> FindUserGroupsByRecommendation(long recommendationId);
    }
}
