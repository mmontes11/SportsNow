<%@ Page Title="" Language="C#" MasterPageFile="~/SportsNow.Master" AutoEventWireup="true"
    CodeBehind="Recommend.aspx.cs" Inherits="Es.Udc.DotNet.SportsNow.Web.Pages.Events.Recommend" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <p>
        <asp:Label ID="lblRecommendationSuccesful" meta:resourcekey="lblRecommendationSuccesful"
            runat="server" Visible="false" ForeColor="Green"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblParametersNotValid" meta:resourcekey="lblParametersNotValid" runat="server"
            Visible="False" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblDuplicatedRecommendation" meta:resourcekey="lblDuplicatedRecommendation"
            runat="server" Visible="False" ForeColor="Red"></asp:Label>
    </p>
    <form id="form1" runat="server">
    <asp:Label ID="lblEventName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" Style="margin-top: 2px;">
        <asp:TextBox ID="recommendTextBox" runat="server" placeholder="Make a recommendation..."
            Width="200px" Columns="16" Height="80px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
        <div style="text-align: left; width: 150px; margin: auto">
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
        </div>
        <div class="button">
            <asp:Button ID="btnComment" runat="server" meta:resourcekey="btnRecommend" OnClick="BtnRecommendClick" />
        </div>
    </asp:Panel>
    </form>
</asp:Content>
