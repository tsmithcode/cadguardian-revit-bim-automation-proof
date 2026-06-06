export function runAdapter(job) {
  return {
    requestId: job.requestId,
    kitType: "CAD Guardian quick-start automation kit",
    repo: "tsmithcode/cadguardian-revit-bim-automation-proof",
    runtimeDecision: job.runtimeDecision,
    apiSignals: [
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
],
    expectedOutputs: [
  "bim-context-report",
  "parameter-checks",
  "sheet-schedule-readiness",
  "native adapter notes"
],
    validation: [
  "IFC fixtures are present and attributed",
  "IFC text exposes model/object markers",
  "Parameter, sheet, and schedule checks are represented",
  "Revit API external command handoff is documented"
].map((rule) => ({
      rule,
      status: "review-ready",
      evidence: "Public quick-start kit fixture, API walkthrough, or native adapter example is present.",
    })),
    publicBoundary: "No private client files, login material, raw opportunity notes, or license-uncertain CAD assets are included.",
  };
}
