using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Es.Udc.DotNet.SportsNow.Web.Properties;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Groups
{
    public partial class AllGroups : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoUserGroups.Visible = false;
            lblUserSubscribed.Visible = false;

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            IUserGroupService userGroupService = container.Resolve<IUserGroupService>();

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.SportsNow_defaultCount;
            }


            /* Get Groups Info */
            List<GroupDTO> groups = userGroupService.GetAllGroupsInfo(startIndex, count);

            if (groups.Count == 0)
            {
                lblNoUserGroups.Visible = true;
                return;
            }
            gvUserGroups.DataSource = groups;
            gvUserGroups.DataBind();

            if (!SessionManager.IsUserAuthenticated(Context))
            {
                gvUserGroups.Columns[5].Visible = false;
            }
            else
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                List<UserGroup> userGroups = userGroupService.GetGroups(userId, 0, userGroupService.GetNumberOfGroups(userId));
                for (int i = 0; i < gvUserGroups.Rows.Count; i++)
                {
                    GridViewRow gridViewRow = gvUserGroups.Rows[i];
                    long groupId = Convert.ToInt64(gridViewRow.Cells[0].Text);

                    foreach (UserGroup ug in userGroups)
                    {
                        if (ug.id == groupId)
                        {
                            gridViewRow.Cells[5].Controls[0].Visible = false;
                            break;
                        }
                    }
                }
            }

            int numberOfGroups = userGroupService.GetNumberOfAllGroups();
            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "./AllGroups.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if ((startIndex + count) < numberOfGroups)
            {
                String url = "./AllGroups.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }
        }

        protected void gvUserGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SubscribeUser")
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
                userGroupService.SubscribeUserProfile(userId, groupsId);

                String url = String.Format("./AllGroups.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

    }
}