using System.Text.Json;

public static class FinanceDataStore
{
    public static readonly List<QuarterlyReport> Reports;
    public static readonly List<RegionEntity> Regions;
    public static readonly List<DepartmentEntity> Departments;
    public static readonly List<YearEntity> Years;
    public static readonly List<SegmentEntity> Segments;

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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading seed data: {ex.Message}");
            Reports = new List<QuarterlyReport>();
            Regions = new List<RegionEntity>();
            Departments = new List<DepartmentEntity>();
            Years = new List<YearEntity>();
            Segments = new List<SegmentEntity>();
        }
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
