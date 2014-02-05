using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.SportsNow.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        List<Category> FindAllCategories(int startIndex, int count);

        int GetNumCategories();
    }
}
