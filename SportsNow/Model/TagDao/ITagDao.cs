using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        List<Tag> FindAllTags();

        List<Tag> FindPopularTags(int count);

        Tag GetTagByName(string name);

        List<Comment> GetCommentsByTag(long tagId);
        
        Tag FindTagByName(string tagName);
    }
}
