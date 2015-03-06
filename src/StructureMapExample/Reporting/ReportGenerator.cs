using System;
using System.Collections.Generic;
using System.Linq;
using StructureMapExample.EmployeeTypes;
using StructureMapExample.Utils;

namespace StructureMapExample.Reporting
{
    //This class is kind of a mess...
    public class ReportGenerator : IReportGenerator
    {
        public IEnumerable<string> GenerateReport(string reportTitle, IEnumerable<Employee> employeesOnReport)
        {
            var columns = new IColumn[]
            {
                new Column<int>("ID", 5, x => x.Id),
                new Column<string>("Last Name", 17, x => x.LastName),
                new Column<string>("First Name", 17, x => x.FirstName),
                new Column<DateTime>("Hired", 11, x => x.HireDate, x => x.ToString("MM/dd/yyyy")),
                new Column<string>("Type", 11, x => x.GetType().Name),

                //Everything after this participates in the summary columns and will require
                //changing the 'columnsToSkip' variable down below. There is also an assumption
                //that all summary particpants are Column<double>. Reordering these changes the
                //overall grid as well as the summary totals section.
                new Column<double>("Earnings", 10, x => x.Earnings, x => x.ToSemiCurrency()),
                new Column<double>("FICA", 10, x => x.FICA),
                new Column<double>("Fed Tax", 10, x => x.FedTax),
                new Column<double>("State Tax", 10, x => x.StateTax),
                new Column<double>("Net Pay", 10, x => x.NetPay, x => x.ToSemiCurrency())
            };

            var contents = new List<string>();
            
            //Report header
            var paddingAmount = (int)Math.Floor(columns.Sum(x => x.Width) / 2.0);
            contents.Add("Employee Report".PadLeftWithContents(paddingAmount));
            contents.Add(reportTitle.PadLeftWithContents(paddingAmount));
            contents.Add(DateTime.Now.ToString("MM/dd/yyyy").PadLeftWithContents(paddingAmount));
            contents.Add("");
            contents.Add("Homer Simpson".PadLeftWithContents(paddingAmount));
            contents.Add("\r\n");

            //Table Header
            contents.Add(columns.Aggregate("", (current, col) => 
                current + col.Title
                    .PadLeftWithContents((int)Math.Floor(col.Width/2.0))
                    .PadRight(col.Width)));
            contents.Add(columns.Aggregate("",
                (current, col) => current + ("-".Repeat(col.Width - 1)) + " "));

            //Body of table
            contents.AddRange(employeesOnReport.Select(employee => 
                        columns.Aggregate("", (current, col) => 
                                current + col.DisplayText(employee)
                                    .PadRight(col.Width))));

            //Summary underlines
            const int columnsToSkip = 5;
            var gap = " ".Repeat(columns.Take(columnsToSkip).Sum(x => x.Width));
            var underlines = columns.Skip(columnsToSkip).Select(x => "-".Repeat(x.Width - 1) + " ");
            contents.Add(gap + String.Join("", underlines));

            //Summary Totals
            contents.Add(CreateSummaryRow("Total", columnsToSkip, columns, employeesOnReport, x => x.Sum()));
            contents.Add(CreateSummaryRow("Mean", columnsToSkip, columns, employeesOnReport, x => x.Average()));
            contents.Add(CreateSummaryRow("Maximum", columnsToSkip, columns, employeesOnReport, x => x.Max()));
            contents.Add(CreateSummaryRow("Minimum", columnsToSkip, columns, employeesOnReport, x => x.Min()));

            //Counter and spacing for next report.
            contents.Add("\r\nCount = " + employeesOnReport.Count());
            contents.Add("\r\n\r\n\r\r\n");
            return contents;
        }

        private string CreateSummaryRow(
            string header, 
            int columnsToSkip, 
            IEnumerable<IColumn> columns,
            IEnumerable<Employee> employees,
            Func<IEnumerable<double>, double> aggregateFunction)
        {
            var gap = " ".Repeat(columns.Take(columnsToSkip).Sum(x => x.Width) - header.Length);
            var summaryValues = columns
                .Skip(columnsToSkip)
                .Cast<Column<double>>()
                .Select(col => aggregateFunction(employees
                    .Select(x =>col.GetValueImpl(x)))
                    .ToSemiCurrency().PadRight(col.Width - 1));
            return header + gap + String.Join(" ", summaryValues);
        }

        private class Column<T> : IColumn
        {
            public Column(
                string title, 
                int width, 
                Func<Employee, T> valueFunc,
                Func<T, string> formatter = null)
            {
                Title = title;
                Width = width;
                GetValueImpl = valueFunc;
                Formatter = formatter ?? (x => x.ToString());
            }

            public string Title { get; private set; }
            public int Width { get; private set; }
            public Func<Employee,string> DisplayText {
                get { return emp => Formatter(GetValueImpl(emp)); }
            }

            public Func<Employee, string> GetValue {
                get { return emp => GetValueImpl(emp).ToString(); }
            }
            public Func<Employee, T> GetValueImpl { get; private set; }
            public Func<T, string> Formatter { get; private set; }
        }

        private interface IColumn
        {
            string Title { get; }
            int Width { get; }
            Func<Employee, string> DisplayText { get; }
            Func<Employee, string> GetValue { get; }
        }
    }
}