# Revit and BIM Workflow Automation Proof

CAD Guardian proof repo for technical interviews, buyer reviews, and peer walkthroughs.

> This CAD library is in development. This is an early public preview for feedback on the best business case, workflow shape, and proof path.

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
npm run doctor
npm run verify
npm run demo
npm run sanitize
```

Expected demo output: `reports/demo-validation-report.json` with a review-ready status, validation checks, stop conditions, and the public CAD data boundary.

## Runtime model
This repo is tiered:

- Public demo: runs anywhere with Node.js and synthetic fixtures.
- Optional native/runtime check: `npm run runtime:check` reports whether local CAD/API tooling appears available.
- Real CAD files: stay in an AgentOps-controlled private library unless explicitly approved for a private runtime receipt.

## Guides
- [User guide](docs/USER_GUIDE.md)
- [Runtime guide](docs/RUNTIME_GUIDE.md)
- [API references](docs/API_REFERENCES.md)
- [Expected outcome](docs/EXPECTED_OUTCOME.md)
- [Development preview warning](docs/DEVELOPMENT_PREVIEW.md)

## Official references
- [Revit API Developer Guide](https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html) - Model, parameter, document, and add-in vocabulary.
- [Autodesk APS Automation APIs](https://aps.autodesk.com/automation-apis) - External automation option when workload/runtime compatibility is proven.
- [AWS API Gateway](https://docs.aws.amazon.com/apigateway/latest/developerguide/welcome.html) - API front door, status endpoints, and service boundary discussion.
- [AWS Step Functions](https://docs.aws.amazon.com/step-functions/latest/dg/welcome.html) - State-machine orchestration, retries, and staged workflow discussion.
- [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) - Event-driven job/API shape when the platform standard is Azure.
- [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) - Queue and service-bus vocabulary for async CAD work.

## Public CAD data boundary
buildingSMART IFC samples are referenced as open BIM fixtures. This repo does not bundle RVT/RFA files.

This repository is built for public proof. It includes source inventory manifests, synthetic input fixtures, validation examples, and adapter code shaped for walkthroughs. It does not include private drawings, proprietary project files, login material, raw opportunity notes, or native CAD files that AgentOps marks catalog-only.

## Related service page
https://www.cadguardian.com/services/revit-bim-automation
