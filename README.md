# Revit and BIM Workflow Quick-Start Automation Kit

CAD Guardian Pareto quick-start automation kit for drafters, CAD automation peers, technical interviews, and buyer-facing business-case discussions.

> This CAD library is in development. This is an early public preview for feedback on the best business case, workflow shape, and proof path.

## Why this exists

Prove BIM context, families, parameters, sheets, schedules, and reviewer gates before a Revit API add-in touches a live model.

## Fast run

```bash
npm run doctor
npm run verify
npm run demo
dotnet build quickstart
```

`npm run demo` runs the C# quickstart and writes `reports/quickstart-report.json`.

## What is worth reusing

- `quickstart/Program.cs`: a small C# package-readiness engine with fixture receipts, Pareto checks, native runtime gates, and a JSON report.
- `native/`: optional API/runtime examples for the licensed CAD environment.
- `fixtures/public/`: approved public CAD fixtures only.
- `docs/USER_GUIDE.md`: how to run and adapt the kit.
- `docs/INTERVIEW_SCRIPT.md`: how to explain the business case without guessing.

## STAR story

**Situation:** A BIM team wants faster model-adjacent output, but families, parameters, sheets, schedules, and review ownership make automation risky.

**Task:** Prove model context and document checks before a Revit API add-in touches a live model.

**Action:** Bundle public IFC fixtures, validate model-context signals, and show a Revit external command scaffold for parameters, sheets, schedules, and family instances.

**Result:** A reviewer can run a safe BIM package check and discuss the Revit API boundary with concrete class names.

## Pareto checks

- **Model context gate:** Avoids automating against a model before project, family, and object context is visible. Handoff: `Document`, `FilteredElementCollector`, `BuiltInCategory`, and model ownership checks.
- **Family and parameter readiness:** Turns ambiguous BIM requests into inspectable parameter and family checks. Handoff: `FamilyInstance`, `Parameter`, `BuiltInParameter`, and transaction-scoped edits.
- **Sheet and schedule boundary:** Keeps output automation tied to reviewable sheets and schedules rather than hidden model mutation. Handoff: `ViewSheet`, `ViewSchedule`, and rollback-first external command proof.

## API and runtime signals

- IExternalCommand
- ExternalCommandData
- Document
- Transaction
- FilteredElementCollector
- Parameter
- FamilyInstance
- ViewSheet
- ViewSchedule
- BuiltInCategory
- BuiltInParameter

## Public fixture boundary

Only approved public sample files are bundled. No client files, private drawings, credentials, raw opportunity notes, or license-uncertain CAD assets are included.

## Service page

https://www.cadguardian.com/services/revit-bim-automation
