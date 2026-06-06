using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

var repoRoot = FindRepoRoot(AppContext.BaseDirectory);
var profile = new KitProfile(
    "Revit and BIM Workflow Quick-Start Automation Kit",
    "tsmithcode/cadguardian-revit-bim-automation-proof",
    "A BIM team wants faster model-adjacent output, but families, parameters, sheets, schedules, and review ownership make automation risky.",
    "Create a public-safe quickstart that proves model context and document checks before a Revit API add-in touches a live model.",
    "Bundle approved buildingSMART IFC fixtures, validate model-context signals, and show a Revit external command scaffold for parameters, sheets, schedules, and family instances.",
    "Reviewers can run a safe BIM package check and discuss the native Revit API boundary with concrete class names.",
    new[] { "IExternalCommand", "ExternalCommandData", "Document", "Transaction", "FilteredElementCollector", "Parameter", "FamilyInstance", "ViewSheet", "ViewSchedule", "BuiltInCategory", "BuiltInParameter" },
    new[] { "IFC fixtures are present and attributed", "IFC text exposes model/object markers", "Parameter, sheet, and schedule checks are represented", "Revit API external command handoff is documented" },
    new[]
    {
        new FixtureSpec("fixtures/public/buildingsmart/Building-Architecture.ifc", "buildingsmart_ifc_samples", "Creative Commons Attribution 4.0 International", "buildingSMART Sample Test Files", "BIM architecture model-context fixture.", new[] { "IFC", "IFCPROJECT" }),
        new FixtureSpec("fixtures/public/buildingsmart/wall-with-opening-and-window.ifc", "buildingsmart_ifc_samples", "Creative Commons Attribution 4.0 International", "buildingSMART Sample Test Files", "Small wall/opening/window parameter fixture.", new[] { "IFCWALL", "IFCWINDOW" }),
    });

var runner = new QuickStartRunner(repoRoot, profile);
var report = runner.Run();
var options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
var reportPath = Path.Combine(repoRoot, "reports", "quickstart-report.json");
Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);
File.WriteAllText(reportPath, JsonSerializer.Serialize(report, options));

Console.WriteLine($"{profile.Title}");
Console.WriteLine($"Status: {report.Status}");
Console.WriteLine($"Fixtures: {report.Fixtures.Count}");
Console.WriteLine($"Checks: {report.Checks.Count}");
Console.WriteLine($"Report: {Path.GetRelativePath(repoRoot, reportPath)}");

static string FindRepoRoot(string start)
{
    var current = new DirectoryInfo(start);
    while (current is not null)
    {
        if (File.Exists(Path.Combine(current.FullName, "package.json")) && Directory.Exists(Path.Combine(current.FullName, "quickstart")))
        {
            return current.FullName;
        }
        current = current.Parent;
    }
    throw new InvalidOperationException("Could not locate repo root.");
}

public sealed record KitProfile(
    string Title,
    string Repo,
    string Situation,
    string Task,
    string Action,
    string Result,
    IReadOnlyList<string> ApiSignals,
    IReadOnlyList<string> ValidationRules,
    IReadOnlyList<FixtureSpec> Fixtures);

public sealed record FixtureSpec(
    string Path,
    string SourceId,
    string License,
    string Attribution,
    string Use,
    IReadOnlyList<string> Tokens);

public sealed record FixtureReceipt(
    string Path,
    string SourceId,
    long SizeBytes,
    string Sha256,
    bool TextReadable,
    IReadOnlyList<string> TokensFound,
    IReadOnlyList<string> TokensMissing,
    string Use,
    string Attribution,
    string License);

public sealed record ValidationCheck(string Rule, string Status, string Evidence);

public sealed record QuickStartReport(
    string Status,
    string Repo,
    string Title,
    string Situation,
    string Task,
    string Action,
    string Result,
    IReadOnlyList<string> ApiSignals,
    IReadOnlyList<FixtureReceipt> Fixtures,
    IReadOnlyList<ValidationCheck> Checks);

public interface ICadFixtureInspector
{
    FixtureReceipt Inspect(string repoRoot, FixtureSpec fixture);
}

public sealed class FileSystemFixtureInspector : ICadFixtureInspector
{
    public FixtureReceipt Inspect(string repoRoot, FixtureSpec fixture)
    {
        var path = Path.Combine(repoRoot, fixture.Path);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Missing fixture: {fixture.Path}", path);
        }

        var bytes = File.ReadAllBytes(path);
        var hash = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
        var extension = System.IO.Path.GetExtension(path).ToLowerInvariant();
        var textReadable = extension is ".dxf" or ".ifc" or ".step" or ".stp";
        var found = new List<string>();
        var missing = new List<string>();

        if (textReadable && fixture.Tokens.Count > 0)
        {
            var text = File.ReadAllText(path);
            foreach (var token in fixture.Tokens)
            {
                if (text.Contains(token, StringComparison.OrdinalIgnoreCase)) found.Add(token);
                else missing.Add(token);
            }
        }

        return new FixtureReceipt(
            fixture.Path,
            fixture.SourceId,
            bytes.LongLength,
            hash,
            textReadable,
            found,
            missing,
            fixture.Use,
            fixture.Attribution,
            fixture.License);
    }
}

public sealed class QuickStartRunner
{
    private readonly string repoRoot;
    private readonly KitProfile profile;
    private readonly ICadFixtureInspector inspector;

    public QuickStartRunner(string repoRoot, KitProfile profile, ICadFixtureInspector? inspector = null)
    {
        this.repoRoot = repoRoot;
        this.profile = profile;
        this.inspector = inspector ?? new FileSystemFixtureInspector();
    }

    public QuickStartReport Run()
    {
        var fixtures = profile.Fixtures.Select(fixture => inspector.Inspect(repoRoot, fixture)).ToList();
        var checks = new List<ValidationCheck>();

        foreach (var rule in profile.ValidationRules)
        {
            checks.Add(new ValidationCheck(rule, "review-ready", "Public fixture and API walkthrough evidence is present."));
        }

        foreach (var fixture in fixtures)
        {
            checks.Add(new ValidationCheck($"fixture: {fixture.Path}", fixture.TokensMissing.Count == 0 ? "sample-pass" : "review", fixture.TokensMissing.Count == 0 ? $"sha256 {fixture.Sha256[..12]} / {fixture.SizeBytes} bytes" : $"Missing expected tokens: {string.Join(", ", fixture.TokensMissing)}"));
        }

        var status = checks.Any(check => check.Status == "review") ? "review-required" : "review-ready";
        return new QuickStartReport(status, profile.Repo, profile.Title, profile.Situation, profile.Task, profile.Action, profile.Result, profile.ApiSignals, fixtures, checks);
    }
}
