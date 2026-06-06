# Native Runtime

The public kit runs without licensed CAD software. The examples in `native/` are intentionally optional.

## Runtime decision

Use C# for IFC/package validation and a Revit API external command only after model ownership and parameter rules are clear.

## Native/API examples

- native/revit-addin/CadGuardianRevitCommand.cs

## Rule

Do not claim native geometry mutation, conversion, plotting, PDM state changes, or model edits unless a local tool receipt is produced with approved files and tooling.
