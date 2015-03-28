using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the CatalogSchema class.
    /// </summary>
    public class CatalogSchema : BindableSchemaBase, IEquatable<CatalogSchema>, ISyncItem<CatalogSchema>
    {
        private string _image;
        private string _name;
        private string _reference;
        private string _description;
        private string _specification;
        private string _moreInfo;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
 
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
 
        public string Reference
        {
            get { return _reference; }
            set { SetProperty(ref _reference, value); }
        }
 
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
 
        public string Specification
        {
            get { return _specification; }
            set { SetProperty(ref _specification, value); }
        }
 
        public string MoreInfo
        {
            get { return _moreInfo; }
            set { SetProperty(ref _moreInfo, value); }
        }

        public override string DefaultTitle
        {
            get { return Name; }
        }

        public override string DefaultSummary
        {
            get { return Reference; }
        }

        public override string DefaultImageUrl
        {
            get { return Image; }
        }

        public override string DefaultContent
        {
            get { return Reference; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLowerInvariant())
                {
                    case "image":
                        return String.Format("{0}", Image); 
                    case "name":
                        return String.Format("{0}", Name); 
                    case "reference":
                        return String.Format("{0}", Reference); 
                    case "description":
                        return String.Format("{0}", Description); 
                    case "specification":
                        return String.Format("{0}", Specification); 
                    case "moreinfo":
                        return String.Format("{0}", MoreInfo); 
                    case "defaulttitle":
                        return DefaultTitle;
                    case "defaultsummary":
                        return DefaultSummary;
                    case "defaultimageurl":
                        return DefaultImageUrl;
                    default:
                        break;
                }
            }
            return String.Empty;
        }

        public bool Equals(CatalogSchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(CatalogSchema other)
        {

            return this.Id == other.Id && (this.Image != other.Image || this.Name != other.Name || this.Reference != other.Reference || this.Description != other.Description || this.Specification != other.Specification || this.MoreInfo != other.MoreInfo);
        }

        public void Sync(CatalogSchema other)
        {
            this.Image = other.Image;
            this.Name = other.Name;
            this.Reference = other.Reference;
            this.Description = other.Description;
            this.Specification = other.Specification;
            this.MoreInfo = other.MoreInfo;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CatalogSchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
