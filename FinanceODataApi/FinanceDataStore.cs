using System.Text.Json;

public static class FinanceDataStore
{
    public static readonly List<QuarterlyReport> Reports;
    public static readonly List<RegionEntity> Regions;
    public static readonly List<DepartmentEntity> Departments;
    public static readonly List<YearEntity> Years;
    public static readonly List<SegmentEntity> Segments;

    // FIX 4: Pre-aggregated views for Templafy charts
    public static readonly List<RevenueByDepartment> RevenueByDepartment;
    public static readonly List<RevenueByMonth> RevenueByMonth;
    public static readonly List<RevenueByQuarter> RevenueByQuarter;
    public static readonly List<RevenueByYear> RevenueByYear;
    public static readonly List<KpiSummary> KpiSummary;

    static FinanceDataStore()
    {
        try
        {
            // Load from seed_data_v3.json at app startup
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "seed_data_v3.json");
            var jsonContent = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<SeedData>(jsonContent, options);

            if (data == null)
            {
                throw new InvalidOperationException("Failed to deserialize seed data");
            }

            Reports = data.QuarterlyReports ?? new List<QuarterlyReport>();
            Regions = data.Regions ?? new List<RegionEntity>();
            Departments = data.Departments ?? new List<DepartmentEntity>();
            Years = data.Years ?? new List<YearEntity>();
            Segments = data.Segments ?? new List<SegmentEntity>();

            Console.WriteLine($"Loaded {Reports.Count} quarterly reports from seed data");
            Console.WriteLine($"Loaded {Regions.Count} regions, {Departments.Count} departments, {Years.Count} years, {Segments.Count} segments");

            // FIX 4: Compute pre-aggregated views for Templafy charts
            RevenueByDepartment = ComputeRevenueByDepartment();
            RevenueByMonth = ComputeRevenueByMonth();
            RevenueByQuarter = ComputeRevenueByQuarter();
            RevenueByYear = ComputeRevenueByYear();
            KpiSummary = ComputeKpiSummary();

            Console.WriteLine($"Computed {RevenueByDepartment.Count} RevenueByDepartment records");
            Console.WriteLine($"Computed {RevenueByMonth.Count} RevenueByMonth records");
            Console.WriteLine($"Computed {RevenueByQuarter.Count} RevenueByQuarter records");
            Console.WriteLine($"Computed {RevenueByYear.Count} RevenueByYear records");
            Console.WriteLine($"Computed {KpiSummary.Count} KpiSummary records");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading seed data: {ex.Message}");
            Reports = new List<QuarterlyReport>();
            Regions = new List<RegionEntity>();
            Departments = new List<DepartmentEntity>();
            Years = new List<YearEntity>();
            Segments = new List<SegmentEntity>();
            RevenueByDepartment = new List<RevenueByDepartment>();
            RevenueByMonth = new List<RevenueByMonth>();
            RevenueByQuarter = new List<RevenueByQuarter>();
            RevenueByYear = new List<RevenueByYear>();
            KpiSummary = new List<KpiSummary>();
        }
    }

    // RevenueByDepartment: one record per unique Region+Year+Segment+Department combo
    // Directly maps from each QuarterlyReport record
    private static List<RevenueByDepartment> ComputeRevenueByDepartment()
    {
        return Reports.Select(r => new RevenueByDepartment
        {
            Id = $"{r.Region}-{r.Year}-{r.Segment}-{r.Department}",
            Region = r.Region,
            Year = r.Year,
            Segment = r.Segment,
            Department = r.Department,
            AnnualRevenue = r.AnnualRevenue,
            AnnualExpenses = r.AnnualExpenses,
            AnnualProfit = r.AnnualProfit,
            GrowthRate = r.GrowthRate,
            HeadCount = r.HeadCount
        }).ToList();
    }

    // RevenueByMonth: flatten all MonthlyData across all reports, summing Revenue/Expenses/Profit
    // per Region+Year+Segment+MonthIndex across all departments
    private static List<RevenueByMonth> ComputeRevenueByMonth()
    {
        return Reports
            .SelectMany(r => r.MonthlyData.Select(m => new
            {
                Region = r.Region,
                Year = r.Year,
                Segment = r.Segment,
                m.Month,
                m.MonthIndex,
                m.Revenue,
                m.Expenses,
                m.Profit
            }))
            .GroupBy(x => new { x.Region, x.Year, x.Segment, x.MonthIndex, x.Month })
            .Select(g => new RevenueByMonth
            {
                Id = $"{g.Key.Region}-{g.Key.Year}-{g.Key.Segment}-{g.Key.MonthIndex}",
                Region = g.Key.Region,
                Year = g.Key.Year,
                Segment = g.Key.Segment,
                Month = g.Key.Month,
                MonthIndex = g.Key.MonthIndex,
                Revenue = g.Sum(x => x.Revenue),
                Expenses = g.Sum(x => x.Expenses),
                Profit = g.Sum(x => x.Profit)
            })
            .OrderBy(x => x.Region)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Segment)
            .ThenBy(x => x.MonthIndex)
            .ToList();
    }

    // RevenueByQuarter: flatten all QuarterlyData across all reports, summing per
    // Region+Year+Segment+QuarterIndex across all departments
    private static List<RevenueByQuarter> ComputeRevenueByQuarter()
    {
        return Reports
            .SelectMany(r => r.QuarterlyData.Select(q => new
            {
                Region = r.Region,
                Year = r.Year,
                Segment = r.Segment,
                q.Quarter,
                q.QuarterIndex,
                q.Revenue,
                q.Expenses,
                q.Profit
            }))
            .GroupBy(x => new { x.Region, x.Year, x.Segment, x.QuarterIndex, x.Quarter })
            .Select(g => new RevenueByQuarter
            {
                Id = $"{g.Key.Region}-{g.Key.Year}-{g.Key.Segment}-{g.Key.QuarterIndex}",
                Region = g.Key.Region,
                Year = g.Key.Year,
                Segment = g.Key.Segment,
                Quarter = g.Key.Quarter,
                QuarterIndex = g.Key.QuarterIndex,
                Revenue = g.Sum(x => x.Revenue),
                Expenses = g.Sum(x => x.Expenses),
                Profit = g.Sum(x => x.Profit)
            })
            .OrderBy(x => x.Region)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Segment)
            .ThenBy(x => x.QuarterIndex)
            .ToList();
    }

    // RevenueByYear: sum AnnualRevenue/Expenses/Profit per Region+Year+Segment
    // across all departments, average GrowthRate, sum HeadCount
    private static List<RevenueByYear> ComputeRevenueByYear()
    {
        return Reports
            .GroupBy(r => new { r.Region, r.Year, r.Segment })
            .Select(g => new RevenueByYear
            {
                Id = $"{g.Key.Region}-{g.Key.Year}-{g.Key.Segment}",
                Region = g.Key.Region,
                Year = g.Key.Year,
                Segment = g.Key.Segment,
                TotalRevenue = g.Sum(r => r.AnnualRevenue),
                TotalExpenses = g.Sum(r => r.AnnualExpenses),
                TotalProfit = g.Sum(r => r.AnnualProfit),
                AvgGrowthRate = g.Average(r => r.GrowthRate),
                TotalHeadCount = g.Sum(r => r.HeadCount)
            })
            .OrderBy(x => x.Region)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Segment)
            .ToList();
    }

    // KpiSummary: flatten all KPIs per Region+Year+Segment+Department+KpiName
    private static List<KpiSummary> ComputeKpiSummary()
    {
        return Reports
            .SelectMany(r => r.KPIs.Select(k => new KpiSummary
            {
                Id = $"{r.Region}-{r.Year}-{r.Segment}-{r.Department}-{k.Name}",
                Region = r.Region,
                Year = r.Year,
                Segment = r.Segment,
                Department = r.Department,
                KpiName = k.Name,
                KpiValue = k.Value,
                Unit = k.Unit
            }))
            .OrderBy(x => x.Region)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Segment)
            .ThenBy(x => x.Department)
            .ThenBy(x => x.KpiName)
            .ToList();
    }

    private class SeedData
    {
        public List<QuarterlyReport>? QuarterlyReports { get; set; }
        public List<RegionEntity>? Regions { get; set; }
        public List<DepartmentEntity>? Departments { get; set; }
        public List<YearEntity>? Years { get; set; }
        public List<SegmentEntity>? Segments { get; set; }
    }
}
