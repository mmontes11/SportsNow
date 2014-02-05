using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.SportsNow.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        public List<Category> FindAllCategories(int startIndex, int count)
        {
            ObjectSet<Category> categories = Context.CreateObjectSet<Category>();

            List<Category> result =
                (from c in categories
                 orderby c.id
                 select c).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int GetNumCategories()
        {
            ObjectSet<Category> categories = Context.CreateObjectSet<Category>();

            int result =
                (from c in categories
                 orderby c.id
                 select c).ToList().Count;

            return result;
        }
    }
}
