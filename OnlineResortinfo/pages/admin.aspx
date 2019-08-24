<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="OnlineResortinfo.pages.admin" %>

<%@ Register TagPrefix="asp" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1; maximum-scale=1" />
    <meta name="description" content="website description" />
    <meta name="keywords" content="website keywords, website keywords" />
    <meta http-equiv="content-type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href="../css/admin.css" title="style" />
    <link type="text/css" rel="stylesheet" href="../css/lightbox.css" />
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#menu-icon").click(function () {
                $("#nav").slideToggle("slow");
            });
        });
    </script>
    <style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("../Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("../Images/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</head>
<body lang="en">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <div id="main">
        <div id="header">
            <!--For mobile Browser view-->
            <div id="modmenu">
                <div id="menu-icon">
                    <div class="title">
                        <a href="home.aspx">
                            <asp:Label ID="lbltitlem" runat="server"></asp:Label></a></div>
                </div>
                <ul id="nav" style="display: none;" class="mod-menu">
                    <asp:ListView ID="listmobilenav" runat="server" ItemPlaceholderID="id">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><a href='<%# Eval("alias") %>'>
                                <%# Eval("menu_name") %></a></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
            <!--For Disktop  view-->
            <div id="menu_header">
                <div id="logo_text">
                    <div style="margin-top: 10px; margin-left: 15px; font-size: 20px;">
                        <a href="home.aspx">
                            <asp:Label ID="lbltitled" runat="server"></asp:Label></a></div>
                </div>
                <div id="menubar">
                    <ul id="menu">
                        <asp:ListView ID="listdesktopnav" runat="server" ItemPlaceholderID="id">
                            <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li><a href='<%# Eval("alias") %>'>
                                    <%# Eval("menu_name") %></a></li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
        </div>
        <div id="site_content">
            <div id="content">
                <h2>
                    <asp:Label ID="lbladmindes" runat="server"></asp:Label></h2>
                <br />
                <asp:Label ID="lblpromted" runat="server"></asp:Label>
                 
                <asp:MultiView ID="MultiView1" runat="server">
                    <!--post-->
                    <asp:View ID="View1" runat="server">
                        <asp:MultiView ID="MultiView2" runat="server">
                            <!--view post-->
                            <asp:View ID="View7" runat="server">
                                <asp:Button ID="btnnewpost" runat="server" Text="Create Post" OnClick="btnnewpost_Click" /><br />
                                <asp:GridView CssClass="gridview" ID="GridViewpost" runat="server" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="GridViewpost_RowCommand">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpsdid" runat="server" Text='<%# Eval ("pst_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img width="44" height="44" class="sam" src='../images/profilepic/<%#Eval ("userimage") %>' />
                                                Posted by:<%# Eval("username")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title" SortExpression="pst_title">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval ("pst_ID") %>'
                                                    CommandName="post_edit">
                                                        
                                   <span style="color:#000000;"> <%# Eval("pst_title")%></span>
                                    
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dte" HeaderText="Date" ReadOnly="True" SortExpression="dte" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval ("pst_ID") %>'
                                                    CommandName="MyDelete"><spa style="color:#000000;">Delete</spa></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                             
                            </asp:View>
                            <!--Create post-->
                            <asp:View ID="View8" runat="server">
                       
                                <asp:MultiView ID="MultiView4" runat="server">
                                
                                    <asp:View ID="View14" runat="server">
                                         <asp:FileUpload ID="filepostimg" runat="server" />
                                         <asp:LinkButton ID="lnkview" runat="server" onclick="lnkview_Click"><span style="color:#000000;">Viewphoto</span></asp:LinkButton>
                                          <br /> <br />
                                    </asp:View>
                                    <asp:View ID="View15" runat="server">
                                        <asp:Image ID="photopost" runat="server" Width="200" Height="200" /><br />
                                         <asp:LinkButton ID="lnkupload" runat="server" onclick="lnkupload_Click"><span style="color:#000000;">Upload photo</span></asp:LinkButton>
                                    <br /> <br />
                                    </asp:View>
                                </asp:MultiView>
                               
                           
                            
                                   
                                    <asp:TextBox ID="txttitlepost" Placeholder="Post Title" runat="server"></asp:TextBox>
                               
                                <br />
                                <fieldset style="border: none;">
                                    <legend>Content</legend>
                                <%-- <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                      <<Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Editor" />
                                        </Triggers>
                                        <ContentTemplate>--%>
                                           <%-- <asp:HtmlEditor ID="Editor" runat="server" Height="400px" Width="600px" />--%>
                                     <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    <asp:UpdatePanel ID="viewPanel" runat="server">
                                        <ContentTemplate>
                                            <cc1:editor id="desEditor" runat="server" height="470px" width="800px" autofocus="true"
                                                htmlpanelcssclass="ajax__htmleditor_editor_default" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="desEditor" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </fieldset>

                                <table>
                                    <tr>
                                        <td>Category:</td>
                                        <td>
                                            <asp:DropDownList ID="Dropcategorypost" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                                         <asp:Label ID="Label2" runat="server"></asp:Label>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <!--end post-->
                    <!--comment-->
                    <asp:View ID="View2" runat="server">
                        <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="LinqDataSource1"
                            OnRowCommand="GridView1_RowCommand" PagerSettings-PageButtonCount="5">
                            <Columns>
                                <asp:TemplateField FooterText="Author" HeaderText="Author">
                                    <ItemTemplate>
                                        <br />
                                        <div>
                                            <div>
                                                <img src='../images/profilepic/<%# Eval("Userimage") %>' width="44" height="44" />
                                            </div>
                                            <div class="namecomment">
                                                <%# Eval("Username")%>
                                            </div>
                                            <div>
                                                <a style="color: #000000;" href='mailto:<%# Eval("c_email") %>'>
                                                    <%# Eval("c_email") %></a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Comment" HeaderText="Comment">
                                    <ItemTemplate>
                                        <%# Eval("c_content") %>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="In Responce To" HeaderText="In Responce To">
                                    <ItemTemplate>
                                        <%# Eval("comment_count") %>&nbsp;&nbsp;
                                        <%# Eval("post_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkevent" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                    CommandName="deletecomment"><span style="color:#000000;">Delete</span></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="OnlineResortinfo.databaselinqDataContext"
                            EntityTypeName="" Select="new (pst_id, user_id, Username,Userimage, c_status, c_ID, c_content, date, c_email, c_ip, comment_count, post_title)"
                            TableName="viewcomments">
                        </asp:LinqDataSource>
                    </asp:View>
                    <!--end comment-->
                    <!--user-->
                    <asp:View ID="View3" runat="server">
                        <asp:MultiView ID="MultiView3" runat="server">
                            <!--view user-->
                            <asp:View ID="View9" runat="server">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <asp:GridView ID="GridView2" CssClass="gridview" runat="server" AllowPaging="True" 
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    DataSourceID="LinqDataSource3" onrowcommand="GridView2_RowCommand">
                                     <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img class="sam" src='../images/profilepic/<%# Eval("Image") %>' width="44" height="44" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Username" SortExpression="username">
                                            <ItemTemplate>
                                                <a style="color: #000000;" href='admin.aspx?pg=rl&md=<%# Eval("id") %>'><%# Eval("username") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Name" SortExpression="fname">
                                            <ItemTemplate>
                                            
                                                    <%# Eval("fname") %>&nbsp;<%# Eval("lname") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Address" SortExpression="email">
                                            <ItemTemplate>
                                                <a style="color: #000000;" href='mailto:<%# Eval("email") %>'>
                                                    <%# Eval("email") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                      
                                        <asp:TemplateField HeaderText="Role" SortExpression="type">
                                            <ItemTemplate>
                                                <a style="color: #000000;" href='admin.aspx?pg=rl&md=<%# Eval("id") %>'>
                                                    <%# Eval("Permision")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            
                                                <asp:LinkButton ID="deletes" CommandArgument='<%# Eval("id") %>' CommandName="medelete" runat="server"> <span style="color: #000000;">Delete</span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              
                                <asp:LinqDataSource ID="LinqDataSource3" runat="server" 
                                    ContextTypeName="OnlineResortinfo.databaselinqDataContext" EntityTypeName="" 
                                    Select="new (id, username, lname, fname, email, Permision, image)" 
                                    TableName="view_allusers">
                                </asp:LinqDataSource>
                              
                            </asp:View>
                            <!--add role-->
                            <asp:View ID="View10" runat="server">
                                <asp:Label ID="lbluserpromted" runat="server"></asp:Label><br />
                                <asp:Image ID="Imguser" runat="server" /><br /><br />
                                <table>
                                    <tr>
                                        <td>
                                            Role
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="droprole" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnrolesave" runat="server" Text="Save" OnClick="btnrolesave_Click" />
                                <asp:Button ID="btnusercancel" runat="server" Text="Cancel" OnClick="btnusercancel_Click" />
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <!--end user-->
                    <!--setting-->
                    <asp:View ID="View4" runat="server">
                        <asp:Button Text="Site title" BorderStyle="None" ID="btnsitename" CssClass="Initial"
                            runat="server" OnClick="btnsitename_Click" />
                        <asp:Button Text="About" BorderStyle="None" ID="btnabout" CssClass="Initial" runat="server"
                            OnClick="btnabout_Click" Visible="false" />
                        <asp:Button Text="FAQ" BorderStyle="None" ID="btnfaq" CssClass="Initial" runat="server"
                            OnClick="btnfaq_Click" />
                        <asp:MultiView ID="MainView" runat="server">
                            <asp:View ID="View11" runat="server">
                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                    <tr>
                                        <td>
                                            
                                            <table>
                                                <tr>
                                                    <td>
                                                        Site Title:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtsitetitle" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            
                                        </td>
                                    </tr>
                                    <tr style="float:left;">
                                        <td><asp:Button ID="btnsitesave" runat="server" Text="Change" OnClick="btnsitesave_Click" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View12" runat="server">
                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                    <tr>
                                        <td>
                                            <fieldset style="border: none;">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="EditorAbout" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:HtmlEditor ID="EditorAbout" runat="server" Height="400px" Width="600px" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="float:left;">
                                            <asp:Button ID="btnAboutsave" runat="server" Text="Save" OnClick="btnAboutsave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View13" runat="server">
                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                    <tr>
                                        <td>
                                            <fieldset style="border: none;">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="EditorFAQ" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:HtmlEditor ID="EditorFAQ" runat="server" Height="400px" Width="600px" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr style="float:left;">
                                        <td><asp:Button ID="btnfaqsave" runat="server" Text="Save" OnClick="btnfaqsave_Click" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>
                    <!--end setting-->
                    <!--media-->
                    <asp:View ID="View5" runat="server">
                     
                        <asp:MultiView ID="MultiView5" runat="server">
                        <!--view gallery list-->
                            <asp:View ID="View16" runat="server">
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                <br />
                                <asp:GridView ID="Gridgallery_view" CssClass="gridview" runat="server" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    onrowcommand="Gridgallery_view_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("gallery_id") %>' CommandName="select"><img src='../images/postimg/<%# Eval("filename") %>' width="100" height="100" /></asp:LinkButton>
                                                   
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title" SortExpression="title">
                                            <ItemTemplate>
                                                <%# Eval("title") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="date" HeaderText="date" ReadOnly="True" SortExpression="Date" />
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View17" runat="server">
                        
                               <i>Upload photo</i><br/>
                                <asp:FileUpload ID="filegallerimg" runat="server" /><br/>

                                <asp:Button ID="Uploadimage" runat="server" Text="Save Image" 
                                    onclick="Uploadimage_Click" /><br/>

                                <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="id">
                                    <LayoutTemplate>

                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                     
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                     <img src='../images/gallery/<%# Eval("image") %>' width="200" height="200" />
                                     <a href='admin.aspx?pg=del&id=<%# Eval("id") %>'><span style="color:red;">Delete</span></a>
                                    </ItemTemplate>
                                </asp:ListView>
                               
                            </asp:View>
                               <asp:View ID="View18" runat="server">
                               <table>
                               <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Delete" onclick="Button1_Click" /></td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Button2_Click" /></td>
                               </tr>
                               </table>
                               </asp:View>
                        </asp:MultiView>
                       
                    </asp:View>
                    <!--end media-->
                    <!--page-->
                    <asp:View ID="View6" runat="server">
                        404 error
                    </asp:View>
                    <!--end page-->
                </asp:MultiView>
            </div>
        </div>
        <div id="content_footer">
        </div>
        <div id="footer">
            Copyright &copy; 2014
        </div>
    </div>
    </form>
</body>
</html>
