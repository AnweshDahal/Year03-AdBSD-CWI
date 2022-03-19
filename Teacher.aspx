﻿<%@ Page Title="Teacher" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="ADbSD_Coursework_I.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div>
            <h3>Teachers</h3>
        </div>
        <div>
            <div class="form-group mb-2">
                <asp:Label ID="idLBL" runat="server" Text="Teacher ID"></asp:Label>
                <asp:TextBox ID="idTB" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="This Field is Required!" ControlToValidate="idTB" ForeColor="#FF6666"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ErrorMessage="Only Numbers Allowed!" ControlToValidate="idTB" ForeColor="#FF6666" ValidationExpression="\d+"></asp:RegularExpressionValidator>
             </div> 
            <div class="form-group mb-2">
                <asp:Label ID="teacherNameLBL" runat="server" Text="Teacher Name"></asp:Label>
                <asp:TextBox ID="teacherNameTB" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="This Field is Required!" ControlToValidate="teacherNameTB" ForeColor="#FF6666"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group mb-2">
                <asp:Label ID="teacherEmailLBL" runat="server" Text="Teacher Email"></asp:Label>
                <asp:TextBox ID="teacherEmailTB" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="This Field is Required!" ControlToValidate="teacherEmailTB" ForeColor="#FF6666"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group mb-2">
                <asp:Button ID="submitTeacherBTN" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="submitTeacherBTN_Click"/>
            </div>
        </div>
        <div>

            <asp:GridView 
                    ID="teacherGV" 
                    runat="server" 
                    CssClass="table" 
                    DataKeyNames="id" 
                    OnRowEditing="OnRowEditing"
                    OnRowCancelingEdit="OnRowCancelEditing"
                    OnRowDeleting="OnRowDeleting" 
                    EmptyDataText="No Record Has Been Added!" 
                    AutoGenerateDeleteButton="True" 
                    AutoGenerateEditButton="True" 
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
    </div>
</asp:Content>
