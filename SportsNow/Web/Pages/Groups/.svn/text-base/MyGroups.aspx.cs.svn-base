using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Es.Udc.DotNet.SportsNow.Web.Properties;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Groups
{
    public partial class MyGroups : SpecificCulturePage
    {

        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {

            long userId = SessionManager.GetUserSession(Context).UserProfileId;

            pbpDataSource.ObjectCreating += PbpDataSource_ObjectCreating;

            pbpDataSource.TypeName =
                "Es.Udc.DotNet.SportsNow.Model.UserGroupService.UserGroupService";

            pbpDataSource.EnablePaging = true;

            pbpDataSource.SelectMethod = "GetGroups";

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            IUserGroupService userGroupService =
                container.Resolve<IUserGroupService>();

            if (userGroupService.GetNumberOfGroups(userId) == 0) lblNoUserGroups.Visible = true;

            pbpDataSource.StartRowIndexParameterName = "startIndex";
            pbpDataSource.MaximumRowsParameterName = "count";

            pbpDataSource.SelectCountMethod = "GetNumberOfGroups";

            pbpDataSource.SelectParameters.Add("userProfileId", DbType.Int64, userId.ToString(CultureInfo.InvariantCulture));

            gvUserGroups.AllowPaging = true;
            int count = Settings.Default.SportsNow_defaultCount;
            gvUserGroups.PageSize = count;

            gvUserGroups.DataSource = pbpDataSource;
            gvUserGroups.DataBind();
        }

        protected void gvUserGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UnsubscribeUser")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvUserGroups.Rows[index];

                long groupId = long.Parse(row.Cells[0].Text);
                List<long> groupsId = new List<long> { groupId };
                long userId = SessionManager.GetUserSession(Context).UserProfileId;

                /* Get the Service */
                IUnityContainer container =
                    (IUnityContainer)HttpContext.Current.
                        Application["unityContainer"];
                IUserGroupService userGroupService = container.Resolve<IUserGroupService>();

                /* Subscribe User */
                userGroupService.UnsubscribeUserProfile(userId, groupsId);
                lblUnsubscribedUser.Visible = true;

                String url = String.Format("./MyGroups.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
                lblUnsubscribedUser.Visible = true;
            }
        }

        protected void GvUserGroupsIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserGroups.PageIndex = e.NewPageIndex;
            gvUserGroups.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            /* Get the Service */
            IUnityContainer container = (IUnityContainer)
                HttpContext.Current.Application["unityContainer"];

            IUserGroupService userGroupService = new UserGroupService();
            userGroupService = (IUserGroupService)
                container.BuildUp(userGroupService.GetType(), userGroupService);

            e.ObjectInstance = userGroupService;
        }

    }
}