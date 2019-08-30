<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminsetup.aspx.cs" Inherits="online_adds.pages.adminsetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../css/bootstrap.css" />
    <link type="text/css" rel="Stylesheet" href="../css/modern-business.css" />
    <link type="text/css" rel="Stylesheet" href="../css/modal.css" />
    <style type="text/css">
        .promtedinformation
        {
            border: 1px solid #E6DB55;
            background-color: #FFFFE0;
        }
        .promtederror
        {
            border: 1px solid #CC0000;
            background-color: #FFEBE8;
        }
        .sam
        {
            border-radius: 20px;
        }
         .gtooltip
        {
            height: 25px;
            background: black;
            opacity: 0.7;
            color: white;
            margin: -30px 0 20px 0;
            transition: all 200ms linear 0s;
            text-decoration: none;
        }
    </style>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none;">
        <asp:Label ID="lblfullname" runat="server"></asp:Label>
        <asp:Label ID="lblprofilepic" runat="server"></asp:Label>
        <asp:Label ID="lblip" runat="server"></asp:Label>
        <asp:Label ID="lblDateTime" runat="server"></asp:Label>
    </div>
    <asp:Label ID="lblpromted" runat="server"></asp:Label>
    <asp:MultiView ID="MultiView1" runat="server">
        <!--view post-->
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="Panelviewpost" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddNew" CssClass="btn btn-default" runat="server" Text="Add Post"
                                OnClick="btnAddNew_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" CssClass="btn btn-default" runat="server" Text="Refresh"
                                OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            <asp:Label ID="useridpost" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lbladview" runat="server" Visible="false"></asp:Label>
                <asp:GridView CssClass="mGrid" ID="GridView1" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" AllowSorting="True" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblpsdid" runat="server" Text='<%# Eval ("pst_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img width="44" height="44" class="sam" src='../profilepic/thumb/<%#Eval ("userimage") %>' />
                                Posted by:<%# Eval("Username")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title" SortExpression="pst_title">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval ("pst_ID") %>'
                                    CommandName="post_edit">
                                     <img src='../images/oreginal/<%# Eval("Image") %>' width="100" height="100"/><br/>
                                    <%# Eval("pst_title")%>
                                    
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="dte" HeaderText="Date" ReadOnly="True" SortExpression="dte" />
                        <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="True" SortExpression="status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval ("pst_ID") %>'
                                    CommandName="MyDelete"><div class="imgdelete"></div></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Underline="False"
                        BorderStyle="None" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            
            </asp:Panel>
            <asp:Panel ID="Paneladdpost" runat="server" Visible="false">
                <div style="display: none;">
                    <asp:Label ID="postid" runat="server"></asp:Label></div>
                <asp:Label ID="imgpostpromted" runat="server"></asp:Label>
                <!--SHOW/HIDE post images-->
                <asp:MultiView ID="MultiView4" runat="server">
                    <asp:View ID="View13" runat="server">
                        <asp:FileUpload ID="postimgupload" runat="server" />
                        <p>
                            <asp:LinkButton ID="lnkshow" runat="server" Visible="false" OnClick="lnkshow_Click">Show Image</asp:LinkButton></p>
                    </asp:View>
                    <asp:View ID="View14" runat="server">
                        <asp:Image ID="imageitem" runat="server" Width="128px" Height="128px" />
                        
                        <br />
                        <p>
                            <asp:LinkButton ID="lnkchangeimage" runat="server" OnClick="lnkchangeimage_Click">Change Image</asp:LinkButton></p>
                    </asp:View>
                </asp:MultiView>
                <br />
                <br />
               &nbsp;<asp:Label ID="lblrequiredfiled" runat="server"></asp:Label>
                <asp:TextBox CssClass="form-control" ID="txttitlepost" runat="server" placeholder="Title"
                    ControlToValidate="txttitle"></asp:TextBox><br />
                Content
                <asp:UpdatePanel ID="viewPanel" runat="server">
                    <ContentTemplate>
                        <cc1:Editor ID="desEditor" runat="server" Height="470px" Width="1138px" AutoFocus="true"
                            HtmlPanelCssClass="ajax__htmleditor_editor_default" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="desEditor" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />
                <asp:Button ID="btncancelpost" runat="server" Text="Cancel" CssClass="btn btn-default"
                    OnClick="btncancelpost_Click" />
                <asp:Button ID="btnsavepost" runat="server" Text="Save" CssClass="btn btn-primary"
                    OnClick="btnsavepost_Click" />
                <asp:DropDownList ID="Dropcategorypost" runat="server">
                </asp:DropDownList>
               
            </asp:Panel>
            <asp:Panel ID="Paneldeletepost" runat="server" Visible="false">
                <div style="position: absolute; margin: 50px;">
                    <asp:Label ID="lblpostdelpromted" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="btndelpostcancel" CssClass="btn btn-default" runat="server" Text="Cancel"
                        OnClick="btndelpostcancel_Click" />
                    <asp:Button ID="btndelpost" CssClass="btn btn-danger" runat="server" Text="Ok" OnClick="btndelpost_Click" />
                    <asp:Label ID="Label5" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </asp:View>
        <!--view Comment-->
        <asp:View ID="View2" runat="server">
            <asp:LinkButton ID="lnkallcomment" runat="server" OnClick="lnkallcomment_Click">All</asp:LinkButton>&nbsp;|
            <asp:LinkButton ID="lnkapproved" runat="server" OnClick="lnkapproved_Click">Approved</asp:LinkButton>&nbsp;|
            <asp:LinkButton ID="lnkpinding" runat="server" OnClick="lnkpinding_Click">Pinding</asp:LinkButton>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiView7" runat="server">
                        <!--All Comment-->
                        <asp:View ID="View21" runat="server">
                            <br />
                            <b>All Comment</b>
                            <br />
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnRowCommand="GridView2_RowCommand" AllowSorting="True" CellPadding="4" ForeColor="#333333"
                                GridLines="None" DataSourceID="LinqDataSource2">
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Position="TopAndBottom" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField FooterText="Author" HeaderText="Author">
                                        <ItemTemplate>
                                            <div class="authorcomment">
                                                <div class="imgcomment">
                                                    <img class="sam" src='../profilepic/thumb/<%#Eval("image") %>' width="44" height="44" />
                                                </div>
                                                <div class="namecomment">
                                                    <%# Eval("Username")%>
                                                </div>
                                                <div class="emailcomment">
                                                    <a href='mailto:<%# Eval("c_email") %>'>
                                                        <%# Eval("c_email") %></a>
                                                </div>
                                                <div class="ipcomment">
                                                    <%# Eval("c_ip") %>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Comment" HeaderText="Comment">
                                        <ItemTemplate>
                                            <%# Eval("c_content") %>
                                            <br />
                                            <div class="nav_event">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkevent" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName='<%# Eval("c_status") %>'> <%# Eval("c_status") %></asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkreply" runat="server">Reply</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkedit" runat="server">Edit</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnktrash" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName="Trash">Trash</asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkevent" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="In Responce To" HeaderText="In Responce To">
                                        <ItemTemplate>
                                            <%# Eval("comment_count") %>&nbsp;&nbsp;
                                            <%# Eval("post_title") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BorderStyle="None" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="online_adds.databaseDataContext"
                                EntityTypeName="" Select="new (c_ID, c_content, date, c_email, c_ip, c_status, comment_count, post_title, user_id, Username, pst_id, image)"
                                TableName="view_comments">
                            </asp:LinqDataSource>
                        </asp:View>
                        <!--Approved Comment-->
                        <asp:View ID="View22" runat="server">
                            <br />
                            <b>Approved Comment</b>
                            <br />
                            <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" DataSourceID="approvedlinq" ForeColor="#333333"
                                GridLines="None" OnRowCommand="GridView5_RowCommand">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField FooterText="Author" HeaderText="Author">
                                        <ItemTemplate>
                                            <div class="authorcomment">
                                                <div class="imgcomment">
                                                    <img class="sam" src='../profilepic/thumb/<%#Eval("image") %>' width="44" height="44" />
                                                </div>
                                                <div class="namecomment">
                                                    <%# Eval("username") %>
                                                </div>
                                                <div class="emailcomment">
                                                    <a href='mailto:<%# Eval("c_email") %>'>
                                                        <%# Eval("c_email") %></a>
                                                </div>
                                                <div class="ipcomment">
                                                    <%# Eval("c_ip") %>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Comment" HeaderText="Comment">
                                        <ItemTemplate>
                                            <%# Eval("c_content") %>
                                            <br />
                                            <div class="nav_event">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkeventapp" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName='<%# Eval("c_status") %>'> <%# Eval("c_status") %></asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkreplyapp" runat="server">Reply</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkeditapp" runat="server">Edit</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnktrashapp" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName="Trash">Trash</asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkeventapp" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="In Responce To" HeaderText="In Responce To">
                                        <ItemTemplate>
                                            <%# Eval("comment_count") %>&nbsp;&nbsp;
                                            <%# Eval("post_title") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:LinqDataSource ID="approvedlinq" runat="server" ContextTypeName="online_adds.databaseDataContext"
                                EntityTypeName="" TableName="view_comments" Where="c_status == @c_status" Select="new (c_ID, c_content, date, c_email, c_ip, comment_count, post_title, image, pst_id, user_id, Username, c_status)">
                                <WhereParameters>
                                    <asp:Parameter DefaultValue="Approved" Name="c_status" Type="String" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </asp:View>
                        <!--Pinding Comment-->
                        <asp:View ID="View23" runat="server">
                            <br />
                            <b>Pinding Comment</b>
                            <br />
                            <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" DataSourceID="Linqcommentpinding"
                                ForeColor="#333333" GridLines="None" OnRowCommand="GridView6_RowCommand">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField FooterText="Author" HeaderText="Author">
                                        <ItemTemplate>
                                            <div class="authorcomment">
                                                <div class="imgcomment">
                                                    <img class="sam" src='../profilepic/thumb/<%#Eval("image") %>' width="44" height="44" />
                                                </div>
                                                <div class="namecomment">
                                                    <%# Eval("username") %>
                                                </div>
                                                <div class="emailcomment">
                                                    <a href='mailto:<%# Eval("c_email") %>'>
                                                        <%# Eval("c_email") %></a>
                                                </div>
                                                <div class="ipcomment">
                                                    <%# Eval("c_ip") %>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Comment" HeaderText="Comment">
                                        <ItemTemplate>
                                            <%# Eval("c_content") %>
                                            <br />
                                            <div class="nav_event">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkeventPin" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName='<%# Eval("c_status") %>'> <%# Eval("c_status") %></asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkreplyPin" runat="server">Reply</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnkeditPin" runat="server">Edit</asp:LinkButton>
                                                        |<asp:LinkButton ID="lnktrashPin" runat="server" CommandArgument='<%# Eval("c_ID") %>'
                                                            CommandName="Trash">Trash</asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkeventPin" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="In Responce To" HeaderText="In Responce To">
                                        <ItemTemplate>
                                            <%# Eval("comment_count") %>&nbsp;&nbsp;
                                            <%# Eval("post_title") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:LinqDataSource ID="Linqcommentpinding" runat="server" ContextTypeName="online_adds.databaseDataContext"
                                EntityTypeName="" TableName="view_comments" Where="c_status == @c_status" Select="new (c_ID, c_content, date, c_email, c_ip, comment_count, post_title, image, pst_id, user_id, Username, c_status)">
                                <WhereParameters>
                                    <asp:Parameter DefaultValue="Pinding" Name="c_status" Type="String" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkallcomment" />
                    <asp:AsyncPostBackTrigger ControlID="lnkapproved" />
                    <asp:AsyncPostBackTrigger ControlID="lnkpinding" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <!--end view-->
        <!--view appearance-->
        <asp:View ID="View3" runat="server">
        </asp:View>
        <!--end view-->
        <!--view user-->
        <asp:View ID="View4" runat="server">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiView3" runat="server">
                        <asp:View ID="View11" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <td>
                                            <asp:Button ID="btnuser" runat="server" CssClass="btn btn-default" OnClick="btnuser_Click"
                                                Text="Add User" />
                                        </td>
                                        <td>
                                            <asp:Button CssClass="btn btn-default" ID="Button4" runat="server" OnClick="Button4_Click"
                                                Text="Refresh" />
                                        </td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <asp:GridView ID="Griduser" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                DataSourceID="LinqDataSource3" OnRowCommand="Griduser_RowCommand">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                 <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img class="sam" src='../profilepic/thumb/<%# Eval("Image") %>' width="44"
                                                height="44" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                    <asp:TemplateField HeaderText="Name" SortExpression="fname">
                                        <ItemTemplate>
                                            <a href='adminsetup.aspx?pg=Userview&userid=<%# Eval("id") %>'>
                                                <%# Eval("fname") %>&nbsp;<%# Eval("mname") %>&nbsp;<%# Eval("lname") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="email" HeaderText="Email Address" ReadOnly="True" SortExpression="email" />
                                    <asp:BoundField DataField="address" HeaderText="Address" ReadOnly="True" SortExpression="address" />
                                    <asp:BoundField DataField="gender" HeaderText="Gender" ReadOnly="True" SortExpression="gender" />
                                    <asp:BoundField DataField="type" HeaderText="Role" ReadOnly="True" SortExpression="type" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqDataSource3" runat="server" ContextTypeName="online_adds.databaseDataContext"
                                EntityTypeName="" Select="new (username, fname, lname, email, type, address, id, mname, gender, Permision, Image)"
                                TableName="View_allusers">
                            </asp:LinqDataSource>
                        </asp:View>
                        <asp:View ID="View12" runat="server">
                            <div style="display: none;">
                                <asp:Label ID="Label2" runat="server"></asp:Label></div>
                            <asp:Label ID="lbluser" runat="server"></asp:Label><br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match"
                                ControlToCompare="txtpass1" ControlToValidate="txtpass2"></asp:CompareValidator>
                            <br />
                            <asp:Image CssClass="sam" ID="userimage" runat="server" Visible="False" Width="100"
                                Height="100" />

                            <table>
                                <tr>
                                    <td>
                                        Username:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtusername" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        E-mail:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtemailuser" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password <i>twice, required&nbsp;</i>
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtpass1" runat="server" TextMode="Password"
                                            CausesValidation="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtpass2" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First Name:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtfname" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Name:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtlname" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Middle Name:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtmname" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address:
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control" ID="txtaddress" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gender:
                                    </td>
                                    <td>
                                        <asp:DropDownList CssClass="form-control" ID="drpgender" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        Role:&nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList CssClass="form-control" ID="droprole" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnSaveuser" CssClass="btn btn-primary" runat="server" Text="Save"
                                OnClick="btnSaveuser_Click" />
                            <asp:Button ID="canceluser" CssClass="btn btn-default" runat="server" Text="Cancel"
                                OnClick="canceluser_Click" />
                            <asp:Label CssClass="promtedinformation" ID="lblpromteduser" runat="server" Visible="False"></asp:Label>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="canceluser" />
                    <asp:AsyncPostBackTrigger ControlID="btnuser" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <!--end view-->
        <!--view setting-->
        <asp:View ID="View5" runat="server">
            <br />
            <table>
                <tr>
                    <td>
                        Site Title
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txtsitetitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tagline
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txttagline" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txtemail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <i>In a few words, explain what this site is about.</i>
                    </td>
                </tr>
            </table>
            
            <asp:Button ID="btnfaq" CssClass="btn btn-default" runat="server" Text="view FAQ" onclick="btnfaq_Click" />
            <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="Button3_Click" />
            <asp:MultiView ID="MultiView2" runat="server">
                <asp:View ID="View8" runat="server">
                    <br />
                    <fieldset>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:Editor ID="Editorfaq" runat="server" Height="470px" Width="1000px" AutoFocus="true"
                                    HtmlPanelCssClass="ajax__htmleditor_editor_default" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Editorfaq" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                    <br />
                </asp:View>
                <asp:View ID="View9" runat="server">
                </asp:View>
               <%-- <asp:View ID="View10" runat="server">
                    <br />
                    <fieldset>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <cc1:Editor ID="Editorabout" runat="server" Height="470px" Width="1000px" AutoFocus="true"
                                    HtmlPanelCssClass="ajax__htmleditor_editor_default" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Editorabout" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                    <br />
                </asp:View>--%>
            </asp:MultiView>
        </asp:View>
        <!--end view-->
        <!--view media-->
        <asp:View ID="View6" runat="server">
            <asp:MultiView ID="MultiView6" runat="server">
                <asp:View ID="View19" runat="server">
                   
                   <%-- <br />
                    <i>Upload Image</i>
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                    <asp:FileUpload ID="filegallerimg" runat="server" />
                    <br />
                   
                      
                          <i>Where to Save&nbsp;</i>
                                <asp:DropDownList ID="droptitlepost" runat="server">
                                </asp:DropDownList>
                         
                   
                    <br />
                    <table>
                        <tr>
                            <td> <asp:Button ID="Button7" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="Button7_Click" /></td>
                           
                        </tr>
                    </table>
                   
                   
                    <br />--%>
                             <%--<asp:Label ID="mediapromted" runat="server"></asp:Label>--%><br/>
                           
                   <div class="col-lg-6">
                       <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" 
                        Text="Refresh" onclick="Button1_Click" />
                    <br /><br />
                       <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="id">
                           <LayoutTemplate>
                               <div class="row">
                                   <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                               </div>
                           </LayoutTemplate>
                           <ItemTemplate>
                               <div class="col-lg-4 col-md-4 col-sm-6">
                                   <a href='adminsetup.aspx?pg=addgallery&mediaid=<%# Eval("gallery_id") %>'>
                                       <img src='../images/oreginal/<%# Eval("filename") %>' class="img-responsive img-home-portfolio" /></a>
                                   <div class="gtooltip" style="text-align: center;">
                                       <%# Eval("title") %>
                                   </div>
                               </div>
                           </ItemTemplate>
                       </asp:ListView>
                    </div>
                </asp:View>
                <asp:View ID="View20" runat="server">
                   
                 <%--    <script type="text/javascript" src="../js/jquery-1.4.1.js"></script>
                      <script language="javascript" type="text/javascript">
                          $(function () {
                              var scntDiv = $('#FileUploaders');
                              var i = $('#FileUploader p').size() + 1;

                              $('#addUploader').live('click', function () {
                                  $('<p><input type="file" id="FileUploader' + i + '" name="FileUploader' + i + '" /></label> <a href="#" id="removeUploader">Remove</a></p>').appendTo(scntDiv);
                                  i++;
                                  return false;
                              });

                              $('#removeUploader').live('click', function () {
                                  if (i > 2) {
                                      $(this).parent('p').remove();
                                      i--;
                                  }
                                  return false;
                              });
                          });
                      </script>--%>
                    <asp:Panel ID="Panel1" runat="server">
                     <b><asp:Label ID="Label6" runat="server"></asp:Label></b><br />
                     <asp:Image ID="mediathumb" runat="server" Width="150" Height="150" />
              
                        <br />
                        <div style="padding: 10px;">
                            <div id="FileUploaders">
                                <p>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </p>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        Description:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdescription" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                           
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnUploadAll" runat="server" Text="Upload Image"
                                            OnClick="btnUploadAll_Click" />
                                    </td>
                                    <td>
                                        <asp:Button CssClass="btn btn-default" ID="btnbckgallery" runat="server" Text="Back to gallery"
                                            OnClick="btnbckgallery_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="col-lg-12">
                            <asp:ListView ID="listgalleryslcted" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <div class="row">
                                        <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%--  <div class="col-md-3 portfolio-item" style="margin: 10px 65px 0 auto; float: left;">--%>
                                    <div class="col-md-3 portfolio-item">
                                        <img class="img-responsive" src='../images/goreginal/<%# Eval("Image") %>' />
                                        <div style="margin: -40px 0 0 0; transition: all 200ms linear 0s; position: absolute;">
                                            <a href='adminsetup.aspx?pg=selcimage&id=<%# Eval("id") %>'>
                                                <div class="imgdelete">
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server">
                        <asp:Label ID="lbldeleteimagepromted" runat="server"></asp:Label>
                        <asp:Button ID="brtdelteimage" CssClass="btn btn-danger" runat="server" 
                            Text="Delete" onclick="brtdelteimage_Click" />
                        <asp:Button ID="btncanceldelimage" CssClass="btn btn-primary" runat="server" 
                            Text="Cancel" onclick="btncanceldelimage_Click" />
                    </asp:Panel>
                   
                </asp:View>
            </asp:MultiView>
        </asp:View>
        <!--end view-->
        <asp:View ID="View7" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <p class="error-404">
                            404</p>
                        <p class="lead promtederror">
                            The page you're looking for could not be found.</p>
                    </div>
                </div>
            </div>
            <!-- /.container -->
            <div class="container">
                <hr>
                <footer>
           
        </footer>
            </div>
            <!-- /.container -->
        </asp:View>
        <!---->
        <asp:View ID="View15" runat="server">
            <asp:MultiView ID="MultiView5" runat="server">
                <asp:View ID="View16" runat="server">
                    <asp:Button ID="Button5" CssClass="btn btn-default" runat="server" Text="Add Page"
                        OnClick="Button5_Click" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" DataSourceID="LinqDataSource5" ForeColor="#333333"
                        GridLines="None" OnRowCommand="GridView4_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Pages Title" SortExpression="menu_name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnktitlepage" CommandName="Editpages" runat="server" CommandArgument='<%# Eval("menu_id") %>'><%# Eval("menu_name") %></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visible">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkvisible" CommandArgument='<%# Eval("menu_id") %>' runat="server"
                                                CommandName='<%# Eval("Visible") %>'><%# Eval("Visible")%></asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkvisible" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton CommandArgument='<%# Eval("menu_id") %>' CommandName="delete" ID="lnkmndel"
                                        runat="server"><div class="imgdelete"></div></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSource5" runat="server" ContextTypeName="online_adds.databaseDataContext"
                        EntityTypeName="" Select="new (menu_id, menu_name, Visible)" TableName="View_menuvisibles">
                    </asp:LinqDataSource>
                </asp:View>
                <asp:View ID="View17" runat="server">
                    <asp:Label ID="lblpgpromted" runat="server"></asp:Label><br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnpgcancel" CssClass="btn btn-default" runat="server" Text="Cancel"
                                    OnClick="btnpgcancel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnpgdelete" CssClass="btn btn-danger" runat="server" Text="Delete"
                                    OnClick="btnpgdelete_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View18" runat="server">
                    <asp:Label ID="lblmenuid" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblotherpageid" runat="server" Visible="false"></asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtpagestitle" runat="server" Placeholder="Page Title"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblpromtedpages" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    Content
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <cc1:Editor ID="Editorpages" runat="server" Height="470px" Width="1138px" AutoFocus="true"
                                HtmlPanelCssClass="ajax__htmleditor_editor_default" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="desEditor" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button CssClass="btn btn-default" ID="Button6" runat="server" Text="Cancel"
                                    OnClick="Button6_Click1" />
                            </td>
                            <td>
                                <asp:Button CssClass="btn btn-primary" ID="Button8" runat="server" Text="Save" OnClick="Button8_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>
    </form>
</body>
</html>
