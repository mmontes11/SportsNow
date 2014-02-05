using System;

using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;

namespace Es.Udc.DotNet.SportsNow.Web.Pages.User
{

    public partial class Logout : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/MainPage.aspx");


        }
    }
}
