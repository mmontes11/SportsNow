using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model
{
    public class CommentEventDTO
    {
        public long Id { get; private set; }

        public string LoginName { get; private set; }

        public DateTime DateComment { get; set; }

        public string CommentText { get; private set; }

        public long EventId { get; private set; }

        public CommentEventDTO(long id, string loginName, DateTime dateComment, string commentText, long eventId)
        {           
            this.Id = id;
            this.CommentText = commentText;
            this.DateComment = dateComment;
            this.LoginName = loginName;
            this.EventId = eventId;
        }
    }
}
