<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyGroups.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Groups.MyGroups"
    MasterPageFile="~/SportsNow.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div>
        <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <p>
            <asp:Label ID="lblNoUserGroups" meta:resourcekey="lblNoUserGroups" runat="server"
                Visible="False"></asp:Label></p>
        <p>
            <asp:Label ID="lblUnsubscribedUser" meta:resourcekey="lblUnsubscribedUser" ForeColor="Green"
                runat="server" Visible="False"></asp:Label></p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvUserGroups" runat="server" AutoGenerateColumns="False" CellPadding="2"
                    CssClass="tableInfo" GridLines="None" OnPageIndexChanging="GvUserGroupsIndexChanging"
                    OnRowCommand="gvUserGroups_RowCommand">
                    <AlternatingRowStyle BackColor="LightGray" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="<%$ Resources:Common, groupId %>" />
                        <asp:BoundField DataField="name" HeaderText="<%$ Resources:Common, groupName %>" />
                        <asp:BoundField DataField="descrip" HeaderText="<%$ Resources:Common, groupDescription %>" />
                        <asp:ButtonField ButtonType="Button" CommandName="UnsubscribeUser" HeaderText="<%$ Resources:Common, unsubscribe %>"
                            ItemStyle-HorizontalAlign="Center" ShowHeader="True" Text="-">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                    </Columns>
                    <HeaderStyle BackColor="#097099" ForeColor="White" HorizontalAlign="Center" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        </form>
    </div>
</asp:Content>
