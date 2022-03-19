<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAssignment.aspx.cs" Inherits="ADbSD_Coursework_I.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Student Assignment</h1>
    </div>
    <div>
        <div class="form-group mb-2">
            <asp:Label ID="studentSelectLBL" runat="server" Text="Select Student"></asp:Label>
            <asp:DropDownList ID="studentDropDown" runat="server" CssClass="form-control" DataSourceID="studentSelectDatasource" DataTextField="STUDENT_NAME" DataValueField="STUDENT_ID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="studentSelectDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_BCMS %>" ProviderName="<%$ ConnectionStrings:ConnectionString_BCMS.ProviderName %>" SelectCommand="SELECT &quot;STUDENT_ID&quot;, &quot;STUDENT_NAME&quot; FROM &quot;STUDENT&quot;"></asp:SqlDataSource>
            </div>
        <div class="form-group mb-2">
            <asp:Button ID="submitTeacherModuleBTN" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="submitStudentAssignmentBTN_Click"/>
            <asp:Button ID="resetBTN" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="resetBTN_Click"/>
        </div>
    </div>
    <div>
        <asp:GridView 
                    ID="studentAssignmentGV" 
                    runat="server" 
                    CssClass="table" 
                    DataKeyNames="Submission Number" 
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
