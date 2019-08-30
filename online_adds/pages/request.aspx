<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true"
    CodeBehind="request.aspx.cs" Inherits="online_adds.pages.frm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script src="../js/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="../js/jquery-1.4.1.js"></script>
 <link type="text/css" rel="Stylesheet" href="../css/pirobox.css" />
     <script type="text/javascript" src="../js/pirobox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $().piroBox({
                my_speed: 400, //animation speed
                bg_alpha: 0.1, //background opacity
                slideShow: false, // true == slideshow on, false == slideshow off
                slideSpeed: 4, //slideshow duration in seconds(3 to 6 Recommended)
                close_all: '.piro_close,.piro_overlay'// add class .piro_overlay(with comma)if you want overlay click close piroBox

            });
        });
    </script>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/lightbox-2.6.min.js" type="text/javascript"></script>

   
    <style type="text/css">
        .promtedinformation
        {
            border: 1px solid #E6DB55;
            background-color: #FFFFE0;
        }
        .promtederror
        {
            border: 1px solid #CC0000;
            background-color: #FFEBE8;
        }
        .gtooltip
        {
            height: 40px;
            background: black;
            opacity: 0.7;
            color: white;
            text-align: center;
            margin: -40px 0 0 0;
            transition: all 200ms linear 0s;
            text-decoration: none;
        }
    </style>
    <!-- Pirobox setup and styles end-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                <asp:Label ID="lblslct_title" runat="server"></asp:Label>
                <asp:Label ID="smltooltip" runat="server"></asp:Label>
            </h1>
           
                    <asp:Label ID="lblsitemap" runat="server"></asp:Label></li>
            
        </div>
    </div>
    <div class="row">

        <asp:MultiView ID="MultiView1" runat="server">
            <!--gallery-->
          
            <asp:View ID="View1" runat="server">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View7" runat="server">
                       <div class="col-lg-12">
                            <asp:ListView ID="listviewgallery" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <div class="row">
                                        <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                    </div>
                                   
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <!-- /.row -->
                                     <div class="col-md-3 portfolio-item">
                                        <a href='request.aspx?qZcZSFkBxBw=Fa7v03FdlaUM7Tb4YLoWLg=&3FE8tKkfmgY=<%# Eval("gallery_id") %>'
                                            title="<%# Eval("title") %>">
                                            <img class="img-responsive" src='../images/gthumbs/<%# Eval("filename") %>' />
                                            <div class="gtooltip">
                                                <%# Eval("title") %></div>
                                        </a>
                                    </div>
                                </ItemTemplate>
                                 <EmptyDataTemplate>
                                        <spam>There’s no item in gallery</spam>
                                    </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </asp:View>
                    <asp:View ID="View8" runat="server">
                    

                        <div class="col-lg-12">
                            <asp:Label ID="lblselectedgallery" runat="server" Visible="False"></asp:Label>
                            <div class="row">
                            <div class="col-md-3 portfolio-item">
                              
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="image-set">
                            <asp:ListView ID="listgalleryslcted" runat="server" ItemPlaceholderID="id">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                         <div class="col-md-3 portfolio-item">
                   <a href='../images/goreginal/<%# Eval("image")%>' title='<%# Eval("description") %>' class="pirobox_header"><img class="img-responsive" alt="sfs" src='../images/gthumbs/<%# Eval("image")%>'/></a>                     
                                        
                                        </div>
                                    </ItemTemplate>

                                   
                                </asp:ListView>

                                
                            </div>
                                
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </asp:View>
            <!--end-->
            <!--Contact-->
            <asp:View ID="View2" runat="server">
                <div class="col-lg-8">
                    <!-- insert the page content here -->
                    <div class="well">
                    
                            <asp:Label ID="lblmsgbox" runat="server"></asp:Label>
                            <br />
                            Name
                            <asp:TextBox CssClass="form-control" ID="txtname" runat="server" Width="200"></asp:TextBox>
                            Email:
                            <asp:TextBox CssClass="form-control" ID="txtemail" runat="server" Width="200"></asp:TextBox>
                            Message:<asp:TextBox CssClass="form-control" Rows="6" ID="txtbody" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                  
                        <br />
                        <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Sent Message"
                            OnClick="Button1_Click" />
                    </div>
                </div>
            </asp:View>
            <!--end-->
            <!--faq-->
            <asp:View ID="View3" runat="server">
                <div class="col-lg-12">
                    <asp:Label ID="lblfaq" runat="server"></asp:Label>
                </div>
            </asp:View>
            <!--end-->
            <!--contact-->
            <asp:View ID="View4" runat="server">
            </asp:View>
            <!--end-->
            <!--logout-->
            <asp:View ID="View5" runat="server">
            </asp:View>
            <!--end-->
            <!--other-->
            <!--About us-->
            <asp:View ID="View6" runat="server">
                <div class="col-lg-12">
                    <asp:Label ID="lblaboutus" runat="server"></asp:Label>
                </div>
            </asp:View>
            <!--end-->
        </asp:MultiView>
        <asp:Panel ID="Panelsidebar" runat="server">
            <div class="col-lg-4">
                <div class="well">
                    <h4>
                        Search Destination
                    </h4>
                    <div class="input-group">
                        <asp:TextBox CssClass="form-control" ID="txtsearchblg" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button CssClass="btn btn-default fa fa-search" ID="Button2" runat="server" Text="Search"
                                OnClick="Button2_Click" />
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
                                        <li><a href='frm.aspx?frm=req&retrive=<%# Eval("name") %>'>
                                            <%# Eval("name") %></a> </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /well -->
            </div>
        </asp:Panel>
    </div>
</asp:Content>
