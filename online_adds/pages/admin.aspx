<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true"
    CodeBehind="admin.aspx.cs" Inherits="online_adds.pages.admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
              <h1><asp:Label  CssClass="page-header" ID="lbltitle" runat="server"></asp:Label></h1>
            <ol class="breadcrumb">
                <li><a href="home.aspx">Home</a></li>
                <li class="active">Admin</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
           
            <h2><asp:Label  CssClass="page-header" ID="lbltab" runat="server"></asp:Label></h2>
            <asp:ListView ID="listadmintab" runat="server" ItemPlaceholderID="id">
                <LayoutTemplate>
                    <ul id="myTab" class="nav nav-tabs">
                        <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li><a href='#<%# Eval("menu_name") %>' data-toggle="tab">
                        <%# Eval("menu_name") %></a> </li>
                </ItemTemplate>
            </asp:ListView>
            <div id="myTabContent" class="tab-content">
                <asp:ListView ID="listidtab" runat="server" ItemPlaceholderID="tabid">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="tabid"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="tab-pane fade" id='<%# Eval("menu_name") %>'>
                           
                            <iframe src='<%# Eval("alias") %>' style="border: none; width:100%;height:850px "></iframe>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
               
            </div>
        </div>
    </div>
</asp:Content>
