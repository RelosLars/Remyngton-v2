﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Remyngton_v2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <br />

    <asp:CheckBox ID="isTournament" runat="server" Text="Tournament match" OnCheckedChanged="isTournament_CheckedChanged"/>
    <br />
    <asp:DropDownList ID="Tournaments" Visible="false" runat="server"></asp:DropDownList>
    <br />
    <asp:TextBox ID="PlayerCount" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="MpLink" runat="server"></asp:TextBox>
    <asp:Button ID="getMatch" runat="server" OnClick="GetMatch" Text="View Scores"/>
    <asp:Button ID="Button1" runat="server" Text="refresh"/>
</asp:Content>
