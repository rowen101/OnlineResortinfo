<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="OnlineResortinfo._default" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html lang="en"> <!--<![endif]-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
     <style type="text/css">
        .TextLightI
        {
            font-family: "Segoe UI Light" , "Segoe UI Web Light" , "Segoe UI Web Regular" , "Segoe UI" , "Segoe UI Symbol" , "HelveticaNeue-Light" , "Helvetica Neue" ,Arial !important;
        }
        .WatermarkedInputContainer
        {
            position: relative;
        }
       
    </style>
 
    <!-- Basic Page Needs
  ================================================== -->
	<meta charset="utf-8">
	<title>zBoomMusic Free Html5 Responsive Template</title>
	<meta name="description" content="Free Html5 Templates and Free Responsive Themes Designed by Kimmy | zerotheme.com">
	<meta name="author" content="www.zerotheme.com">
	<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
	<meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <!-- Mobile Specific Metas
  ================================================== -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    
    <!-- CSS
  ================================================== -->
  	<link rel="stylesheet" href="../css/zerogrid.css">
	<link rel="stylesheet" href="../css/style.css">
    <link rel="stylesheet" href="../css/responsive.css">
	<link rel="stylesheet" href="../css/responsiveslides.css" />
	<link rel="stylesheet" href="../css/modal.css" />
	<!--[if lt IE 8]>
       <div style=' clear: both; text-align:center; position: relative;'>
         <a href="http://windows.microsoft.com/en-US/internet-explorer/products/ie/home?ocid=ie6_countdown_bannercode">
           <img src="http://storage.ie6countdown.com/assets/100/images/banners/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today." />
        </a>
      </div>
    <![endif]-->
    <!--[if lt IE 9]>
		<script src="js/html5.js"></script>
		<script src="js/css3-mediaqueries.js"></script>
	<![endif]-->
	

	<script src="../js/jquery.min.js"></script>
	<script src="../js/responsiveslides.js"></script>
	<script>
	    $(function () {
	        $("#slider").responsiveSlides({
	            auto: true,
	            pager: false,
	            nav: true,
	            speed: 500,
	            maxwidth: 962,
	            namespace: "centered-btns"
	        });
	    });
	</script>
   <link type="text/css"  rel="Stylesheet"  href="../css/menu.css" />
</head>
<body>
    <form id="form1" runat="server">
   <!--------------Header--------------->
   <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

    <asp:Label ID="lbladminbar" runat="server"></asp:Label>
  
<header>
<br/>
<div class="zerogrid" style="padding:10px 0 10px 0;">
   <span style="float:right;font-size:20px;" class="TextLightI WatermarkedInputContainer" >
<asp:LinkButton ID="lbluser" runat="server"></asp:LinkButton>
   <asp:LinkButton ID="Loginlnk" runat="server" Text="Login" 
        onclick="Loginlnk_Click1"></asp:LinkButton>
<asp:LinkButton ID="lblSignUp" runat="server" Text="Register" 
        onclick="lblSignUp_Click1"></asp:LinkButton>
<asp:Label ID="successLabel" runat="server" ></asp:Label>
   </span>
</div>
	<div class="wrap-header zerogrid">
		<div id="logo"><a href="home.aspx">
      <p style="font-size:35px;"><asp:Label CssClass="TextLightI WatermarkedInputContainer"  ID="lbltitle" runat="server"></asp:Label></a></p>
        </a>
        </div>
		
		<div id="search">
		 <asp:LinkButton CssClass="button-search" ID="lnksearch" runat="server" 
                onclick="lnksearch_Click"></asp:LinkButton>
			<asp:TextBox ID="txtsearch" placeholder="Search..." runat="server"></asp:TextBox>
		</div>
	</div>
</header>

<nav>
	<div class="wrap-nav zerogrid">
		<div class="menu">
			<ul>
            <asp:ListView ID="listmenunav" runat="server" ItemPlaceholderID="id">
                    <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                    <li id="<%# Eval("menu_name") %>"><a href='<%# Eval("alias") %>'><%# Eval("menu_name") %></a></li>
                
                    </ItemTemplate>
                </asp:ListView>
			
			</ul>
		</div>
		
		<div class="minimenu"><div>MENU</div>
			<select onchange="location=this.value">
			
                 <asp:ListView ID="listminimenu" runat="server" ItemPlaceholderID="id">
                    <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                    <li><a href='<%# Eval("alias") %>'><%# Eval("menu_name") %></a></li>
                <option value='<%# Eval("alias") %>'><%# Eval("menu_name") %></option>
                    </ItemTemplate>
                </asp:ListView>
				
			</select>
		</div>		
	</div>
</nav>

<div class="featured">
	<div class="wrap-featured zerogrid">
		<div class="slider">
			<div class="rslides_container">
				<ul class="rslides" id="slider">
					<li><img src="../images/slide1.png"/></li>
					<li><img src="../images/slide2.png"/></li>
					<li><img src="../images/slide3.png"/></li>
				</ul>
			</div>
		</div>
	</div>
</div>

<!--------------Content--------------->
<section id="content">
	<div class="wrap-content zerogrid">
		
		<div class="row block02">
			<div class="col-2-3">
				<div class="wrap-col">
					<div class="heading"><h2>Resort</h2></div>

					<!--list Article-->
                        <asp:ListView ID="listarticle" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                             
                                <article class="row">
					                <div class="col-1-3">
						                <div class="wrap-col">
							                <img src='../images/postimg/<%# Eval("filename") %>'/>
						                </div>
					                </div>
					                <div class="col-2-3">
						                <div class="wrap-col">
							                <h2><a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title") %></a></h2>
							                <div class="info">By <%# Eval("username") %> on <%# Eval("dte") %> with <a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("comment_count") %> Commnets</a></div>
							                <p><%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>&nbsp;[...]</p>
						                </div>
					                </div>
				                </article>

                                    
                                </ItemTemplate>

                                 <EmptyDataTemplate>
                                <span><b>Oop!.. </b></span><br/>
     
                             </EmptyDataTemplate>
                            </asp:ListView>

                    <!--end Article-->
					
				</div>
			</div>
			<div class="col-1-3">
				<div class="wrap-col">
					
					<div class="box">
						<div class="heading"><h2>Category</h2></div>
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
</section>
<!--------------Footer--------------->
<footer>
	<div class="wrap-footer zerogrid">
		<div class="row block09">
		
			<div class="col-1-4">
				<div class="wrap-col">
					<div class="box">
						<div class="heading"><h2>Most Visited Post</h2></div>
						<div class="content">
                            <asp:ListView ID="Listlatestpost" runat="server" ItemPlaceholderID="id">
                           
                                <LayoutTemplate>
                                 <ul>
                                <asp:PlaceHolder ID="id" runat="server"></asp:PlaceHolder>
                                </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                <li><a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title")%></a></li>
                                </ItemTemplate>
                                
                            </asp:ListView>
					</div>
				</div>
                </div>
			</div>
				<div class="col-1-4">
				<div class="wrap-col">
					<div class="box">
						<div class="heading"><h2>Latest Post</h2></div>
						<div class="content">
							<div class="tag">
							<asp:ListView ID="listoppost" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title") %></a>
                                </ItemTemplate>
                            </asp:ListView>
							</div>
						</div>
					</div>
				</div>
			</div>
		
		</div>
		
		<div class="row copyright">
			<p>Copyright © &nbsp;<asp:Label ID="lblfooter" runat="server"></asp:Label></p>
		</div>
	</div>
</footer>
    </form>

   
</body>
</html>
