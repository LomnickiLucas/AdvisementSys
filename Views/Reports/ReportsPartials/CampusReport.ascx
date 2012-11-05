<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <form id="Form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <script language="c#" runat="server">
    public void Page_Load(object sender, EventArgs e)
    {
            ReportParameter[] parameters = new ReportParameter[4] { new ReportParameter("Program", ViewData["Program"].ToString()), new ReportParameter("StartDate", ViewData["StartDate"].ToString()), new ReportParameter("EndDate", ViewData["EndDate"].ToString()), new ReportParameter("User", ViewData["User"].ToString()) };
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
    <LocalReport ReportPath="Views\Reports\ReportsPartials\CampusReport.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT program.programname, program.programcode, issue.status, campus.cname, issue.issuename, issue.date
FROM issue INNER JOIN
student ON issue.studentid = student.studentid INNER JOIN
program ON student.programcode = program.programcode INNER JOIN
campus ON student.campus = campus.cname"></asp:SqlDataSource>

</form>