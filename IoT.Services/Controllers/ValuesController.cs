using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Runtime.Serialization;
using System.Web.Http;
using IoT.Services.Model;

namespace IoT.Services.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly DataEntities Context = new DataEntities();

        [DataContract]
        public class DeviceDataEntry
        {
            [DataMember(Name = "value")]
            public decimal Value { get; set; }
            [DataMember(Name = "sourceId")]
            public int SourceId { get; set; }
            [DataMember(Name = "sourceDescription")]
            public string SourceDescription { get; set; }
            [DataMember(Name = "dataType")]
            public string DataType { get; set; }
            [DataMember(Name = "direction")]
            public string Direction { get; set; }
        }

        // GET api/values
        public IEnumerable<DeviceEntry> Get()
        {
            return Context.DeviceEntries;
        }

        // GET api/values/5
        public DeviceEntry Get(Guid id)
        {
            var item = Context.DeviceEntries.Find(id);

            return item;
        }

        // POST api/values
        public void Post([FromBody]DeviceDataEntry value)
        {
            var entry = new DeviceEntry
            {
                id = Guid.NewGuid(),
                dataType = value.DataType,
                value = value.Value,
                direction = value.Direction,
                sourceDescription = value.SourceDescription,
                sourceId = value.SourceId,
                timestamp = DateTime.Now
            };

            Context.DeviceEntries.Add(entry);
            Context.SaveChanges();
        }

        // PUT api/values/5
        public void Put(Guid id, [FromBody]DeviceDataEntry value)
        {
            var item = Context.DeviceEntries.Find(id);

            if (item != null)
            {
                item.value = value.Value;
                item.dataType = value.DataType;
                item.direction = value.Direction;
                item.sourceId = value.SourceId;
                item.sourceDescription = value.SourceDescription;
                item.timestamp = DateTime.Now;

                Context.DeviceEntries.AddOrUpdate(item);
                Context.SaveChanges();
            }
        }

        // DELETE api/values/5
        public void Delete(Guid id)
        {
            var item = Context.DeviceEntries.Find(id);

            if (item != null)
            {
                Context.DeviceEntries.Remove(item);
                Context.SaveChanges();
            }
        }
    }
}
