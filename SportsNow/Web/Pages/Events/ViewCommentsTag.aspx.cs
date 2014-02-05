using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.SportsNow.Model;
using Es.Udc.DotNet.SportsNow.Model.SportEventService;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.Events
{
    public partial class ViewCommentsTag : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ISportEventService sportEventService = container.Resolve<ISportEventService>();

            long tagId = Convert.ToInt64(Request.Params.Get("tagId"));
            List<CommentEventDTO> comments = null;

            try
            {
                comments = sportEventService.GetCommentsByTag(tagId);
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("../NotFound.aspx"));
            }

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
                    dateLabel.Text = comments[i].DateComment.ToString("d/M/yyyy H:mm:ss");

                    Label tagsLabel = ((Label)commentList.Items[i].Controls[i].FindControl("lblTags"));
                    List<Tag> tags = sportEventService.GetTags(Convert.ToInt64(commentList.DataKeys[i]));
                    string tagsString = "";
                    foreach (Tag tag in tags)
                    {
                        tagsString += tag.name + " ";
                    }
                    tagsLabel.Text = tagsString;
                    tagsLabel.DataBind();

                    Comment comment = sportEventService.FindComment(Convert.ToInt64(commentList.DataKeys[i]));
                    if (SessionManager.IsUserAuthenticated(Context) && comment.UserProfile.id == SessionManager.GetUserSession(Context).UserProfileId)
                    {
                        HyperLink changeTagsLabel =
                            ((HyperLink)commentList.Items[i].Controls[i].FindControl("lnkChangeTags"));
                        changeTagsLabel.NavigateUrl = "ChangeTags.aspx?eventId=" + comments[i].EventId + "&commentId=" + comment.id;
                    }

                }
            }
        }
    }
}