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
    ISetTemplateBuilder<T>
  where T : class
  {
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

    public ICreateItemRequestParametersBuilder<T> ItemTemplatePath(string itemTemplate)
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

    public ICreateItemRequestParametersBuilder<T> ItemTemplateId(string itemTemplate)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemTemplate, this.GetType().Name + ".ItemTemplate");
      ItemIdValidator.ValidateItemId(itemTemplate, this.GetType().Name + ".itemTemplate");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemParametersAccumulator.ItemTemplate,
        this.GetType().Name + ".ItemTemplate");

      //igk spike to use one restrictions for all paths 
      itemTemplate = itemTemplate.TrimStart('/');

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, itemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName)
    {
      base.AddFieldsRawValuesByName(fieldsRawValuesByName);

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return (ICreateItemRequestParametersBuilder<T>)this;

    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue)
    {
      base.AddFieldsRawValuesByName(fieldKey, fieldValue);

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return (ICreateItemRequestParametersBuilder<T>)this;
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

    new public ICreateItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    new public ICreateItemRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    new public ICreateItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }
  }
}

