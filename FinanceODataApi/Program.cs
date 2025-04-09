// Program.cs (.NET 8 style with OData)

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(opt =>
    opt.Select().Filter().OrderBy().Count().SetMaxTop(100)
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
    return builder.GetEdmModel();
}

// Model
public class QuarterlyReport
{
    public int Id { get; set; }
    public string Quarter { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}

// Controller
public class QuarterlyReportsController : ControllerBase
{
    private static readonly List<QuarterlyReport> Reports = new()
    {
        new() { Id = 1, Quarter = "Q1-2024", Department = "Sales", Region = "North America", Revenue = 120000, Expenses = 80000, Profit = 40000 },
        new() { Id = 2, Quarter = "Q1-2024", Department = "Marketing", Region = "Europe", Revenue = 90000, Expenses = 60000, Profit = 30000 },
        new() { Id = 3, Quarter = "Q2-2024", Department = "Sales", Region = "North America", Revenue = 140000, Expenses = 85000, Profit = 55000 },
        new() { Id = 4, Quarter = "Q2-2024", Department = "Marketing", Region = "Europe", Revenue = 95000, Expenses = 65000, Profit = 30000 },
        new() { Id = 5, Quarter = "Q3-2024", Department = "Sales", Region = "Asia", Revenue = 135000, Expenses = 87000, Profit = 48000 },
        new() { Id = 6, Quarter = "Q3-2024", Department = "Marketing", Region = "Asia", Revenue = 97000, Expenses = 62000, Profit = 35000 },
        new() { Id = 7, Quarter = "Q4-2024", Department = "Sales", Region = "North America", Revenue = 150000, Expenses = 95000, Profit = 55000 },
        new() { Id = 8, Quarter = "Q4-2024", Department = "Marketing", Region = "Europe", Revenue = 105000, Expenses = 70000, Profit = 35000 },
        new() { Id = 9, Quarter = "Q1-2024", Department = "Support", Region = "North America", Revenue = 50000, Expenses = 30000, Profit = 20000 },
        new() { Id = 10, Quarter = "Q2-2024", Department = "Support", Region = "Asia", Revenue = 60000, Expenses = 35000, Profit = 25000 },
        new() { Id = 11, Quarter = "Q3-2024", Department = "Support", Region = "Europe", Revenue = 55000, Expenses = 33000, Profit = 22000 },
        new() { Id = 12, Quarter = "Q4-2024", Department = "Support", Region = "North America", Revenue = 58000, Expenses = 34000, Profit = 24000 },
    };

    [EnableQuery]
    public IActionResult Get() => Ok(Reports);
}
