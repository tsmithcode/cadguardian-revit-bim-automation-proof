# Runtime Guide

## Default public runtime

The default runtime is Node.js plus synthetic fixtures:

```bash
npm run doctor
npm run verify
npm run demo
```

Expected output: `reports/demo-validation-report.json`.

## Optional native/runtime path

Run:

```bash
npm run runtime:check
```

This command only reports visible local runtime hints. It does not prove CAD execution.

## Runtime decision for this proof

Revit API add-in or utility only after model context and parameter ownership are clear.

## AgentOps boundary

buildingSMART IFC samples are referenced as open BIM fixtures. This repo does not bundle RVT/RFA files.

Native CAD files, private client material, credentials, source-system exports, and raw opportunity notes stay outside this public repo.
