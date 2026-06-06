import { existsSync } from "node:fs";

const runtimeHints = [
  "Revit API",
  "BIM",
  "IFC",
  "families",
  "parameters",
  "sheets",
  "schedules"
];
const commonLocalHints = [
  "/Applications/Autodesk",
  "/Applications",
  "C:/Program Files/Autodesk",
  "C:/Program Files/SOLIDWORKS Corp",
  "C:/Program Files/Bentley",
];

const visibleHints = commonLocalHints.filter((path) => existsSync(path));

console.log("Revit and BIM Workflow Automation Proof");
console.log("Runtime vocabulary:", runtimeHints.join(", "));
console.log("Visible local runtime hints:", visibleHints.length > 0 ? visibleHints.join(", ") : "none detected");
console.log("This check does not prove CAD execution. Native geometry, conversion, repair, or API execution requires a separate local tool receipt.");
