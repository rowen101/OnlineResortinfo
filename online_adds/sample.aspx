<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sample.aspx.cs" Inherits="online_adds.sample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
      
        <table>
            <tr>
                <td>
                    Descrypt
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Encrypt
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Encrypt" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
   
        <table>
            <tr>
                <td>
                    Encrypt
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                   Descrypt
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="Descrypt" 
                        onclick="Button3_Click"/>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:FileUpload ID="FileUpload1" runat="server" />

        <asp:Button ID="Button2" runat="server" Text="Button" onclick="Button2_Click" />
         
    </div>
    </form>
</body>
</html>
