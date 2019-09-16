﻿<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tournaments.aspx.cs" Inherits="Remyngton_v2.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="*Tournament name"></asp:Label>
    <br />
    <asp:TextBox ID="TournamentName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="List of Teams"></asp:Label>
    <asp:FileUpload ID="FileUploadTeamlist" runat="server" />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Mappool (can be added and edited later)"></asp:Label>
    <asp:FileUpload ID="FileUploadMappool" runat="server" />
    <br />
    <asp:Button ID="CreateTournament" runat="server" Text="Create Tournament" OnClick="CreateTournament_Click" />
    
</asp:Content>
