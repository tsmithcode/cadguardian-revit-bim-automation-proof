import { existsSync } from "node:fs";

const runtimeHints = [
  "IExternalCommand",
  "ExternalCommandData",
  "Document",
  "Transaction",
  "FilteredElementCollector",
  "Parameter",
  "FamilyInstance",
  "ViewSheet",
  "ViewSchedule",
  "BuiltInCategory",
  "BuiltInParameter"
];
const commonLocalHints = [
  "/Applications/Autodesk",
  "/Applications",
  "C:/Program Files/Autodesk",
  "C:/Program Files/SOLIDWORKS Corp",
  "C:/Program Files/Bentley",
];
const visibleHints = commonLocalHints.filter((path) => existsSync(path));

console.log("Revit and BIM Workflow Quick-Start Automation Kit");
console.log("API/native vocabulary:", runtimeHints.join(", "));
console.log("Visible local runtime hints:", visibleHints.length > 0 ? visibleHints.join(", ") : "none detected");
console.log("Public quickstart is runnable without licensed CAD. Native adapters require the matching local CAD/runtime environment.");
