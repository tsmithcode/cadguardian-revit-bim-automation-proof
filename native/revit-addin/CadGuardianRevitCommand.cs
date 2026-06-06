// Optional native example. Requires Revit API references and a licensed Revit runtime.
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

[Transaction(TransactionMode.Manual)]
public sealed class CadGuardianRevitCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uiDoc = commandData.Application.ActiveUIDocument;
        Document doc = uiDoc.Document;

        using Transaction tx = new(doc, "CAD Guardian BIM readiness audit");
        tx.Start();

        var familyInstances = new FilteredElementCollector(doc)
            .OfClass(typeof(FamilyInstance))
            .ToElements();
        var sheets = new FilteredElementCollector(doc)
            .OfClass(typeof(ViewSheet))
            .ToElements();
        var schedules = new FilteredElementCollector(doc)
            .OfClass(typeof(ViewSchedule))
            .ToElements();

        foreach (Element element in familyInstances.Take(10))
        {
            Parameter? mark = element.get_Parameter(BuiltInParameter.ALL_MODEL_MARK);
            _ = mark?.AsString();
        }

        tx.RollBack();
        TaskDialog.Show("CAD Guardian", $"Families={familyInstances.Count}, Sheets={sheets.Count}, Schedules={schedules.Count}");
        return Result.Succeeded;
    }
}
