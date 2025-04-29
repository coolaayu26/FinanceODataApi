// Program.cs (.NET 8 style with OData)

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;
using System.ComponentModel.DataAnnotations;

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

    // Define entity types and sets
    var quarterlyReport = builder.EntitySet<QuarterlyReport>("QuarterlyReports").EntityType;
    var monthlyBreakdown = builder.EntityType<MonthlyBreakdown>();
    var kpi = builder.EntityType<Kpi>();

    // Define contained navigation properties
    quarterlyReport.HasMany(q => q.MonthlyData).Contained();
    quarterlyReport.HasMany(q => q.KPIs).Contained();

    return builder.GetEdmModel();
}


// Model
public class QuarterlyReport
{
    public string Id { get; set; } = string.Empty;
    public string Quarter { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
    public List<string> Metrics { get; set; } = new();

    [Contained]
    public virtual IList<MonthlyBreakdown> MonthlyData { get; set; } = new List<MonthlyBreakdown>();

    [Contained]
    public virtual IList<Kpi> KPIs { get; set; } = new List<Kpi>();
}


public class MonthlyBreakdown
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Add a unique ID
    public string? QuarterlyReportId { get; set; } // Link back to parent
    public string Month { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
}

public class Kpi
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Add a unique ID
    public string? QuarterlyReportId { get; set; } // Link back to parent
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
}

// Controller
public class QuarterlyReportsController : ControllerBase
{
    private static readonly List<QuarterlyReport> Reports = new()
    {
        new()
        {
            Id = "3", Quarter = "Q1-2023", Department = "Private Equity", Region = "North America",
            Revenue = 124257.35M, Expenses = 77241.19M, Profit = 47016.16M, Metrics = new List<string>{"EBITDA", "Net New Clients" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 11720.25M, Expenses = 6421.04M },
                new() { Month = "February", Revenue = 7439.63M, Expenses = 5303.17M },
                new() { Month = "March", Revenue = 9540.42M, Expenses = 5394.46M },
                new() { Month = "April", Revenue = 11629.32M, Expenses = 6296.18M },
                new() { Month = "May", Revenue = 7884.16M, Expenses = 5008.62M },
                new() { Month = "June", Revenue = 10383.82M, Expenses = 8087.3M },
                new() { Month = "July", Revenue = 10539.75M, Expenses = 5988.36M },
                new() { Month = "August", Revenue = 7984.94M, Expenses = 6602.39M },
                new() { Month = "September", Revenue = 12795.41M, Expenses = 5901.85M },
                new() { Month = "October", Revenue = 13231.65M, Expenses = 7307.42M },
                new() { Month = "November", Revenue = 8211.21M, Expenses = 5773.09M },
                new() { Month = "December", Revenue = 13000.92M, Expenses = 7224.25M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 192.0M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 175.0M, Unit = "%" }
            }
        },

        new()
        {
            Id = "4", Quarter = "Q1-2023", Department = "Research", Region = "South America",
            Revenue = 187292.19M, Expenses = 135068.89M, Profit = 52223.29999999999M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 17107.81M, Expenses = 11149.59M },
                new() { Month = "February", Revenue = 14193.56M, Expenses = 12584.52M },
                new() { Month = "March", Revenue = 18568.7M, Expenses = 12927.47M },
                new() { Month = "April", Revenue = 16245.02M, Expenses = 12721.97M },
                new() { Month = "May", Revenue = 18334.18M, Expenses = 9450.16M },
                new() { Month = "June", Revenue = 13337.29M, Expenses = 10478.36M },
                new() { Month = "July", Revenue = 17393.93M, Expenses = 10950.57M },
                new() { Month = "August", Revenue = 16876.03M, Expenses = 11319.49M },
                new() { Month = "September", Revenue = 16517.5M, Expenses = 12424.32M },
                new() { Month = "October", Revenue = 12762.37M, Expenses = 10494.45M },
                new() { Month = "November", Revenue = 13496.78M, Expenses = 9975.93M },
                new() { Month = "December", Revenue = 14572.09M, Expenses = 13201.18M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 169.54M, Unit = "%" },
                new() { Name = "EBITDA", Value = 19.16M, Unit = "%" }
            }
        },

        new()
        {
            Id = "5", Quarter = "Q3-2023", Department = "Compliance", Region = "Asia",
            Revenue = 190346.59M, Expenses = 119355.1M, Profit = 70991.48999999999M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 13874.67M, Expenses = 11904.93M },
                new() { Month = "February", Revenue = 13993.88M, Expenses = 11365.23M },
                new() { Month = "March", Revenue = 17023.49M, Expenses = 11598.19M },
                new() { Month = "April", Revenue = 13545.18M, Expenses = 11836.84M },
                new() { Month = "May", Revenue = 15062.41M, Expenses = 11700.41M },
                new() { Month = "June", Revenue = 15590.66M, Expenses = 8270.97M },
                new() { Month = "July", Revenue = 13681.91M, Expenses = 9398.71M },
                new() { Month = "August", Revenue = 17598.43M, Expenses = 8381.46M },
                new() { Month = "September", Revenue = 15413.99M, Expenses = 10819.2M },
                new() { Month = "October", Revenue = 18308.73M, Expenses = 9053.6M },
                new() { Month = "November", Revenue = 14832.11M, Expenses = 8212.95M },
                new() { Month = "December", Revenue = 18394.83M, Expenses = 11335.41M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 91.11M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 162.51M, Unit = "%" }
            }
        },

        new()
        {
            Id = "6", Quarter = "Q3-2023", Department = "Wealth Management", Region = "Middle East",
            Revenue = 83188.97M, Expenses = 67034.14M, Profit = 16154.830000000002M, Metrics = new List<string>{"Net Margin", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 5470.96M, Expenses = 5611.8M },
                new() { Month = "February", Revenue = 6033.12M, Expenses = 5165.83M },
                new() { Month = "March", Revenue = 8256.47M, Expenses = 4646.32M },
                new() { Month = "April", Revenue = 8625.2M, Expenses = 4204.4M },
                new() { Month = "May", Revenue = 8703.84M, Expenses = 4768.54M },
                new() { Month = "June", Revenue = 7708.92M, Expenses = 4567.9M },
                new() { Month = "July", Revenue = 5329.54M, Expenses = 6288.38M },
                new() { Month = "August", Revenue = 5227.45M, Expenses = 6930.96M },
                new() { Month = "September", Revenue = 9474.14M, Expenses = 6179.62M },
                new() { Month = "October", Revenue = 7126.81M, Expenses = 4154.74M },
                new() { Month = "November", Revenue = 7009.12M, Expenses = 7556.42M },
                new() { Month = "December", Revenue = 4312.43M, Expenses = 4872.6M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 8.46M, Unit = "%" },
                new() { Name = "EBITDA", Value = 190.34M, Unit = "%" }
            }
        },

        new()
        {
            Id = "7", Quarter = "Q3-2023", Department = "Risk", Region = "Europe",
            Revenue = 113091.2M, Expenses = 77810.87M, Profit = 35280.33M, Metrics = new List<string>{"AUM", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 8496.4M, Expenses = 8100.21M },
                new() { Month = "February", Revenue = 9424.74M, Expenses = 7416.32M },
                new() { Month = "March", Revenue = 9685.88M, Expenses = 7423.86M },
                new() { Month = "April", Revenue = 11561.82M, Expenses = 5543.89M },
                new() { Month = "May", Revenue = 11567.2M, Expenses = 5541.64M },
                new() { Month = "June", Revenue = 7105.42M, Expenses = 6534.47M },
                new() { Month = "July", Revenue = 8364.92M, Expenses = 4801.27M },
                new() { Month = "August", Revenue = 9930.11M, Expenses = 6680.38M },
                new() { Month = "September", Revenue = 6626.67M, Expenses = 4869.47M },
                new() { Month = "October", Revenue = 6616.26M, Expenses = 6064.42M },
                new() { Month = "November", Revenue = 8241.71M, Expenses = 7759.16M },
                new() { Month = "December", Revenue = 8767.0M, Expenses = 7311.22M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 117.08M, Unit = "%" },
                new() { Name = "IRR", Value = 105.52M, Unit = "%" }
            }
        },

        new()
        {
            Id = "8", Quarter = "Q3-2023", Department = "Risk", Region = "Middle East",
            Revenue = 145170.23M, Expenses = 100433.31M, Profit = 44736.92000000001M, Metrics = new List<string>{"AUM", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12427.33M, Expenses = 9620.12M },
                new() { Month = "February", Revenue = 11704.01M, Expenses = 8254.64M },
                new() { Month = "March", Revenue = 12160.98M, Expenses = 9465.6M },
                new() { Month = "April", Revenue = 12483.92M, Expenses = 9116.0M },
                new() { Month = "May", Revenue = 9250.76M, Expenses = 9922.11M },
                new() { Month = "June", Revenue = 12240.66M, Expenses = 10269.67M },
                new() { Month = "July", Revenue = 10654.72M, Expenses = 9017.05M },
                new() { Month = "August", Revenue = 11316.19M, Expenses = 6679.61M },
                new() { Month = "September", Revenue = 12259.77M, Expenses = 6584.83M },
                new() { Month = "October", Revenue = 11353.02M, Expenses = 8352.18M },
                new() { Month = "November", Revenue = 13701.13M, Expenses = 9459.47M },
                new() { Month = "December", Revenue = 10562.8M, Expenses = 8478.32M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 110.6M, Unit = "%" },
                new() { Name = "AUM", Value = 26.45M, Unit = "%" }
            }
        },

        new()
        {
            Id = "9", Quarter = "Q3-2023", Department = "Wealth Management", Region = "Africa",
            Revenue = 216117.75M, Expenses = 155274.15M, Profit = 60843.600000000006M, Metrics = new List<string>{"Net Margin", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 19365.47M, Expenses = 11649.68M },
                new() { Month = "February", Revenue = 16162.04M, Expenses = 12636.7M },
                new() { Month = "March", Revenue = 17232.45M, Expenses = 12614.33M },
                new() { Month = "April", Revenue = 17838.96M, Expenses = 13977.22M },
                new() { Month = "May", Revenue = 16630.61M, Expenses = 11997.81M },
                new() { Month = "June", Revenue = 20607.63M, Expenses = 12976.85M },
                new() { Month = "July", Revenue = 16731.52M, Expenses = 12353.03M },
                new() { Month = "August", Revenue = 17941.08M, Expenses = 11648.61M },
                new() { Month = "September", Revenue = 16220.96M, Expenses = 11250.2M },
                new() { Month = "October", Revenue = 20873.38M, Expenses = 12680.16M },
                new() { Month = "November", Revenue = 17048.99M, Expenses = 12126.72M },
                new() { Month = "December", Revenue = 16324.71M, Expenses = 13871.31M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 30.51M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 71.09M, Unit = "%" }
            }
        },

        new()
        {
            Id = "10", Quarter = "Q2-2023", Department = "Private Equity", Region = "North America",
            Revenue = 81339.87M, Expenses = 59169.44M, Profit = 22170.429999999993M, Metrics = new List<string>{"AUM", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 4764.24M, Expenses = 5644.18M },
                new() { Month = "February", Revenue = 8236.42M, Expenses = 5545.86M },
                new() { Month = "March", Revenue = 9214.7M, Expenses = 3585.69M },
                new() { Month = "April", Revenue = 6844.35M, Expenses = 3974.04M },
                new() { Month = "May", Revenue = 9757.39M, Expenses = 5977.57M },
                new() { Month = "June", Revenue = 5067.69M, Expenses = 6289.34M },
                new() { Month = "July", Revenue = 8761.98M, Expenses = 4870.49M },
                new() { Month = "August", Revenue = 9062.36M, Expenses = 6136.99M },
                new() { Month = "September", Revenue = 4288.44M, Expenses = 4323.27M },
                new() { Month = "October", Revenue = 5276.81M, Expenses = 3515.58M },
                new() { Month = "November", Revenue = 8927.17M, Expenses = 4941.05M },
                new() { Month = "December", Revenue = 7042.55M, Expenses = 5148.67M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 98.4M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 73.54M, Unit = "%" }
            }
        },

        new()
        {
            Id = "11", Quarter = "Q4-2023", Department = "Sales", Region = "North America",
            Revenue = 127954.63M, Expenses = 80266.34M, Profit = 47688.29000000001M, Metrics = new List<string>{"IRR", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12601.49M, Expenses = 6383.26M },
                new() { Month = "February", Revenue = 10540.78M, Expenses = 7663.38M },
                new() { Month = "March", Revenue = 11778.68M, Expenses = 5187.34M },
                new() { Month = "April", Revenue = 11797.42M, Expenses = 6681.38M },
                new() { Month = "May", Revenue = 8291.84M, Expenses = 8001.9M },
                new() { Month = "June", Revenue = 11791.59M, Expenses = 7623.8M },
                new() { Month = "July", Revenue = 13504.92M, Expenses = 5375.64M },
                new() { Month = "August", Revenue = 12256.95M, Expenses = 5514.55M },
                new() { Month = "September", Revenue = 10691.73M, Expenses = 5699.5M },
                new() { Month = "October", Revenue = 10104.68M, Expenses = 6703.36M },
                new() { Month = "November", Revenue = 10189.73M, Expenses = 4804.95M },
                new() { Month = "December", Revenue = 9532.09M, Expenses = 8523.22M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net New Clients", Value = 124.76M, Unit = "%" },
                new() { Name = "EBITDA", Value = 79.8M, Unit = "%" }
            }
        },

        new()
        {
            Id = "12", Quarter = "Q2-2023", Department = "Wealth Management", Region = "Africa",
            Revenue = 266982.53M, Expenses = 215579.42M, Profit = 51403.110000000015M, Metrics = new List<string>{"AUM", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 23644.75M, Expenses = 16523.87M },
                new() { Month = "February", Revenue = 24177.43M, Expenses = 19378.33M },
                new() { Month = "March", Revenue = 20743.49M, Expenses = 17646.35M },
                new() { Month = "April", Revenue = 22490.74M, Expenses = 19721.83M },
                new() { Month = "May", Revenue = 24497.51M, Expenses = 18347.41M },
                new() { Month = "June", Revenue = 23909.9M, Expenses = 18070.47M },
                new() { Month = "July", Revenue = 22536.53M, Expenses = 17785.23M },
                new() { Month = "August", Revenue = 22837.74M, Expenses = 16531.42M },
                new() { Month = "September", Revenue = 21933.48M, Expenses = 18214.64M },
                new() { Month = "October", Revenue = 20524.65M, Expenses = 18733.89M },
                new() { Month = "November", Revenue = 21297.15M, Expenses = 19852.75M },
                new() { Month = "December", Revenue = 22929.75M, Expenses = 19652.3M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 20.61M, Unit = "%" },
                new() { Name = "AUM", Value = 57.34M, Unit = "%" }
            }
        },

        new()
        {
            Id = "13", Quarter = "Q2-2023", Department = "Compliance", Region = "South America",
            Revenue = 242406.78M, Expenses = 183132.57M, Profit = 59274.20999999999M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 20423.91M, Expenses = 14240.04M },
                new() { Month = "February", Revenue = 17817.4M, Expenses = 13984.29M },
                new() { Month = "March", Revenue = 22894.4M, Expenses = 14842.22M },
                new() { Month = "April", Revenue = 20602.05M, Expenses = 13304.33M },
                new() { Month = "May", Revenue = 21206.83M, Expenses = 16399.77M },
                new() { Month = "June", Revenue = 17302.58M, Expenses = 14409.65M },
                new() { Month = "July", Revenue = 17418.74M, Expenses = 16169.91M },
                new() { Month = "August", Revenue = 22938.08M, Expenses = 16247.19M },
                new() { Month = "September", Revenue = 21521.35M, Expenses = 14191.9M },
                new() { Month = "October", Revenue = 18776.87M, Expenses = 13283.85M },
                new() { Month = "November", Revenue = 19498.97M, Expenses = 15015.92M },
                new() { Month = "December", Revenue = 19109.54M, Expenses = 17064.78M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 121.34M, Unit = "%" },
                new() { Name = "IRR", Value = 91.7M, Unit = "%" }
            }
        },

        new()
        {
            Id = "14", Quarter = "Q1-2023", Department = "Research", Region = "North America",
            Revenue = 257804.96M, Expenses = 190802.0M, Profit = 67002.95999999999M, Metrics = new List<string>{"Return on Equity", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 24433.94M, Expenses = 14745.46M },
                new() { Month = "February", Revenue = 21156.24M, Expenses = 16101.67M },
                new() { Month = "March", Revenue = 20544.33M, Expenses = 15341.1M },
                new() { Month = "April", Revenue = 19294.82M, Expenses = 13958.52M },
                new() { Month = "May", Revenue = 22596.14M, Expenses = 14974.99M },
                new() { Month = "June", Revenue = 20937.13M, Expenses = 15049.9M },
                new() { Month = "July", Revenue = 19087.57M, Expenses = 17633.04M },
                new() { Month = "August", Revenue = 19283.12M, Expenses = 14247.29M },
                new() { Month = "September", Revenue = 22613.7M, Expenses = 17291.27M },
                new() { Month = "October", Revenue = 23040.88M, Expenses = 15840.22M },
                new() { Month = "November", Revenue = 21396.52M, Expenses = 17510.7M },
                new() { Month = "December", Revenue = 19557.07M, Expenses = 14335.74M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 146.98M, Unit = "%" },
                new() { Name = "Net Margin", Value = 191.65M, Unit = "%" }
            }
        },

        new()
        {
            Id = "15", Quarter = "Q1-2023", Department = "Private Equity", Region = "Africa",
            Revenue = 227332.06M, Expenses = 188708.31M, Profit = 38623.75M, Metrics = new List<string>{"EBITDA", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 19720.95M, Expenses = 14273.15M },
                new() { Month = "February", Revenue = 20159.45M, Expenses = 15199.97M },
                new() { Month = "March", Revenue = 16643.01M, Expenses = 16926.66M },
                new() { Month = "April", Revenue = 19452.68M, Expenses = 15504.03M },
                new() { Month = "May", Revenue = 17595.51M, Expenses = 15087.65M },
                new() { Month = "June", Revenue = 19115.57M, Expenses = 15200.58M },
                new() { Month = "July", Revenue = 18794.44M, Expenses = 16997.75M },
                new() { Month = "August", Revenue = 16257.25M, Expenses = 16554.31M },
                new() { Month = "September", Revenue = 21190.88M, Expenses = 17630.15M },
                new() { Month = "October", Revenue = 21510.06M, Expenses = 15267.1M },
                new() { Month = "November", Revenue = 21180.72M, Expenses = 15655.46M },
                new() { Month = "December", Revenue = 19811.45M, Expenses = 15066.68M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 105.34M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 144.08M, Unit = "%" }
            }
        },

        new()
        {
            Id = "16", Quarter = "Q4-2023", Department = "Wealth Management", Region = "Asia",
            Revenue = 159439.39M, Expenses = 120395.17M, Profit = 39044.220000000016M, Metrics = new List<string>{"AUM", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 11918.47M, Expenses = 8938.25M },
                new() { Month = "February", Revenue = 12782.15M, Expenses = 11161.97M },
                new() { Month = "March", Revenue = 14635.54M, Expenses = 9518.1M },
                new() { Month = "April", Revenue = 15414.73M, Expenses = 10835.3M },
                new() { Month = "May", Revenue = 14770.38M, Expenses = 9463.35M },
                new() { Month = "June", Revenue = 15629.28M, Expenses = 8828.3M },
                new() { Month = "July", Revenue = 13193.23M, Expenses = 11074.73M },
                new() { Month = "August", Revenue = 14686.08M, Expenses = 11360.34M },
                new() { Month = "September", Revenue = 15079.05M, Expenses = 8934.33M },
                new() { Month = "October", Revenue = 14074.69M, Expenses = 8760.39M },
                new() { Month = "November", Revenue = 10607.44M, Expenses = 9265.92M },
                new() { Month = "December", Revenue = 14087.03M, Expenses = 10750.47M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 25.23M, Unit = "%" },
                new() { Name = "EBITDA", Value = 181.09M, Unit = "%" }
            }
        },

        new()
        {
            Id = "17", Quarter = "Q4-2023", Department = "Compliance", Region = "Europe",
            Revenue = 275921.23M, Expenses = 240772.56M, Profit = 35148.669999999984M, Metrics = new List<string>{"Net Margin", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 22286.35M, Expenses = 19076.22M },
                new() { Month = "February", Revenue = 24926.45M, Expenses = 19738.26M },
                new() { Month = "March", Revenue = 24364.89M, Expenses = 20168.78M },
                new() { Month = "April", Revenue = 20305.56M, Expenses = 19659.47M },
                new() { Month = "May", Revenue = 22531.16M, Expenses = 20550.29M },
                new() { Month = "June", Revenue = 20705.71M, Expenses = 18958.54M },
                new() { Month = "July", Revenue = 22847.45M, Expenses = 18893.42M },
                new() { Month = "August", Revenue = 22180.66M, Expenses = 21529.59M },
                new() { Month = "September", Revenue = 22041.43M, Expenses = 19821.17M },
                new() { Month = "October", Revenue = 23405.45M, Expenses = 19349.48M },
                new() { Month = "November", Revenue = 21825.5M, Expenses = 19446.95M },
                new() { Month = "December", Revenue = 24950.71M, Expenses = 19359.33M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 60.39M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 130.37M, Unit = "%" }
            }
        },

        new()
        {
            Id = "18", Quarter = "Q1-2023", Department = "Compliance", Region = "Europe",
            Revenue = 280788.91M, Expenses = 213894.02M, Profit = 66894.88999999998M, Metrics = new List<string>{"Net Margin", "Net New Clients" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 25830.58M, Expenses = 16019.15M },
                new() { Month = "February", Revenue = 23418.75M, Expenses = 17840.44M },
                new() { Month = "March", Revenue = 22247.74M, Expenses = 16332.36M },
                new() { Month = "April", Revenue = 22606.8M, Expenses = 17670.1M },
                new() { Month = "May", Revenue = 22731.94M, Expenses = 18894.48M },
                new() { Month = "June", Revenue = 21030.03M, Expenses = 18644.81M },
                new() { Month = "July", Revenue = 23766.06M, Expenses = 18018.0M },
                new() { Month = "August", Revenue = 25486.3M, Expenses = 17129.58M },
                new() { Month = "September", Revenue = 24958.74M, Expenses = 16324.76M },
                new() { Month = "October", Revenue = 22369.08M, Expenses = 18318.94M },
                new() { Month = "November", Revenue = 21727.22M, Expenses = 17153.96M },
                new() { Month = "December", Revenue = 25444.94M, Expenses = 18432.57M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 127.63M, Unit = "%" },
                new() { Name = "AUM", Value = 175.37M, Unit = "%" }
            }
        },

        new()
        {
            Id = "19", Quarter = "Q1-2023", Department = "Wealth Management", Region = "North America",
            Revenue = 104950.48M, Expenses = 78952.02M, Profit = 25998.459999999992M, Metrics = new List<string>{"EBITDA", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 7875.68M, Expenses = 7252.81M },
                new() { Month = "February", Revenue = 7933.58M, Expenses = 5411.42M },
                new() { Month = "March", Revenue = 5945.44M, Expenses = 8354.41M },
                new() { Month = "April", Revenue = 6135.14M, Expenses = 7190.54M },
                new() { Month = "May", Revenue = 10928.44M, Expenses = 7420.13M },
                new() { Month = "June", Revenue = 9278.96M, Expenses = 7243.17M },
                new() { Month = "July", Revenue = 7775.14M, Expenses = 5260.43M },
                new() { Month = "August", Revenue = 10553.23M, Expenses = 6838.14M },
                new() { Month = "September", Revenue = 10413.38M, Expenses = 7395.43M },
                new() { Month = "October", Revenue = 8368.63M, Expenses = 7659.11M },
                new() { Month = "November", Revenue = 8080.34M, Expenses = 7190.56M },
                new() { Month = "December", Revenue = 7778.93M, Expenses = 7544.09M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 135.18M, Unit = "%" },
                new() { Name = "IRR", Value = 153.5M, Unit = "%" }
            }
        },

        new()
        {
            Id = "20", Quarter = "Q4-2023", Department = "Compliance", Region = "North America",
            Revenue = 155849.96M, Expenses = 98034.73M, Profit = 57815.229999999996M, Metrics = new List<string>{"Net New Clients", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12543.91M, Expenses = 7130.45M },
                new() { Month = "February", Revenue = 15735.13M, Expenses = 9521.11M },
                new() { Month = "March", Revenue = 10776.13M, Expenses = 8108.09M },
                new() { Month = "April", Revenue = 14822.47M, Expenses = 7796.63M },
                new() { Month = "May", Revenue = 12645.88M, Expenses = 6786.49M },
                new() { Month = "June", Revenue = 10622.75M, Expenses = 7865.18M },
                new() { Month = "July", Revenue = 11737.21M, Expenses = 9635.34M },
                new() { Month = "August", Revenue = 15971.29M, Expenses = 9460.77M },
                new() { Month = "September", Revenue = 15259.97M, Expenses = 6353.22M },
                new() { Month = "October", Revenue = 12433.65M, Expenses = 8771.11M },
                new() { Month = "November", Revenue = 14200.85M, Expenses = 7926.16M },
                new() { Month = "December", Revenue = 14093.5M, Expenses = 6495.6M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 36.94M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 67.33M, Unit = "%" }
            }
        },

        new()
        {
            Id = "21", Quarter = "Q2-2023", Department = "Private Equity", Region = "Middle East",
            Revenue = 109241.19M, Expenses = 76717.74M, Profit = 32523.449999999997M, Metrics = new List<string>{"EBITDA", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 6659.54M, Expenses = 4913.23M },
                new() { Month = "February", Revenue = 9534.8M, Expenses = 5222.56M },
                new() { Month = "March", Revenue = 7523.91M, Expenses = 6095.26M },
                new() { Month = "April", Revenue = 6975.94M, Expenses = 7135.67M },
                new() { Month = "May", Revenue = 9599.33M, Expenses = 6210.51M },
                new() { Month = "June", Revenue = 6845.2M, Expenses = 8091.8M },
                new() { Month = "July", Revenue = 7910.03M, Expenses = 4944.28M },
                new() { Month = "August", Revenue = 7673.36M, Expenses = 6000.76M },
                new() { Month = "September", Revenue = 9149.15M, Expenses = 4524.5M },
                new() { Month = "October", Revenue = 8578.84M, Expenses = 6670.61M },
                new() { Month = "November", Revenue = 7710.77M, Expenses = 6642.28M },
                new() { Month = "December", Revenue = 9518.87M, Expenses = 7912.97M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 123.4M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 119.7M, Unit = "%" }
            }
        },

        new()
        {
            Id = "22", Quarter = "Q3-2023", Department = "Compliance", Region = "North America",
            Revenue = 231467.01M, Expenses = 193638.76M, Profit = 37828.25M, Metrics = new List<string>{"Net New Clients", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 18326.01M, Expenses = 15789.43M },
                new() { Month = "February", Revenue = 21533.98M, Expenses = 16657.16M },
                new() { Month = "March", Revenue = 19870.42M, Expenses = 14728.87M },
                new() { Month = "April", Revenue = 18249.2M, Expenses = 16734.46M },
                new() { Month = "May", Revenue = 17736.95M, Expenses = 15562.44M },
                new() { Month = "June", Revenue = 21422.06M, Expenses = 16854.72M },
                new() { Month = "July", Revenue = 21373.87M, Expenses = 16826.9M },
                new() { Month = "August", Revenue = 20903.99M, Expenses = 14549.77M },
                new() { Month = "September", Revenue = 18921.0M, Expenses = 14968.91M },
                new() { Month = "October", Revenue = 16674.07M, Expenses = 16133.97M },
                new() { Month = "November", Revenue = 16346.68M, Expenses = 14506.11M },
                new() { Month = "December", Revenue = 16723.41M, Expenses = 17817.29M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 43.04M, Unit = "%" },
                new() { Name = "AUM", Value = 180.6M, Unit = "%" }
            }
        },

        new()
        {
            Id = "23", Quarter = "Q4-2023", Department = "Research", Region = "Asia",
            Revenue = 124879.4M, Expenses = 96091.13M, Profit = 28788.26999999999M, Metrics = new List<string>{"IRR", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 13247.19M, Expenses = 6852.81M },
                new() { Month = "February", Revenue = 7952.12M, Expenses = 9593.64M },
                new() { Month = "March", Revenue = 10776.6M, Expenses = 7958.21M },
                new() { Month = "April", Revenue = 11056.54M, Expenses = 6731.23M },
                new() { Month = "May", Revenue = 11235.9M, Expenses = 6121.81M },
                new() { Month = "June", Revenue = 10585.01M, Expenses = 6766.41M },
                new() { Month = "July", Revenue = 8885.94M, Expenses = 6575.32M },
                new() { Month = "August", Revenue = 13245.38M, Expenses = 9636.12M },
                new() { Month = "September", Revenue = 11073.53M, Expenses = 6409.68M },
                new() { Month = "October", Revenue = 11056.29M, Expenses = 7323.15M },
                new() { Month = "November", Revenue = 13277.03M, Expenses = 7441.7M },
                new() { Month = "December", Revenue = 13226.58M, Expenses = 7979.34M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 28.07M, Unit = "%" },
                new() { Name = "IRR", Value = 100.93M, Unit = "%" }
            }
        },

        new()
        {
            Id = "24", Quarter = "Q4-2023", Department = "Wealth Management", Region = "Africa",
            Revenue = 247832.17M, Expenses = 212249.69M, Profit = 35582.48000000001M, Metrics = new List<string>{"EBITDA", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 23341.77M, Expenses = 16121.31M },
                new() { Month = "February", Revenue = 23261.92M, Expenses = 17714.83M },
                new() { Month = "March", Revenue = 17897.51M, Expenses = 18688.0M },
                new() { Month = "April", Revenue = 22140.23M, Expenses = 17005.67M },
                new() { Month = "May", Revenue = 23618.34M, Expenses = 15885.45M },
                new() { Month = "June", Revenue = 23453.59M, Expenses = 16131.5M },
                new() { Month = "July", Revenue = 23621.57M, Expenses = 19343.32M },
                new() { Month = "August", Revenue = 21537.24M, Expenses = 18734.97M },
                new() { Month = "September", Revenue = 18931.5M, Expenses = 17673.99M },
                new() { Month = "October", Revenue = 22975.25M, Expenses = 15787.43M },
                new() { Month = "November", Revenue = 19861.74M, Expenses = 15989.8M },
                new() { Month = "December", Revenue = 21059.6M, Expenses = 16357.16M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 41.15M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 104.74M, Unit = "%" }
            }
        },

        new()
        {
            Id = "25", Quarter = "Q4-2023", Department = "Private Equity", Region = "Middle East",
            Revenue = 285726.99M, Expenses = 227696.78M, Profit = 58030.20999999999M, Metrics = new List<string>{"IRR", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 24917.06M, Expenses = 17619.98M },
                new() { Month = "February", Revenue = 22806.8M, Expenses = 19902.77M },
                new() { Month = "March", Revenue = 25541.76M, Expenses = 20580.76M },
                new() { Month = "April", Revenue = 25213.95M, Expenses = 17910.87M },
                new() { Month = "May", Revenue = 24332.7M, Expenses = 17182.43M },
                new() { Month = "June", Revenue = 22570.58M, Expenses = 18932.03M },
                new() { Month = "July", Revenue = 26735.07M, Expenses = 17711.02M },
                new() { Month = "August", Revenue = 26267.58M, Expenses = 18127.27M },
                new() { Month = "September", Revenue = 22307.67M, Expenses = 20840.58M },
                new() { Month = "October", Revenue = 23767.24M, Expenses = 16992.51M },
                new() { Month = "November", Revenue = 26363.66M, Expenses = 17049.32M },
                new() { Month = "December", Revenue = 24737.86M, Expenses = 18711.85M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 26.1M, Unit = "%" },
                new() { Name = "Net Margin", Value = 181.22M, Unit = "%" }
            }
        },

        new()
        {
            Id = "26", Quarter = "Q1-2023", Department = "Wealth Management", Region = "Asia",
            Revenue = 156211.02M, Expenses = 115020.18M, Profit = 41190.84M, Metrics = new List<string>{"Return on Equity", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 14062.81M, Expenses = 11538.67M },
                new() { Month = "February", Revenue = 15003.22M, Expenses = 8813.72M },
                new() { Month = "March", Revenue = 15080.88M, Expenses = 9335.3M },
                new() { Month = "April", Revenue = 14768.69M, Expenses = 11190.48M },
                new() { Month = "May", Revenue = 15494.32M, Expenses = 8539.52M },
                new() { Month = "June", Revenue = 14333.78M, Expenses = 9178.88M },
                new() { Month = "July", Revenue = 13391.57M, Expenses = 9215.63M },
                new() { Month = "August", Revenue = 10769.46M, Expenses = 9993.95M },
                new() { Month = "September", Revenue = 13361.97M, Expenses = 11567.5M },
                new() { Month = "October", Revenue = 11788.76M, Expenses = 9607.08M },
                new() { Month = "November", Revenue = 12480.33M, Expenses = 10165.72M },
                new() { Month = "December", Revenue = 15340.34M, Expenses = 10624.52M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 107.99M, Unit = "%" },
                new() { Name = "Net Margin", Value = 166.15M, Unit = "%" }
            }
        },

        new()
        {
            Id = "27", Quarter = "Q2-2023", Department = "Sales", Region = "South America",
            Revenue = 220353.68M, Expenses = 134770.57M, Profit = 85583.10999999999M, Metrics = new List<string>{"IRR", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 17941.73M, Expenses = 12042.88M },
                new() { Month = "February", Revenue = 17383.95M, Expenses = 10446.19M },
                new() { Month = "March", Revenue = 15978.65M, Expenses = 9239.79M },
                new() { Month = "April", Revenue = 20575.58M, Expenses = 10464.97M },
                new() { Month = "May", Revenue = 16578.37M, Expenses = 10739.3M },
                new() { Month = "June", Revenue = 17296.94M, Expenses = 12542.4M },
                new() { Month = "July", Revenue = 17273.53M, Expenses = 12622.95M },
                new() { Month = "August", Revenue = 15544.21M, Expenses = 12251.23M },
                new() { Month = "September", Revenue = 18903.48M, Expenses = 12576.25M },
                new() { Month = "October", Revenue = 19881.85M, Expenses = 11801.89M },
                new() { Month = "November", Revenue = 20304.11M, Expenses = 9956.03M },
                new() { Month = "December", Revenue = 17627.04M, Expenses = 12394.69M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 58.81M, Unit = "%" },
                new() { Name = "EBITDA", Value = 96.68M, Unit = "%" }
            }
        },

        new()
        {
            Id = "28", Quarter = "Q3-2023", Department = "Wealth Management", Region = "Middle East",
            Revenue = 114049.25M, Expenses = 89377.72M, Profit = 24671.53M, Metrics = new List<string>{"IRR", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12080.9M, Expenses = 8702.73M },
                new() { Month = "February", Revenue = 8824.19M, Expenses = 6169.68M },
                new() { Month = "March", Revenue = 10356.11M, Expenses = 7854.97M },
                new() { Month = "April", Revenue = 10178.82M, Expenses = 6829.5M },
                new() { Month = "May", Revenue = 7120.86M, Expenses = 8242.9M },
                new() { Month = "June", Revenue = 9333.95M, Expenses = 8620.69M },
                new() { Month = "July", Revenue = 7348.16M, Expenses = 6895.49M },
                new() { Month = "August", Revenue = 6703.34M, Expenses = 9038.09M },
                new() { Month = "September", Revenue = 7635.64M, Expenses = 8329.91M },
                new() { Month = "October", Revenue = 10656.2M, Expenses = 8990.59M },
                new() { Month = "November", Revenue = 11437.66M, Expenses = 8195.37M },
                new() { Month = "December", Revenue = 8354.16M, Expenses = 8503.31M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 45.93M, Unit = "%" },
                new() { Name = "Net Margin", Value = 142.0M, Unit = "%" }
            }
        },

        new()
        {
            Id = "29", Quarter = "Q3-2023", Department = "Risk", Region = "Africa",
            Revenue = 169668.44M, Expenses = 115292.61M, Profit = 54375.83M, Metrics = new List<string>{"EBITDA", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 17048.22M, Expenses = 11025.14M },
                new() { Month = "February", Revenue = 16122.45M, Expenses = 10961.39M },
                new() { Month = "March", Revenue = 14608.29M, Expenses = 9042.21M },
                new() { Month = "April", Revenue = 13446.01M, Expenses = 8897.68M },
                new() { Month = "May", Revenue = 11320.68M, Expenses = 10799.88M },
                new() { Month = "June", Revenue = 16948.65M, Expenses = 10104.83M },
                new() { Month = "July", Revenue = 16167.79M, Expenses = 8236.12M },
                new() { Month = "August", Revenue = 12044.44M, Expenses = 11289.23M },
                new() { Month = "September", Revenue = 15609.99M, Expenses = 10795.96M },
                new() { Month = "October", Revenue = 12783.39M, Expenses = 9245.65M },
                new() { Month = "November", Revenue = 13524.42M, Expenses = 8456.92M },
                new() { Month = "December", Revenue = 12969.91M, Expenses = 10112.0M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 135.13M, Unit = "%" },
                new() { Name = "AUM", Value = 43.94M, Unit = "%" }
            }
        },

        new()
        {
            Id = "30", Quarter = "Q2-2023", Department = "Sales", Region = "Middle East",
            Revenue = 294337.89M, Expenses = 179432.77M, Profit = 114905.12000000002M, Metrics = new List<string>{"Net Margin", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 24853.27M, Expenses = 14161.71M },
                new() { Month = "February", Revenue = 23520.79M, Expenses = 16250.49M },
                new() { Month = "March", Revenue = 24214.88M, Expenses = 13617.28M },
                new() { Month = "April", Revenue = 26438.69M, Expenses = 14159.23M },
                new() { Month = "May", Revenue = 24499.53M, Expenses = 13827.25M },
                new() { Month = "June", Revenue = 24820.2M, Expenses = 13032.54M },
                new() { Month = "July", Revenue = 27061.89M, Expenses = 15745.05M },
                new() { Month = "August", Revenue = 23352.03M, Expenses = 13911.64M },
                new() { Month = "September", Revenue = 26780.63M, Expenses = 16702.11M },
                new() { Month = "October", Revenue = 22336.05M, Expenses = 13315.22M },
                new() { Month = "November", Revenue = 22735.22M, Expenses = 14170.06M },
                new() { Month = "December", Revenue = 24480.5M, Expenses = 14201.3M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 184.12M, Unit = "%" },
                new() { Name = "Net Margin", Value = 82.88M, Unit = "%" }
            }
        },

        new()
        {
            Id = "31", Quarter = "Q2-2023", Department = "Sales", Region = "North America",
            Revenue = 172962.6M, Expenses = 134598.31M, Profit = 38364.29000000001M, Metrics = new List<string>{"EBITDA", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12082.05M, Expenses = 11398.89M },
                new() { Month = "February", Revenue = 16414.37M, Expenses = 12112.8M },
                new() { Month = "March", Revenue = 15326.04M, Expenses = 11025.98M },
                new() { Month = "April", Revenue = 13650.64M, Expenses = 11497.01M },
                new() { Month = "May", Revenue = 12978.32M, Expenses = 10037.2M },
                new() { Month = "June", Revenue = 15537.89M, Expenses = 11328.59M },
                new() { Month = "July", Revenue = 13804.98M, Expenses = 9378.64M },
                new() { Month = "August", Revenue = 16401.21M, Expenses = 9715.42M },
                new() { Month = "September", Revenue = 12402.51M, Expenses = 11340.98M },
                new() { Month = "October", Revenue = 16561.13M, Expenses = 11946.46M },
                new() { Month = "November", Revenue = 12931.26M, Expenses = 11772.47M },
                new() { Month = "December", Revenue = 13776.13M, Expenses = 12369.24M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net New Clients", Value = 25.92M, Unit = "%" },
                new() { Name = "AUM", Value = 151.64M, Unit = "%" }
            }
        },

        new()
        {
            Id = "32", Quarter = "Q3-2023", Department = "Sales", Region = "Middle East",
            Revenue = 173042.92M, Expenses = 134261.72M, Profit = 38781.20000000001M, Metrics = new List<string>{"IRR", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12100.1M, Expenses = 12458.6M },
                new() { Month = "February", Revenue = 11871.53M, Expenses = 9358.33M },
                new() { Month = "March", Revenue = 15056.58M, Expenses = 9294.38M },
                new() { Month = "April", Revenue = 16947.57M, Expenses = 11065.02M },
                new() { Month = "May", Revenue = 15749.8M, Expenses = 10000.11M },
                new() { Month = "June", Revenue = 12576.75M, Expenses = 9241.51M },
                new() { Month = "July", Revenue = 16664.39M, Expenses = 11401.55M },
                new() { Month = "August", Revenue = 15528.67M, Expenses = 13154.59M },
                new() { Month = "September", Revenue = 14596.08M, Expenses = 12503.59M },
                new() { Month = "October", Revenue = 17109.92M, Expenses = 10097.46M },
                new() { Month = "November", Revenue = 15634.42M, Expenses = 10779.05M },
                new() { Month = "December", Revenue = 13380.7M, Expenses = 10534.25M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 160.7M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 134.22M, Unit = "%" }
            }
        },

        new()
        {
            Id = "33", Quarter = "Q3-2023", Department = "Compliance", Region = "North America",
            Revenue = 226300.18M, Expenses = 162572.22M, Profit = 63727.95999999999M, Metrics = new List<string>{"EBITDA", "Net New Clients" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 16611.8M, Expenses = 14547.03M },
                new() { Month = "February", Revenue = 21506.71M, Expenses = 14072.48M },
                new() { Month = "March", Revenue = 19461.19M, Expenses = 12736.22M },
                new() { Month = "April", Revenue = 20767.49M, Expenses = 14216.93M },
                new() { Month = "May", Revenue = 16774.36M, Expenses = 12665.73M },
                new() { Month = "June", Revenue = 18334.33M, Expenses = 13029.16M },
                new() { Month = "July", Revenue = 19327.06M, Expenses = 14350.62M },
                new() { Month = "August", Revenue = 19342.72M, Expenses = 15456.02M },
                new() { Month = "September", Revenue = 19482.5M, Expenses = 12668.57M },
                new() { Month = "October", Revenue = 17149.09M, Expenses = 12170.04M },
                new() { Month = "November", Revenue = 21366.2M, Expenses = 11802.11M },
                new() { Month = "December", Revenue = 18855.05M, Expenses = 14485.66M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net New Clients", Value = 157.0M, Unit = "%" },
                new() { Name = "Net Margin", Value = 139.32M, Unit = "%" }
            }
        },

        new()
        {
            Id = "34", Quarter = "Q3-2023", Department = "Research", Region = "Africa",
            Revenue = 196704.0M, Expenses = 118739.07M, Profit = 77964.93M, Metrics = new List<string>{"AUM", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 16295.57M, Expenses = 9357.97M },
                new() { Month = "February", Revenue = 17284.94M, Expenses = 11787.85M },
                new() { Month = "March", Revenue = 14350.94M, Expenses = 8705.09M },
                new() { Month = "April", Revenue = 14838.03M, Expenses = 11465.06M },
                new() { Month = "May", Revenue = 17727.83M, Expenses = 11000.49M },
                new() { Month = "June", Revenue = 18551.87M, Expenses = 8184.27M },
                new() { Month = "July", Revenue = 16789.8M, Expenses = 9199.33M },
                new() { Month = "August", Revenue = 14068.36M, Expenses = 10871.96M },
                new() { Month = "September", Revenue = 15621.37M, Expenses = 8646.69M },
                new() { Month = "October", Revenue = 18225.45M, Expenses = 10636.08M },
                new() { Month = "November", Revenue = 15936.47M, Expenses = 9979.57M },
                new() { Month = "December", Revenue = 17142.29M, Expenses = 11145.75M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 75.16M, Unit = "%" },
                new() { Name = "IRR", Value = 20.69M, Unit = "%" }
            }
        },

        new()
        {
            Id = "35", Quarter = "Q1-2023", Department = "Compliance", Region = "Asia",
            Revenue = 94359.96M, Expenses = 73652.56M, Profit = 20707.40000000001M, Metrics = new List<string>{"EBITDA", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 6335.03M, Expenses = 7231.9M },
                new() { Month = "February", Revenue = 5600.49M, Expenses = 5475.4M },
                new() { Month = "March", Revenue = 7852.46M, Expenses = 8056.85M },
                new() { Month = "April", Revenue = 7844.03M, Expenses = 5107.0M },
                new() { Month = "May", Revenue = 5046.06M, Expenses = 4410.67M },
                new() { Month = "June", Revenue = 9859.99M, Expenses = 7705.45M },
                new() { Month = "July", Revenue = 7486.39M, Expenses = 5049.07M },
                new() { Month = "August", Revenue = 7584.39M, Expenses = 5939.95M },
                new() { Month = "September", Revenue = 5649.89M, Expenses = 5578.61M },
                new() { Month = "October", Revenue = 5433.63M, Expenses = 5892.06M },
                new() { Month = "November", Revenue = 8190.02M, Expenses = 7988.93M },
                new() { Month = "December", Revenue = 6230.41M, Expenses = 4736.33M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 180.41M, Unit = "%" },
                new() { Name = "Net Margin", Value = 198.55M, Unit = "%" }
            }
        },

        new()
        {
            Id = "36", Quarter = "Q4-2023", Department = "Wealth Management", Region = "Africa",
            Revenue = 115320.99M, Expenses = 94098.67M, Profit = 21222.320000000007M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 9629.74M, Expenses = 8045.35M },
                new() { Month = "February", Revenue = 11466.64M, Expenses = 9284.55M },
                new() { Month = "March", Revenue = 8709.05M, Expenses = 9108.69M },
                new() { Month = "April", Revenue = 9239.95M, Expenses = 8898.92M },
                new() { Month = "May", Revenue = 8531.6M, Expenses = 9533.09M },
                new() { Month = "June", Revenue = 9114.29M, Expenses = 6148.7M },
                new() { Month = "July", Revenue = 12539.48M, Expenses = 7545.59M },
                new() { Month = "August", Revenue = 8834.59M, Expenses = 8849.99M },
                new() { Month = "September", Revenue = 7225.57M, Expenses = 7878.0M },
                new() { Month = "October", Revenue = 7325.76M, Expenses = 9434.23M },
                new() { Month = "November", Revenue = 9341.45M, Expenses = 9478.67M },
                new() { Month = "December", Revenue = 8517.32M, Expenses = 5957.6M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 36.78M, Unit = "%" },
                new() { Name = "AUM", Value = 125.62M, Unit = "%" }
            }
        },

        new()
        {
            Id = "37", Quarter = "Q4-2023", Department = "Sales", Region = "Africa",
            Revenue = 80750.4M, Expenses = 51024.1M, Profit = 29726.299999999996M, Metrics = new List<string>{"Net Margin", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 6937.46M, Expenses = 3095.01M },
                new() { Month = "February", Revenue = 5081.33M, Expenses = 3954.32M },
                new() { Month = "March", Revenue = 4156.68M, Expenses = 2268.23M },
                new() { Month = "April", Revenue = 6664.76M, Expenses = 3098.88M },
                new() { Month = "May", Revenue = 5113.95M, Expenses = 3812.77M },
                new() { Month = "June", Revenue = 6456.52M, Expenses = 3087.46M },
                new() { Month = "July", Revenue = 5661.7M, Expenses = 3861.64M },
                new() { Month = "August", Revenue = 5387.01M, Expenses = 4521.21M },
                new() { Month = "September", Revenue = 7570.65M, Expenses = 4491.94M },
                new() { Month = "October", Revenue = 4068.16M, Expenses = 4076.97M },
                new() { Month = "November", Revenue = 6848.27M, Expenses = 3209.2M },
                new() { Month = "December", Revenue = 6748.0M, Expenses = 4254.88M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "IRR", Value = 116.35M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 129.27M, Unit = "%" }
            }
        },

        new()
        {
            Id = "38", Quarter = "Q4-2023", Department = "Risk", Region = "Europe",
            Revenue = 148266.73M, Expenses = 125968.81M, Profit = 22297.920000000013M, Metrics = new List<string>{"Net Margin", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 13142.75M, Expenses = 10865.73M },
                new() { Month = "February", Revenue = 14903.24M, Expenses = 10521.15M },
                new() { Month = "March", Revenue = 14473.47M, Expenses = 9957.71M },
                new() { Month = "April", Revenue = 12940.93M, Expenses = 11560.66M },
                new() { Month = "May", Revenue = 10502.56M, Expenses = 11669.25M },
                new() { Month = "June", Revenue = 11925.93M, Expenses = 12389.68M },
                new() { Month = "July", Revenue = 14577.0M, Expenses = 12257.59M },
                new() { Month = "August", Revenue = 14540.76M, Expenses = 11638.27M },
                new() { Month = "September", Revenue = 10001.02M, Expenses = 9449.07M },
                new() { Month = "October", Revenue = 10204.36M, Expenses = 10657.74M },
                new() { Month = "November", Revenue = 14956.25M, Expenses = 11608.81M },
                new() { Month = "December", Revenue = 11278.01M, Expenses = 9571.25M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 82.14M, Unit = "%" },
                new() { Name = "IRR", Value = 70.77M, Unit = "%" }
            }
        },

        new()
        {
            Id = "39", Quarter = "Q1-2023", Department = "Risk", Region = "Africa",
            Revenue = 131619.56M, Expenses = 93181.64M, Profit = 38437.92M, Metrics = new List<string>{"Net New Clients", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 9695.0M, Expenses = 6951.12M },
                new() { Month = "February", Revenue = 8609.05M, Expenses = 5792.93M },
                new() { Month = "March", Revenue = 13726.76M, Expenses = 8837.36M },
                new() { Month = "April", Revenue = 13072.25M, Expenses = 8109.68M },
                new() { Month = "May", Revenue = 8918.77M, Expenses = 8742.95M },
                new() { Month = "June", Revenue = 13717.46M, Expenses = 9510.16M },
                new() { Month = "July", Revenue = 13710.13M, Expenses = 5769.64M },
                new() { Month = "August", Revenue = 9879.63M, Expenses = 5816.58M },
                new() { Month = "September", Revenue = 13646.49M, Expenses = 8142.51M },
                new() { Month = "October", Revenue = 8604.43M, Expenses = 7387.28M },
                new() { Month = "November", Revenue = 8739.23M, Expenses = 9566.59M },
                new() { Month = "December", Revenue = 9160.57M, Expenses = 6763.17M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 162.03M, Unit = "%" },
                new() { Name = "EBITDA", Value = 180.67M, Unit = "%" }
            }
        },

        new()
        {
            Id = "40", Quarter = "Q4-2023", Department = "Compliance", Region = "Africa",
            Revenue = 246475.72M, Expenses = 185594.78M, Profit = 60880.94M, Metrics = new List<string>{"Net Margin", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 18117.08M, Expenses = 17172.22M },
                new() { Month = "February", Revenue = 17658.29M, Expenses = 13526.9M },
                new() { Month = "March", Revenue = 20983.32M, Expenses = 15232.16M },
                new() { Month = "April", Revenue = 20209.31M, Expenses = 14996.48M },
                new() { Month = "May", Revenue = 19768.19M, Expenses = 16429.43M },
                new() { Month = "June", Revenue = 19960.05M, Expenses = 15093.14M },
                new() { Month = "July", Revenue = 19351.53M, Expenses = 17113.68M },
                new() { Month = "August", Revenue = 20254.84M, Expenses = 14992.0M },
                new() { Month = "September", Revenue = 22499.12M, Expenses = 16055.25M },
                new() { Month = "October", Revenue = 20346.62M, Expenses = 16726.67M },
                new() { Month = "November", Revenue = 18279.26M, Expenses = 16048.13M },
                new() { Month = "December", Revenue = 20401.19M, Expenses = 15446.84M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 112.05M, Unit = "%" },
                new() { Name = "IRR", Value = 82.86M, Unit = "%" }
            }
        },

        new()
        {
            Id = "41", Quarter = "Q1-2023", Department = "Wealth Management", Region = "Asia",
            Revenue = 97710.47M, Expenses = 58788.96M, Profit = 38921.51M, Metrics = new List<string>{"Net New Clients", "Net Margin" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 9066.97M, Expenses = 5332.29M },
                new() { Month = "February", Revenue = 7846.46M, Expenses = 4996.26M },
                new() { Month = "March", Revenue = 10975.11M, Expenses = 5538.47M },
                new() { Month = "April", Revenue = 5364.09M, Expenses = 6746.8M },
                new() { Month = "May", Revenue = 5570.47M, Expenses = 3784.65M },
                new() { Month = "June", Revenue = 9500.49M, Expenses = 4860.14M },
                new() { Month = "July", Revenue = 10660.61M, Expenses = 6288.89M },
                new() { Month = "August", Revenue = 5799.19M, Expenses = 6294.58M },
                new() { Month = "September", Revenue = 7671.85M, Expenses = 4777.75M },
                new() { Month = "October", Revenue = 5672.69M, Expenses = 5513.77M },
                new() { Month = "November", Revenue = 7619.35M, Expenses = 4287.71M },
                new() { Month = "December", Revenue = 10417.87M, Expenses = 6839.12M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 48.38M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 144.41M, Unit = "%" }
            }
        },

        new()
        {
            Id = "42", Quarter = "Q4-2023", Department = "Sales", Region = "South America",
            Revenue = 81392.12M, Expenses = 60088.93M, Profit = 21303.189999999995M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 9325.45M, Expenses = 6120.19M },
                new() { Month = "February", Revenue = 6669.42M, Expenses = 6980.93M },
                new() { Month = "March", Revenue = 9182.91M, Expenses = 6764.86M },
                new() { Month = "April", Revenue = 6496.47M, Expenses = 4388.15M },
                new() { Month = "May", Revenue = 4471.11M, Expenses = 3411.9M },
                new() { Month = "June", Revenue = 9757.06M, Expenses = 4632.44M },
                new() { Month = "July", Revenue = 4827.79M, Expenses = 4932.4M },
                new() { Month = "August", Revenue = 9042.28M, Expenses = 4785.61M },
                new() { Month = "September", Revenue = 6258.73M, Expenses = 5420.02M },
                new() { Month = "October", Revenue = 7773.19M, Expenses = 4841.7M },
                new() { Month = "November", Revenue = 4309.3M, Expenses = 4115.91M },
                new() { Month = "December", Revenue = 7109.45M, Expenses = 4994.02M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 61.61M, Unit = "%" },
                new() { Name = "AUM", Value = 51.78M, Unit = "%" }
            }
        },

        new()
        {
            Id = "43", Quarter = "Q1-2023", Department = "Sales", Region = "Asia",
            Revenue = 227818.5M, Expenses = 162001.42M, Profit = 65817.07999999999M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 18358.52M, Expenses = 13995.0M },
                new() { Month = "February", Revenue = 21764.16M, Expenses = 12514.05M },
                new() { Month = "March", Revenue = 18269.23M, Expenses = 15378.06M },
                new() { Month = "April", Revenue = 20000.22M, Expenses = 11656.85M },
                new() { Month = "May", Revenue = 19076.54M, Expenses = 14044.94M },
                new() { Month = "June", Revenue = 17975.01M, Expenses = 15325.33M },
                new() { Month = "July", Revenue = 16684.5M, Expenses = 15208.34M },
                new() { Month = "August", Revenue = 18669.01M, Expenses = 13712.32M },
                new() { Month = "September", Revenue = 19661.99M, Expenses = 12433.7M },
                new() { Month = "October", Revenue = 17091.23M, Expenses = 14272.64M },
                new() { Month = "November", Revenue = 18052.18M, Expenses = 13122.86M },
                new() { Month = "December", Revenue = 18530.14M, Expenses = 14060.68M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Net Margin", Value = 68.37M, Unit = "%" },
                new() { Name = "Net Margin", Value = 162.98M, Unit = "%" }
            }
        },

        new()
        {
            Id = "44", Quarter = "Q3-2023", Department = "Compliance", Region = "Europe",
            Revenue = 96787.94M, Expenses = 69462.4M, Profit = 27325.540000000008M, Metrics = new List<string>{"AUM", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 6624.47M, Expenses = 6242.4M },
                new() { Month = "February", Revenue = 8013.26M, Expenses = 5730.26M },
                new() { Month = "March", Revenue = 6510.34M, Expenses = 6182.35M },
                new() { Month = "April", Revenue = 5428.86M, Expenses = 7785.63M },
                new() { Month = "May", Revenue = 8943.2M, Expenses = 7013.52M },
                new() { Month = "June", Revenue = 6372.47M, Expenses = 4588.21M },
                new() { Month = "July", Revenue = 10173.99M, Expenses = 4858.62M },
                new() { Month = "August", Revenue = 5804.68M, Expenses = 6535.31M },
                new() { Month = "September", Revenue = 9333.79M, Expenses = 3898.21M },
                new() { Month = "October", Revenue = 6363.47M, Expenses = 6565.04M },
                new() { Month = "November", Revenue = 9128.59M, Expenses = 7220.43M },
                new() { Month = "December", Revenue = 9545.22M, Expenses = 6494.42M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 107.02M, Unit = "%" },
                new() { Name = "Net New Clients", Value = 171.42M, Unit = "%" }
            }
        },

        new()
        {
            Id = "45", Quarter = "Q3-2023", Department = "Risk", Region = "Asia",
            Revenue = 153611.04M, Expenses = 119250.23M, Profit = 34360.81000000001M, Metrics = new List<string>{"Return on Equity", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 14135.39M, Expenses = 10640.96M },
                new() { Month = "February", Revenue = 12199.55M, Expenses = 11581.56M },
                new() { Month = "March", Revenue = 11875.2M, Expenses = 11432.7M },
                new() { Month = "April", Revenue = 12440.97M, Expenses = 10646.27M },
                new() { Month = "May", Revenue = 14275.02M, Expenses = 9602.27M },
                new() { Month = "June", Revenue = 10915.61M, Expenses = 9886.62M },
                new() { Month = "July", Revenue = 9896.35M, Expenses = 9555.71M },
                new() { Month = "August", Revenue = 14990.12M, Expenses = 9694.63M },
                new() { Month = "September", Revenue = 14137.29M, Expenses = 10428.56M },
                new() { Month = "October", Revenue = 13405.74M, Expenses = 10892.86M },
                new() { Month = "November", Revenue = 15628.2M, Expenses = 10190.26M },
                new() { Month = "December", Revenue = 12974.37M, Expenses = 9776.78M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 149.4M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 81.45M, Unit = "%" }
            }
        },

        new()
        {
            Id = "46", Quarter = "Q3-2023", Department = "Wealth Management", Region = "Asia",
            Revenue = 123937.99M, Expenses = 100334.98M, Profit = 23603.01000000001M, Metrics = new List<string>{"IRR", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12461.21M, Expenses = 7948.98M },
                new() { Month = "February", Revenue = 7413.91M, Expenses = 8735.25M },
                new() { Month = "March", Revenue = 12169.12M, Expenses = 8864.17M },
                new() { Month = "April", Revenue = 8726.52M, Expenses = 10105.88M },
                new() { Month = "May", Revenue = 10462.37M, Expenses = 8670.81M },
                new() { Month = "June", Revenue = 10900.37M, Expenses = 9902.08M },
                new() { Month = "July", Revenue = 9754.22M, Expenses = 7284.51M },
                new() { Month = "August", Revenue = 7517.89M, Expenses = 9905.4M },
                new() { Month = "September", Revenue = 8942.01M, Expenses = 8698.18M },
                new() { Month = "October", Revenue = 9605.96M, Expenses = 7172.56M },
                new() { Month = "November", Revenue = 11384.66M, Expenses = 7611.57M },
                new() { Month = "December", Revenue = 7974.22M, Expenses = 9686.1M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 159.19M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 173.86M, Unit = "%" }
            }
        },

        new()
        {
            Id = "47", Quarter = "Q4-2023", Department = "Risk", Region = "Africa",
            Revenue = 163410.71M, Expenses = 136564.47M, Profit = 26846.23999999999M, Metrics = new List<string>{"Net New Clients", "AUM" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 12961.38M, Expenses = 9916.25M },
                new() { Month = "February", Revenue = 12605.47M, Expenses = 12788.99M },
                new() { Month = "March", Revenue = 12215.57M, Expenses = 9702.19M },
                new() { Month = "April", Revenue = 15367.24M, Expenses = 9834.04M },
                new() { Month = "May", Revenue = 14509.4M, Expenses = 10395.39M },
                new() { Month = "June", Revenue = 15569.64M, Expenses = 9424.5M },
                new() { Month = "July", Revenue = 11954.52M, Expenses = 12796.9M },
                new() { Month = "August", Revenue = 11791.32M, Expenses = 13070.55M },
                new() { Month = "September", Revenue = 10800.42M, Expenses = 9386.87M },
                new() { Month = "October", Revenue = 13834.01M, Expenses = 12243.99M },
                new() { Month = "November", Revenue = 11908.51M, Expenses = 12555.93M },
                new() { Month = "December", Revenue = 14053.54M, Expenses = 13283.87M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 140.82M, Unit = "%" },
                new() { Name = "Net Margin", Value = 91.74M, Unit = "%" }
            }
        },

        new()
        {
            Id = "48", Quarter = "Q2-2023", Department = "Wealth Management", Region = "Middle East",
            Revenue = 247220.17M, Expenses = 197738.01M, Profit = 49482.16M, Metrics = new List<string>{"Net Margin", "IRR" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 22908.61M, Expenses = 14596.32M },
                new() { Month = "February", Revenue = 20132.13M, Expenses = 16109.6M },
                new() { Month = "March", Revenue = 18390.51M, Expenses = 15080.97M },
                new() { Month = "April", Revenue = 18331.37M, Expenses = 16757.33M },
                new() { Month = "May", Revenue = 21003.3M, Expenses = 16990.67M },
                new() { Month = "June", Revenue = 19514.32M, Expenses = 18119.9M },
                new() { Month = "July", Revenue = 18803.69M, Expenses = 18330.8M },
                new() { Month = "August", Revenue = 23144.63M, Expenses = 15723.09M },
                new() { Month = "September", Revenue = 18426.68M, Expenses = 16403.87M },
                new() { Month = "October", Revenue = 19630.38M, Expenses = 15763.4M },
                new() { Month = "November", Revenue = 19781.47M, Expenses = 17828.35M },
                new() { Month = "December", Revenue = 20635.25M, Expenses = 16149.94M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 173.36M, Unit = "%" },
                new() { Name = "Net Margin", Value = 67.76M, Unit = "%" }
            }
        },

        new()
        {
            Id = "49", Quarter = "Q1-2023", Department = "Wealth Management", Region = "North America",
            Revenue = 103037.59M, Expenses = 80690.39M, Profit = 22347.199999999997M, Metrics = new List<string>{"Net Margin", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 8498.25M, Expenses = 5917.96M },
                new() { Month = "February", Revenue = 11197.05M, Expenses = 7902.24M },
                new() { Month = "March", Revenue = 9858.64M, Expenses = 8216.49M },
                new() { Month = "April", Revenue = 8273.68M, Expenses = 7033.25M },
                new() { Month = "May", Revenue = 7893.57M, Expenses = 7669.73M },
                new() { Month = "June", Revenue = 6423.86M, Expenses = 6138.74M },
                new() { Month = "July", Revenue = 6085.64M, Expenses = 6467.55M },
                new() { Month = "August", Revenue = 8185.95M, Expenses = 6240.3M },
                new() { Month = "September", Revenue = 7429.63M, Expenses = 6298.84M },
                new() { Month = "October", Revenue = 6932.7M, Expenses = 7388.27M },
                new() { Month = "November", Revenue = 8550.39M, Expenses = 5438.9M },
                new() { Month = "December", Revenue = 5884.64M, Expenses = 4757.0M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "AUM", Value = 181.93M, Unit = "%" },
                new() { Name = "IRR", Value = 175.17M, Unit = "%" }
            }
        },

        new()
        {
            Id = "50", Quarter = "Q4-2023", Department = "Sales", Region = "Africa",
            Revenue = 231053.86M, Expenses = 182205.95M, Profit = 48847.909999999974M, Metrics = new List<string>{"IRR", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 20739.34M, Expenses = 15618.31M },
                new() { Month = "February", Revenue = 19599.57M, Expenses = 14144.58M },
                new() { Month = "March", Revenue = 21802.7M, Expenses = 15190.17M },
                new() { Month = "April", Revenue = 19510.66M, Expenses = 14912.34M },
                new() { Month = "May", Revenue = 20095.38M, Expenses = 13577.28M },
                new() { Month = "June", Revenue = 18760.22M, Expenses = 13806.75M },
                new() { Month = "July", Revenue = 22083.24M, Expenses = 15451.83M },
                new() { Month = "August", Revenue = 19368.11M, Expenses = 15439.32M },
                new() { Month = "September", Revenue = 22041.34M, Expenses = 16455.38M },
                new() { Month = "October", Revenue = 21362.07M, Expenses = 13394.68M },
                new() { Month = "November", Revenue = 17652.97M, Expenses = 15517.03M },
                new() { Month = "December", Revenue = 18904.88M, Expenses = 15893.01M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 62.24M, Unit = "%" },
                new() { Name = "Return on Equity", Value = 159.36M, Unit = "%" }
            }
        },

        new()
        {
            Id = "51", Quarter = "Q3-2023", Department = "Compliance", Region = "Asia",
            Revenue = 286101.74M, Expenses = 184505.37M, Profit = 101596.37M, Metrics = new List<string>{"Net New Clients", "EBITDA" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 24522.68M, Expenses = 15502.5M },
                new() { Month = "February", Revenue = 21058.09M, Expenses = 17082.36M },
                new() { Month = "March", Revenue = 21160.32M, Expenses = 16667.18M },
                new() { Month = "April", Revenue = 24492.51M, Expenses = 16140.2M },
                new() { Month = "May", Revenue = 22137.49M, Expenses = 17371.38M },
                new() { Month = "June", Revenue = 24512.71M, Expenses = 14657.87M },
                new() { Month = "July", Revenue = 25317.86M, Expenses = 17151.23M },
                new() { Month = "August", Revenue = 26712.68M, Expenses = 16328.81M },
                new() { Month = "September", Revenue = 22250.04M, Expenses = 15870.68M },
                new() { Month = "October", Revenue = 24550.66M, Expenses = 14281.81M },
                new() { Month = "November", Revenue = 25094.49M, Expenses = 15773.3M },
                new() { Month = "December", Revenue = 24331.91M, Expenses = 16077.1M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "Return on Equity", Value = 84.05M, Unit = "%" },
                new() { Name = "EBITDA", Value = 163.19M, Unit = "%" }
            }
        },

        new()
        {
            Id = "52", Quarter = "Q2-2023", Department = "Sales", Region = "Asia",
            Revenue = 247106.98M, Expenses = 180775.09M, Profit = 66331.89000000001M, Metrics = new List<string>{"AUM", "Return on Equity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { Month = "January", Revenue = 20164.68M, Expenses = 13920.44M },
                new() { Month = "February", Revenue = 19574.38M, Expenses = 14541.69M },
                new() { Month = "March", Revenue = 21201.83M, Expenses = 14273.18M },
                new() { Month = "April", Revenue = 22537.9M, Expenses = 13809.47M },
                new() { Month = "May", Revenue = 21520.61M, Expenses = 15714.67M },
                new() { Month = "June", Revenue = 22976.5M, Expenses = 14251.48M },
                new() { Month = "July", Revenue = 19848.9M, Expenses = 14250.88M },
                new() { Month = "August", Revenue = 21501.98M, Expenses = 15561.66M },
                new() { Month = "September", Revenue = 22745.49M, Expenses = 13900.11M },
                new() { Month = "October", Revenue = 18680.0M, Expenses = 16180.66M },
                new() { Month = "November", Revenue = 19489.19M, Expenses = 15043.22M },
                new() { Month = "December", Revenue = 23581.4M, Expenses = 16992.22M }
            },
            KPIs = new List<Kpi>
            {
                new() { Name = "EBITDA", Value = 174.99M, Unit = "%" },
                new() { Name = "IRR", Value = 152.45M, Unit = "%" }
            }
        }
    };

    [EnableQuery]
    public IActionResult Get() => Ok(Reports);

    [EnableQuery]
    public IActionResult Get([FromODataUri] string key)
    {
        var report = Reports.FirstOrDefault(r => r.Id == key);
        if (report == null)
        {
            return NotFound();
        }
        return Ok(report);
    }
}
