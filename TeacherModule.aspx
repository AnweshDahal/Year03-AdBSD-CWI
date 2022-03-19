<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherModule.aspx.cs" Inherits="ADbSD_Coursework_I.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Module Assignment</h1>
    </div>
    <div>
        <div class="row mb-3">
            <div class="col-6">
                <div class="form-group">
                    <asp:Label ID="teacherSelectLBL" runat="server" Text="Select Teacher"></asp:Label>

                    <asp:DropDownList ID="teacherDropDown" runat="server" CssClass="form-control" DataSourceID="teacherSelectDatasource" DataTextField="NAME" DataValueField="ID">
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="teacherSelectDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_BCMS %>" ProviderName="<%$ ConnectionStrings:ConnectionString_BCMS.ProviderName %>" SelectCommand="SELECT &quot;ID&quot;, &quot;NAME&quot; FROM &quot;TEACHER&quot;"></asp:SqlDataSource>

                </div>
            </div>
            <div class="col-6">

                <asp:DropDownList ID="moduleDropdown" runat="server" CssClass="form-control" DataSourceID="moduleSelectDataSource" DataTextField="MODULE_NAME" DataValueField="MODULE_CODE">
                </asp:DropDownList>
                <asp:SqlDataSource ID="moduleSelectDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_BCMS %>" ProviderName="<%$ ConnectionStrings:ConnectionString_BCMS.ProviderName %>" SelectCommand="SELECT &quot;MODULE_CODE&quot;, &quot;MODULE_NAME&quot; FROM &quot;MODULE&quot;"></asp:SqlDataSource>

            </div>
        </div>
        
    </div>

</asp:Content>
