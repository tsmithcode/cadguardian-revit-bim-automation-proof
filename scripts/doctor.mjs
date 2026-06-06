import { execFileSync } from "node:child_process";
import { existsSync } from "node:fs";

const required = [
  "README.md",
  "quickstart/Program.cs",
  "quickstart/quickstart.csproj",
  "docs/STAR.md",
  "docs/USER_GUIDE.md",
  "docs/RUNTIME_GUIDE.md",
  "docs/API_REFERENCES.md",
  "docs/EXPECTED_OUTCOME.md",
  "fixtures/public",
  "native/revit-addin/CadGuardianRevitCommand.cs"
];
const missing = required.filter((file) => !existsSync(file));

console.log("Revit and BIM Workflow Quick-Start Automation Kit");
console.log("Kit type: CAD Guardian Pareto quick-start automation kit");
console.log("Business impact: " + "Prove BIM context, families, parameters, sheets, schedules, and reviewer gates before a Revit API add-in touches a live model.");
console.log("Development preview: " + "This CAD library is in development. This is an early public preview for feedback on the best business case, workflow shape, and proof path.");

try {
  const dotnet = execFileSync("dotnet", ["--version"], { encoding: "utf8" }).trim();
  console.log("dotnet:", dotnet);
} catch {
  throw new Error("dotnet is required for the C# quickstart.");
}

if (missing.length > 0) throw new Error(`Missing required files: ${missing.join(", ")}`);
console.log("Doctor passed. Next: npm run kit");
