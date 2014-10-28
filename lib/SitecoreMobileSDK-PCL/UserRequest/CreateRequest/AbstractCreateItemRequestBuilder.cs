namespace Sitecore.MobileSDK.UserRequest.CreateRequest
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Request.Template;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UserRequest.ChangeRequest;
  using Sitecore.MobileSDK.Validators;


  public abstract class AbstractCreateItemRequestBuilder<T> :
    AbstractChangeItemRequestBuilder<T>,
    ICreateItemRequestParametersBuilder<T>,
    ISetNewItemNameBuilder<T>,
    ISetTemplateBuilder<T>
  where T : class
  {
    protected bool IsCreateFromBranch { get; private set; }

    protected CreateItemParameters itemParametersAccumulator = new CreateItemParameters(null, null, null);

    public ICreateItemRequestParametersBuilder<T> ItemName(string itemName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemName, this.GetType().Name + ".ItemName");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemParametersAccumulator.ItemName,
        this.GetType().Name + ".ItemName");

      this.itemParametersAccumulator =
        new CreateItemParameters(itemName, this.itemParametersAccumulator.ItemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    public ISetNewItemNameBuilder<T> ItemTemplatePath(string itemTemplate)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemTemplate, this.GetType().Name + ".ItemTemplate");
      ItemPathValidator.ValidateItemTemplate(itemTemplate, this.GetType().Name + ".itemTemplate");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemParametersAccumulator.ItemTemplate,
        this.GetType().Name + ".ItemTemplate");

      //igk spike to use one restrictions for all paths 
      itemTemplate = itemTemplate.TrimStart('/');

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, itemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    public ISetNewItemNameBuilder<T> ItemTemplateId(string itemTemplate)
    {
      this.SetItemTemplateId(itemTemplate);
      this.IsCreateFromBranch = false;

      return this;
    }

    public ISetNewItemNameBuilder<T> BranchId(string branchId)
    //    public ICreateItemRequestParametersBuilder<T> BranchId(string branchId)
    {
      this.SetItemTemplateId(branchId);
      this.IsCreateFromBranch = true;

      return this;
    }

    private void SetItemTemplateId(string itemTemplateOrBranch)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemTemplateOrBranch, this.GetType().Name + ".ItemTemplate");
      ItemIdValidator.ValidateItemId(itemTemplateOrBranch, this.GetType().Name + ".itemTemplate");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemParametersAccumulator.ItemTemplate,
        this.GetType().Name + ".ItemTemplate");

      //igk spike to use one restrictions for all paths 
      string trimmedTemplate = itemTemplateOrBranch.TrimStart('/');

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, trimmedTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName)
    {
      base.AddFieldsRawValuesByNameToSet(fieldsRawValuesByName);

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return this;
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue)
    {
      base.AddFieldsRawValuesByNameToSet(fieldName, fieldValue);

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return this;
    }

    private bool CheckForDuplicate(string key)
    {
      bool isDuplicate = false;

      if (null != this.itemParametersAccumulator.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.itemParametersAccumulator.FieldsRawValuesByName)
        {
          isDuplicate = fieldElem.Key.Equals(key);
        }
      }

      return isDuplicate;
    }

    new public ICreateItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    new public ICreateItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    new public ICreateItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFieldsToRead(fields);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFieldsToRead(fieldParams);
    }
  }
}

