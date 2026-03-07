namespace FinanceODataApi
{
    public static class DataFixer
    {
        public static void FixQuarterlyReportIds(List<QuarterlyReport> reports)
        {
            foreach (var report in reports)
            {
                foreach (var monthly in report.MonthlyData)
                {
                    monthly.QuarterlyReportId = report.Id;
                }
                foreach (var kpi in report.KPIs)
                {
                    kpi.QuarterlyReportId = report.Id;
                }
            }
        }
    }
}
