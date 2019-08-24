<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frm.aspx.cs" Inherits="OnlineResortinfo.pages.frm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="MultiView1" runat="server">
        <!--gallery-->
        <asp:View ID="View1" runat="server">
            <div class="wrap-content zerogrid">
            <br/>
                <span class="TextLightI WatermarkedInputContainer" style="font-size: 25px; margin: 0 0 0  20px;">
                    <asp:Label ID="lblgallerysitemap" runat="server"></asp:Label> </span>
                <div class="row block03">
                    <asp:MultiView ID="MultiView2" runat="server">
                        <!--select gallery-->
                        <asp:View ID="View6" runat="server">
                            <asp:ListView ID="listgalleryallbum" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="col-1-4">
                                        <div class="wrap-col">
                                            <article>
						                       
                                                <a href='#'> <img src='../images/gallery/<%# Eval("filename") %>'/></a>
						                        <span class="TextLightI WatermarkedInputContainer" style="font-size:20px;"><a href='frm.aspx?req=galleryslct&id=<%# Eval("gallery_id") %>'><%# Eval("title") %></a></span>
					                        </article>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:View>
                        <!--selecte gallery-->
                        <asp:View ID="View7" runat="server">
                           
                            <div class="col-1-4">
                                <div class="wrap-col">
                                   
                                    <article>
                                        <asp:Image ID="imggallery" runat="server" />
                                     
						                <span class="TextLightI WatermarkedInputContainer" style="font-size:20px;"><asp:Label ID="lbltitlegallery" runat="server"></asp:Label></span>
					                </article>
                                </div>
                            </div>
                             <asp:ListView ID="listgallery" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="col-1-4">
                                        <div class="wrap-col">
                                            <article>
						                        <img src='galleryallbumview.aspx?imgtype=thumbnail&id=<%# Eval("gallery_id") %>'/>
						                        <span class="TextLightI WatermarkedInputContainer" style="font-size:20px;">sample</span>
					                        </article>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
           
        </asp:View>
        <!--Contact-->
        <asp:View ID="View2" runat="server">
          

        </asp:View>
        <!--faq-->
        <asp:View ID="View3" runat="server">
         <div class="wrap-content zerogrid">
          <br/>
                <span class="TextLightI WatermarkedInputContainer" style="font-size: 25px; margin: 0 0 0  20px;">
                   FAQ </span>
                <div class="row block03">
                    <div class="col-2-3">
                        <asp:Label ID="lblfaq" runat="server"></asp:Label>
                    </div>
                    <div class="col-1-3">
                        <div class="wrap-col">
                          
                            <div class="box">
                                <div class="heading">
                                    <h2>
                                        Category</h2>
                                </div>
                                <div class="content">
                                    <div class="list">
                                        <ul>
                                          
                                            <asp:ListView ID="listcategory" runat="server" ItemPlaceholderID="id">

                                            <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                             <li><a href='fr.aspx?res=category&rd=<%# Eval("id") %>'><%# Eval("name") %></a></li>
                                            </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <!--about-->
        <asp:View ID="View4" runat="server">
            <div class="wrap-content zerogrid">
               <br/>
                <span class="TextLightI WatermarkedInputContainer" style="font-size: 25px; margin: 0 0 0  20px;">
                   About </span>
                <div class="row block03">
                    <div class="col-2-3">
                        <asp:Label ID="lblabout" runat="server"></asp:Label>
                    </div>
                    <div class="col-1-3">
                        <div class="wrap-col">
                            
                            <div class="box">
                                 <div class="heading">
                                    <h2>
                                        Category</h2>
                                </div>
                                <div class="content">
                                    <div class="list">
                                        <ul>
                                          
                                            <asp:ListView ID="listcategory2" runat="server" ItemPlaceholderID="id">

                                            <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                              <li><a href='fr.aspx?res=category&rd=<%# Eval("id") %>'><%# Eval("name") %></a></li>
                                            </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <!--user page-->
        <asp:View ID="View5" runat="server">
          
        </asp:View>
    </asp:MultiView>
</asp:Content>
