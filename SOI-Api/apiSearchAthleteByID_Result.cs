//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOI_Api
{
    using System;
    
    public partial class apiSearchAthleteByID_Result
    {
        public string athl_guid { get; set; }
        public string athl_first { get; set; }
        public string athl_middle { get; set; }
        public string athl_last { get; set; }
        public string athl_group { get; set; }
        public string athl_imagepath { get; set; }
        public string delg_guid { get; set; }
        public string delg_name { get; set; }
        public string game_guid { get; set; }
        public string game_name { get; set; }
        public string sprt_guid { get; set; }
        public string sprt_name { get; set; }
        public string evnt_guid { get; set; }
        public string evnt_name { get; set; }
        public string rslt_bib { get; set; }
        public string rslt_division { get; set; }
        public Nullable<System.DateTime> rslt_date { get; set; }
        public string rslt_score { get; set; }
        public string rslt_place { get; set; }
        public int rslt_round { get; set; }
        public string rslt_matchid { get; set; }
        public string rslt_matchstatus { get; set; }
        public string rslt_divisionstatus { get; set; }
        public string rslt_venue { get; set; }
        public Nullable<bool> rslt_personalbest { get; set; }
        public string EventType { get; set; }
        public bool IsBracketed { get; set; }
    }
}