using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> GetEventComments(long sportEventId)
        {
            ObjectSet<Comment> comments =
                Context.CreateObjectSet<Comment>();

            List<Comment> result =
                (from c in comments.Include("UserProfile")
                     where c.SportEvent.id == sportEventId
                     orderby c.dateComment descending 
                     select c).ToList();
            
            return result;
        }

        public List<Tag> GetTagsByComment(long commentId)
        {
            ObjectSet<Comment> comments =
                Context.CreateObjectSet<Comment>();

            List<Tag> result =
                (from c in comments
                 where c.id == commentId
                 select c.Tags).First().ToList();

            return result;
        }

        public UserProfile FindUserProfileByCommentId(long commentId)
        {
            ObjectSet<Comment> comments =
                Context.CreateObjectSet<Comment>();

            UserProfile result = (from c in comments
                                  where c.id == commentId
                                  select c.UserProfile).First();
            return result;
        }

        public SportEvent FinEventByCommentId(long commentId)
        {
            ObjectSet<Comment> comments =
                Context.CreateObjectSet<Comment>();

            SportEvent result = (from c in comments
                                  where c.id == commentId
                                  select c.SportEvent).First();
            return result;
        }
    }
}
