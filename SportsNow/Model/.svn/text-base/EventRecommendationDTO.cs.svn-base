using System;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public class EventRecommendationDto
    {
        public string UserName { get; set; }

        public DateTime RecommendationDate { get; set; }

        public string RecommendationWhy { get; set; }

        public long RecommendationId { get; set; }

        public string EventName { get; set; }

        public long EventId { get; set; }

        public string Groups { get; set; }

        public EventRecommendationDto(long eventId, string eventName, Recommendation recommendation, string userName, string groups)
        {
            EventId = eventId;
            EventName = eventName;
            RecommendationId = recommendation.id;
            RecommendationWhy = recommendation.why;
            RecommendationDate = recommendation.dateRecommendation;
            UserName = userName;
            Groups = groups;
        }

        public EventRecommendationDto(){}
    }
}
