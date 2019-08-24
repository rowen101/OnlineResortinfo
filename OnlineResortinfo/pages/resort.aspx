<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="resort.aspx.cs" Inherits="OnlineResortinfo.pages.resort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css" href="../css/pirobox.css"  rel="Stylesheet"/>
    <style type="text/css">
        .activemenu
        {
            background-color: #141414;
            color:white;
        }
        .txtcomment
        {
          width: 591px; 
          height: 140px;
        }
    </style>
  <link rel="stylesheet" type="text/css" href="../styles.css"></link>

<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="../js/cufon-yui.js"></script>
<script type="text/javascript" src="../fonts/aura_400.font.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $('#Resort').addClass('activemenu');
    </script>
    <div class="wrap-content zerogrid">
        <div class="row block03">
            <div class="col-2-3">
                <asp:MultiView ID="MultiView1" runat="server">
                    <!--view all post-->
                    <asp:View ID="View1" runat="server">
                        <div class="wrap-col">
                            <asp:ListView ID="listresort" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <article>
						<a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><img src='../../images/postimg/<%# Eval("filename") %>'/></a>
						<h2><a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title") %></a></h2>
						<div class="info">[By <%# Eval("username") %> on <%# Eval("dte") %> with <a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("comment_count") %> Commnets</a>]</div>
						<p><%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>&nbsp;[...]</p>
					                </article>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:View>
                    <!--view selected post-->
                    <asp:View ID="View2" runat="server">
                        <div class="wrap-col">
                       
                          
                        
                            
                            <article>
                        <asp:Image ID="imgpost" runat="server" />
                       
                        <h2><asp:HyperLink ID="hreftitle" runat="server"></asp:HyperLink></h2>
                        <asp:Label ID="lblpstinfo" runat="server"></asp:Label>
				
                        <asp:Label ID="lblcontentslcted" runat="server"></asp:Label>
			            
                            
                              </article>

                                 <!--gallery thumnails view-->
                               

						<style type="text/css">
						    .gthumb
						    {
						        width: 150px;
						        height: 150px;
						        margin: 0 0 8px 13px;
						        float: left;
						    }
						</style>
							
							
						<!---->
                        
                        <script type="text/javascript">
                            $(document).ready(function () {

                                /*Your ShineTime Welcome Image*/
                                var default_image = '../images/large/default.jpg';
                                var default_caption = 'Welcome to ShineTime';

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
                                    $('#loader').css('background-image', 'url("../images/interface/loader.gif")');
                                }

                                function hidePreloader() {
                                    $('#loader').css('background-image', 'url("")');
                                }

                            });
</script>

<div id="container">
<div id="containertitle">
    <asp:Label ID="gallerytitle" runat="server"></asp:Label>
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
       <asp:ListView ID="listgallery" runat="server" ItemPlaceholderID="id">
           <LayoutTemplate>
               <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
           </LayoutTemplate>
           <ItemTemplate>
             
                   <div class="thumbnailimage">
                       <div class="thumb_container">
                           <div class="large_thumb">
                               <img src="../../images/gallery/<%# Eval("image") %>" class="large_thumb_image" alt="thumb" />
                               <img src="../../images/gallery/<%# Eval("image") %>" class="large_image" rel="<%# Eval("filename") %>" />
                               <div class="large_thumb_border">
                               </div>
                               <div class="large_thumb_shine">
                               </div>
                           </div>
                       </div>
                   </div>
           </ItemTemplate>
       </asp:ListView>

                   
       <!--end entry-->          
       
  
	   
	      
   </div>
</div>	
					<!---->	
                         
                                
                     
                           <!--gallery thumnails view end-->

                           
                            
                            <asp:ListView ID="listcomment" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                            
                         
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>

                       
                                </LayoutTemplate>

                                <ItemTemplate>
                                    <article class="row">
					                <div class="col-1-3">
						                <div class="wrap-col">
							                <img src='../../images/profilepic/<%# Eval("Userimage") %><%# Eval("profilepic") %>'/>
						                </div>
					                </div>
					                <div class="col-2-3">
						                <div class="wrap-col">
							                <h2><a href="#">Comment by <%# Eval("username") %><%# Eval("name") %></a></h2>
							                <div class="info">Comment on <%# Eval("date") %> </div>
							                <p><%# Eval("c_content") %></p>
						                </div>
					                </div>

                          
				                </article>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="heading">
                                        <h4>
                                            No Comment</h4>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                       
                        </div>

                        <!--comment-->
                        <div class="comment">
                            <asp:MultiView ID="MultiView2" runat="server">
                            <!--cookie null-->
                                <asp:View ID="View3" runat="server">
                                    Your email address will not be published. Required fields are marked *
                                    <div>
                                        <asp:TextBox ID="txtname" runat="server" placeholder="Name"></asp:TextBox></div>
                                    <div>
                                        <asp:TextBox ID="txtemail" runat="server" placeholder="Email"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format" ForeColor="#FF3300"></asp:RegularExpressionValidator></div>
                                    <div>
                                        <asp:TextBox ID="txtwebsite" runat="server" placeholder="Website"></asp:TextBox></div>
                                     <div> <asp:TextBox CssClass="txtcomment" ID="txtcomment" runat="server" TextMode="MultiLine"></asp:TextBox></div>
                                </asp:View>
                                <!--cookie-->
                                <asp:View ID="View4" runat="server">
                                    <div><asp:Image ID="Image1" runat="server"  Height="64" Width="64"/></div>
                                    <div> <div> <asp:TextBox CssClass="txtcomment" ID="txtusercomment" runat="server" TextMode="MultiLine"></asp:TextBox></div></div>
                                </asp:View>
                            </asp:MultiView>
                            <div>
                                <asp:Button ID="btncomment" runat="server" Text="Submit" 
                                    onclick="btncomment_Click" /></div>
                        </div>
                        <br/>
                    </asp:View>
                </asp:MultiView>
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
</asp:Content>
