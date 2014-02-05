using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment,Int64>
    {
        List<Comment> GetEventComments(long sportEventId);

        List<Tag> GetTagsByComment(long commentId);

        UserProfile FindUserProfileByCommentId(long commentId);

        SportEvent FinEventByCommentId(long commentId);
    }
}
