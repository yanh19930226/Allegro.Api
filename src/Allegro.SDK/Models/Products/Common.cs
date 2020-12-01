using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Products
{
    public class Category
    {
        public string id { get; set; }
    }

    public class Eans
    {

    }

    public class Images
    {
        public string url { get; set; }
    }

    public class Parameter
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class OfferRequirement
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class compatibilityList
    {
        public string id { get; set; }
        public string type { get; set; }

        public List<compatibilityitem> item { get; set; }
    }
    public class compatibilityitem
    {

    }

    public class tecdocSpecification
    {
        public string id { get; set; }
    }
    public class description
    {
        public List<Section> sections { get; set; }
    }

    public class Section
    {
        public List<SectionItem> items { get; set; }
    }

    public class SectionItem
    {
        public string type{ get; set; }
    }

}
