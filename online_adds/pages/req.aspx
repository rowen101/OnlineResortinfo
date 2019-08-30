<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="req.aspx.cs" Inherits="online_adds.pages.signup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../css/login.css" />
    <link type="text/css" rel="Stylesheet" href="../css/modern-business.css" />
    <link type="text/css" rel="Stylesheet" href="../css/bootstrap.css" />
    <link rel="shortcut icon" href="../images/1379608351_95057.ico" />
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
</head>
<body>
    <form id="form1" runat="server">
    <div id="login_form">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 
 
       
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
            <center>
                       <asp:Label ID="promt" runat="server"></asp:Label>
                        <asp:Label ID="lbl_sitename" runat="server"></asp:Label></center>
                    <br />
                <fieldset>
                    
                    <legend>Login</legend>
                 
                    <asp:TextBox CssClass="form-control" ID="txtuser" placeholder="UserName" runat="server" ></asp:TextBox><br />
                    <asp:TextBox CssClass="form-control" ID="txtpass" placeholder="Password" runat="server"
                        TextMode="Password"  ></asp:TextBox><br />
                    <asp:CheckBox ID="chkRememberMe" runat="server" Text="&nbsp;Remind me" />
                    <br />
<asp:Button ID="btnlogin" runat="server" Text="login" CssClass="btn btn-primary"
                        OnClick="btnlogin_Click" />
                     <asp:Button ID="Button1" runat="server" Text="Sign Up" 
                        CssClass="btn btn-default" onclick="Button1_Click"
                        />
                    
                   
                </fieldset>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <fieldset>
                 
                    <center>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                         <asp:Label ID="promted2" runat="server"></asp:Label><br/>
                        <asp:Label ID="lbltitle2" runat="server"></asp:Label>
                       <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemailuser" ErrorMessage="Invalid Email Format" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                        </center>
                    <br />
                   <legend>Register</legend>
                  <%-- Upload Photo
                   <asp:FileUpload ID="fileuserimage" runat="server" />--%>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match" ControlToCompare="txtpass1" ControlToValidate="txtpass2"></asp:CompareValidator>
                     <table>
                    <tr>
                        <td>Username:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtusername" required="required" runat="server"></asp:TextBox></td>
                            <td>
                                &nbsp;</td>
                    </tr>
                     <tr>
                        <td>E-mail:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtemailuser"  required="required" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtpass1" runat="server" required="required"  TextMode="Password" CausesValidation="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                  <td>Retype Password&nbsp;</td>
                        <td><asp:TextBox CssClass="form-control" ID="txtpass2" runat="server" required="required"  TextMode="Password"></asp:TextBox></td>
                    </tr>
                  <%--   <tr>
                        <td>First Name:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtfname" required="required" runat="server"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>Last Name:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtlname" required="required"  runat="server"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>Middle Name:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtmname" required="required"  runat="server"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>Address:</td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtaddress" required="required"  runat="server"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>Gender:</td>
                        <td>
                            <asp:DropDownList CssClass="form-control" ID="drpgender"   runat="server">
                            </asp:DropDownList>
                          </td>
                    </tr>--%>
                   
                   </table>       
                   
                   
                      <br /> <asp:Image ID="Image1"  runat="server" ImageUrl="~/pages/CImage.aspx"/> 
                    <table>
                   <tr>
                        <td>Security Code&nbsp;</td>
                         <td><asp:TextBox CssClass="form-control" ID="txtimgcode" runat="server"></asp:TextBox></td>
   
  
                   </tr>
                </table>
                
                <br/>
                    <a  class="btn btn-default" href="req.aspx?pg=login">Back to Login</a>
                    <asp:Button ID="btnreg" runat="server" Text="Sign Up" 
                        CssClass="btn btn-primary" onclick="btnreg_Click"
                       />
                </fieldset>
            </asp:View>
        </asp:MultiView>
        
    

    </div>
    </form>
</body>
</html>
