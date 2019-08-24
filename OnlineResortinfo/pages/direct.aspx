<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="direct.aspx.cs" Inherits="OnlineResortinfo.pages.direct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/jquery-ui-1.10.2.custom.css" />
    <link rel="stylesheet" type="text/css" href="../css/login.css" />
  
    <script type="text/javascript" src="../js/default.js"></script>
        <script type="text/javascript" src="../js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <asp:Label ID="promt" runat="server"></asp:Label><br/><br/>
     <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format" ForeColor="#FF3300"></asp:RegularExpressionValidator><br/><br/>
        <asp:CompareValidator   ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch" ControlToCompare="txtregpassword" ForeColor="#FF3300" ControlToValidate="txtregccpassword"></asp:CompareValidator>
  
        <section class="register">
<h1><asp:Label ID="lbltitlesite" runat="server"></asp:Label></h1>

    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="View1" runat="server">
        <div class="reg_section personal_info">
            <h3>Login your account</h3>
           <asp:TextBox ID="txtuser" runat="server" placeholder="Username"></asp:TextBox>
           <asp:TextBox ID="txtpass" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remind me"></asp:CheckBox>
        </div>


        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="reg_section personal_info">
                <h3>
                    Register</h3>
               <asp:TextBox ID="txtregusername" runat="server" placeholder="Username"></asp:TextBox>
               <asp:TextBox ID="txtemail" runat="server" placeholder="Email Address"></asp:TextBox>
               <asp:TextBox ID="txtregpassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
               <asp:TextBox ID="txtregccpassword" runat="server" TextMode="Password" placeholder="Cc Password"></asp:TextBox>
           </div>
        </asp:View>
        </asp:MultiView> 

                <p class="submit">
                <asp:Button ID="btnsubmit" runat="server" onclick="btnsubmit_Click"></asp:Button>
                </p>
        </section>
    </div>
    </form>
</body>
</html>
