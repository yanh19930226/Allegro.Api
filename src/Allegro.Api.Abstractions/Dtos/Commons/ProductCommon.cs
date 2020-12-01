using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.Api.Abstractions.Dtos.Commons
{
    public class CategoryDto
    {
        public string id { get; set; }
    }

    public class EansDto
    {

    }

    public class ImagesDto
    {
        public string url { get; set; }
    }

    public class ParameterDto
    {
        public string id { get; set; }
    }
    public class OfferRequirementDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class compatibilityListDto
    {
        public string id { get; set; }
        public string type { get; set; }

        public List<compatibilityitemDto> item { get; set; }
    }
    public class compatibilityitemDto
    {

    }

    public class tecdocSpecificationDto
    {
        public string id { get; set; }
    }
    public class descriptionDto
    {
        public List<SectionDto> sections { get; set; }
    }

    public class SectionDto
    {
        public List<SectionItemDto> items { get; set; }
    }

    public class SectionItemDto
    {
        public string type { get; set; }
    }
}
