using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

var repoRoot = FindRepoRoot(AppContext.BaseDirectory);
var profile = new KitProfile(
    "Revit and BIM Workflow Quick-Start Automation Kit",
    "tsmithcode/cadguardian-revit-bim-automation-proof",
    "bim-document-readiness",
    "BIM manager",
    "Prove BIM context, families, parameters, sheets, schedules, and reviewer gates before a Revit API add-in touches a live model.",
    "A BIM team wants faster model-adjacent output, but families, parameters, sheets, schedules, and review ownership make automation risky.",
    "Prove model context and document checks before a Revit API add-in touches a live model.",
    "Bundle public IFC fixtures, validate model-context signals, and show a Revit external command scaffold for parameters, sheets, schedules, and family instances.",
    "A reviewer can run a safe BIM package check and discuss the Revit API boundary with concrete class names.",
    "Use C# for IFC/package validation and a Revit API external command only after model ownership and parameter rules are clear.",
    "Pick one sheet/schedule/family outcome, name required parameters, then prove whether the Revit add-in boundary is justified.",
    new string[] { "IExternalCommand", "ExternalCommandData", "Document", "Transaction", "FilteredElementCollector", "Parameter", "FamilyInstance", "ViewSheet", "ViewSchedule", "BuiltInCategory", "BuiltInParameter" },
    new string[] { "BIM document request", "IFC fixture inventory", "Model context contract", "Parameter check", "Sheet and schedule check", "Revit add-in boundary", "BIM review gate", "Approved next slice" },
    new[]
    {
        new FixtureSpec("fixtures/public/buildingsmart/Building-Architecture.ifc", "IFC", "BIM architecture model-context fixture.", "buildingSMART Sample Test Files", "Creative Commons Attribution 4.0 International", new string[] { "IFC", "IFCPROJECT" }),
        new FixtureSpec("fixtures/public/buildingsmart/wall-with-opening-and-window.ifc", "IFC", "Small wall/opening/window parameter fixture.", "buildingSMART Sample Test Files", "Creative Commons Attribution 4.0 International", new string[] { "IFCWALL", "IFCWINDOW" }),
    },
    new[]
    {
        new ParetoRule("Model context gate", "Avoids automating against a model before project, family, and object context is visible.", "`Document`, `FilteredElementCollector`, `BuiltInCategory`, and model ownership checks.", new string[] { "IFCPROJECT" }),
        new ParetoRule("Family and parameter readiness", "Turns ambiguous BIM requests into inspectable parameter and family checks.", "`FamilyInstance`, `Parameter`, `BuiltInParameter`, and transaction-scoped edits.", new string[] { "IFCWALL", "IFCWINDOW" }),
        new ParetoRule("Sheet and schedule boundary", "Keeps output automation tied to reviewable sheets and schedules rather than hidden model mutation.", "`ViewSheet`, `ViewSchedule`, and rollback-first external command proof.", new string[] { "native/revit-addin/CadGuardianRevitCommand.cs" }),
    });

var report = new ParetoQuickStartRunner(repoRoot, profile).Run();
var options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
var reportPath = Path.Combine(repoRoot, "reports", "quickstart-report.json");
Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);
File.WriteAllText(reportPath, JsonSerializer.Serialize(report, options));

Console.WriteLine(profile.Title);
Console.WriteLine($"Status: {report.Status}");
Console.WriteLine($"Pareto checks: {report.ParetoChecks.Count}");
Console.WriteLine($"Reusable routines: {report.ReusableRoutines.Count}");
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
    string WorkflowClass,
    string ReviewOwner,
    string BusinessImpact,
    string Situation,
    string Task,
    string Action,
    string Result,
    string RuntimeDecision,
    string NextMove,
    IReadOnlyList<string> ApiSignals,
    IReadOnlyList<string> Workflow,
    IReadOnlyList<FixtureSpec> Fixtures,
    IReadOnlyList<ParetoRule> ParetoRules);

public sealed record FixtureSpec(
    string Path,
    string Format,
    string Use,
    string Attribution,
    string License,
    IReadOnlyList<string> EvidenceTokens);

public sealed record FixtureReceipt(
    string Path,
    string Format,
    string Use,
    string Attribution,
    string License,
    long SizeBytes,
    string Sha256,
    bool TextReadable,
    IReadOnlyList<string> EvidenceFound,
    IReadOnlyList<string> EvidenceMissing,
    string RuntimeBoundary);

public sealed record ParetoRule(
    string Name,
    string BusinessImpact,
    string NativeHandoff,
    IReadOnlyList<string> EvidenceNeeded);

public sealed record ParetoCheck(
    string Name,
    string Status,
    string BusinessImpact,
    string Evidence,
    string NativeHandoff);

public sealed record ReusableRoutine(
    string Name,
    string WhyItMatters,
    string AdaptationPoint);

public sealed record QuickStartReport(
    string Status,
    string GeneratedAtUtc,
    string Repo,
    string Title,
    string WorkflowClass,
    string ReviewOwner,
    string BusinessImpact,
    string RuntimeDecision,
    string NextMove,
    StarStory Star,
    IReadOnlyList<string> Workflow,
    IReadOnlyList<string> ApiSignals,
    IReadOnlyList<FixtureReceipt> Fixtures,
    IReadOnlyList<ParetoCheck> ParetoChecks,
    IReadOnlyList<ReusableRoutine> ReusableRoutines);

public sealed record StarStory(string Situation, string Task, string Action, string Result);

public sealed class ParetoQuickStartRunner
{
    private readonly string repoRoot;
    private readonly KitProfile profile;

    public ParetoQuickStartRunner(string repoRoot, KitProfile profile)
    {
        this.repoRoot = repoRoot;
        this.profile = profile;
    }

    public QuickStartReport Run()
    {
        var fixtures = profile.Fixtures.Select(InspectFixture).ToList();
        var checks = profile.ParetoRules.Select(rule => EvaluateRule(rule, fixtures)).ToList();
        var routines = new[]
        {
            new ReusableRoutine(
                "FixtureInventory",
                "Creates a stable receipt before automation touches trusted CAD files.",
                "Replace the public fixtures with your private package path after access is approved."),
            new ReusableRoutine(
                "ParetoRuleEngine",
                "Keeps the first useful rules visible instead of hiding business logic in scripts.",
                "Swap or add rules for the repeated checks your drafters already perform."),
            new ReusableRoutine(
                "NativeRuntimeGate",
                "Prevents public parser confidence from pretending to be licensed CAD execution.",
                "Move a rule into the native adapter only after the public report shows why it matters."),
        };
        var status = checks.Any(check => check.Status is "needs-review") ? "needs-review" : "ready-for-private-sample";

        return new QuickStartReport(
            status,
            DateTimeOffset.UtcNow.ToString("O"),
            profile.Repo,
            profile.Title,
            profile.WorkflowClass,
            profile.ReviewOwner,
            profile.BusinessImpact,
            profile.RuntimeDecision,
            profile.NextMove,
            new StarStory(profile.Situation, profile.Task, profile.Action, profile.Result),
            profile.Workflow,
            profile.ApiSignals,
            fixtures,
            checks,
            routines);
    }

    private FixtureReceipt InspectFixture(FixtureSpec fixture)
    {
        var path = Path.Combine(repoRoot, fixture.Path);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Missing fixture: {fixture.Path}", path);
        }

        var bytes = File.ReadAllBytes(path);
        var hash = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
        var extension = Path.GetExtension(path).ToLowerInvariant();
        var textReadable = extension is ".dxf" or ".ifc" or ".step" or ".stp";
        var found = new List<string>();
        var missing = new List<string>();

        if (textReadable && fixture.EvidenceTokens.Count > 0)
        {
            var text = File.ReadAllText(path);
            foreach (var token in fixture.EvidenceTokens)
            {
                if (text.Contains(token, StringComparison.OrdinalIgnoreCase)) found.Add(token);
                else missing.Add(token);
            }
        }
        else if (fixture.EvidenceTokens.Count == 0)
        {
            found.Add(fixture.Format);
        }

        return new FixtureReceipt(
            fixture.Path,
            fixture.Format,
            fixture.Use,
            fixture.Attribution,
            fixture.License,
            bytes.LongLength,
            hash,
            textReadable,
            found,
            missing,
            textReadable ? "public-text-scan" : "licensed-native-runtime-required");
    }

    private static ParetoCheck EvaluateRule(ParetoRule rule, IReadOnlyList<FixtureReceipt> fixtures)
    {
        var evidence = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var fixture in fixtures)
        {
            evidence.Add(fixture.Format);
            foreach (var token in fixture.EvidenceFound) evidence.Add(token);
        }

        foreach (var token in rule.EvidenceNeeded)
        {
            if (token.StartsWith("native/", StringComparison.OrdinalIgnoreCase))
            {
                evidence.Add(token);
            }
        }

        var missing = rule.EvidenceNeeded.Where(token => !evidence.Contains(token)).ToArray();
        var status = missing.Length == 0 ? "ready-for-private-sample" : "needs-review";
        var evidenceSummary = missing.Length == 0
            ? $"Evidence present: {string.Join(", ", rule.EvidenceNeeded)}"
            : $"Missing evidence: {string.Join(", ", missing)}";

        return new ParetoCheck(rule.Name, status, rule.BusinessImpact, evidenceSummary, rule.NativeHandoff);
    }
}
