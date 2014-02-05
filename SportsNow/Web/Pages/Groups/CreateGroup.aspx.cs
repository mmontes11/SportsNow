using System;
using System.Web;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Groups
{
    public partial class CreateGroup : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void BtnCreateGroupClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Create an Account. */
                string groupName = txtGroupName.Text;
                string groupDescription = txtGroupDescription.Text;

                /* Get the Service */
                IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
                IUserGroupService userGroupService = container.Resolve<IUserGroupService>();

                UserGroup userGroup = userGroupService.CreateUserGroup(SessionManager.GetUserSession(Context).UserProfileId, groupName, groupDescription);

                if (userGroup != null)
                {
                    LogManager.RecordMessage("User Group " + userGroup.id + " created.", MessageType.Info);
                    Response.Redirect(Response.ApplyAppPathModifier("./AllGroups.aspx"));
                }
                else
                {
                    lblGroupAlreadyExists.Visible = true;
                }
            }
        }
    }
}