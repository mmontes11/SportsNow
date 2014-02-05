<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Groups.CreateGroup"
    MasterPageFile="~/SportsNow.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="CreateAccountForm" method="post" runat="server">
        <p>
            <asp:Label ID="lblGroupAlreadyExists" meta:resourcekey="lblGroupAlreadyExists" ForeColor="red" Visible="False" runat="server"></asp:Label></p>     
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclGroupName" runat="server" Text="<%$ Resources:Common, groupName %>" />
            </span><span class="entry">
                <asp:TextBox ID="txtGroupName" runat="server" Width="200px" Columns="16" 
                MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="typeGroupNameValidator" runat="server" ControlToValidate="txtGroupName"
                    ValidationExpression="^([\S\s]{0,20})$" meta:resourcekey="typeGroupNameValidator"
                    CssClass="errorMessage" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                    Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>" CssClass="errorMessage"></asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclGroupDescription" runat="server" Text="<%$ Resources:Common, groupDescription %>" /></span>
            <span class="entry">
                <asp:TextBox ID="txtGroupDescription" runat="server" Width="200px" 
                Columns="16" Height="80px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                <asp:RegularExpressionValidator ID="typeGroupDescriptionValidator" runat="server"
                    ControlToValidate="txtGroupDescription" ValidationExpression="^([\S\s]{0,100})$"
                    meta:resourcekey="typeGroupDescriptionValidator" CssClass="errorMessage" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvGroupDescription" runat="server" ControlToValidate="txtGroupDescription"
                    Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>" CssClass="errorMessage"></asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="button">
            <asp:Button ID="btnCreate" runat="server" meta:resourcekey="btnCreate" OnClick="BtnCreateGroupClick" />
        </div>
        </form>
        <br />
        <br />
    </div>
</asp:Content>
