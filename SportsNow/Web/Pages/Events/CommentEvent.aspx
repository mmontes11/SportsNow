<%@ Page Title="" Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    CodeBehind="CommentEvent.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.CommentEvent" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="CommentForm" runat="server">
    <asp:Label ID="lblEventName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
    <div class="field">
        <span class="label">
            <asp:Localize ID="lclTags" runat="server" Text="<%$ Resources:Common, tags %>" />
        </span><span class="entry">
            <asp:TextBox ID="txtTags" runat="server" Width="200px"></asp:TextBox>
        </span>
    </div>
    <br />
    <div class="field">
        <span class="label">
            <asp:Localize ID="lclComment" runat="server" Text="<%$ Resources:Common, comment %>" />
        </span><span class="entry">
            <asp:TextBox ID="txtComment" runat="server" Width="200px" 
                Columns="16" Height="80px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="typeCommentValidator" runat="server"
                    ControlToValidate="txtComment" ValidationExpression="^([\S\s]{0,100})$"
                    meta:resourcekey="typeCommentValidator" CssClass="errorMessage" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" />
        </span>
    </div>
    <div class="button">
        <asp:Button ID="btnComment" runat="server" meta:resourcekey="btnComment" OnClick="BtnCommentClick" />
    </div>
    </form>
</asp:Content>
