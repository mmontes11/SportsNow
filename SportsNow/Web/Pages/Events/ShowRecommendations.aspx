<%@ Page Title="" Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    CodeBehind="ShowRecommendations.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.ShowRecommendations" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p>
        <asp:Label ID="lblNoRecommendations" meta:resourcekey="lblNoRecommendations" runat="server"
            Visible="False"></asp:Label></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvRecommendations" runat="server" AutoGenerateColumns="False" CellPadding="2"
                CssClass="tableInfo" GridLines="None" HeaderStyle-HorizontalAlign="Center" OnPageIndexChanging="GvRecommendationsIndexChanging">
                <AlternatingRowStyle BackColor="LightGray" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="eventId" DataNavigateUrlFormatString="ViewComments.aspx?eventId={0}"
                        DataTextField="eventName" HeaderText="Event" />
                    <asp:BoundField DataField="recommendationDate" HeaderText="<%$ Resources:Common, dateRecommendation %>"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="userName" HeaderText="<%$ Resources:Common, userProfileName %>" />
                    <asp:BoundField DataField="recommendationWhy" HeaderText="<%$ Resources:Common, why %>" />
                    <asp:BoundField DataField="groups" HeaderText="<%$ Resources:Common, groups %>" />
                </Columns>
                <HeaderStyle BackColor="#097099" ForeColor="White" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</asp:Content>
