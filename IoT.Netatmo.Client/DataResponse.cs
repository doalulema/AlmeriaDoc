using System;
using System.Runtime.Serialization;

namespace IoT.Netatmo.Client
{
    [DataContract]
    public class DataResponse
    {
        [DataMember(Name = "body")]
        public DataCapture Data { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "time_exec")]
        public float ExecutionTime { get; set; }
        [DataMember(Name = "time_server")]
        public long TimeServerTimestamp { get; set; }
    }

    [DataContract]
    public class DataCapture
    {
        [DataMember(Name = "devices")]
        public DataDevice[] Devices { get; set; }
        [DataMember(Name = "user")]
        public DeviceUser User { get; set; }
    }

    [DataContract]
    public class DataDevice
    {
        [DataMember(Name = "_id")]
        public string Id { get; set; }
        [DataMember(Name = "cipher_id")]
        public string CipherId { get; set; }
        [DataMember(Name = "date_setup")]
        public long DateSetupTimestamp { private get; set; }
        [DataMember(Name = "last_setup")]
        public long LastSetupTimestamp { private get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "last_status_store")]
        public long LastStatusStoreTimestamp { private get; set; }
        [DataMember(Name = "module_name")]
        public string ModuleName { get; set; }
        [DataMember(Name = "firmware")]
        public int Firmware { get; set; }
        [DataMember(Name = "last_upgrade")]
        public long LastUpgradeTimestamp { private get; set; }
        [DataMember(Name = "wifi_status")]
        public int WifiStatus { get; set; }
        [DataMember(Name = "co2_calibrating")]
        public bool Co2Calibrating { get; set; }
        [DataMember(Name = "station_name")]
        public string StationName { get; set; }
        [DataMember(Name = "data_type")]
        public string[] DataType { get; set; }
        [DataMember(Name = "place")]
        public DataPlace Place { get; set; }
        [DataMember(Name = "dashboard_data")]
        public Dashboard DashboardData { get; set; }

        public DateTime DateSetup {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(DateSetupTimestamp);
            }
        }
        public DateTime LastSetup {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(LastSetupTimestamp);
            }
        }
        public DateTime LastStatusStore {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(LastStatusStoreTimestamp);
            }
        }
        public DateTime LastUpgrade {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(LastUpgradeTimestamp);
            }
        }
    }

    [DataContract]
    public class DeviceUser
    {
        [DataMember(Name = "mail")]
        public string Email { get; set; }
        [DataMember(Name = "administrative")]
        public AdministrativeInfo Administrative { get; set; }
    }

    [DataContract]
    public class AdministrativeInfo
    {
        [DataMember(Name = "lang")]
        public string Language { get; set; }
        [DataMember(Name = "reg_locale")]
        public string RegionLocale { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "unit")]
        public int Unit { get; set; }
        [DataMember(Name = "windunit")]
        public int WindUnit { get; set; }
        [DataMember(Name = "pressureunit")]
        public int PressureUnit { get; set; }
        [DataMember(Name = "feel_like_algo")]
        public int FeelLikeAlgo { get; set; }
    }

    [DataContract]
    public class DataPlace
    {
        [DataMember(Name = "altitude")]
        public int Altitude { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }
        [DataMember(Name = "location")]
        public float[] Location { get; set; }
    }

    [DataContract]
    public class Dashboard
    {
        [DataMember(Name = "time_utc")]
        public long TimeUtcTimestamp { private get; set; }
        [DataMember(Name = "Temperature")]
        public float Temperature { get; set; }
        [DataMember(Name = "CO2")]
        public int Co2 { get; set; }
        [DataMember(Name = "Humidity")]
        public int Humidity { get; set; }
        [DataMember(Name = "Noise")]
        public int Noise { get; set; }
        [DataMember(Name = "Pressure")]
        public float Pressure { get; set; }
        [DataMember(Name = "AbsolutePressure")]
        public float AbsolutePressure { get; set; }
        [DataMember(Name = "min_temp")]
        public float MinTemp { get; set; }
        [DataMember(Name = "max_temp")]
        public float MaxTemp { get; set; }
        [DataMember(Name = "date_min_temp")]
        public long DateMinTempTimestamp { private get; set; }
        [DataMember(Name = "date_max_temp")]
        public long DateMaxTempTimestamp { private get; set; }

        public DateTime TimeUtc {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(TimeUtcTimestamp);
            }
        }
        public DateTime DateMinTemp {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(DateMinTempTimestamp);
            }
        }
        public DateTime DateMaxTemp {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(DateMaxTempTimestamp);
            }
        }
}

    public enum WifiStatusEnum
    {
        Good = 56,
        Average = 71,
        Bad = 86
    }
}