using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class ChangeTags : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ISportEventService sportEventService =
                container.Resolve<ISportEventService>();

            long eventId = Convert.ToInt32(Request.Params.Get("eventId"));
            try
            {
                lblEventName.Text = sportEventService.FindSportEvent(eventId).name;
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("../NotFound.aspx"));
            }

            if (!IsPostBack)
            {
                txtTags.Text = "";

                long commentId = Convert.ToInt32(Request.Params.Get("commentId"));
                try
                {
                    Comment comment = sportEventService.FindComment(commentId);
                    if (comment.UserProfile.id == SessionManager.GetUserSession(Context).UserProfileId)
                    {
                        List<Tag> tags = sportEventService.GetTags(commentId);
                        foreach (Tag tag in tags)
                        {
                            txtTags.Text += tag.name + @" ";
                        }
                    }
                    else
                    {
                        Response.Redirect(Response.ApplyAppPathModifier("../Forbidden.aspx"));
                    }
                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect(Response.ApplyAppPathModifier("../NotFound.aspx"));
                }
            }
        }

        protected void BtnChangeTagsClick(object sender, EventArgs e)
        {
            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ISportEventService sportEventService =
                container.Resolve<ISportEventService>();

            long eventId = Convert.ToInt32(Request.Params.Get("eventId"));
            long commentId = Convert.ToInt32(Request.Params.Get("commentId"));
            List<long> tagIds = new List<long>();

            string[] tagNames = { };
            string tags = txtTags.Text.Trim();
            if (tags != "")
            {
                tagNames = tags.Split(Convert.ToChar(" "));
            }

            List<Tag> createdTags = sportEventService.CreateTag(tagNames.Distinct().ToList());
            foreach (Tag createdTag in createdTags)
            {
                tagIds.Add(createdTag.id);
            }
            sportEventService.TagComment(tagIds, commentId);


            String url = String.Format("./ViewComments.aspx?eventId=" + eventId);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}