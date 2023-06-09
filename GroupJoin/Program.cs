
List<MandatoryField> mandatoryFields = new List<MandatoryField>
{
    new MandatoryField { FieldId = 1, FieldName = "Field One" },
    new MandatoryField { FieldId = 2, FieldName = "Field Two" },
    new MandatoryField { FieldId = 10, FieldName = "Field Ten" },
    new MandatoryField { FieldId = 11, FieldName = "Field Eleven" },
    new MandatoryField { FieldId = 19, FieldName = "Field Nineteen" }
};

List<ShareFieldsValue> shareFieldsValues = new List<ShareFieldsValue>
{
    new ShareFieldsValue { ShareFieldId = 1, ShareFieldName = "Field One", ShareValue = "Some value one" },
    new ShareFieldsValue { ShareFieldId = 2, ShareFieldName = "Field Two", ShareValue = "Some value one" },
    new ShareFieldsValue { ShareFieldId = 3, ShareFieldName = "Field Three", ShareValue = "Some value one" }
};


var mandatoryFieldsWithValues = mandatoryFields
    .GroupJoin(
        shareFieldsValues,
        mf => mf.FieldId,
        sv => sv.ShareFieldId,
        (mf, sv) => new { mf, sv }
    ).SelectMany(
        x => x.sv.DefaultIfEmpty(),
        (mf, sv) => new ShareFieldsValue 
        {
            ShareFieldId = mf.mf.FieldId,
            ShareFieldName = mf.mf.FieldName,
            ShareValue = sv?.ShareValue
        }
    ).ToList();

Console.ReadLine();




public class MandatoryField
{
    public int FieldId { get; set; }
    public string FieldName { get; set; }
}

public class ShareFieldsValue
{
    public int ShareFieldId { get; set; }
    public string ShareFieldName { get; set; }
    public string ShareValue { get; set; }
}