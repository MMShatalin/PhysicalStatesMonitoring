﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Ipt;

namespace MonitorForms
{
    /// <summary>Настройки приложения.</summary>
    public class Settings
    {
        #region Свойства

        //public List<ObjectInfo> ScudValues;

        public string ScudIp { get; set; }
        public int ScudPort { get; set; }
        public string IptIp { get; set; }
        public int IptPort { get; set; }
        public string LogFile { get; set; }
        public string EmulPath { get; set; }
        public bool ErrorLogVisible { get; set; }
        public bool ScudListVisible { get; set; }
        public bool IptListVisible { get; set; }
        public int IptFreqIndex { get; set; }
        public Kks Kks { get; set; }

        [XmlArrayItem]
        public double[] Lambdas { get; set; }

        [XmlArrayItem]
        public double[] Alphas { get; set; }

        [XmlIgnore]
        public IPAddress IptIpAddress
        {
            get
            {
                return IPAddress.Parse(IptIp.CleanIp());
            }
        }

        [XmlIgnore]
        public IPAddress ScudIpAddress
        {
            get
            {
                return IPAddress.Parse(ScudIp.CleanIp());
            }
        }

        private static XmlSerializer Serializer { get; set; }

        private static string SettingsPath
        {
            get
            {
                return "settings.xml";
            }
        }

        #endregion

        static Settings()
        {
            Serializer = new XmlSerializer(typeof(Settings));
        }

        private static Settings GetDefaultSettings()
        {
            var set = new Settings
                      {
                          IptIp = "192.168.008.002",
                          IptPort = 2040,
                          ScudIp = "192.168.008.001",
                          ScudPort = 1024,
                          LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.txt"),
                          ErrorLogVisible = false,
                          ScudListVisible = true,
                          IptListVisible = true,
                          IptFreqIndex = 0,
                          Kks = new Kks(),
                          //ScudValues = new List<ObjectInfo>
                          //             {
                          //                 new ObjectInfo("PCore", 60, 15, 17),
                          //                 new ObjectInfo("TCold", 82, 280, 320),
                          //                 new ObjectInfo("THot", 77, 280, 320),
                          //                 new ObjectInfo("PSg", 232, 6, 8),
                          //                 new ObjectInfo("H12", 102, 0, 100),
                          //                 new ObjectInfo("H11", 101, 0, 100),
                          //                 new ObjectInfo("H10", 100, 0, 100),
                          //                 new ObjectInfo("LPres", 241, 4, 9),
                          //                 new ObjectInfo("LSg", 237, 0, 10),
                          //                 new ObjectInfo("Cbor", 51, 0, 10),
                          //                 new ObjectInfo("Cborf", 53, 0, 40),
                          //                 new ObjectInfo("Fmakeup", 63, 10000, 90000),
                          //                 new ObjectInfo("Nakz", 54, 1200, 3200),
                          //                 new ObjectInfo("Ntg", 59, 0, 1200),
                          //                 new ObjectInfo("Ao", 243, -50, 10)
                          //             },
                          Lambdas = new[] {0.0127, 0.0317, 0.1180, 0.3170, 1.4000, 3.9200},
                          Alphas = new[] {0.0340, 0.2020, 0.1840, 0.4030, 0.1430, 0.0340}
                      };
            //set.ScudValues.Add("PCore", 60);
            //set.ScudValues.Add("TCold", 82);
            //set.ScudValues.Add("THot", 77);
            //set.ScudValues.Add("PSg", 232);
            //set.ScudValues.Add("H12", 102);
            //set.ScudValues.Add("H11", 101);
            //set.ScudValues.Add("H10", 100);
            //set.ScudValues.Add("LPres", 241);
            //set.ScudValues.Add("LSg", 237);
            //set.ScudValues.Add("Cbor", 51);
            //set.ScudValues.Add("Cborf", 53);
            //set.ScudValues.Add("Fmakeup", 63);
            //set.ScudValues.Add("Nakz", 54);
            //set.ScudValues.Add("Ntg", 59);
            //set.ScudValues.Add("Ao", 243);

            return set;
        }

        public static Settings Read()
        {
            if (!File.Exists(SettingsPath))
            {
                return GetDefaultSettings();
            }
            using (var reader = new StreamReader(SettingsPath))
            {
                try
                {
                    return (Settings) Serializer.Deserialize(reader);
                }
                catch (Exception)
                {
                    return GetDefaultSettings();
                }
            }
        }

        public static void Save(Settings settings)
        {
            using (var writer = new StreamWriter(SettingsPath))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                Serializer.Serialize(writer, settings, ns);
            }
        }
    }
}