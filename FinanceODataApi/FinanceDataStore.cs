public static class FinanceDataStore
{
    public static readonly List<QuarterlyReport> Reports = new()
    {
        new() {
            Id = 1, Quarter = "Q1-2023", Department = "Sales", Region = "North America",
            Revenue = 120000, Expenses = 80000, Profit = 40000,
            Metrics = new List<string> { "Revenue Growth", "Customer Acquisition" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 1, Month = "January", Revenue = 38000, Expenses = 25000 },
                new() { QuarterlyReportId = 1, Month = "February", Revenue = 40000, Expenses = 27000 },
                new() { QuarterlyReportId = 1, Month = "March", Revenue = 42000, Expenses = 28000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 1, Name = "EBITDA", Value = 42000, Target = 45000 },
                new() { QuarterlyReportId = 1, Name = "Net Margin", Value = 33.3m, Target = 35.0m }
            }
        },
        new() {
            Id = 2, Quarter = "Q1-2023", Department = "Marketing", Region = "Europe",
            Revenue = 90000, Expenses = 60000, Profit = 30000,
            Metrics = new List<string> { "Brand Awareness", "Lead Generation" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 2, Month = "January", Revenue = 28000, Expenses = 18000 },
                new() { QuarterlyReportId = 2, Month = "February", Revenue = 30000, Expenses = 20000 },
                new() { QuarterlyReportId = 2, Month = "March", Revenue = 32000, Expenses = 22000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 2, Name = "IRR", Value = 15.5m, Target = 18.0m },
                new() { QuarterlyReportId = 2, Name = "Net New Clients", Value = 45, Target = 50 }
            }
        },
        new() {
            Id = 3, Quarter = "Q1-2023", Department = "Finance", Region = "Asia",
            Revenue = 110000, Expenses = 70000, Profit = 40000,
            Metrics = new List<string> { "Cost Control", "Revenue Optimization" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 3, Month = "January", Revenue = 35000, Expenses = 22000 },
                new() { QuarterlyReportId = 3, Month = "February", Revenue = 37000, Expenses = 24000 },
                new() { QuarterlyReportId = 3, Month = "March", Revenue = 38000, Expenses = 24000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 3, Name = "Return on Equity", Value = 22.5m, Target = 25.0m },
                new() { QuarterlyReportId = 3, Name = "AUM", Value = 2500000, Target = 3000000 }  // Fixed: was duplicate "Return on Equity"
            }
        },
        new() {
            Id = 4, Quarter = "Q1-2023", Department = "Support", Region = "North America",
            Revenue = 50000, Expenses = 30000, Profit = 20000,
            Metrics = new List<string> { "Customer Satisfaction", "Response Time" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 4, Month = "January", Revenue = 16000, Expenses = 9000 },
                new() { QuarterlyReportId = 4, Month = "February", Revenue = 17000, Expenses = 10000 },
                new() { QuarterlyReportId = 4, Month = "March", Revenue = 17000, Expenses = 11000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 4, Name = "EBITDA", Value = 21000, Target = 23000 },
                new() { QuarterlyReportId = 4, Name = "Net Margin", Value = 40.0m, Target = 42.0m }
            }
        },
        new() {
            Id = 5, Quarter = "Q2-2023", Department = "Sales", Region = "North America",
            Revenue = 140000, Expenses = 85000, Profit = 55000,
            Metrics = new List<string> { "Market Share", "Sales Velocity" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 5, Month = "April", Revenue = 45000, Expenses = 27000 },
                new() { QuarterlyReportId = 5, Month = "May", Revenue = 47000, Expenses = 28000 },
                new() { QuarterlyReportId = 5, Month = "June", Revenue = 48000, Expenses = 30000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 5, Name = "IRR", Value = 18.2m, Target = 20.0m },
                new() { QuarterlyReportId = 5, Name = "Return on Equity", Value = 24.0m, Target = 26.0m }
            }
        },
        new() {
            Id = 6, Quarter = "Q2-2023", Department = "Marketing", Region = "Europe",
            Revenue = 95000, Expenses = 65000, Profit = 30000,
            Metrics = new List<string> { "Campaign Performance", "ROI" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 6, Month = "April", Revenue = 30000, Expenses = 20000 },
                new() { QuarterlyReportId = 6, Month = "May", Revenue = 32000, Expenses = 22000 },
                new() { QuarterlyReportId = 6, Month = "June", Revenue = 33000, Expenses = 23000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 6, Name = "AUM", Value = 2800000, Target = 3200000 },
                new() { QuarterlyReportId = 6, Name = "Net New Clients", Value = 52, Target = 60 }
            }
        },
        new() {
            Id = 7, Quarter = "Q2-2023", Department = "Finance", Region = "Asia",
            Revenue = 115000, Expenses = 72000, Profit = 43000,
            Metrics = new List<string> { "Budget Adherence", "Financial Planning" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 7, Month = "April", Revenue = 37000, Expenses = 23000 },
                new() { QuarterlyReportId = 7, Month = "May", Revenue = 39000, Expenses = 24000 },
                new() { QuarterlyReportId = 7, Month = "June", Revenue = 39000, Expenses = 25000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 7, Name = "EBITDA", Value = 45000, Target = 48000 },
                new() { QuarterlyReportId = 7, Name = "Net Margin", Value = 37.4m, Target = 40.0m }
            }
        },
        new() {
            Id = 8, Quarter = "Q2-2023", Department = "Support", Region = "Europe",
            Revenue = 55000, Expenses = 33000, Profit = 22000,
            Metrics = new List<string> { "Ticket Resolution", "Customer Retention" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 8, Month = "April", Revenue = 17000, Expenses = 10000 },
                new() { QuarterlyReportId = 8, Month = "May", Revenue = 19000, Expenses = 11000 },
                new() { QuarterlyReportId = 8, Month = "June", Revenue = 19000, Expenses = 12000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 8, Name = "IRR", Value = 16.8m, Target = 19.0m },
                new() { QuarterlyReportId = 8, Name = "Return on Equity", Value = 23.5m, Target = 25.0m }
            }
        },
        new() {
            Id = 9, Quarter = "Q3-2023", Department = "Sales", Region = "Asia",
            Revenue = 135000, Expenses = 87000, Profit = 48000,
            Metrics = new List<string> { "Territory Expansion", "Deal Size" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 9, Month = "July", Revenue = 43000, Expenses = 28000 },
                new() { QuarterlyReportId = 9, Month = "August", Revenue = 45000, Expenses = 29000 },
                new() { QuarterlyReportId = 9, Month = "September", Revenue = 47000, Expenses = 30000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 9, Name = "AUM", Value = 3100000, Target = 3500000 },
                new() { QuarterlyReportId = 9, Name = "Net New Clients", Value = 58, Target = 65 }
            }
        },
        new() {
            Id = 10, Quarter = "Q3-2023", Department = "Marketing", Region = "North America",
            Revenue = 97000, Expenses = 62000, Profit = 35000,
            Metrics = new List<string> { "Digital Engagement", "Conversion Rate" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 10, Month = "July", Revenue = 31000, Expenses = 20000 },
                new() { QuarterlyReportId = 10, Month = "August", Revenue = 33000, Expenses = 21000 },
                new() { QuarterlyReportId = 10, Month = "September", Revenue = 33000, Expenses = 21000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 10, Name = "EBITDA", Value = 36000, Target = 39000 },
                new() { QuarterlyReportId = 10, Name = "Net Margin", Value = 36.1m, Target = 38.0m }
            }
        },
        new() {
            Id = 11, Quarter = "Q3-2023", Department = "Finance", Region = "Europe",
            Revenue = 118000, Expenses = 74000, Profit = 44000,
            Metrics = new List<string> { "Cash Flow", "Asset Management" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 11, Month = "July", Revenue = 38000, Expenses = 24000 },
                new() { QuarterlyReportId = 11, Month = "August", Revenue = 40000, Expenses = 25000 },
                new() { QuarterlyReportId = 11, Month = "September", Revenue = 40000, Expenses = 25000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 11, Name = "IRR", Value = 17.5m, Target = 20.0m },
                new() { QuarterlyReportId = 11, Name = "Return on Equity", Value = 25.2m, Target = 27.0m }
            }
        },
        new() {
            Id = 12, Quarter = "Q3-2023", Department = "Support", Region = "Asia",
            Revenue = 60000, Expenses = 35000, Profit = 25000,
            Metrics = new List<string> { "SLA Compliance", "First Response Time" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 12, Month = "July", Revenue = 19000, Expenses = 11000 },
                new() { QuarterlyReportId = 12, Month = "August", Revenue = 20000, Expenses = 12000 },
                new() { QuarterlyReportId = 12, Month = "September", Revenue = 21000, Expenses = 12000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 12, Name = "AUM", Value = 2950000, Target = 3300000 },
                new() { QuarterlyReportId = 12, Name = "Net New Clients", Value = 48, Target = 55 }
            }
        },
        new() {
            Id = 13, Quarter = "Q4-2023", Department = "Sales", Region = "North America",
            Revenue = 150000, Expenses = 95000, Profit = 55000,
            Metrics = new List<string> { "Year-end Performance", "Pipeline Conversion" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 13, Month = "October", Revenue = 48000, Expenses = 30000 },
                new() { QuarterlyReportId = 13, Month = "November", Revenue = 50000, Expenses = 32000 },
                new() { QuarterlyReportId = 13, Month = "December", Revenue = 52000, Expenses = 33000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 13, Name = "EBITDA", Value = 58000, Target = 62000 },
                new() { QuarterlyReportId = 13, Name = "Net Margin", Value = 36.7m, Target = 39.0m }
            }
        },
        new() {
            Id = 14, Quarter = "Q4-2023", Department = "Marketing", Region = "Europe",
            Revenue = 105000, Expenses = 70000, Profit = 35000,
            Metrics = new List<string> { "Holiday Campaign", "Year-end ROI" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 14, Month = "October", Revenue = 33000, Expenses = 22000 },
                new() { QuarterlyReportId = 14, Month = "November", Revenue = 35000, Expenses = 23000 },
                new() { QuarterlyReportId = 14, Month = "December", Revenue = 37000, Expenses = 25000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 14, Name = "IRR", Value = 19.2m, Target = 21.0m },
                new() { QuarterlyReportId = 14, Name = "Return on Equity", Value = 26.8m, Target = 28.0m }
            }
        },
        new() {
            Id = 15, Quarter = "Q4-2023", Department = "Finance", Region = "Asia",
            Revenue = 125000, Expenses = 78000, Profit = 47000,
            Metrics = new List<string> { "Annual Close", "Audit Preparation" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 15, Month = "October", Revenue = 40000, Expenses = 25000 },
                new() { QuarterlyReportId = 15, Month = "November", Revenue = 42000, Expenses = 26000 },
                new() { QuarterlyReportId = 15, Month = "December", Revenue = 43000, Expenses = 27000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 15, Name = "AUM", Value = 3400000, Target = 3800000 },
                new() { QuarterlyReportId = 15, Name = "Net New Clients", Value = 62, Target = 70 }
            }
        },
        new() {
            Id = 16, Quarter = "Q4-2023", Department = "Support", Region = "North America",
            Revenue = 58000, Expenses = 34000, Profit = 24000,
            Metrics = new List<string> { "Holiday Support", "Customer Satisfaction" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 16, Month = "October", Revenue = 18000, Expenses = 11000 },
                new() { QuarterlyReportId = 16, Month = "November", Revenue = 19000, Expenses = 11000 },
                new() { QuarterlyReportId = 16, Month = "December", Revenue = 21000, Expenses = 12000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 16, Name = "EBITDA", Value = 25000, Target = 27000 },
                new() { QuarterlyReportId = 16, Name = "Net Margin", Value = 41.4m, Target = 43.0m }
            }
        },
        new() {
            Id = 17, Quarter = "Q1-2024", Department = "Sales", Region = "Europe",
            Revenue = 128000, Expenses = 82000, Profit = 46000,
            Metrics = new List<string> { "Q1 Kickoff", "Territory Growth" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 17, Month = "January", Revenue = 40000, Expenses = 26000 },
                new() { QuarterlyReportId = 17, Month = "February", Revenue = 43000, Expenses = 27000 },
                new() { QuarterlyReportId = 17, Month = "March", Revenue = 45000, Expenses = 29000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 17, Name = "IRR", Value = 20.5m, Target = 22.0m },
                new() { QuarterlyReportId = 17, Name = "Return on Equity", Value = 27.5m, Target = 29.0m }
            }
        },
        new() {
            Id = 18, Quarter = "Q1-2024", Department = "Marketing", Region = "Asia",
            Revenue = 92000, Expenses = 61000, Profit = 31000,
            Metrics = new List<string> { "Brand Relaunch", "Market Entry" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 18, Month = "January", Revenue = 29000, Expenses = 19000 },
                new() { QuarterlyReportId = 18, Month = "February", Revenue = 30000, Expenses = 20000 },
                new() { QuarterlyReportId = 18, Month = "March", Revenue = 33000, Expenses = 22000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 18, Name = "AUM", Value = 2700000, Target = 3100000 },
                new() { QuarterlyReportId = 18, Name = "Net New Clients", Value = 51, Target = 58 }
            }
        },
        new() {
            Id = 19, Quarter = "Q1-2024", Department = "Finance", Region = "North America",
            Revenue = 112000, Expenses = 71000, Profit = 41000,
            Metrics = new List<string> { "Q1 Forecasting", "Budget Review" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 19, Month = "January", Revenue = 36000, Expenses = 23000 },
                new() { QuarterlyReportId = 19, Month = "February", Revenue = 37000, Expenses = 23000 },
                new() { QuarterlyReportId = 19, Month = "March", Revenue = 39000, Expenses = 25000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 19, Name = "EBITDA", Value = 43000, Target = 46000 },
                new() { QuarterlyReportId = 19, Name = "Net Margin", Value = 36.6m, Target = 38.5m }
            }
        },
        new() {
            Id = 20, Quarter = "Q1-2024", Department = "Support", Region = "Europe",
            Revenue = 53000, Expenses = 32000, Profit = 21000,
            Metrics = new List<string> { "Service Quality", "Response Optimization" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 20, Month = "January", Revenue = 17000, Expenses = 10000 },
                new() { QuarterlyReportId = 20, Month = "February", Revenue = 17000, Expenses = 10000 },
                new() { QuarterlyReportId = 20, Month = "March", Revenue = 19000, Expenses = 12000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 20, Name = "IRR", Value = 18.5m, Target = 20.5m },
                new() { QuarterlyReportId = 20, Name = "Return on Equity", Value = 24.8m, Target = 26.5m }
            }
        },
        new() {
            Id = 21, Quarter = "Q2-2024", Department = "Sales", Region = "North America",
            Revenue = 145000, Expenses = 88000, Profit = 57000,
            Metrics = new List<string> { "Mid-year Growth", "Customer Expansion" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 21, Month = "April", Revenue = 46000, Expenses = 28000 },
                new() { QuarterlyReportId = 21, Month = "May", Revenue = 48000, Expenses = 29000 },
                new() { QuarterlyReportId = 21, Month = "June", Revenue = 51000, Expenses = 31000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 21, Name = "AUM", Value = 3600000, Target = 4000000 },
                new() { QuarterlyReportId = 21, Name = "Net New Clients", Value = 67, Target = 75 }
            }
        },
        new() {
            Id = 22, Quarter = "Q2-2024", Department = "Marketing", Region = "Europe",
            Revenue = 99000, Expenses = 66000, Profit = 33000,
            Metrics = new List<string> { "Spring Campaign", "Lead Quality" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 22, Month = "April", Revenue = 31000, Expenses = 21000 },
                new() { QuarterlyReportId = 22, Month = "May", Revenue = 33000, Expenses = 22000 },
                new() { QuarterlyReportId = 22, Month = "June", Revenue = 35000, Expenses = 23000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 22, Name = "EBITDA", Value = 35000, Target = 38000 },
                new() { QuarterlyReportId = 22, Name = "Net Margin", Value = 33.3m, Target = 36.0m }
            }
        },
        new() {
            Id = 23, Quarter = "Q2-2024", Department = "Finance", Region = "Asia",
            Revenue = 119000, Expenses = 75000, Profit = 44000,
            Metrics = new List<string> { "Mid-year Review", "Investment Strategy" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 23, Month = "April", Revenue = 38000, Expenses = 24000 },
                new() { QuarterlyReportId = 23, Month = "May", Revenue = 40000, Expenses = 25000 },
                new() { QuarterlyReportId = 23, Month = "June", Revenue = 41000, Expenses = 26000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 23, Name = "IRR", Value = 19.8m, Target = 22.0m },
                new() { QuarterlyReportId = 23, Name = "EBITDA", Value = 46000, Target = 49000 }  // Fixed: was duplicate "IRR"
            }
        },
        new() {
            Id = 24, Quarter = "Q2-2024", Department = "Support", Region = "North America",
            Revenue = 57000, Expenses = 33000, Profit = 24000,
            Metrics = new List<string> { "Escalation Management", "Customer Feedback" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 24, Month = "April", Revenue = 18000, Expenses = 10000 },
                new() { QuarterlyReportId = 24, Month = "May", Revenue = 19000, Expenses = 11000 },
                new() { QuarterlyReportId = 24, Month = "June", Revenue = 20000, Expenses = 12000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 24, Name = "Return on Equity", Value = 25.5m, Target = 27.5m },
                new() { QuarterlyReportId = 24, Name = "Net New Clients", Value = 54, Target = 60 }
            }
        },
        new() {
            Id = 25, Quarter = "Q3-2024", Department = "Sales", Region = "Asia",
            Revenue = 142000, Expenses = 90000, Profit = 52000,
            Metrics = new List<string> { "Regional Expansion", "Partner Network" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 25, Month = "July", Revenue = 45000, Expenses = 29000 },
                new() { QuarterlyReportId = 25, Month = "August", Revenue = 47000, Expenses = 30000 },
                new() { QuarterlyReportId = 25, Month = "September", Revenue = 50000, Expenses = 31000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 25, Name = "AUM", Value = 3800000, Target = 4200000 },
                new() { QuarterlyReportId = 25, Name = "EBITDA", Value = 55000, Target = 58000 }
            }
        },
        new() {
            Id = 26, Quarter = "Q3-2024", Department = "Marketing", Region = "North America",
            Revenue = 103000, Expenses = 68000, Profit = 35000,
            Metrics = new List<string> { "Fall Initiative", "Product Launch" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 26, Month = "July", Revenue = 33000, Expenses = 22000 },
                new() { QuarterlyReportId = 26, Month = "August", Revenue = 34000, Expenses = 22000 },
                new() { QuarterlyReportId = 26, Month = "September", Revenue = 36000, Expenses = 24000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 26, Name = "IRR", Value = 21.0m, Target = 23.0m },
                new() { QuarterlyReportId = 26, Name = "Net Margin", Value = 34.0m, Target = 36.5m }
            }
        },
        new() {
            Id = 27, Quarter = "Q3-2024", Department = "Finance", Region = "Europe",
            Revenue = 122000, Expenses = 76000, Profit = 46000,
            Metrics = new List<string> { "Q3 Analysis", "Risk Management" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 27, Month = "July", Revenue = 39000, Expenses = 24000 },
                new() { QuarterlyReportId = 27, Month = "August", Revenue = 41000, Expenses = 26000 },
                new() { QuarterlyReportId = 27, Month = "September", Revenue = 42000, Expenses = 26000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 27, Name = "Return on Equity", Value = 28.2m, Target = 30.0m },
                new() { QuarterlyReportId = 27, Name = "Net New Clients", Value = 71, Target = 78 }
            }
        },
        new() {
            Id = 28, Quarter = "Q3-2024", Department = "Support", Region = "Asia",
            Revenue = 62000, Expenses = 36000, Profit = 26000,
            Metrics = new List<string> { "Proactive Support", "Self-Service Adoption" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 28, Month = "July", Revenue = 20000, Expenses = 11000 },
                new() { QuarterlyReportId = 28, Month = "August", Revenue = 20000, Expenses = 12000 },
                new() { QuarterlyReportId = 28, Month = "September", Revenue = 22000, Expenses = 13000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 28, Name = "AUM", Value = 3200000, Target = 3600000 },
                new() { QuarterlyReportId = 28, Name = "EBITDA", Value = 27500, Target = 30000 }
            }
        },
        new() {
            Id = 29, Quarter = "Q4-2024", Department = "Sales", Region = "Europe",
            Revenue = 155000, Expenses = 97000, Profit = 58000,
            Metrics = new List<string> { "Year-end Push", "Account Renewals" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 29, Month = "October", Revenue = 50000, Expenses = 31000 },
                new() { QuarterlyReportId = 29, Month = "November", Revenue = 52000, Expenses = 32000 },
                new() { QuarterlyReportId = 29, Month = "December", Revenue = 53000, Expenses = 34000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 29, Name = "IRR", Value = 22.5m, Target = 24.0m },
                new() { QuarterlyReportId = 29, Name = "Net Margin", Value = 37.4m, Target = 39.0m }
            }
        },
        new() {
            Id = 30, Quarter = "Q4-2024", Department = "Marketing", Region = "Asia",
            Revenue = 108000, Expenses = 71000, Profit = 37000,
            Metrics = new List<string> { "Holiday Campaign", "Brand Loyalty" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 30, Month = "October", Revenue = 34000, Expenses = 23000 },
                new() { QuarterlyReportId = 30, Month = "November", Revenue = 36000, Expenses = 23000 },
                new() { QuarterlyReportId = 30, Month = "December", Revenue = 38000, Expenses = 25000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 30, Name = "Return on Equity", Value = 29.5m, Target = 31.0m },
                new() { QuarterlyReportId = 30, Name = "Net New Clients", Value = 76, Target = 82 }
            }
        },
        new() {
            Id = 31, Quarter = "Q4-2024", Department = "Finance", Region = "North America",
            Revenue = 130000, Expenses = 81000, Profit = 49000,
            Metrics = new List<string> { "Annual Planning", "Year-end Close" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 31, Month = "October", Revenue = 42000, Expenses = 26000 },
                new() { QuarterlyReportId = 31, Month = "November", Revenue = 43000, Expenses = 27000 },
                new() { QuarterlyReportId = 31, Month = "December", Revenue = 45000, Expenses = 28000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 31, Name = "AUM", Value = 4100000, Target = 4500000 },
                new() { QuarterlyReportId = 31, Name = "EBITDA", Value = 52000, Target = 55000 }
            }
        },
        new() {
            Id = 32, Quarter = "Q4-2024", Department = "Support", Region = "Europe",
            Revenue = 60000, Expenses = 35000, Profit = 25000,
            Metrics = new List<string> { "Holiday Coverage", "Year-end Review" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 32, Month = "October", Revenue = 19000, Expenses = 11000 },
                new() { QuarterlyReportId = 32, Month = "November", Revenue = 20000, Expenses = 11000 },
                new() { QuarterlyReportId = 32, Month = "December", Revenue = 21000, Expenses = 13000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 32, Name = "IRR", Value = 20.2m, Target = 22.0m },
                new() { QuarterlyReportId = 32, Name = "Net Margin", Value = 41.7m, Target = 43.5m }
            }
        },
        new() {
            Id = 33, Quarter = "Q1-2025", Department = "Sales", Region = "North America",
            Revenue = 160000, Expenses = 99000, Profit = 61000,
            Metrics = new List<string> { "New Year Launch", "Strategic Accounts" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 33, Month = "January", Revenue = 51000, Expenses = 32000 },
                new() { QuarterlyReportId = 33, Month = "February", Revenue = 53000, Expenses = 33000 },
                new() { QuarterlyReportId = 33, Month = "March", Revenue = 56000, Expenses = 34000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 33, Name = "Return on Equity", Value = 31.0m, Target = 33.0m },
                new() { QuarterlyReportId = 33, Name = "AUM", Value = 4400000, Target = 4800000 }
            }
        },
        new() {
            Id = 34, Quarter = "Q1-2025", Department = "Marketing", Region = "Europe",
            Revenue = 96000, Expenses = 63000, Profit = 33000,
            Metrics = new List<string> { "Q1 Strategy", "Digital Transformation" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 34, Month = "January", Revenue = 30000, Expenses = 20000 },
                new() { QuarterlyReportId = 34, Month = "February", Revenue = 32000, Expenses = 21000 },
                new() { QuarterlyReportId = 34, Month = "March", Revenue = 34000, Expenses = 22000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 34, Name = "Net New Clients", Value = 59, Target = 65 },
                new() { QuarterlyReportId = 34, Name = "EBITDA", Value = 35000, Target = 37500 }
            }
        },
        new() {
            Id = 35, Quarter = "Q1-2025", Department = "Finance", Region = "Asia",
            Revenue = 126000, Expenses = 79000, Profit = 47000,
            Metrics = new List<string> { "Q1 Forecasting", "Investment Portfolio" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 35, Month = "January", Revenue = 40000, Expenses = 25000 },
                new() { QuarterlyReportId = 35, Month = "February", Revenue = 42000, Expenses = 26000 },
                new() { QuarterlyReportId = 35, Month = "March", Revenue = 44000, Expenses = 28000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 35, Name = "IRR", Value = 23.0m, Target = 25.0m },
                new() { QuarterlyReportId = 35, Name = "Net Margin", Value = 37.3m, Target = 39.5m }
            }
        },
        new() {
            Id = 36, Quarter = "Q1-2025", Department = "Support", Region = "North America",
            Revenue = 64000, Expenses = 37000, Profit = 27000,
            Metrics = new List<string> { "Service Excellence", "Customer Success" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 36, Month = "January", Revenue = 20000, Expenses = 12000 },
                new() { QuarterlyReportId = 36, Month = "February", Revenue = 21000, Expenses = 12000 },
                new() { QuarterlyReportId = 36, Month = "March", Revenue = 23000, Expenses = 13000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 36, Name = "AUM", Value = 3500000, Target = 3900000 },
                new() { QuarterlyReportId = 36, Name = "Return on Equity", Value = 26.5m, Target = 28.5m }  // Fixed: was duplicate "AUM"
            }
        },
        new() {
            Id = 37, Quarter = "Q2-2025", Department = "Sales", Region = "Asia",
            Revenue = 148000, Expenses = 92000, Profit = 56000,
            Metrics = new List<string> { "Market Penetration", "Customer Acquisition" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 37, Month = "April", Revenue = 47000, Expenses = 29000 },
                new() { QuarterlyReportId = 37, Month = "May", Revenue = 49000, Expenses = 31000 },
                new() { QuarterlyReportId = 37, Month = "June", Revenue = 52000, Expenses = 32000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 37, Name = "EBITDA", Value = 59000, Target = 62000 },
                new() { QuarterlyReportId = 37, Name = "Net New Clients", Value = 83, Target = 90 }
            }
        },
        new() {
            Id = 38, Quarter = "Q2-2025", Department = "Marketing", Region = "North America",
            Revenue = 107000, Expenses = 70000, Profit = 37000,
            Metrics = new List<string> { "Spring Campaign", "Content Marketing" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 38, Month = "April", Revenue = 34000, Expenses = 22000 },
                new() { QuarterlyReportId = 38, Month = "May", Revenue = 36000, Expenses = 24000 },
                new() { QuarterlyReportId = 38, Month = "June", Revenue = 37000, Expenses = 24000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 38, Name = "IRR", Value = 22.8m, Target = 24.5m },
                new() { QuarterlyReportId = 38, Name = "Net Margin", Value = 34.6m, Target = 37.0m }
            }
        },
        new() {
            Id = 39, Quarter = "Q2-2025", Department = "Finance", Region = "Europe",
            Revenue = 128000, Expenses = 80000, Profit = 48000,
            Metrics = new List<string> { "Mid-year Forecast", "Compliance Review" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 39, Month = "April", Revenue = 41000, Expenses = 26000 },
                new() { QuarterlyReportId = 39, Month = "May", Revenue = 43000, Expenses = 27000 },
                new() { QuarterlyReportId = 39, Month = "June", Revenue = 44000, Expenses = 27000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 39, Name = "Return on Equity", Value = 30.2m, Target = 32.0m },
                new() { QuarterlyReportId = 39, Name = "AUM", Value = 4600000, Target = 5000000 }
            }
        },
        new() {
            Id = 40, Quarter = "Q2-2025", Department = "Support", Region = "Asia",
            Revenue = 66000, Expenses = 38000, Profit = 28000,
            Metrics = new List<string> { "24/7 Coverage", "Response Time" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 40, Month = "April", Revenue = 21000, Expenses = 12000 },
                new() { QuarterlyReportId = 40, Month = "May", Revenue = 22000, Expenses = 13000 },
                new() { QuarterlyReportId = 40, Month = "June", Revenue = 23000, Expenses = 13000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 40, Name = "Net New Clients", Value = 62, Target = 68 },
                new() { QuarterlyReportId = 40, Name = "EBITDA", Value = 29500, Target = 32000 }
            }
        },
        new() {
            Id = 41, Quarter = "Q3-2025", Department = "Sales", Region = "Europe",
            Revenue = 163000, Expenses = 101000, Profit = 62000,
            Metrics = new List<string> { "European Expansion", "Cross-sell Success" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 41, Month = "July", Revenue = 52000, Expenses = 32000 },
                new() { QuarterlyReportId = 41, Month = "August", Revenue = 54000, Expenses = 34000 },
                new() { QuarterlyReportId = 41, Month = "September", Revenue = 57000, Expenses = 35000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 41, Name = "IRR", Value = 24.2m, Target = 26.0m },
                new() { QuarterlyReportId = 41, Name = "Net Margin", Value = 38.0m, Target = 40.0m }
            }
        },
        new() {
            Id = 42, Quarter = "Q3-2025", Department = "Marketing", Region = "Asia",
            Revenue = 112000, Expenses = 73000, Profit = 39000,
            Metrics = new List<string> { "Fall Promotions", "Partnership Marketing" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 42, Month = "July", Revenue = 36000, Expenses = 23000 },
                new() { QuarterlyReportId = 42, Month = "August", Revenue = 37000, Expenses = 24000 },
                new() { QuarterlyReportId = 42, Month = "September", Revenue = 39000, Expenses = 26000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 42, Name = "Return on Equity", Value = 31.5m, Target = 33.5m },
                new() { QuarterlyReportId = 42, Name = "Net New Clients", Value = 88, Target = 95 }
            }
        },
        new() {
            Id = 43, Quarter = "Q3-2025", Department = "Finance", Region = "North America",
            Revenue = 134000, Expenses = 83000, Profit = 51000,
            Metrics = new List<string> { "Q3 Review", "Capital Allocation" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 43, Month = "July", Revenue = 43000, Expenses = 27000 },
                new() { QuarterlyReportId = 43, Month = "August", Revenue = 44000, Expenses = 27000 },
                new() { QuarterlyReportId = 43, Month = "September", Revenue = 47000, Expenses = 29000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 43, Name = "Net Margin", Value = 38.1m, Target = 40.0m },
                new() { QuarterlyReportId = 43, Name = "AUM", Value = 4900000, Target = 5300000 }  // Fixed: was duplicate "Net Margin"
            }
        },
        new() {
            Id = 44, Quarter = "Q3-2025", Department = "Support", Region = "Europe",
            Revenue = 68000, Expenses = 39000, Profit = 29000,
            Metrics = new List<string> { "Automation Initiative", "User Training" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 44, Month = "July", Revenue = 22000, Expenses = 12000 },
                new() { QuarterlyReportId = 44, Month = "August", Revenue = 22000, Expenses = 13000 },
                new() { QuarterlyReportId = 44, Month = "September", Revenue = 24000, Expenses = 14000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 44, Name = "EBITDA", Value = 30500, Target = 33000 },
                new() { QuarterlyReportId = 44, Name = "IRR", Value = 21.8m, Target = 23.5m }
            }
        },
        new() {
            Id = 45, Quarter = "Q4-2025", Department = "Sales", Region = "North America",
            Revenue = 170000, Expenses = 105000, Profit = 65000,
            Metrics = new List<string> { "Record Quarter", "Enterprise Deals" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 45, Month = "October", Revenue = 54000, Expenses = 33000 },
                new() { QuarterlyReportId = 45, Month = "November", Revenue = 57000, Expenses = 35000 },
                new() { QuarterlyReportId = 45, Month = "December", Revenue = 59000, Expenses = 37000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 45, Name = "Return on Equity", Value = 33.0m, Target = 35.0m },
                new() { QuarterlyReportId = 45, Name = "Net New Clients", Value = 95, Target = 102 }  // Fixed: was duplicate "Return on Equity"
            }
        },
        new() {
            Id = 46, Quarter = "Q4-2025", Department = "Marketing", Region = "Europe",
            Revenue = 115000, Expenses = 75000, Profit = 40000,
            Metrics = new List<string> { "Holiday Campaign", "Year-end Success" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 46, Month = "October", Revenue = 36000, Expenses = 24000 },
                new() { QuarterlyReportId = 46, Month = "November", Revenue = 38000, Expenses = 25000 },
                new() { QuarterlyReportId = 46, Month = "December", Revenue = 41000, Expenses = 26000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 46, Name = "AUM", Value = 5200000, Target = 5600000 },
                new() { QuarterlyReportId = 46, Name = "Net Margin", Value = 34.8m, Target = 37.0m }
            }
        },
        new() {
            Id = 47, Quarter = "Q4-2025", Department = "Finance", Region = "Asia",
            Revenue = 138000, Expenses = 86000, Profit = 52000,
            Metrics = new List<string> { "Annual Close", "Strategic Planning" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 47, Month = "October", Revenue = 44000, Expenses = 28000 },
                new() { QuarterlyReportId = 47, Month = "November", Revenue = 46000, Expenses = 28000 },
                new() { QuarterlyReportId = 47, Month = "December", Revenue = 48000, Expenses = 30000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 47, Name = "IRR", Value = 25.5m, Target = 27.0m },
                new() { QuarterlyReportId = 47, Name = "EBITDA", Value = 55000, Target = 58000 }
            }
        },
        new() {
            Id = 48, Quarter = "Q4-2025", Department = "Support", Region = "North America",
            Revenue = 70000, Expenses = 40000, Profit = 30000,
            Metrics = new List<string> { "Year-end Review", "Process Improvement" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 48, Month = "October", Revenue = 22000, Expenses = 13000 },
                new() { QuarterlyReportId = 48, Month = "November", Revenue = 23000, Expenses = 13000 },
                new() { QuarterlyReportId = 48, Month = "December", Revenue = 25000, Expenses = 14000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 48, Name = "Return on Equity", Value = 28.5m, Target = 30.5m },
                new() { QuarterlyReportId = 48, Name = "Net New Clients", Value = 72, Target = 78 }
            }
        },
        new() {
            Id = 49, Quarter = "Q1-2026", Department = "Sales", Region = "Asia",
            Revenue = 152000, Expenses = 94000, Profit = 58000,
            Metrics = new List<string> { "New Year Growth", "Market Leadership" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 49, Month = "January", Revenue = 48000, Expenses = 30000 },
                new() { QuarterlyReportId = 49, Month = "February", Revenue = 51000, Expenses = 31000 },
                new() { QuarterlyReportId = 49, Month = "March", Revenue = 53000, Expenses = 33000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 49, Name = "AUM", Value = 5500000, Target = 6000000 },
                new() { QuarterlyReportId = 49, Name = "Net Margin", Value = 38.2m, Target = 40.5m }
            }
        },
        new() {
            Id = 50, Quarter = "Q1-2026", Department = "Marketing", Region = "North America",
            Revenue = 118000, Expenses = 77000, Profit = 41000,
            Metrics = new List<string> { "Brand Refresh", "Digital Innovation" },
            MonthlyData = new List<MonthlyBreakdown>
            {
                new() { QuarterlyReportId = 50, Month = "January", Revenue = 37000, Expenses = 24000 },
                new() { QuarterlyReportId = 50, Month = "February", Revenue = 39000, Expenses = 26000 },
                new() { QuarterlyReportId = 50, Month = "March", Revenue = 42000, Expenses = 27000 }
            },
            KPIs = new List<Kpi>
            {
                new() { QuarterlyReportId = 50, Name = "Return on Equity", Value = 32.5m, Target = 34.5m },
                new() { QuarterlyReportId = 50, Name = "IRR", Value = 24.0m, Target = 26.0m }  // Fixed: was duplicate "Return on Equity"
            }
        }
    };

    static FinanceDataStore()
    {
        // Fix QuarterlyReport IDs
        DataFixer.FixQuarterlyReportIds(Reports);

        // Assign deterministic IDs to all MonthlyBreakdown and Kpi entries
        foreach (var report in Reports)
        {
            for (int i = 0; i < report.MonthlyData.Count; i++)
            {
                report.MonthlyData[i].Id = DataFixer.GenerateDeterministicId(report.Id, "monthly", i);
            }

            for (int i = 0; i < report.KPIs.Count; i++)
            {
                report.KPIs[i].Id = DataFixer.GenerateDeterministicId(report.Id, "kpi", i);
            }
        }
    }

    // Generate distinct entities for dropdown endpoints
    public static readonly List<RegionEntity> Regions = Reports
        .Select(r => r.Region)
        .Distinct()
        .OrderBy(r => r)
        .Select((r, index) => new RegionEntity { Id = index + 1, Region = r })
        .ToList();

    public static readonly List<DepartmentEntity> Departments = Reports
        .Select(r => r.Department)
        .Distinct()
        .OrderBy(d => d)
        .Select((d, index) => new DepartmentEntity { Id = index + 1, Department = d })
        .ToList();

    public static readonly List<QuarterEntity> Quarters = Reports
        .Select(r => r.Quarter)
        .Distinct()
        .OrderBy(q => {
            // Parse "Q1-2023" format for chronological sorting
            var parts = q.Split('-');
            var year = int.Parse(parts[1]);
            var quarter = int.Parse(parts[0].Substring(1));
            return year * 10 + quarter;
        })
        .Select((q, index) => new QuarterEntity { Id = index + 1, Quarter = q })
        .ToList();
}
