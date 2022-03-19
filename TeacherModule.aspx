﻿<%@ Page Title="Teacher Module" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherModule.aspx.cs" Inherits="ADbSD_Coursework_I.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Teacher Module</h1>
    </div>
    <div>
        <div class="form-group mb-2">
            <asp:Label ID="teacherSelectLBL" runat="server" Text="Select Teacher"></asp:Label>
            <asp:DropDownList ID="teacherDropDown" runat="server" CssClass="form-control" DataSourceID="teacherSelectDatasource" DataTextField="NAME" DataValueField="ID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="teacherSelectDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_BCMS %>" ProviderName="<%$ ConnectionStrings:ConnectionString_BCMS.ProviderName %>" SelectCommand="SELECT &quot;ID&quot;, &quot;NAME&quot; FROM &quot;TEACHER&quot;"></asp:SqlDataSource>
            </div>
        <div class="form-group mb-2">
            <asp:Button ID="submitTeacherModuleBTN" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="submitTeacherModuleBTN_Click"/>
            <asp:Button ID="resetBTN" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="resetBTN_Click"/>
        </div>
    </div>
    <div>
        <asp:GridView 
                    ID="teacherModuleGV" 
                    runat="server" 
                    CssClass="table" 
                    DataKeyNames="teacher_id" 
                    EmptyDataText="No Record Has Been Added!" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
    </div>
</asp:Content>
