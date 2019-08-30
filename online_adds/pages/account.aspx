<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true"
    CodeBehind="account.aspx.cs" Inherits="online_adds.pages.account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      
        .error,.ok
        {
            border: 1px solid #CC0000;
            background-color: #FFEBE8;
            padding: 5px 0;
            text-align: center;
            margin-top: 10px;
        }
        .ok
        {
            border: 1px solid #E6DB55;
            background-color: #FFFFE0;
         
           
        }
        .sam
        {
            border-radius: 65px;
            -webkit-border-radius: 65px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                <asp:Label ID="lblslct_title" runat="server" Text="Account"></asp:Label>
                <small>
                    <asp:Label ID="smltooltip" runat="server"></asp:Label></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="home.aspx">Home</a></li>
                <li class="active">Account</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <asp:Label ID="Label1" runat="server"></asp:Label><br />
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div style="width: 300px; float: left;">
                        <h4>
                            User Info</h4>
                        <br />
                       
                                <asp:MultiView ID="MultiView2" runat="server">
                                    <asp:View ID="View3" runat="server">
                                        <asp:Image CssClass="sam" ID="imageitem" runat="server" Width="140px" Height="128px" />
                                        <br />
                                        <asp:LinkButton ID="lnkchangeimage" runat="server" OnClick="lnkchangeimage_Click">Change Image</asp:LinkButton><br />
                                    </asp:View>
                                    <asp:View ID="View4" runat="server">
                                        <asp:FileUpload ID="fileuserimage" runat="server" />
                                        <asp:LinkButton ID="lnkshowimage" runat="server" OnClick="lnkshowimage_Click">Show Image</asp:LinkButton><br />
                                    </asp:View>
                                </asp:MultiView>
                           
                        <br />
                        <table>
                            <tr>
                                <td>
                                    Username
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtusername" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    First Name
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtfname" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Last Name
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtlname" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Middle Name
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtmname" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtaddress" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email Address
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtemail" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Gender
                                </td>
                                <td>
                                    <asp:DropDownList CssClass="form-control" ID="dropgender" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button CssClass="btn btn-primary" ID="btnsubmit" runat="server" Text="Save"
                                        OnClick="btnsubmit_Click" />
                                </td>
                                <td>
                                    <asp:Button CssClass="btn btn-danger" ID="Button1" runat="server" Text="Deactivate account "
                                        OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 300px; float: left;">
                        <h4>
                            Change Account</h4>
                        <table>
                            <tr>
                                <td>
                                    Old Password
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtolduserpass" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    New Password
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtpass1" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Retype Password
                                </td>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="txtpass2" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match"
                                        ControlToCompare="txtpass1" ControlToValidate="txtpass2" ForeColor="#CC0000"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnchangeaccount" CssClass="btn btn-primary" runat="server" 
                            Text="Save Account" onclick="btnchangeaccount_Click"  />
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
    
                    <asp:Label ID="Label2" runat="server"></asp:Label><br />
                    <table>
                        <tr>
                            
                            <td>
                                <asp:Button CssClass="btn btn-default" ID="Button3" runat="server" Text="Cancel"
                                    OnClick="Button3_Click" />
                            </td>
                            <td>
                                <asp:Button CssClass="btn btn-danger" ID="Button2" runat="server" Text="Remove" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
