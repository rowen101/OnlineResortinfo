<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true"
    CodeBehind="frm.aspx.cs" Inherits="online_adds.pages.frm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
               Category
            </h1>
            <ol class="breadcrumb">
                <li>
                    <asp:LinkButton ID="lnkhome" runat="server" Text="Home" onclick="lnkhome_Click" 
                        ></asp:LinkButton>
                    </li>
                <li class="active">
                    <asp:Label ID="lblblog" runat="server"></asp:Label></li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <asp:ListView ID="ListView_blog" runat="server" ItemPlaceholderID="id">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <h1>
                        <a href='Destination.aspx?frm=read&id=<%# Eval("pst_ID") %>'>
                            <%# Eval("pst_title") %></a></h1>
                    <p class="lead">
                        by&nbsp;<%# Eval("Username")%></p>
                    <hr>
                    <p>
                        <i class="fa fa-clock-o"></i>&nbsp;Posted&nbsp;in&nbsp;<%# Eval("category")%>&nbsp;&nbsp;on&nbsp;<%# Eval("dte") %>&nbsp;
                        <a href='Destination.aspx?frm=read&id=<%# Eval("pst_ID") %>'>
                            <%# Eval("comment_count") %>&nbsp;Comment</a></p>
                    <hr>
                    <a href='Destination.aspx?frm=read&id=<%# Eval("pst_ID") %>'>
                        <img src='../images/oreginal/<%# Eval("Image") %>' class="img-responsive">
                    </a>
                    &nbsp;&nbsp;&nbsp;<hr>
                    <p>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>'></asp:Label>&nbsp;[...]
                    </p>
                    <a class="btn btn-primary" href='Destination.aspx?frm=read&id=<%# Eval("pst_ID") %>'>Read
                        More </a>
                    <hr>


                </ItemTemplate>
                <EmptyDataTemplate>
                    <span><b>Sorry these category is Empty Please Choose another Category</b></span>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
              <div class="col-lg-4">
            <div class="well">
                <h4>
                     Search Destination
                    
                    </h4>
                <div class="input-group">
                    <asp:TextBox cssClass="form-control" ID="txtsearchblg" runat="server"></asp:TextBox>
                    <span class="input-group-btn">
                    
                        <asp:Button cssClass="btn btn-default fa fa-search" ID="Button1" 
                        runat="server" Text="Search" onclick="Button1_Click"/>
                  
                    </span>
                </div>
                <!-- /input-group -->
            </div>
            <!-- /well -->
            <div class="well">
                
                <h4>
                    Categories</h4>
                <div class="row">
                    <div class="col-lg-6">
                        <ul class="list-unstyled">
                            <asp:ListView ID="Listcategory" runat="server" ItemPlaceholderID="id">
                            <LayoutTemplate>
                             <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                            <li><a href='frm.aspx?frm=req&retrive=<%# Eval("name") %>'><%# Eval("name") %></a> </li>
                            </ItemTemplate>
                            </asp:ListView>
                            
                        </ul>
                    </div>
                  
                </div>
            </div>
        </div>
</asp:Content>
