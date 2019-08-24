<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Remyngton_v2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="MpLink" runat="server"></asp:TextBox>
    <asp:Button ID="getMatch" runat="server" OnClick="GetMatch"/>
    <br />
    <asp:TextBox ID="PlayerCount" placeholder="Only enter for tournaments" runat="server"></asp:TextBox>

</asp:Content>
