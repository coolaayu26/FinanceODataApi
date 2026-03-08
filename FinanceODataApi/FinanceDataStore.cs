public static class FinanceDataStore
{
    public static readonly List<RegionSummary> RegionSummaries;
    public static readonly List<DepartmentSummary> DepartmentSummaries;
    public static readonly List<YearSummary> YearSummaries;
    public static readonly List<SegmentSummary> SegmentSummaries;

    static FinanceDataStore()
    {
        // Generate all data in-memory with reproducible seed
        var baseData = GenerateBaseData();

        RegionSummaries = ComputeRegionSummaries(baseData);
        DepartmentSummaries = ComputeDepartmentSummaries(baseData);
        YearSummaries = ComputeYearSummaries(baseData);
        SegmentSummaries = ComputeSegmentSummaries(baseData);

        Console.WriteLine($"Generated {baseData.Count} base records");
        Console.WriteLine($"Computed {RegionSummaries.Count} RegionSummaries");
        Console.WriteLine($"Computed {DepartmentSummaries.Count} DepartmentSummaries");
        Console.WriteLine($"Computed {YearSummaries.Count} YearSummaries");
        Console.WriteLine($"Computed {SegmentSummaries.Count} SegmentSummaries");
    }

    // Base flat record structure (288 records total)
    private class BaseRecord
    {
        public string Region { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Segment { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal Profit { get; set; }
        public decimal GrowthRate { get; set; }
        public int HeadCount { get; set; }
        public List<MonthlyDataItem> MonthlyData { get; set; } = new();
        public List<QuarterlyDataItem> QuarterlyData { get; set; } = new();
        public List<KpiDataItem> KPIs { get; set; } = new();
    }

    private static List<BaseRecord> GenerateBaseData()
    {
        var random = new Random(42); // Fixed seed for reproducibility

        var regions = new[] { "Africa", "Asia", "Europe", "Middle East", "North America", "South America" };
        var departments = new[] { "Compliance", "Private Equity", "Research", "Risk", "Sales", "Wealth Management" };
        var years = new[] { "2023", "2024", "2025", "2026" };
        var segments = new[] { "Corporate", "Institutional" };

        // Revenue ranges by department (base before multipliers)
        var revenueRanges = new Dictionary<string, (int Min, int Max)>
        {
            ["Sales"] = (200000, 380000),
            ["Private Equity"] = (180000, 320000),
            ["Wealth Management"] = (160000, 300000),
            ["Research"] = (150000, 280000),
            ["Compliance"] = (120000, 250000),
            ["Risk"] = (100000, 220000)
        };

        // Region multipliers
        var regionMultipliers = new Dictionary<string, decimal>
        {
            ["North America"] = 1.3m,
            ["Europe"] = 1.2m,
            ["Asia"] = 1.1m,
            ["Middle East"] = 0.9m,
            ["South America"] = 0.85m,
            ["Africa"] = 0.8m
        };

        // Year multipliers
        var yearMultipliers = new Dictionary<string, decimal>
        {
            ["2023"] = 1.0m,
            ["2024"] = 1.08m,
            ["2025"] = 1.17m,
            ["2026"] = 1.26m
        };

        // Segment multipliers
        var segmentMultipliers = new Dictionary<string, decimal>
        {
            ["Institutional"] = 1.15m,
            ["Corporate"] = 0.88m
        };

        var kpiNames = new[] { "AUM", "EBITDA", "IRR", "Net New Clients" };
        var months = new[] { "January", "February", "March", "April", "May", "June",
                            "July", "August", "September", "October", "November", "December" };

        var records = new List<BaseRecord>();

        // Generate 288 records (6 regions × 6 departments × 4 years × 2 segments)
        foreach (var region in regions)
        {
            foreach (var department in departments)
            {
                foreach (var year in years)
                {
                    foreach (var segment in segments)
                    {
                        var (minRev, maxRev) = revenueRanges[department];
                        var baseRevenue = random.Next(minRev, maxRev);

                        // Apply multipliers
                        var revenue = baseRevenue
                            * regionMultipliers[region]
                            * yearMultipliers[year]
                            * segmentMultipliers[segment];

                        // Expense ratio between 0.58 and 0.78
                        var expenseRatio = 0.58m + (decimal)random.NextDouble() * 0.20m;
                        var expenses = revenue * expenseRatio;
                        var profit = revenue - expenses;

                        // Growth rate between -5% and +15%
                        var growthRate = -5m + (decimal)random.NextDouble() * 20m;

                        // HeadCount proportional to revenue
                        var headCount = (int)(revenue / 5000) + random.Next(20, 50);

                        // Generate monthly data (12 months)
                        var monthlyData = new List<MonthlyDataItem>();
                        for (int i = 0; i < 12; i++)
                        {
                            var monthRevenue = revenue / 12 * (0.8m + (decimal)random.NextDouble() * 0.4m);
                            var monthExpenses = monthRevenue * expenseRatio;
                            monthlyData.Add(new MonthlyDataItem
                            {
                                Month = months[i],
                                MonthIndex = i + 1,
                                Revenue = Math.Round(monthRevenue, 2),
                                Expenses = Math.Round(monthExpenses, 2),
                                Profit = Math.Round(monthRevenue - monthExpenses, 2)
                            });
                        }

                        // Generate quarterly data (4 quarters)
                        var quarterlyData = new List<QuarterlyDataItem>();
                        for (int q = 0; q < 4; q++)
                        {
                            var quarterRevenue = monthlyData.Skip(q * 3).Take(3).Sum(m => m.Revenue);
                            var quarterExpenses = monthlyData.Skip(q * 3).Take(3).Sum(m => m.Expenses);
                            quarterlyData.Add(new QuarterlyDataItem
                            {
                                Quarter = $"Q{q + 1}-{year}",
                                QuarterIndex = q + 1,
                                Revenue = Math.Round(quarterRevenue, 2),
                                Expenses = Math.Round(quarterExpenses, 2),
                                Profit = Math.Round(quarterRevenue - quarterExpenses, 2)
                            });
                        }

                        // Generate KPIs (4 per record)
                        var kpis = new List<KpiDataItem>();
                        foreach (var kpiName in kpiNames)
                        {
                            kpis.Add(new KpiDataItem
                            {
                                Department = department,
                                Name = kpiName,
                                Value = Math.Round(50m + (decimal)random.NextDouble() * 100m, 2),
                                Unit = "%"
                            });
                        }

                        records.Add(new BaseRecord
                        {
                            Region = region,
                            Department = department,
                            Year = year,
                            Segment = segment,
                            Revenue = Math.Round(revenue, 2),
                            Expenses = Math.Round(expenses, 2),
                            Profit = Math.Round(profit, 2),
                            GrowthRate = Math.Round(growthRate, 2),
                            HeadCount = headCount,
                            MonthlyData = monthlyData,
                            QuarterlyData = quarterlyData,
                            KPIs = kpis
                        });
                    }
                }
            }
        }

        return records;
    }

    // Compute RegionSummary (6 records)
    private static List<RegionSummary> ComputeRegionSummaries(List<BaseRecord> baseData)
    {
        return baseData
            .GroupBy(r => r.Region)
            .Select((g, index) => new RegionSummary
            {
                Id = index + 1,
                Region = g.Key,
                TotalRevenue = g.Sum(r => r.Revenue),
                TotalExpenses = g.Sum(r => r.Expenses),
                TotalProfit = g.Sum(r => r.Profit),

                // DepartmentData: group by department (6 items)
                DepartmentData = g.GroupBy(r => r.Department)
                    .Select(dg => new DepartmentDataItem
                    {
                        Department = dg.Key,
                        Revenue = dg.Sum(r => r.Revenue),
                        Expenses = dg.Sum(r => r.Expenses),
                        Profit = dg.Sum(r => r.Profit),
                        GrowthRate = dg.Average(r => r.GrowthRate),
                        HeadCount = dg.Sum(r => r.HeadCount)
                    }).ToList(),

                // MonthlyTotals: sum across all records by month (12 items)
                MonthlyTotals = Enumerable.Range(1, 12)
                    .Select(monthIndex => new MonthlyDataItem
                    {
                        Month = g.SelectMany(r => r.MonthlyData).First(m => m.MonthIndex == monthIndex).Month,
                        MonthIndex = monthIndex,
                        Revenue = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Revenue),
                        Expenses = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Expenses),
                        Profit = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Profit)
                    }).ToList(),

                // QuarterlyTotals: sum across all records by quarter (4 items)
                QuarterlyTotals = Enumerable.Range(1, 4)
                    .Select(quarterIndex => new QuarterlyDataItem
                    {
                        Quarter = $"Q{quarterIndex}",
                        QuarterIndex = quarterIndex,
                        Revenue = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Revenue),
                        Expenses = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Expenses),
                        Profit = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Profit)
                    }).ToList(),

                // YearlyTotals: group by year (4 items)
                YearlyTotals = g.GroupBy(r => r.Year)
                    .Select(yg => new YearlyDataItem
                    {
                        Year = yg.Key,
                        Revenue = yg.Sum(r => r.Revenue),
                        Expenses = yg.Sum(r => r.Expenses),
                        Profit = yg.Sum(r => r.Profit)
                    }).ToList(),

                // SegmentData: group by segment (2 items)
                SegmentData = g.GroupBy(r => r.Segment)
                    .Select(sg => new SegmentDataItem
                    {
                        Segment = sg.Key,
                        Revenue = sg.Sum(r => r.Revenue),
                        Expenses = sg.Sum(r => r.Expenses),
                        Profit = sg.Sum(r => r.Profit)
                    }).ToList(),

                // KPIs: all KPIs flattened
                KPIs = g.SelectMany(r => r.KPIs).ToList()
            })
            .OrderBy(r => r.Region)
            .ToList();
    }

    // Compute DepartmentSummary (6 records)
    private static List<DepartmentSummary> ComputeDepartmentSummaries(List<BaseRecord> baseData)
    {
        return baseData
            .GroupBy(r => r.Department)
            .Select((g, index) => new DepartmentSummary
            {
                Id = index + 1,
                Department = g.Key,
                TotalRevenue = g.Sum(r => r.Revenue),
                TotalExpenses = g.Sum(r => r.Expenses),
                TotalProfit = g.Sum(r => r.Profit),

                RegionData = g.GroupBy(r => r.Region)
                    .Select(rg => new RegionDataItem
                    {
                        Region = rg.Key,
                        Revenue = rg.Sum(r => r.Revenue),
                        Expenses = rg.Sum(r => r.Expenses),
                        Profit = rg.Sum(r => r.Profit),
                        GrowthRate = rg.Average(r => r.GrowthRate),
                        HeadCount = rg.Sum(r => r.HeadCount)
                    }).ToList(),

                MonthlyTotals = Enumerable.Range(1, 12)
                    .Select(monthIndex => new MonthlyDataItem
                    {
                        Month = g.SelectMany(r => r.MonthlyData).First(m => m.MonthIndex == monthIndex).Month,
                        MonthIndex = monthIndex,
                        Revenue = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Revenue),
                        Expenses = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Expenses),
                        Profit = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Profit)
                    }).ToList(),

                QuarterlyTotals = Enumerable.Range(1, 4)
                    .Select(quarterIndex => new QuarterlyDataItem
                    {
                        Quarter = $"Q{quarterIndex}",
                        QuarterIndex = quarterIndex,
                        Revenue = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Revenue),
                        Expenses = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Expenses),
                        Profit = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Profit)
                    }).ToList(),

                YearlyTotals = g.GroupBy(r => r.Year)
                    .Select(yg => new YearlyDataItem
                    {
                        Year = yg.Key,
                        Revenue = yg.Sum(r => r.Revenue),
                        Expenses = yg.Sum(r => r.Expenses),
                        Profit = yg.Sum(r => r.Profit)
                    }).ToList(),

                SegmentData = g.GroupBy(r => r.Segment)
                    .Select(sg => new SegmentDataItem
                    {
                        Segment = sg.Key,
                        Revenue = sg.Sum(r => r.Revenue),
                        Expenses = sg.Sum(r => r.Expenses),
                        Profit = sg.Sum(r => r.Profit)
                    }).ToList(),

                KPIs = g.SelectMany(r => r.KPIs).ToList()
            })
            .OrderBy(d => d.Department)
            .ToList();
    }

    // Compute YearSummary (4 records)
    private static List<YearSummary> ComputeYearSummaries(List<BaseRecord> baseData)
    {
        return baseData
            .GroupBy(r => r.Year)
            .Select((g, index) => new YearSummary
            {
                Id = index + 1,
                Year = g.Key,
                TotalRevenue = g.Sum(r => r.Revenue),
                TotalExpenses = g.Sum(r => r.Expenses),
                TotalProfit = g.Sum(r => r.Profit),

                RegionData = g.GroupBy(r => r.Region)
                    .Select(rg => new RegionDataItem
                    {
                        Region = rg.Key,
                        Revenue = rg.Sum(r => r.Revenue),
                        Expenses = rg.Sum(r => r.Expenses),
                        Profit = rg.Sum(r => r.Profit),
                        GrowthRate = rg.Average(r => r.GrowthRate),
                        HeadCount = rg.Sum(r => r.HeadCount)
                    }).ToList(),

                DepartmentData = g.GroupBy(r => r.Department)
                    .Select(dg => new DepartmentDataItem
                    {
                        Department = dg.Key,
                        Revenue = dg.Sum(r => r.Revenue),
                        Expenses = dg.Sum(r => r.Expenses),
                        Profit = dg.Sum(r => r.Profit),
                        GrowthRate = dg.Average(r => r.GrowthRate),
                        HeadCount = dg.Sum(r => r.HeadCount)
                    }).ToList(),

                MonthlyTotals = Enumerable.Range(1, 12)
                    .Select(monthIndex => new MonthlyDataItem
                    {
                        Month = g.SelectMany(r => r.MonthlyData).First(m => m.MonthIndex == monthIndex).Month,
                        MonthIndex = monthIndex,
                        Revenue = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Revenue),
                        Expenses = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Expenses),
                        Profit = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Profit)
                    }).ToList(),

                QuarterlyTotals = Enumerable.Range(1, 4)
                    .Select(quarterIndex => new QuarterlyDataItem
                    {
                        Quarter = $"Q{quarterIndex}",
                        QuarterIndex = quarterIndex,
                        Revenue = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Revenue),
                        Expenses = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Expenses),
                        Profit = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Profit)
                    }).ToList(),

                SegmentData = g.GroupBy(r => r.Segment)
                    .Select(sg => new SegmentDataItem
                    {
                        Segment = sg.Key,
                        Revenue = sg.Sum(r => r.Revenue),
                        Expenses = sg.Sum(r => r.Expenses),
                        Profit = sg.Sum(r => r.Profit)
                    }).ToList(),

                KPIs = g.SelectMany(r => r.KPIs).ToList()
            })
            .OrderBy(y => y.Year)
            .ToList();
    }

    // Compute SegmentSummary (2 records)
    private static List<SegmentSummary> ComputeSegmentSummaries(List<BaseRecord> baseData)
    {
        return baseData
            .GroupBy(r => r.Segment)
            .Select((g, index) => new SegmentSummary
            {
                Id = index + 1,
                Segment = g.Key,
                TotalRevenue = g.Sum(r => r.Revenue),
                TotalExpenses = g.Sum(r => r.Expenses),
                TotalProfit = g.Sum(r => r.Profit),

                RegionData = g.GroupBy(r => r.Region)
                    .Select(rg => new RegionDataItem
                    {
                        Region = rg.Key,
                        Revenue = rg.Sum(r => r.Revenue),
                        Expenses = rg.Sum(r => r.Expenses),
                        Profit = rg.Sum(r => r.Profit),
                        GrowthRate = rg.Average(r => r.GrowthRate),
                        HeadCount = rg.Sum(r => r.HeadCount)
                    }).ToList(),

                DepartmentData = g.GroupBy(r => r.Department)
                    .Select(dg => new DepartmentDataItem
                    {
                        Department = dg.Key,
                        Revenue = dg.Sum(r => r.Revenue),
                        Expenses = dg.Sum(r => r.Expenses),
                        Profit = dg.Sum(r => r.Profit),
                        GrowthRate = dg.Average(r => r.GrowthRate),
                        HeadCount = dg.Sum(r => r.HeadCount)
                    }).ToList(),

                MonthlyTotals = Enumerable.Range(1, 12)
                    .Select(monthIndex => new MonthlyDataItem
                    {
                        Month = g.SelectMany(r => r.MonthlyData).First(m => m.MonthIndex == monthIndex).Month,
                        MonthIndex = monthIndex,
                        Revenue = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Revenue),
                        Expenses = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Expenses),
                        Profit = g.SelectMany(r => r.MonthlyData).Where(m => m.MonthIndex == monthIndex).Sum(m => m.Profit)
                    }).ToList(),

                QuarterlyTotals = Enumerable.Range(1, 4)
                    .Select(quarterIndex => new QuarterlyDataItem
                    {
                        Quarter = $"Q{quarterIndex}",
                        QuarterIndex = quarterIndex,
                        Revenue = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Revenue),
                        Expenses = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Expenses),
                        Profit = g.SelectMany(r => r.QuarterlyData).Where(q => q.QuarterIndex == quarterIndex).Sum(q => q.Profit)
                    }).ToList(),

                YearlyTotals = g.GroupBy(r => r.Year)
                    .Select(yg => new YearlyDataItem
                    {
                        Year = yg.Key,
                        Revenue = yg.Sum(r => r.Revenue),
                        Expenses = yg.Sum(r => r.Expenses),
                        Profit = yg.Sum(r => r.Profit)
                    }).ToList(),

                KPIs = g.SelectMany(r => r.KPIs).ToList()
            })
            .OrderBy(s => s.Segment)
            .ToList();
    }
}
