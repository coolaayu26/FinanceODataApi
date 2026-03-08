using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add OData services
builder.Services.AddControllers().AddOData(opt =>
    opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(1000)
        .AddRouteComponents("odata", GetEdmModel()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware order for Templafy compatibility
app.UseHttpsRedirection();
app.UseCors();

// OData response headers
app.Use(async (context, next) =>
{
    await next();
    if (context.Request.Path.StartsWithSegments("/odata"))
    {
        context.Response.Headers["OData-Version"] = "4.0";
        context.Response.Headers["Cache-Control"] = "no-cache, no-store";
    }
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    // Register nested array types as ComplexTypes (NOT entity sets)
    builder.ComplexType<DepartmentDataItem>();
    builder.ComplexType<RegionDataItem>();
    builder.ComplexType<MonthlyDataItem>();
    builder.ComplexType<QuarterlyDataItem>();
    builder.ComplexType<YearlyDataItem>();
    builder.ComplexType<SegmentDataItem>();
    builder.ComplexType<KpiDataItem>();

    // Register top-level summary entity sets (one record per dimension value)
    builder.EntitySet<RegionSummary>("RegionSummaries");
    builder.EntitySet<DepartmentSummary>("DepartmentSummaries");
    builder.EntitySet<YearSummary>("YearSummaries");
    builder.EntitySet<SegmentSummary>("SegmentSummaries");

    return builder.GetEdmModel();
}

// ============= NESTED ARRAY MODELS (ComplexTypes) =============

public class DepartmentDataItem
{
    public string? Department { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
    public decimal GrowthRate { get; set; }
    public int HeadCount { get; set; }
}

public class RegionDataItem
{
    public string? Region { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
    public decimal GrowthRate { get; set; }
    public int HeadCount { get; set; }
}

public class MonthlyDataItem
{
    public string? Month { get; set; }
    public int MonthIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class QuarterlyDataItem
{
    public string? Quarter { get; set; }
    public int QuarterIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class YearlyDataItem
{
    public string? Year { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class SegmentDataItem
{
    public string? Segment { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class KpiDataItem
{
    public string? Department { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public string? Unit { get; set; }
}

// ============= TOP-LEVEL ENTITY MODELS (One record per dimension value) =============

// 6 records - user picks region, gets all chart arrays for that region
public class RegionSummary
{
    [Key] public int Id { get; set; }
    public string? Region { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit { get; set; }

    // Arrays for charts - aggregated across ALL years, segments, departments
    public IList<DepartmentDataItem> DepartmentData { get; set; } = new List<DepartmentDataItem>();  // pie chart (6 items)
    public IList<MonthlyDataItem> MonthlyTotals { get; set; } = new List<MonthlyDataItem>();        // line chart (12 items)
    public IList<QuarterlyDataItem> QuarterlyTotals { get; set; } = new List<QuarterlyDataItem>();  // bar chart (4 items)
    public IList<YearlyDataItem> YearlyTotals { get; set; } = new List<YearlyDataItem>();          // YoY trend (4 items)
    public IList<SegmentDataItem> SegmentData { get; set; } = new List<SegmentDataItem>();         // segment split (2 items)
    public IList<KpiDataItem> KPIs { get; set; } = new List<KpiDataItem>();                        // KPI table
}

// 6 records - user picks department, gets all chart arrays for that department
public class DepartmentSummary
{
    [Key] public int Id { get; set; }
    public string? Department { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit { get; set; }

    public IList<RegionDataItem> RegionData { get; set; } = new List<RegionDataItem>();             // pie chart (6 items)
    public IList<MonthlyDataItem> MonthlyTotals { get; set; } = new List<MonthlyDataItem>();        // line chart (12 items)
    public IList<QuarterlyDataItem> QuarterlyTotals { get; set; } = new List<QuarterlyDataItem>();  // bar chart (4 items)
    public IList<YearlyDataItem> YearlyTotals { get; set; } = new List<YearlyDataItem>();          // YoY trend (4 items)
    public IList<SegmentDataItem> SegmentData { get; set; } = new List<SegmentDataItem>();         // segment split (2 items)
    public IList<KpiDataItem> KPIs { get; set; } = new List<KpiDataItem>();                        // KPI table
}

// 4 records - user picks year, gets all chart arrays for that year
public class YearSummary
{
    [Key] public int Id { get; set; }
    public string? Year { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit { get; set; }

    public IList<RegionDataItem> RegionData { get; set; } = new List<RegionDataItem>();             // pie chart (6 items)
    public IList<DepartmentDataItem> DepartmentData { get; set; } = new List<DepartmentDataItem>();  // bar chart (6 items)
    public IList<MonthlyDataItem> MonthlyTotals { get; set; } = new List<MonthlyDataItem>();        // line chart (12 items)
    public IList<QuarterlyDataItem> QuarterlyTotals { get; set; } = new List<QuarterlyDataItem>();  // grouped bar (4 items)
    public IList<SegmentDataItem> SegmentData { get; set; } = new List<SegmentDataItem>();         // segment split (2 items)
    public IList<KpiDataItem> KPIs { get; set; } = new List<KpiDataItem>();                        // KPI table
}

// 2 records - user picks segment, gets all chart arrays for that segment
public class SegmentSummary
{
    [Key] public int Id { get; set; }
    public string? Segment { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit { get; set; }

    public IList<RegionDataItem> RegionData { get; set; } = new List<RegionDataItem>();             // pie chart (6 items)
    public IList<DepartmentDataItem> DepartmentData { get; set; } = new List<DepartmentDataItem>();  // bar chart (6 items)
    public IList<MonthlyDataItem> MonthlyTotals { get; set; } = new List<MonthlyDataItem>();        // line chart (12 items)
    public IList<QuarterlyDataItem> QuarterlyTotals { get; set; } = new List<QuarterlyDataItem>();  // grouped bar (4 items)
    public IList<YearlyDataItem> YearlyTotals { get; set; } = new List<YearlyDataItem>();          // YoY trend (4 items)
    public IList<KpiDataItem> KPIs { get; set; } = new List<KpiDataItem>();                        // KPI table
}

// ============= CONTROLLERS =============

public class RegionSummariesController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.RegionSummaries);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.RegionSummaries.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class DepartmentSummariesController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.DepartmentSummaries);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.DepartmentSummaries.FirstOrDefault(d => d.Id == key) is { } d
            ? Ok(d) : NotFound();
}

public class YearSummariesController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.YearSummaries);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.YearSummaries.FirstOrDefault(y => y.Id == key) is { } y
            ? Ok(y) : NotFound();
}

public class SegmentSummariesController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.SegmentSummaries);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.SegmentSummaries.FirstOrDefault(s => s.Id == key) is { } s
            ? Ok(s) : NotFound();
}
