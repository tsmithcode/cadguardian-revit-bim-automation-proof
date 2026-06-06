# Runtime Guide

## Public runtime

The default kit runs with local .NET and does not require licensed CAD software.

```bash
dotnet run --project quickstart
```

## Native runtime

Use C# for IFC/package validation and a Revit API external command only after model ownership and parameter rules are clear.

Native examples are intentionally optional. They should be used only inside the matching licensed CAD environment after the package boundary is proven.

## Native handoff points

- **Model context gate:** `Document`, `FilteredElementCollector`, `BuiltInCategory`, and model ownership checks.
- **Family and parameter readiness:** `FamilyInstance`, `Parameter`, `BuiltInParameter`, and transaction-scoped edits.
- **Sheet and schedule boundary:** `ViewSheet`, `ViewSchedule`, and rollback-first external command proof.
