# Expected Outcome

After running `npm run demo`, the repo writes:

```
reports/demo-validation-report.json
```

The report should contain:

- `requestId`: cadg-revit-demo-001
- `runtimeDecision`: Revit API add-in or utility only after model context and parameter ownership are clear.
- `expectedOutputs`: parameter map, sheet package manifest, schedule validation, review issue list
- `validation`: one review-ready row per validation rule
- `publicBoundary`: a reminder that private files and native CAD binaries are not bundled

## Stop conditions

The proof should stop instead of overclaiming when:

- Accepted output examples are missing.
- Native runtime execution cannot produce a local tool receipt.
- Reviewer ownership is unclear.
- The requested proof requires private files in a public repo.
