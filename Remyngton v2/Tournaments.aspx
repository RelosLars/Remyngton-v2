<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tournaments.aspx.cs" Inherits="Remyngton_v2.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="*Tournament name"></asp:Label>
    <br />
    <asp:TextBox ID="TournamentName" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="*Start date"></asp:Label>
    <asp:Calendar ID="StartDate" runat="server"></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="List of Teams"></asp:Label>
    <asp:FileUpload ID="FileUploadTeamlist" runat="server" />
    <br />
    <asp:Button ID="CreateTournament" runat="server" Text="Create Tournament" OnClick="CreateTournament_Click" />
    
</asp:Content>
