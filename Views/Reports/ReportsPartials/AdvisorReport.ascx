﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <form id="Form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <script language="c#" runat="server">
    public void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["employeeID"].DefaultValue = ViewData["EmpID"].ToString();

        SqlDataSource2.SelectParameters["empID"].DefaultValue = ViewData["EmpID"].ToString();

        SqlDataSource3.SelectParameters["empID"].DefaultValue = ViewData["EmpID"].ToString();

        ReportParameter[] parameters = new ReportParameter[2] { new ReportParameter("EmpID", ViewData["EmpID"].ToString()), new ReportParameter("User", ViewData["User"].ToString()) };
        ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.Visible = true;
            ReportViewer1.ShowPageNavigationControls = false;
            ReportViewer1.ShowFindControls = false;
            ReportViewer1.ShowRefreshButton = false;
            ReportViewer1.ShowBackButton = false;
            ReportViewer1.LocalReport.Refresh();
    }
</script>
</asp:ScriptManager>

<br />

<rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="800px">
    <LocalReport ReportPath="Views\Reports\ReportsPartials\AdvisorReport.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="AllIssue" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="Employee" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource3" Name="OutstandingIssue" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT        issuename, date, status, urgency, employeeid, catagory, description, studentid
FROM            issue
WHERE        (employeeid = @empID) AND (DATEADD(month, 1, date) &lt; { fn NOW() }) AND (status = N'In Progress' OR
                         status = N'Pending')
ORDER BY date">
        <SelectParameters>
            <asp:Parameter Name="empID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT        employeeid, fname, lname, phonenum, email, faculty, role, archived
FROM            employee
WHERE        (employeeid = @empID)">
        <SelectParameters>
            <asp:Parameter Name="empID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT        employee.employeeid, employee.fname, employee.lname, employee.phonenum, employee.email, employee.faculty, employee.role, issue.employeeid AS Expr1, 
                         issue.issueid, issue.issuename, issue.date, issue.status, issue.urgency
FROM            employee INNER JOIN
                         issue ON employee.employeeid = issue.employeeid
WHERE        (employee.employeeid = @employeeID)">
        <SelectParameters>
            <asp:Parameter Name="employeeID" />
        </SelectParameters>
    </asp:SqlDataSource>

</form>