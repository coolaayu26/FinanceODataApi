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

// FIX 1: CORS middleware placement - MUST be before UseRouting and UseAuthorization
app.UseHttpsRedirection();
app.UseCors();

// FIX 3: OData response headers middleware
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

    // Register existing entity sets
    var reportsSet = builder.EntitySet<QuarterlyReport>("QuarterlyReports");
    builder.EntitySet<MonthlyBreakdown>("MonthlyBreakdowns");
    builder.EntitySet<QuarterlyBreakdown>("QuarterlyBreakdowns");
    builder.EntitySet<Kpi>("KPIs");
    builder.EntitySet<RegionEntity>("Regions");
    builder.EntitySet<DepartmentEntity>("Departments");
    builder.EntitySet<YearEntity>("Years");
    builder.EntitySet<SegmentEntity>("Segments");

    // Configure navigation property bindings
    reportsSet.HasManyBinding(r => r.MonthlyData, "MonthlyBreakdowns");
    reportsSet.HasManyBinding(r => r.QuarterlyData, "QuarterlyBreakdowns");
    reportsSet.HasManyBinding(r => r.KPIs, "KPIs");

    // FIX 4: Register pre-aggregated view entity sets for Templafy charts
    builder.EntitySet<RevenueByDepartment>("Revenue_by_Department");
    builder.EntitySet<RevenueByMonth>("Revenue_by_Month");
    builder.EntitySet<RevenueByQuarter>("Revenue_by_Quarter");
    builder.EntitySet<RevenueByYear>("Revenue_by_Year");
    builder.EntitySet<KpiSummary>("KPI_Summary");

    return builder.GetEdmModel();
}

// ============= Models =============

// FIX 2: Changed all non-key string properties to nullable (string?)
public class QuarterlyReport
{
    [Key] public int Id { get; set; }
    public string? Region { get; set; }
    public string? Department { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public decimal AnnualRevenue { get; set; }
    public decimal AnnualExpenses { get; set; }
    public decimal AnnualProfit { get; set; }
    public decimal GrowthRate { get; set; }
    public int HeadCount { get; set; }
    public virtual IList<MonthlyBreakdown> MonthlyData { get; set; } = new List<MonthlyBreakdown>();
    public virtual IList<QuarterlyBreakdown> QuarterlyData { get; set; } = new List<QuarterlyBreakdown>();
    public virtual IList<Kpi> KPIs { get; set; } = new List<Kpi>();
}

public class MonthlyBreakdown
{
    [Key] public string Id { get; set; } = string.Empty;
    public int QuarterlyReportId { get; set; }
    public string? Month { get; set; }
    public int MonthIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class QuarterlyBreakdown
{
    [Key] public string Id { get; set; } = string.Empty;
    public int QuarterlyReportId { get; set; }
    public string? Quarter { get; set; }
    public int QuarterIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class Kpi
{
    [Key] public string Id { get; set; } = string.Empty;
    public int QuarterlyReportId { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public string? Unit { get; set; }
}

public class RegionEntity
{
    [Key] public int Id { get; set; }
    public string? Region { get; set; }
}

public class DepartmentEntity
{
    [Key] public int Id { get; set; }
    public string? Department { get; set; }
}

public class YearEntity
{
    [Key] public int Id { get; set; }
    public string? Year { get; set; }
}

public class SegmentEntity
{
    [Key] public int Id { get; set; }
    public string? Segment { get; set; }
}

// FIX 4: Pre-aggregated view models for Templafy charts
public class RevenueByDepartment
{
    [Key] public string Id { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public string? Department { get; set; }
    public decimal AnnualRevenue { get; set; }
    public decimal AnnualExpenses { get; set; }
    public decimal AnnualProfit { get; set; }
    public decimal GrowthRate { get; set; }
    public int HeadCount { get; set; }
}

public class RevenueByMonth
{
    [Key] public string Id { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public string? Month { get; set; }
    public int MonthIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class RevenueByQuarter
{
    [Key] public string Id { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public string? Quarter { get; set; }
    public int QuarterIndex { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

public class RevenueByYear
{
    [Key] public string Id { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal AvgGrowthRate { get; set; }
    public int TotalHeadCount { get; set; }
}

public class KpiSummary
{
    [Key] public string Id { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? Year { get; set; }
    public string? Segment { get; set; }
    public string? Department { get; set; }
    public string? KpiName { get; set; }
    public decimal KpiValue { get; set; }
    public string? Unit { get; set; }
}

// ============= Controllers =============

public class QuarterlyReportsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Reports);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.Reports.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class MonthlyBreakdownsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Reports.SelectMany(r => r.MonthlyData));

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.Reports.SelectMany(r => r.MonthlyData).FirstOrDefault(m => m.Id == key) is { } m
            ? Ok(m) : NotFound();
}

public class QuarterlyBreakdownsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Reports.SelectMany(r => r.QuarterlyData));

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.Reports.SelectMany(r => r.QuarterlyData).FirstOrDefault(q => q.Id == key) is { } q
            ? Ok(q) : NotFound();
}

public class KPIsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Reports.SelectMany(r => r.KPIs));

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.Reports.SelectMany(r => r.KPIs).FirstOrDefault(k => k.Id == key) is { } k
            ? Ok(k) : NotFound();
}

public class RegionsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Regions);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.Regions.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class DepartmentsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Departments);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.Departments.FirstOrDefault(d => d.Id == key) is { } d
            ? Ok(d) : NotFound();
}

public class YearsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Years);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.Years.FirstOrDefault(y => y.Id == key) is { } y
            ? Ok(y) : NotFound();
}

public class SegmentsController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Segments);

    [EnableQuery]
    public IActionResult Get(int key) =>
        FinanceDataStore.Segments.FirstOrDefault(s => s.Id == key) is { } s
            ? Ok(s) : NotFound();
}

// FIX 4: Controllers for pre-aggregated views
public class Revenue_by_DepartmentController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.RevenueByDepartment);

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.RevenueByDepartment.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class Revenue_by_MonthController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.RevenueByMonth);

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.RevenueByMonth.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class Revenue_by_QuarterController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.RevenueByQuarter);

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.RevenueByQuarter.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class Revenue_by_YearController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.RevenueByYear);

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.RevenueByYear.FirstOrDefault(r => r.Id == key) is { } r
            ? Ok(r) : NotFound();
}

public class KPI_SummaryController : ODataController
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.KpiSummary);

    [EnableQuery]
    public IActionResult Get(string key) =>
        FinanceDataStore.KpiSummary.FirstOrDefault(k => k.Id == key) is { } k
            ? Ok(k) : NotFound();
}
