//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IoT.Services.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceEntry
    {
        public System.Guid id { get; set; }
        public decimal value { get; set; }
        public int sourceId { get; set; }
        public string sourceDescription { get; set; }
        public string dataType { get; set; }
        public string direction { get; set; }
        public System.DateTime timestamp { get; set; }
    }
}