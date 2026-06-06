export function classifyBimRisk(job) {
  return job.validationRules.map((rule) => ({ rule, risk: rule.includes('assumptions') ? 'review' : 'check' }));
}


export function runAdapter(job) {
  return {
    requestId: job.requestId,
    runtimeDecision: job.runtimeDecision,
    expectedOutputs: job.expectedOutputs,
    validation: job.validationRules.map((rule) => ({
      rule,
      status: "review-ready",
      evidence: "Synthetic fixture only. Run local CAD checks against AgentOps-approved source files for tool receipts.",
    })),
    publicBoundary: "No private client files, login material, raw opportunity notes, or catalog-only native CAD binaries are included.",
  };
}
