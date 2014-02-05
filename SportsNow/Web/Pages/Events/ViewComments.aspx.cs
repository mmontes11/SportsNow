using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class ViewComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();

            long eventId = Convert.ToInt64(Request.Params.Get("eventId"));
            List<Comment> comments;
            try
            {
                sportEventService.FindSportEvent(eventId);
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("../NotFound.aspx"));
            }

            comments = sportEventService.GetEventComments(eventId);
            commentList.DataSource = comments;
            commentList.DataBind();

            if (comments != null && comments.Count == 0)
            {
                lblNoComments.Visible = true;
            }
            else
            {
                for (int i = 0; i < commentList.Items.Count; i++)
                {
                    Label dateLabel = ((Label)commentList.Items[i].Controls[i].FindControl("lblDate"));
                    if (comments != null) dateLabel.Text = comments[i].dateComment.ToString("d/M/yyyy H:mm:ss");
                    long commentId = Convert.ToInt64(commentList.DataKeys[i]);
                    Label tagsLabel = ((Label)commentList.Items[i].Controls[i].FindControl("lblTags"));
                    List<Tag> tags = sportEventService.GetTags(commentId);
                    string tagsString = "";
                    foreach (Tag tag in tags)
                    {
                        tagsString += tag.name + " ";
                    }
                    tagsLabel.Text = tagsString;
                    tagsLabel.DataBind();

                    Comment comment = sportEventService.FindComment(commentId);
                    if (SessionManager.IsUserAuthenticated(Context) && comment.UserProfile.id == SessionManager.GetUserSession(Context).UserProfileId)
                    {
                        HyperLink changeTagsLabel =
                            ((HyperLink)commentList.Items[i].Controls[i].FindControl("lnkChangeTags"));
                        changeTagsLabel.NavigateUrl = "ChangeTags.aspx?eventId=" + eventId + "&commentId=" +
                                                      Convert.ToInt64(commentList.DataKeys[i]);
                    }

                }
            }



        }
    }
}