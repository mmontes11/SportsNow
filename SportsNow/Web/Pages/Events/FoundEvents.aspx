<%@ Page Title="" Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    CodeBehind="FoundEvents.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.FoundEvents" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="Form1" runat="server">
    <p>
        <asp:Label ID="lblNoEvents" meta:resourcekey="lblNoEvents" runat="server"></asp:Label></p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvEvents" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" CssClass="tableInfo" GridLines="None" HeaderStyle-HorizontalAlign="Center"
                OnPageIndexChanging="GvEventsPageIndexChanging" OnRowCommand="gvEvents_RowCommand">
                <AlternatingRowStyle BackColor="LightGray" />
                <Columns>
                    <asp:BoundField DataField="eventId" HeaderText="<%$ Resources:Common, eventID%>" />
                    <asp:BoundField DataField="eventName" HeaderText="<%$ Resources:Common, nameEvent%>" />
                    <asp:BoundField DataField="categoryName" HeaderText="<%$ Resources:Common, nameCategory%>" />
                    <asp:BoundField DataField="eventDate" HeaderText="<%$ Resources:Common, eventDate%>" />
                    <asp:ButtonField ButtonType="Button" CommandName="Comment" HeaderText="<%$ Resources:Common, comment %>"
                        ItemStyle-HorizontalAlign="Center" ShowHeader="True" Text="+" />
                    <asp:ButtonField ButtonType="Button" CommandName="ViewComments" HeaderText="<%$ Resources:Common, viewComments %>"
                        ItemStyle-HorizontalAlign="Center" ShowHeader="True" Text="View" />
                    <asp:ButtonField ButtonType="Button" CommandName="Recommend" HeaderText="<%$ Resources:Common, recommend %>"
                        ItemStyle-HorizontalAlign="Center" ShowHeader="True" Text="+" />
                </Columns>
                <HeaderStyle BackColor="#097099" ForeColor="White" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    </form>
</asp:Content>
