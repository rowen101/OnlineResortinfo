<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="OnlineResortinfo.pages.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="wrap-content zerogrid">
            <br/>
                <span class="TextLightI WatermarkedInputContainer" style="font-size: 25px; margin: 0 0 0  20px;">
                    My Profile </span>
                <div class="row block03">
                   
                    <asp:Image ID="imgprofile" Height="64" Width="64" runat="server" />
                     <asp:Label ID="Label1" runat="server"></asp:Label>
                    <table>
                    <tr>
                        <td><b>Name</b></td>
                    </tr>
                        <tr>
                            <td>Username:</td>
                            <td>
                                <asp:TextBox ID="txtusername" runat="server" Enabled="False"></asp:TextBox></td>
                        <td><i>&nbsp;Usernames cannot be changed.</i></td>
                        </tr>
                         <tr>
                            <td>First Name:</td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td>Last Name:</td>
                            <td>
                                <asp:TextBox ID="txtlname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Gender:</td>
                            <td>
                                <asp:DropDownList ID="dropgender" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                   <tr>
                     <td><b>Contact Info</b></td>
                   </tr>
                   
                    
                        <tr>
                            <td>E-mail <i>(required)&nbsp;</i></td>
                            <td><asp:TextBox ID="txtemail" runat="server"></asp:TextBox></td>
                            <td><i>&nbsp;
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </i></td>
                        </tr>
                        <tr>
                            <td>Website</td>
                            <td><asp:TextBox ID="txtwebsite" runat="server" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>New Password</td>
                            <td>
                                <asp:TextBox ID="txtpass1" runat="server" TextMode="Password"></asp:TextBox></td>
                                <td><i>&nbsp;If you would like to change the password type a new one. Otherwise leave this blank.</i></td>
                        </tr>
                        <tr>
                        <td></td>
                            <td>
                                <asp:TextBox ID="txtpass2" runat="server" TextMode="Password"></asp:TextBox></td>
                                <td><i>&nbsp;Type your new password again.</i></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ErrorMessage="Password not match" ControlToCompare="txtpass1" 
                                    ControlToValidate="txtpass2"></asp:CompareValidator></td>
                        </tr>
                       
                        <tr>
                            <td>
                            <div class="comment">
                                <asp:Button ID="btnupdateprofile" runat="server" Text="Update Profile" onclick="btnupdateprofile_Click" 
                                   ForeColor="Black" /></div></td>
                        </tr>
                     </table>
                     <br />
                </div>
                </div>
</asp:Content>
