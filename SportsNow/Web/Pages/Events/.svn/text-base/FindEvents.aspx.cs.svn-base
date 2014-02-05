using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class FindEvents : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();

            List<Category> categories = sportEventService.FindAllCategories(0, sportEventService.GetNumCategories());

            foreach (Category c in categories)
            {
                ddlFindByCategory.Items.Add(new ListItem(c.name, c.id.ToString(CultureInfo.InvariantCulture)));
            }
        }

        protected void BtnFindEventsClick(object sender, EventArgs e)
        {
            string keywords = txtKeywordsEvents.Text;
            string category = ddlFindByCategory.SelectedValue;

            String url = String.Format("./FoundEvents.aspx?keywords={0}", keywords);
            if (category != "")
            {
                url += String.Format("&categoryId={0}", category);
            }
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}