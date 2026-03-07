public static class DataFixer
{
    public static void FixQuarterlyReportIds(List<QuarterlyReport> reports)
    {
        // Ensure all QuarterlyReport IDs are sequential and consistent
        for (int i = 0; i < reports.Count; i++)
        {
            reports[i].Id = i + 1;
        }
    }

    public static string GenerateDeterministicId(int parentId, string type, int index)
    {
        return $"{parentId}-{type}-{index}";
    }
}
