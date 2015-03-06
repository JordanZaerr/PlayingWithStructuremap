namespace StructureMapExample.Reporting
{
    public interface IReportModule
    {
        void BuildReports(string inputPath, string outputPath);
    }
}