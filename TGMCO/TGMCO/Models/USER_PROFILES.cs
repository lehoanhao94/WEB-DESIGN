//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGMCO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class USER_PROFILES
    {
        public int USER_PROFILE_ID { get; set; }
        public int USER_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE { get; set; }
        public string AVATAR { get; set; }
        public Nullable<int> POINTS { get; set; }
    }
}
