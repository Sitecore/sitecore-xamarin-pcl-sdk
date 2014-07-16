
namespace Sitecore.MobileSDK.Items
{
  using System;

  public class ItemSourcePOD : IItemSource
  {
    public static IItemSource DefaultItemSourcePOD()
    {
      return new ItemSourcePOD(ItemSource.DefaultDatabase, ItemSource.DefaultLanguage, null);
    }

    public ItemSourcePOD(string database, string language, string version = null)
    {
      this.Database = database;
      this.Language = language;
      this.Version  = version ;
    }

    public virtual IItemSource ShallowCopy()
    {
      return new ItemSourcePOD(this.Database, this.Language, this.Version);
    }

    public override bool Equals (object obj)
    {
      if (object.ReferenceEquals(this, obj))
      {
        return true;
      }

      ItemSourcePOD other = (ItemSourcePOD)obj;
      if (null == other)
      {
        return false;
      }

      bool isDbEqual =  object.Equals ( this.Database, other.Database );
      bool isLangEqual = object.Equals (this.Language, other.Language);
      bool isVersionEqual = object.Equals (this.Version, other.Version);

      return isDbEqual && isLangEqual && isVersionEqual;
    } 

    public override int GetHashCode ()
    {
      return base.GetHashCode() + this.Database.GetHashCode () + this.Language.GetHashCode () + this.Version.GetHashCode ();
    }

    public string Database  { get; protected set; }
    public string Language  { get; protected set; }
    public string Version   { get; protected set; }
    public int VersionNumber 
    { 
      get 
      { 
        if (null == this.Version)
        {
          throw new ArgumentNullException ("[ItemSource.VersionNumber] : Cannot convert nil version to int");
        }

        return Convert.ToInt32 (this.Version); 
      } 
    }
  }
}

