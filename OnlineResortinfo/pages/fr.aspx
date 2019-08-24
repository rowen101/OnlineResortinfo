<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fr.aspx.cs" Inherits="OnlineResortinfo.pages.fr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="wrap-content zerogrid">
        <div class="row block03">
            <div class="col-2-3">
                <asp:MultiView ID="MultiView1" runat="server">
                 <!---search-->
                 <asp:View ID="View1" runat="server">
                        <div class="wrap-col">
                            <asp:ListView ID="listresort" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <article>
						<a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><img src='../images/postimg/<%# Eval("filename") %>'/></a>
						<h2><a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title") %></a></h2>
						<div class="info">[By <%# Eval("username") %> on <%# Eval("dte") %> with <a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("comment_count") %> Commnets</a>]</div>
						<p><%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>&nbsp;[...]</p>
					                </article>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:View>

                    <!---category-->
                 <asp:View ID="View2" runat="server">
                 
                  <div class="wrap-col">
                            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="id">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="id"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <article>
						<a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><img src='../images/postimg/<%# Eval("filename") %>'/></a>
						<h2><a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("pst_title") %></a></h2>
						<div class="info">[By <%# Eval("username") %> on <%# Eval("dte") %> with <a href='resort.aspx?req=rd&id=<%# Eval("pst_id") %>'><%# Eval("comment_count") %> Commnets</a>]</div>
						<p><%# Eval("pst_content").ToString().PadRight(140).Substring(0,140).TrimEnd() %>&nbsp;[...]</p>
					                </article>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                Oops.. no data 
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                 </asp:View>
                </asp:MultiView>

            </div>
            <div class="col-1-3">
                        <div class="wrap-col">
                          
                            <div class="box">
                                <div class="heading">
                                    <h2>
                                        Category</h2>
                                </div>
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

</asp:Content>
