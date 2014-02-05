using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Model.UserGroupService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class Recommend : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDuplicatedRecommendation.Visible = false;
            if (!IsPostBack)
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;

                IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
                IUserGroupService userGroupService = container.Resolve<IUserGroupService>();
                ISportEventService sportEventService = container.Resolve<ISportEventService>();

                List<UserGroup> userGroups = userGroupService.GetGroups(userId, 0,
                    userGroupService.GetNumberOfGroups(userId));

                foreach (var ug in userGroups)
                {
                    CheckBoxList1.Items.Add(new ListItem(ug.name, ug.id.ToString(CultureInfo.InvariantCulture)));
                }

                long eventId = Convert.ToInt32(Request.Params.Get("eventId"));
                try
                {
                    lblEventName.Text = sportEventService.FindSportEvent(eventId).name;
                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect(Response.ApplyAppPathModifier("../NotFound.aspx"));
                }
            }
        }

        protected void BtnRecommendClick(object sender, EventArgs e)
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();

            long eventId = Convert.ToInt32(Request.Params.Get("eventId"));
            String recommendText = recommendTextBox.Text;
            long userId = SessionManager.GetUserSession(Context).UserProfileId;
            List<long> userGroups = new List<long>();

            // ReSharper disable once EmptyForStatement
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    userGroups.Add(Convert.ToInt64(CheckBoxList1.Items[i].Value));
                }
            }

            if (!userGroups.Any())
            {
                lblDuplicatedRecommendation.Visible = false;
                lblRecommendationSuccesful.Visible = false;
                lblParametersNotValid.Visible = true;
                return;
            }

            try
            {
                sportEventService.CreateRecommendation(recommendText, userGroups, userId, eventId);
                recommendTextBox.Text = "";
                CheckBoxList1.ClearSelection();
                lblParametersNotValid.Visible = false;
                lblDuplicatedRecommendation.Visible = false;
                lblRecommendationSuccesful.Visible = true;
            }
            catch (DuplicateInstanceException)
            {
                lblParametersNotValid.Visible = false;
                lblRecommendationSuccesful.Visible = false;
                lblDuplicatedRecommendation.Visible = true;
            }


        }

    }
}