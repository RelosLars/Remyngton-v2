<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditTournaments.aspx.cs" Inherits="Remyngton_v2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="Tournament" runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="*Tournament name"></asp:Label>
    <br />
    <asp:TextBox ID="TournamentName" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="*Start date"></asp:Label>
    
    <asp:Label ID="Label2" runat="server" Text="List of Teams"></asp:Label>
    <asp:FileUpload ID="FileUploadTeamlist" runat="server" />
    <br />
    <asp:Button ID="EditTournament" runat="server" Text="Edit Tournament" OnClick="EditTournament_Click" />
</asp:Content>
