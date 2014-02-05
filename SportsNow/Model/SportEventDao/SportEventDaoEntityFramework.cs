using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.SportEventDao
{
    public class SportEventDaoEntityFramework : GenericDaoEntityFramework<SportEvent, Int64>, ISportEventDao
    {

        public List<SportEvent> FindByKeywordsCategory(string keywords, long? categoryId, int startIndex, int count)
        {
            if (keywords == null) keywords = "";
            string[] searchTerms = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ObjectSet<SportEvent> sportEvents =
                Context.CreateObjectSet<SportEvent>();

            List<SportEvent> result =
                (from se in sportEvents
                 where (categoryId == null || se.Category.id == categoryId)
                 && searchTerms.All(st => se.name.ToUpper().Contains(st.ToUpper()))
                 orderby se.id
                 select se).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int GetNumFindByKC(string keywords, long? categoryId)
        {
            if (keywords == null) keywords = "";
            string[] searchTerms = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ObjectSet<SportEvent> sportEvents =
                Context.CreateObjectSet<SportEvent>();

            int result =
                (from se in sportEvents
                 where (categoryId == null || se.Category.id == categoryId)
                       && searchTerms.All(st => se.name.ToUpper().Contains(st.ToUpper()))
                 orderby se.id
                 select se).Count();

            return result;
        }

        public List<SportEvent> FindByKeywordsCategory(string keywords, int startIndex, int count)
        {
            return FindByKeywordsCategory(keywords, null, startIndex, count);
        }
    }
}
