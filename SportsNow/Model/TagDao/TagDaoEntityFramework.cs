using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        public List<Tag> FindAllTags()
        {
            ObjectSet<Tag> tags = Context.CreateObjectSet<Tag>();

            List<Tag> result =
                (from t in tags
                 orderby t.id
                 select t).ToList();

            return result;
        }

        public List<Tag> FindPopularTags(int count)
        {
            ObjectSet<Tag> tags = Context.CreateObjectSet<Tag>();

            List<Tag> result =
                (from t in tags
                 where t.Comments.Count > 0
                 orderby t.Comments.Count descending 
                 select t).Take(count).ToList();

            return result;
        }

        public Tag GetTagByName(string name)
        {
            ObjectSet<Tag> tags = Context.CreateObjectSet<Tag>();

            List<Tag> result = (from t in tags
                where t.name.Equals(name)
                select t).ToList();
           
            if (result.Count == 0)
            {
                return null;
            }
            return result.First();
        }

        public List<Comment> GetCommentsByTag(long tagId)
        {
            ObjectSet<Tag> tags = Context.CreateObjectSet<Tag>();

            List<Comment> result =
                (from t in tags
                 from c in t.Comments
                 where t.id == tagId
                 orderby c.dateComment descending 
                 select c).ToList();

            return result;
        }

        public Tag FindTagByName(string tagName)
        {
            ObjectSet<Tag> tags = Context.CreateObjectSet<Tag>();

            List<Tag> result =
                (from t in tags
                 where t.name == tagName
                 select t).ToList();

            if (result.Count != 1)
            {
                return null;
            }
            return result.First();
        }
    }
}
