// Program.cs (.NET 8 style with OData)

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(opt =>
    opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(1000)
        .AddRouteComponents("odata", GetEdmModel()));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static Microsoft.OData.Edm.IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<QuarterlyReport>("QuarterlyReports");
    builder.EntitySet<MonthlyBreakdown>("MonthlyBreakdowns");
    builder.EntitySet<Kpi>("KPIs");
    builder.EntitySet<RegionEntity>("Regions");
    builder.EntitySet<DepartmentEntity>("Departments");
    builder.EntitySet<QuarterEntity>("Quarters");
    return builder.GetEdmModel();
}

// Models
public class QuarterlyReport
{
    [Key]
    public int Id { get; set; }
    public string Quarter { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
    public List<string> Metrics { get; set; } = new();
    public List<MonthlyBreakdown> MonthlyData { get; set; } = new();
    public List<Kpi> KPIs { get; set; } = new();
}

public class MonthlyBreakdown
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public int QuarterlyReportId { get; set; }
    public string Month { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
}

public class Kpi
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public int QuarterlyReportId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Target { get; set; }
}

public class RegionEntity
{
    [Key]
    public int Id { get; set; }
    public string Region { get; set; } = string.Empty;
}

public class DepartmentEntity
{
    [Key]
    public int Id { get; set; }
    public string Department { get; set; } = string.Empty;
}

public class QuarterEntity
{
    [Key]
    public int Id { get; set; }
    public string Quarter { get; set; } = string.Empty;
}

// Controllers
public class QuarterlyReportsController : ControllerBase
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Reports);

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var report = FinanceDataStore.Reports.FirstOrDefault(r => r.Id == key);
        if (report == null) return NotFound();
        return Ok(report);
    }
}

public class RegionsController : ControllerBase
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Regions);

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var region = FinanceDataStore.Regions.FirstOrDefault(r => r.Id == key);
        if (region == null) return NotFound();
        return Ok(region);
    }
}

public class DepartmentsController : ControllerBase
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Departments);

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var department = FinanceDataStore.Departments.FirstOrDefault(d => d.Id == key);
        if (department == null) return NotFound();
        return Ok(department);
    }
}

public class QuartersController : ControllerBase
{
    [EnableQuery]
    public IActionResult Get() => Ok(FinanceDataStore.Quarters);

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var quarter = FinanceDataStore.Quarters.FirstOrDefault(q => q.Id == key);
        if (quarter == null) return NotFound();
        return Ok(quarter);
    }
}
