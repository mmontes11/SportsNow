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
    public partial class FoundEvents : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {

            lblNoEvents.Visible = false;

            pbpDataSource.ObjectCreating += PbpDataSource_ObjectCreating;

            pbpDataSource.TypeName =
                "Es.Udc.DotNet.SportsNow.Model.SportEventService.SportEventService";

            pbpDataSource.EnablePaging = true;

            pbpDataSource.SelectMethod = "FindByKeywordsCategory";

            String keywords = Request.Params.Get("keywords");
            if (keywords == null) keywords = "";
            long categoryId = Convert.ToInt32(Request.Params.Get("categoryId"));

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ISportEventService sportEventService =
                container.Resolve<ISportEventService>();

            int numberOfEvents;
            if (categoryId != 0)
            {
                numberOfEvents = sportEventService.GetNumFindByKc(keywords, categoryId);
            }
            else
            {
                numberOfEvents = sportEventService.GetNumFindByK(keywords);
            }
            if (numberOfEvents == 0) lblNoEvents.Visible = true;


            pbpDataSource.SelectParameters.Add("keywords", DbType.String, keywords);

            pbpDataSource.StartRowIndexParameterName =
                "startIndex";
            pbpDataSource.MaximumRowsParameterName =
                "count";
            if (categoryId != 0)
            {
                pbpDataSource.SelectParameters.Add("categoryId", DbType.Int64, categoryId.ToString(CultureInfo.InvariantCulture));
                pbpDataSource.SelectCountMethod = "GetNumFindByKC";
            }
            else
            {
                pbpDataSource.SelectCountMethod = "GetNumFindByK";
            }

            gvEvents.AllowPaging = true;
            int count = Settings.Default.SportsNow_defaultCount;
            gvEvents.PageSize = count;

            gvEvents.DataSource = pbpDataSource;
            gvEvents.DataBind();
        }

        protected void gvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvEvents.Rows[index];
            if (e.CommandName == "Comment")
            {
                String url =
                    "./CommentEvent.aspx?eventId=" + row.Cells[0].Text;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            if (e.CommandName == "ViewComments")
            {
                String url =
                    "./ViewComments.aspx?eventId=" + row.Cells[0].Text;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

            if (e.CommandName == "Recommend")
            {
                String url =
                    "./Recommend.aspx?eventId=" + row.Cells[0].Text;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }

        }

        protected void GvEventsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvents.PageIndex = e.NewPageIndex;
            gvEvents.DataBind();
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