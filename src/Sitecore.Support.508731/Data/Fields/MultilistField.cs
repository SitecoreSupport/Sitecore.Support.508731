using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace Sitecore.Support.Data.Fields
{
  public class MultilistField : Sitecore.Data.Fields.MultilistField
  {
    private static readonly string LinkValidationExcludedFieldsSetting =
      "Sitecore.Support.LinkValidation.ExcludedFields";
    private static readonly ID[] FieldIDs;
    public override void ValidateLinks(LinksValidationResult result)
    {
      if (FieldIDs.Contains(this.InnerField.ID))
      {
        Log.Debug(
          string.Format("SITECORE.SUPPORT: Skipping link validation for the {0} field based on the {1} setting",
            this.InnerField.ID, LinkValidationExcludedFieldsSetting), this);
        return;
      }
      base.ValidateLinks(result);
    }

    static MultilistField()
    {
      var excludedFields = Configuration.Settings.GetSetting(LinkValidationExcludedFieldsSetting,
        "{84E7DF9E-A319-417B-A15D-84B40EA2D8E4}");
      FieldIDs = ID.ParseArray(excludedFields);
    }

    public MultilistField(Field innerField) : base(innerField)
    {
    }
  }
}