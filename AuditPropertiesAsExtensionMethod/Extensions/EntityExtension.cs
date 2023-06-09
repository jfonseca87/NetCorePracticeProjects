using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace ConsoleApp2.Extensions
{
    internal static class EntityExtension
    {
        public static T SetAudit<T>(this T entity, bool isCreatedAction, int userId) where T: class
        {
            Type objectType = entity.GetType();

            if (isCreatedAction)
            {
                PropertyInfo createdByField = objectType.GetProperty("CreatedBy");
                if (createdByField != null)
                {
                    createdByField.SetValue(entity, userId, null);
                }

                PropertyInfo createdAtField = objectType.GetProperty("CreatedAt");
                if (createdAtField != null)
                {
                    createdAtField.SetValue(entity, DateTime.Now, null);
                }
            }
            else 
            {
                PropertyInfo UpdatedByField = objectType.GetProperty("UpdatedBy");
                if (UpdatedByField != null)
                {
                    UpdatedByField.SetValue(entity, userId, null);
                }

                PropertyInfo updatedAtField = objectType.GetProperty("UpdatedAt");
                if (updatedAtField != null)
                {
                    updatedAtField.SetValue(entity, DateTime.Now, null);
                }
            }

            PropertyInfo localIpField = objectType.GetProperty("LocalIp");
            if (localIpField != null)
            { 
                localIpField.SetValue(entity, "192.168.0.1", null);
            }

            PropertyInfo macAddressField = objectType.GetProperty("MacAddress");
            if (macAddressField != null)
            {
                macAddressField.SetValue(entity, GetMACAddress(), null);
            }

            return entity;
        }

        private static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }
}
