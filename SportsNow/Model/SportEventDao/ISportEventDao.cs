using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.SportsNow.Model.SportEventDao  
{
    public interface ISportEventDao : IGenericDao<SportEvent, Int64>
    {

        List<SportEvent> FindByKeywordsCategory(string keywords, long? categoryId, int startIndex, int count);

        int GetNumFindByKC(string keywords, long? category); 

        List<SportEvent> FindByKeywordsCategory(string keywords, int startIndex, int count);

    }
}
