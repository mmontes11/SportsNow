<%@ Page Title="" Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    CodeBehind="ViewComments.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.ViewComments" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <p>
        <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server" Visible="false"></asp:Label>
    </p>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <asp:DataList ID="commentList" runat="server" CellPadding="4" ForeColor="black" Width="469px"
            DataKeyField="id" HorizontalAlign="Center">
            <AlternatingItemStyle BackColor="lightGray" Font-Bold="True" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
            <ItemTemplate>
                <asp:Label ID="lblUserField" meta:resourcekey="lblUserField" runat="server"></asp:Label>
                <asp:Label ID="lblUserProfile" runat="server" Text='<%# Eval("UserProfile.loginName") %>'>
                </asp:Label>
                <asp:Label ID="lblDateField" meta:resourcekey="lblDateField" runat="server"></asp:Label>
                <asp:Label ID="lblDate" runat="server">
                </asp:Label>
                <br />
                <asp:Label ID="lblCommentField" meta:resourcekey="lblCommentField" runat="server"></asp:Label>
                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("value") %>'>
                </asp:Label>
                <br />
                <asp:HyperLink ID="lnkChangeTags" runat="server" CssClass="cloudTags">
                    <asp:Label ID="lblTagsField" meta:resourcekey="lblTagsField" runat="server"></asp:Label>
                    <asp:Label ID="lblTags" runat="server">   
                    </asp:Label>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>
    </form>
</asp:Content>
