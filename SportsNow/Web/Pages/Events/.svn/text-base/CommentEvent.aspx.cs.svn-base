using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Model.SportEventService.Exceptions;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class CommentEvent : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();
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

        protected void BtnCommentClick(object sender, EventArgs e)
        {
            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ISportEventService sportEventService =
                container.Resolve<ISportEventService>();

            long eventId = Convert.ToInt32(Request.Params.Get("eventId"));
            String comment = txtComment.Text;
            long userId = SessionManager.GetUserSession(Context).UserProfileId;
            List<long> tagIds = new List<long>();

            string[] tagNames = { };
            string tags = txtTags.Text.Trim();
            if (txtTags.Text != "")
            {
                tagNames = tags.Split(Convert.ToChar(" "));
            }

            List<Tag> createdTags = sportEventService.CreateTag(tagNames.Distinct().ToList());
            foreach (Tag createdTag in createdTags)
            {
                tagIds.Add(createdTag.id);
            }

            try
            {
                sportEventService.CreateComment(comment, eventId, userId, tagIds);
            }
            catch (SportEventAlreadyStartedException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("../Errors/PastEvent.aspx"));
            }

            String url = String.Format("./ViewComments.aspx?eventId=" + eventId);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }
}