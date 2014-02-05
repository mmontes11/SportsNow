using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web
{

    public partial class SportsNow : System.Web.UI.MasterPage
    {

        public static readonly String UserSessionAttribute = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnkMyGroups != null)
                    lnkMyGroups.Visible = false;
                if (lnkCreateGroup != null)
                    lnkCreateGroup.Visible = false;
                if (lnkShowRecommendations != null)
                    lnkShowRecommendations.Visible = false;

            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text")
                        + @" " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();

            List<Tag> popularTags = sportEventService.FindPopularTags(10);

            Dictionary<long, string> tagsSize = new Dictionary<long, string>();
            // Asignamos a cada tag un tamaño de letra
            for(int i = 0; i < popularTags.Count; i++)
            {
                tagsSize.Add(popularTags[i].id, (24 - 2*i).ToString(CultureInfo.InvariantCulture));
            }
            
            List<Tag> unorderPopularTags = popularTags.OrderBy(t => Convert.ToInt32(t.id)).ToList();
            TagsList.DataSource = unorderPopularTags;
            TagsList.DataBind();

            // Cambiar el tamaño de la lista
            for (int i = 0; i < popularTags.Count; i++)
            {                
                Label tagLabel = ((Label)TagsList.Items[i].Controls[0].FindControl("tag"));
                string size;
                tagsSize.TryGetValue(Convert.ToInt64(TagsList.DataKeys[i]), out size);
                tagLabel.ControlStyle.Font.Size = Convert.ToInt32(size);

                HyperLink viewComments = ((HyperLink)TagsList.Items[i].Controls[0].FindControl("lnkViewCommentsTag"));
                viewComments.NavigateUrl = "Pages/Events/ViewCommentsTag.aspx?tagId=" + Convert.ToInt64(TagsList.DataKeys[i]);
            }



        }
    }
}
