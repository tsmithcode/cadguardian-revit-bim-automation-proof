# Revit and BIM Workflow Quick-Start Automation Kit

CAD Guardian quick-start automation kit for peer walkthroughs, technical interviews, and buyer-facing business-case discussions.

> This CAD library is in development. This is an early public preview for feedback on the best business case, workflow shape, and proof path.

## STAR story

**Situation:** A BIM team wants faster model-adjacent output, but families, parameters, sheets, schedules, and review ownership make automation risky.

**Task:** Create a public-safe quickstart that proves model context and document checks before a Revit API add-in touches a live model.

**Action:** Bundle approved buildingSMART IFC fixtures, validate model-context signals, and show a Revit external command scaffold for parameters, sheets, schedules, and family instances.

**Result:** Reviewers can run a safe BIM package check and discuss the native Revit API boundary with concrete class names.

## Fast run

```bash
npm run doctor
npm run verify
npm run demo
dotnet build quickstart
dotnet run --project quickstart
```

The C# quickstart writes `reports/quickstart-report.json`. The Node demo writes `reports/demo-validation-report.json`.

## What is included

- Runnable C# quickstart in `quickstart/`.
- Optional native/runtime examples in `native/`.
- Safe public fixtures in `fixtures/public/`.
- STAR story, API walkthrough, native runtime notes, interview script, and expected outcome docs.

## Workflow

- BIM document request
- IFC fixture inventory
- Model context contract
- Parameter check
- Sheet and schedule check
- Revit add-in boundary
- BIM review gate
- Approved next slice

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
