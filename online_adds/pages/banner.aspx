<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="banner.aspx.cs" Inherits="online_adds.pages.bannerdel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    <asp:Button ID="Button1" runat="server" Text="Upload banner" 
        onclick="Button1_Click" />

    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" 
        DataSourceID="LinqDataSource1" CellPadding="4" ForeColor="#333333" 
        GridLines="None" onrowcommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
          <asp:TemplateField HeaderText="filename" SortExpression="filename">
            <ItemTemplate>
                <img src='../images/banner/<%# Eval("filename") %>'  height="100" width="100"/>
            </ItemTemplate>
          </asp:TemplateField>
            <asp:BoundField DataField="description" HeaderText="description" 
                ReadOnly="True" SortExpression="description" />
            <asp:BoundField DataField="status" HeaderText="status" ReadOnly="True" 
                SortExpression="status" />
        
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton CommandArgument='<%# Eval("ID") %>' CommandName="Deleteid" ID="lnkdelete" runat="server">Delete</asp:LinkButton>
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
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="online_adds.databaseDataContext" EntityTypeName="" 
        Select="new (ID, filename, description, status, date)" TableName="homebanners">
    </asp:LinqDataSource>
    </form>
</body>
</html>
