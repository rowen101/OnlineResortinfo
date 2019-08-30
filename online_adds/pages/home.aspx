<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="online_adds.pages.home" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <title></title>
    <link rel="shortcut icon" href="../images/1379608351_95057.ico" />
    <!-- Bootstrap core CSS -->
    <link href="../css/bootstrap.css" rel="stylesheet">
    <!-- Add custom CSS here -->
    <link href="../css/modern-business.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link type="text/css" rel="Stylesheet" href="../css/modal.css" />
    <style type="text/css">
        .gtooltip
        {
            height: 80px;
            background: black;
            opacity: 0.7;
            color: white;
            margin: -110px 0 20px 0;
            transition: all 200ms linear 0s;
            text-decoration: none;
        }
        .activemenu
        {
            background-color: #000000;
        }
        :-moz-placeholder
        {
            color: #c9c9c9 !important;
            font-size: 13px;
        }
        
        ::-webkit-input-placeholder
        {
            color: #ccc;
            font-size: 13px;
        }
        .error
        {
            background-color: #ffebe8;
            border: 1px solid #dd3c10;
            padding: 5px 0;
            text-align: center;
            margin-top: 10px;
        }
        .ok
        {
            border: 1px solid blue;
            background-color: skyblue;
            text-align: center;
            margin-top: 10px;
            padding: 5px 0;
        }
        fieldset
        {
            width: auto;
            margin: 0 0 0px 0px;
            margin-bottom: 1em;
            display: block;
        }
    </style>
    <script type="text/javascript">

         function ok(sender, e) {
             $find('ModalPopupExtenderLogin').hide();
             __doPostBack('LoginBtn', e);
         }
         function okJoin(sender, e) {
             $find('ModalPopupExtenderSignup').hide();
             __doPostBack('JoinBtn', e);
         }
         function OnKeyPress(args) {
             if (args.keyCode == Sys.UI.Key.esc) {
                 $find("ModalPopupExtenderLogin").hide();
             }
         }
         function body_onkeydown() {
             if (event.keyCode == 13 || event.keyCode == 27) {
                 var _defaultButtonName = getDefautButtonName(event.keyCode == 13 ? "submitButton" : "cancelButton");
                 var frm = document.forms[0];
                 if (frm && document.all(_defaultButtonName)) {
                     document.all(_defaultButtonName).click();
                 }
             }
         }

         function getDefautButtonName(className) {
             var _defaultButtonName = "";
             var children = document.getElementsByTagName("input");
             for (var i = 0; i < children.length; i++) {
                 var child = children[i];
                 var btnAction = child.buttonAction;
                 if (btnAction == className) {
                     _defaultButtonName = child.id;
                     break;
                 }
             }
             return _defaultButtonName;
         }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderLogin" runat="server" 
             TargetControlID="lnklogin"
             PopupControlID="LoginPanel"
             BackgroundCssClass="modalBackground"
             DropShadow="true"
             OkControlID="LoginBtn"
             OnOkScript="ok()"
             CancelControlID="CancelBtn" />    
       <!--panel login-->     
    <asp:Panel ID="LoginPanel" runat="server" CssClass="modalPopup" Style="display: none" Height="220px" Width="300px" Font-Names="@MS PGothic">
        <center>
            <asp:Label ID="promt" runat="server"></asp:Label>
            <asp:Label ID="Label1" runat="server"></asp:Label></center>
        <br />
        <fieldset>
            <legend>Login</legend>
            <table>
                <tr>
                    <td>Username</td>
                    <td>  <asp:TextBox CssClass="form-control" ID="txtuser" placeholder="UserName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td> <asp:TextBox CssClass="form-control" ID="txtpass" placeholder="Password" runat="server"
                TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><center><asp:CheckBox ID="chkRememberMe" runat="server" Text="&nbsp;Remind me" /></center></td>
                </tr>
                <tr>
                    <td colspan="2"><center> <asp:Button ID="LoginBtn" runat="server" 
                            CssClass="btn btn-primary" Text="Log In"
                 onclick="LoginBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server"  CssClass="btn btn-default" Text="Cancel" Width="52px" buttonAction="submitButton" /></center></td>
                </tr>
            </table>
        </fieldset>
 
   
    </asp:Panel>
    <!--end panel-->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>

                </button>
                <!-- You'll want to use a responsive image option so this logo looks good on devices - I recommend using something like retina.js (do a quick Google search for it and you'll find it) -->
               
               
<a Class="navbar-brand" style="font-family:Monotype Corsiva;font-size:25px;" href="home.aspx"><asp:Label ID="lbl_sitename" runat="server"></asp:Label></a>
            </div>
            
            
             <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse navbar-ex1-collapse">
                    
			        <ul
			      <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="id">
						<LayoutTemplate>
							 class="nav navbar-nav navbar-right">
								<asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
							
							
						</LayoutTemplate>
						<ItemTemplate>
							<li id='<%# Eval("menu_name") %>'><a href='<%# Eval("alias") %>'>
								<%# Eval("menu_name") %></a></li>
						</ItemTemplate>
				 </asp:ListView>
                   <li> <asp:Label ID="lbladmin" runat="server" Visible="False"></asp:Label></li>
			        <li><asp:Label ID="lbluser" runat="server"></asp:Label></li>
                    <li><asp:LinkButton ID="lnklogin" runat="server" Text="Login" 
                           ></asp:LinkButton></li>
               <li><asp:LinkButton ID="lblSignUp" runat="server" onclick="lblSignUp_Click">Register</asp:LinkButton></li>
			        </ul>

			   
            </div>
   
	          <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <div id="myCarousel" class="carousel slide">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0"></li>
            <asp:ListView ID="ListcarouselInd" runat="server" ItemPlaceholderID="id">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <li data-target="#myCarousel" data-slide-to='<%# Eval ("ID") %>'></li>
                </ItemTemplate>
            </asp:ListView>
        </ol>
        <!-- Wrapper for slides -->
        <div class="carousel-inner ">
            <div class="item active">
                <asp:Image CssClass="fill" ID="Imagebannerdef" runat="server" />
                <div class="carousel-caption">
                    <h3 style="font-family:Monotype Corsiva;font-size:23px;">
                        Welcome to Danasan Eco Adventure Park
                        
                </div>
            </div>
            <asp:ListView ID="ListWrapper" runat="server" ItemPlaceholderID="id">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="item">
                        <img style="border-width: 0px;" class="fill" src='../../images/banner/<%# Eval ("filename") %>' />
                        <div class="carousel-caption">
                            <h3 style="font-family:Monotype Corsiva;font-size:23px;">
                                <%# Eval("description") %></h3>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <!-- Controls -->
        <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="icon-prev">
        </span></a><a class="right carousel-control" href="#myCarousel" data-slide="next"><span
            class="icon-next"></span></a>
    </div>
    <div class="section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <hr>
                </div>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                <asp:ListView ID="Listthumbs" runat="server" ItemPlaceholderID="id">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-4 col-sm-6" >
                            <%-- class="img-responsive img-home-portfolio"--%>
                           <%-- <div style="margin-right:5px; margin-bottom:20px;">--%>
                            <a href='Destination.aspx?frm=read&id=<%# Eval ("pst_id") %>'>
                                <img  class="img-responsive img-home-portfolio" src='../images/oreginal/<%# Eval("image") %>' />
                            </a>
                            <div class="gtooltip">
                                <a href='Destination.aspx?frm=read&id=<%# Eval ("pst_id") %>'><b>
                                    <%# Eval("pst_title")%></b></a><br />
                                <%# Eval("pst_content").ToString().PadRight(140).Substring(0, 140).TrimEnd()%>&nbsp;[...]</div>
                        <%--</div>--%>
                            </div>
                            
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </div>
    <!-- /.section -->
    <div class="container">
        <hr>
        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p><asp:Label ID="lblfootertitle" runat="server"></asp:Label>&nbsp;Copyright &copy;&nbsp;<asp:Label ID="lblyr" runat="server"></asp:Label></p>
                </div>
            </div>
        </footer>
    </div>
    <!-- /.container -->
    <!-- JavaScript -->
    <script src="../js/jquery-1.10.2.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../js/modern-business.js"></script>

    <script>
        $('#Home').addClass('activemenu');
        </script>
    </form>
    
</body>
</html>
