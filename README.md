# Revit and BIM Workflow Automation Proof

CAD Guardian proof repo for technical interviews, buyer reviews, and peer walkthroughs.

## Story
A BIM team needs model-adjacent automation without guessing across families, parameters, sheets, schedules, or document ownership.

## Business case
The proof narrows the model/document boundary before anyone automates across a live workshared model.

## Workflow
- BIM document request
- Model context contract
- Revit API adapter boundary
- IFC-safe reference package
- Parameter and schedule checks
- Sheet/export report
- BIM reviewer gate
- Approved next slice

## Stack vocabulary
- Revit API
- BIM
- IFC
- families
- parameters
- sheets
- schedules

## Run

```bash
npm run verify
npm run demo
```

## Public CAD data boundary
buildingSMART IFC samples are referenced as open BIM fixtures. This repo does not bundle RVT/RFA files.

This repository is built for public proof. It includes source inventory manifests, synthetic input fixtures, validation examples, and adapter code shaped for walkthroughs. It does not include private drawings, proprietary project files, login material, raw opportunity notes, or native CAD files that AgentOps marks catalog-only.

## Related service page
https://www.cadguardian.com/services/revit-bim-automation
