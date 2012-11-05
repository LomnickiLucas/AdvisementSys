<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    <script language="c#" runat="server">
    public void Page_Load(object sender, EventArgs e)
    {
        DateTime start = DateTime.Parse(ViewData["StartDate"].ToString());
        DateTime end = DateTime.Parse(ViewData["EndDate"].ToString());
            
        SqlDataSource1.SelectParameters["studID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource1.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource1.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");

        SqlDataSource2.SelectParameters["studentID"].DefaultValue = ViewData["StudentID"].ToString();

        SqlDataSource3.SelectParameters["readmitStudID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource3.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource3.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");

        SqlDataSource4.SelectParameters["lateStudID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource4.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource4.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");

        SqlDataSource5.SelectParameters["probStudID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource5.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource5.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");

        SqlDataSource6.SelectParameters["partStudID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource6.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource6.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");

        SqlDataSource7.SelectParameters["withdrawStudID"].DefaultValue = ViewData["StudentID"].ToString();
        SqlDataSource7.SelectParameters["start"].DefaultValue = start.ToString("MM/dd/yyyy");
        SqlDataSource7.SelectParameters["end"].DefaultValue = end.ToString("MM/dd/yyyy");
            
        ReportParameter[] parameters = new ReportParameter[4] { new ReportParameter("StudentID", ViewData["StudentID"].ToString()), new ReportParameter("StartDate", start.ToString()), new ReportParameter("EndDate", end.ToString()), new ReportParameter("User", ViewData["User"].ToString()) };
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
    <LocalReport ReportPath="Views\Reports\ReportsPartials\StudentIssueReport.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="StudentInfo" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="Issues" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource3" 
                Name="ApplicationForReadmission" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource4" Name="LateEnrollment" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource5" 
                Name="ProbationaryContract" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource6" Name="PartTime" />
            <rsweb:ReportDataSource DataSourceId="SqlDataSource7" Name="Withdraw" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT        issue.issueid, issue.studentid, issue.issuename, applicationForTermOrCompleteProgramWithdrawal.issueid AS Expr1, 
                         applicationForTermOrCompleteProgramWithdrawal.date, applicationForTermOrCompleteProgramWithdrawal.status
FROM            applicationForTermOrCompleteProgramWithdrawal INNER JOIN
                         issue ON applicationForTermOrCompleteProgramWithdrawal.issueid = issue.issueid
WHERE        (issue.studentid = @withdrawStudID) AND (applicationForTermOrCompleteProgramWithdrawal.date &gt;= @start) AND 
                         (applicationForTermOrCompleteProgramWithdrawal.date &lt;= @end)">
        <SelectParameters>
            <asp:Parameter Name="withdrawStudID" />
            <asp:Parameter Name="start" />
            <asp:Parameter Name="end" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT        issue.issueid, issue.studentid, issue.issuename, [part-timeAnd/orAdditionalCourseRegistrationForm].issueid AS Expr1, 
                         [part-timeAnd/orAdditionalCourseRegistrationForm].date, [part-timeAnd/orAdditionalCourseRegistrationForm].status
FROM            [part-timeAnd/orAdditionalCourseRegistrationForm] INNER JOIN
                         issue ON [part-timeAnd/orAdditionalCourseRegistrationForm].issueid = issue.issueid
WHERE        (issue.studentid = @partStudID) AND ([part-timeAnd/orAdditionalCourseRegistrationForm].date &gt;= @start) AND 
                         ([part-timeAnd/orAdditionalCourseRegistrationForm].date &lt;= @end)">
        <SelectParameters>
            <asp:Parameter Name="partStudID" />
            <asp:Parameter Name="start" />
            <asp:Parameter Name="end" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT        probationaryContractPlan.date, probationaryContractPlan.status, probationaryContractPlan.issueid, issue.issueid AS Expr1, issue.studentid, issue.issuename
FROM            issue INNER JOIN
                         probationaryContractPlan ON issue.issueid = probationaryContractPlan.issueid
WHERE        (issue.studentid = @probStudID) AND (probationaryContractPlan.date &gt;= @start) AND (probationaryContractPlan.date &lt;= @end)">
        <SelectParameters>
            <asp:Parameter Name="probStudID" />
            <asp:Parameter Name="start" />
            <asp:Parameter Name="end" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT        issue.studentid, issue.issueid, requestForLateEnrolment.date, requestForLateEnrolment.status, issue.issuename
FROM            issue INNER JOIN
                         requestForLateEnrolment ON issue.issueid = requestForLateEnrolment.issueid
WHERE        (issue.studentid = @lateStudID) AND (requestForLateEnrolment.date &gt;= @start) AND (requestForLateEnrolment.date &lt;= @end)">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="lateStudID" />
            <asp:Parameter Name="start" />
            <asp:Parameter Name="end" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT        applicationForReadmission.status, applicationForReadmission.date, applicationForReadmission.issueid, issue.issueid AS Expr1, issue.studentid, 
                         issue.issuename
FROM            applicationForReadmission INNER JOIN
                         issue ON applicationForReadmission.issueid = issue.issueid
WHERE        (issue.studentid = @readmitStudID) AND (applicationForReadmission.date &gt;= @start) AND (applicationForReadmission.date &lt;= @end)">
        <SelectParameters>
            <asp:Parameter Name="readmitStudID" />
            <asp:Parameter Name="start" />
            <asp:Parameter Name="end" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT        student.fname, student.lname, student.programcode, student.campus, student.email, student.studentid, student.phonenum, program.programcode AS Expr1, 
                         program.programname
FROM            student INNER JOIN
                         program ON student.programcode = program.programcode
WHERE        (student.studentid = @studentID)">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="studentID" />
        </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT        issueid, studentid, issuename, date, status, urgency, employeeid, catagory, description
FROM            issue
WHERE        (studentid = @studID) AND (date &gt;= @start) AND (date &lt;= @end)">
    <SelectParameters>
        <asp:Parameter Name="studID" />
        <asp:Parameter Name="start" />
        <asp:Parameter Name="end" />
    </SelectParameters>
    </asp:SqlDataSource>

</form>