using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Es.Udc.DotNet.SportsNow.Web.Properties;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class ShowRecommendations : SpecificCulturePage
    {

        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            pbpDataSource.ObjectCreating += PbpDataSource_ObjectCreating;

            pbpDataSource.TypeName =
                "Es.Udc.DotNet.SportsNow.Model.SportEventService.SportEventService";

            pbpDataSource.EnablePaging = true;

            pbpDataSource.SelectMethod = "FindRecommendationsByUserProfile";

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ISportEventService sportEventService =
                container.Resolve<ISportEventService>();

            if (sportEventService.NumberOfRecommendationsByUserProfile(userId) == 0) lblNoRecommendations.Visible = true;

            pbpDataSource.StartRowIndexParameterName =
                "startIndex";
            pbpDataSource.MaximumRowsParameterName =
                "count";

            pbpDataSource.SelectParameters.Add("userProfileId", DbType.Int64, userId.ToString(CultureInfo.InvariantCulture));
            pbpDataSource.SelectCountMethod = "NumberOfRecommendationsByUserProfile";

            gvRecommendations.AllowPaging = true;
            int count = Settings.Default.SportsNow_defaultCount;
            gvRecommendations.PageSize = count;

            gvRecommendations.DataSource = pbpDataSource;
            gvRecommendations.DataBind();
        }

        protected void GvRecommendationsIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecommendations.PageIndex = e.NewPageIndex;
            gvRecommendations.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IUnityContainer container = (IUnityContainer)
                HttpContext.Current.Application["unityContainer"];

            ISportEventService sportEventService = new SportEventService();
            sportEventService = (ISportEventService)
                container.BuildUp(sportEventService.GetType(), sportEventService);

            e.ObjectInstance = sportEventService;
        }
    }
}