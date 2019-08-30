<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true"
    CodeBehind="Destination.aspx.cs" Inherits="online_adds.pages.Blog" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <div id="fb-root">
    </div>
    <script>        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=123579064392670";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>




<link rel="stylesheet" type="text/css" href="../styles.css"></link>

<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="../js/cufon-yui.js"></script>
<script type="text/javascript" src="fonts/aura_400.font.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
               Destination
            </h1>
            
                 
                    <asp:Label ID="lblsitemap" runat="server"></asp:Label>
            
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:UpdatePanel runat="server" ID="up">
                        <ContentTemplate>
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
                                    </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<hr>
                                    <p>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>'></asp:Label>&nbsp;[...]
                                    </p>
                                    <a class="btn btn-primary" href='Destination.aspx?frm=read&id=<%# Eval("pst_ID") %>'>Read
                                        More </a>
                                    <hr />
                                </ItemTemplate>

                                 <EmptyDataTemplate>
                                <span><b>Sorry, we couldn't find anything that matched your search.</b></span><br/>
     
                             </EmptyDataTemplate>
                            </asp:ListView>
                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="pg" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="col-lg-12">
                        <ul class="pagination">
                            <asp:DataPager runat="server" ID="pg" PagedControlID="ListView_blog" PageSize="5">
                                <Fields>
                                    <asp:NumericPagerField NumericButtonCssClass="btn btn-default" NextPageText="Next"
                                        PreviousPageText="Back" NextPreviousButtonCssClass="btn btn-primary" ButtonCount="5"
                                        CurrentPageLabelCssClass=" btn btn-primary" />
                                </Fields>
                            </asp:DataPager>
                        </ul>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <p>
                        <i class="fa fa-clock-o"></i>&nbsp; Posted&nbsp;by&nbsp;<asp:Label ID="lblauthor"
                            runat="server"></asp:Label>&nbsp;on&nbsp;
                        <asp:Label ID="lbldate" runat="server"></asp:Label>
                        <a href='#ycomment'>
                            <asp:Label ID="Label1" runat="server"></asp:Label></a>
                    </p>
                    <hr>
                    <asp:Label ID="lblpostimage" runat="server"></asp:Label>
                    <hr>
                    <p class="lead">
                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblcontent" runat="server" Text="Label"></asp:Label></p>




                        <!---->
                    <script type="text/javascript">
                        $(document).ready(function () {

                            /*Your ShineTime Welcome Image*/
                            var default_image = '';
                            var default_caption = '';

                            /*Load The Default Image*/
                            loadPhoto(default_image, default_caption);


                            function loadPhoto($url, $caption) {


                                /*Image pre-loader*/
                                showPreloader();
                                var img = new Image();
                                jQuery(img).load(function () {
                                    jQuery(img).hide();
                                    hidePreloader();

                                }).attr({ "src": $url });

                                $('#largephoto').css('background-image', 'url("' + $url + '")');
                                $('#largephoto').data('caption', $caption);
                            }


                            /* When a thumbnail is clicked*/
                            $('.thumb_container').click(function () {

                                var handler = $(this).find('.large_image');
                                var newsrc = handler.attr('src');
                                var newcaption = handler.attr('rel');
                                loadPhoto(newsrc, newcaption);

                            });

                            /*When the main photo is hovered over*/
                            $('#largephoto').hover(function () {

                                var currentCaption = ($(this).data('caption'));
                                var largeCaption = $(this).find('#largecaption');

                                largeCaption.stop();
                                largeCaption.css('opacity', '0.9');
                                largeCaption.find('.captionContent').html(currentCaption);
                                largeCaption.fadeIn()



                                largeCaption.find('.captionShine').stop();
                                largeCaption.find('.captionShine').css("background-position", "-550px 0");
                                largeCaption.find('.captionShine').animate({ backgroundPosition: '550px 0' }, 700);

                                Cufon.replace('.captionContent');


                            },

	 function () {
	     var largeCaption = $(this).find('#largecaption');
	     largeCaption.find('.captionContent').html('');
	     largeCaption.fadeOut();

	 });



                            /* When a thumbnail is hovered over*/
                            $('.thumb_container').hover(function () {
                                $(this).find(".large_thumb").stop().animate({ marginLeft: -7, marginTop: -7 }, 200);
                                $(this).find(".large_thumb_shine").stop();
                                $(this).find(".large_thumb_shine").css("background-position", "-99px 0");
                                $(this).find(".large_thumb_shine").animate({ backgroundPosition: '99px 0' }, 700);

                            }, function () {
                                $(this).find(".large_thumb").stop().animate({ marginLeft: 0, marginTop: 0 }, 200);
                            });

                            function showPreloader() {
                                $('#loader').css('background-image', 'url("images/interface/loader.gif")');
                            }

                            function hidePreloader() {
                                $('#loader').css('background-image', 'url("")');
                            }

                        });
                    </script>
                        <!---->
                      <!--slider-->
                    
                   <div id="container">
<div id="containertitle">
    <asp:Label ID="lblgallerytitle" runat="server"></asp:Label>
</div>
   <div class="mainframe">
      <div id="largephoto">
	  <div id="loader"></div>
	  
	  
	    <div id="largecaption">
		 <div class="captionShine"></div>
		   <div class="captionContent"></div>
		  
		</div>
		
       <div id="largetrans">  
      </div>
	        
      </div>
      
   </div>
   <div class="thumbnails">
   <br><br><br>

      <!-- start entry-->
       <asp:ListView ID="listslider" runat="server" ItemPlaceholderID="id">
           <LayoutTemplate>
               <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
           </LayoutTemplate>
           <ItemTemplate>
             

                 <div class="thumbnailimage">
                        <div class="thumb_container"> 
                            <div class="large_thumb"> 
                        		<img src="../images/goreginal/<%# Eval("Image") %>" width="54" height="54" class="large_thumb_image" alt="thumb" /> 
                                <img src="../images/goreginal/<%# Eval("Image") %>"  class="large_image" rel='<%# Eval("description") %>' />
                                <div class="large_thumb_border"></div>
                                <div class="large_thumb_shine"></div>
                              </div>
						</div>
                       </div>
           </ItemTemplate>
       </asp:ListView>


                  
       <!--end entry-->  

         </div>
</div>
                           
                                  
                      




                      <!--end slider-->

                    <!--respn-->
                    <h5>
                        <asp:Label ID="lblres" runat="server"></asp:Label>&nbsp;Responses&nbsp;to&nbsp;<asp:Label
                            ID="lblrestitle" runat="server"></asp:Label></h5>
                    <hr />
                    <div class="content">
                        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="id">
                            <LayoutTemplate>
                                <div id="ycomment">
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div style="background: whitesmoke;">
                                    <div style="background: #3276B1; height: 45px;">
                                        <div style="float: left;">
                                            <img src='../profilepic/thumb/<%# Eval ("image") %>' width="45" height="45" />
                                        </div>
                                        <div style="position: relative; margin: 0; top: 10%; color: white; font: normal  'trebuchet ms', arial, sans-serif;
                                            font-size: 15px;">
                                            &nbsp;Comment by&nbsp;<%# Eval("Username") %>&nbsp; Date:<%# Eval("date") %></div>
                                    </div>
                                    <div style="margin: 10px;">
                                        <p>
                                            <%# Eval("c_content") %>
                                        </p>
                                    </div>
                                </div>
                                <hr />
                            </ItemTemplate>
                        </asp:ListView>
                       <%-- <div class="fb-like-box" data-href="https://www.facebook.com/pages/Online-information-System/230535363767722?ref=tn_tnmn"
                            data-width="300" data-colorscheme="light" data-show-faces="true" data-header="true"
                            data-stream="false" data-show-border="false">
                        </div>--%>
                        <hr />
                        
                        <div class="well">
                            <asp:Panel ID="Panel1" runat="server">
                          <i>  <asp:LinkButton ID="lnklogin" runat="server" onclick="lnklogin_Click">Login</asp:LinkButton>&nbsp;to&nbsp;comment</i>
                               <%-- <h4>
                                    Leave a Comment:</h4>
                                <div>
                                    <div style="float: left;">
                                        <asp:Image CssClass="img-profile" ID="imguser" runat="server" />
                                    </div>
                                    <div style="float: left; margin: 0 10px 0  10px;">
                                        <asp:TextBox CssClass="form-control" placeholder="Your Name" ID="txtname" runat="server"
                                            Width="200"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:TextBox CssClass="form-control" placeholder="Email Address" ID="txtemail" runat="server"
                                            Width="200"></asp:TextBox>
                                    </div>
                                    <br />
                                </div>
                                <div>
                                    <asp:TextBox Rows="6" placeholder="Your Comment" CssClass="form-control" ID="txtdesEditor"
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <br />--%>
                            </asp:Panel>
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="headcom">
                                    <h4>
                                        Leave a Comment:</h4>
                                </div>
                                <div>
                                    <asp:Image CssClass="img-profile" ID="imguserlogin" runat="server" Width="45px" Height="45px" />
                                </div>
                                <br />
                                <asp:TextBox CssClass="form-control" ID="txtusercomment" Rows="6" runat="server"
                                    TextMode="MultiLine"></asp:TextBox>
                                <br />
                                 <div>
                                <asp:Button CssClass="btn btn-primary" ID="btnpublis" runat="server" Text="Submit"
                                    OnClick="btnpublis_Click" />
                            </div>
                            </asp:Panel>
                           
                        </div>
                    </div>
                </asp:View>
              
            </asp:MultiView>
        </div>
        <div class="col-lg-4">
            <div class="well">
                <h4>Search Destination </h4>
                    <div class="input-group">
                        <asp:TextBox CssClass="form-control" ID="txtsearchblg" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button CssClass="btn btn-default fa fa-search" ID="Button1" runat="server" Text="Search"
                                OnClick="Button1_Click" />
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
        </div>
    </div>


</asp:Content>
