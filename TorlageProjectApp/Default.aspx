﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TorlageProjectApp._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
#default {
  position: fixed;
  max-height: 100%;
  left: 0;
  right: 0;
	
  /* Preserve aspect ratio */
  min-height: 100%;
}

.body-content {
    padding-left: 0px;
    padding-right: 0px;
}
#picture{   
    width:355px;
    margin-right:20px;
    float:left;
}

    </style>
    

  

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ToConnectionString %>" SelectCommand="SELECT * FROM [Performers]"></asp:SqlDataSource>

    

  
    <div id="spacertopbar"></div>
        <div id="picture">
            <img width="350" height="500" alt="bg image" longdesc="background image"src="Resources/TorlageFinal.jpg" />
        </div>

    <!--img id="default" alt="bg image" longdesc="background image"src="Resources/TorlageFinal.jpg" /-->
    <!--img alt="bg image" longdesc="background image" src="Resources/Torlage1.jpg" style="width: 1166px; height: 619px"/-->
    

  

</asp:Content>
