<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindEvents.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.FindEvents"
    MasterPageFile="~/SportsNow.Master" %>

<asp:Content ID="content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="FindEventsForm" method="post" runat="server">
    <div class="field">
        <span class="label">
            <asp:Localize ID="lclKeywords" runat="server" Text="<%$ Resources:Common, keywordsEvents %>" />
        </span><span class="entry">
            <asp:TextBox ID="txtKeywordsEvents" runat="server" Width="200px" 
            Columns="16"></asp:TextBox>
        </span>
    </div>
    <div class="field">
        <span class="label">
            <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" />
        </span><span class="entry">
            <asp:DropDownList ID="ddlFindByCategory" runat="server" Width="200px">
                <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </span>
    </div>
    <div class="button">
        <asp:Button ID="btnFind" runat="server" meta:resourcekey="btnFind" OnClick="BtnFindEventsClick" />
    </div>
    </form>
</asp:Content>
