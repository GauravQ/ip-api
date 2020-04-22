using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ip_api.Net45
{
    public enum FieldOption
    {
        [Description("status")]

        Status,
        
        [Description("message")] 
        Message,

        [Description("country")]
        Country,

        [Description("countryCode")]
        CountryCode,

        [Description("region")]
        Region,

        [Description("regionName")]
        RegionName,

        [Description("city")]
        City,

        [Description("zip")]
        Zipcode,

        [Description("lat")]
        Latitute,

        [Description("lon")]
        Longitute,

        [Description("timezone")]
        Timezone,

        [Description("isp")]
        ISP,

        [Description("org")]
        Organization,

        [Description("as")]
        AS,

        [Description("query")]
        Query
    }


}
