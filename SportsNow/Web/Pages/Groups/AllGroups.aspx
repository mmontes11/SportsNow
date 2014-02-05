<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllGroups.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Groups.AllGroups"
    MasterPageFile="~/SportsNow.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div>
        <form id="form" runat="server">
        <p>
            <asp:Label ID="lblNoUserGroups" meta:resourcekey="lblNoUserGroups" runat="server"></asp:Label></p>
        <p>
            <asp:Label ID="lblUserSubscribed" meta:resourcekey="lblUserSubscribed" runat="server"
                ForeColor="Green"></asp:Label></p>
        <asp:GridView ID="gvUserGroups" runat="server" CssClass="tableInfo" GridLines="None"
            AutoGenerateColumns="False" OnRowCommand="gvUserGroups_RowCommand" CellPadding="2">
            <AlternatingRowStyle BackColor="LightGray" />
            <Columns>
                <asp:BoundField DataField="groupId" HeaderText="<%$ Resources:Common, groupId %>" />
                <asp:BoundField DataField="groupName" HeaderText="<%$ Resources:Common, groupName %>" />
                <asp:BoundField DataField="groupDescription" HeaderText="<%$ Resources:Common, groupDescription %>" />
                <asp:BoundField DataField="numRecomendaciones" HeaderText="<%$ Resources:Common, numRecommendations %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="numMiembros" HeaderText="<%$ Resources:Common, numMembers %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField ButtonType="Button" CommandName="SubscribeUser" HeaderText="<%$ Resources:Common, subscribe %>"
                    ShowHeader="True" Text="+" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
            </Columns>
            <HeaderStyle BackColor="#097099" ForeColor="White" HorizontalAlign="Center" />
        </asp:GridView>
        <br />
        <div>
            <span>
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span>
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
        </form>
    </div>
</asp:Content>
