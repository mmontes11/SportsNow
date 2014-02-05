<%@ Page Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    Codebehind="UpdateUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.User.UpdateUserProfile"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="UpdateUserProfileForm" method="POST" runat="server">
            <asp:HyperLink ID="lnkChangePassword" runat="server" 
                NavigateUrl="~/Pages/User/ChangePassword.aspx"
                meta:resourcekey="lnkChangePassword"/>
            <div class="field">
                <span class="label"><asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span class="entry">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ControlToValidate="txtFirstName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span class="entry">
                    <asp:TextBox ID="txtSurname" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSurname" runat="server"
                        ControlToValidate="txtSurname" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span class="entry">
                    <asp:TextBox ID="txtEmail" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True" 
                    Width="100px" onselectedindexchanged="ComboLanguageSelectedIndexChanged">
                    </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" Width="100px">
                    </asp:DropDownList></span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" meta:resourcekey="btnUpdate"/>
            </div>
        </form>
    </div>
</asp:Content>
