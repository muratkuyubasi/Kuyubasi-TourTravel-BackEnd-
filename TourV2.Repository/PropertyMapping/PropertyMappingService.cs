﻿using TourV2.Data;
using TourV2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TourV2.Repository
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _loginAuditMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } )},
                { "LoginTime", new PropertyMappingValue(new List<string>() { "LoginTime" } )},
                { "RemoteIP", new PropertyMappingValue(new List<string>() { "RemoteIP" } )},
                { "Status", new PropertyMappingValue(new List<string>() { "Status" } )},
                { "Provider", new PropertyMappingValue(new List<string>() { "Provider" } )}
            };

        private Dictionary<string, PropertyMappingValue> _userMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } )},
                { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                { "FirstName", new PropertyMappingValue(new List<string>() { "FirstName" } )},
                { "LastName", new PropertyMappingValue(new List<string>() { "LastName" } )},
                { "PhoneNumber", new PropertyMappingValue(new List<string>() { "PhoneNumber" } )},
                { "IsActive", new PropertyMappingValue(new List<string>() { "IsActive" } )}
            };

      

        private Dictionary<string, PropertyMappingValue> _nLogMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "MachineName", new PropertyMappingValue(new List<string>() { "MachineName" } )},
                { "Logged", new PropertyMappingValue(new List<string>() { "Logged" } )},
                { "Level", new PropertyMappingValue(new List<string>() { "Level" } )},
                { "Message", new PropertyMappingValue(new List<string>() { "Message" } )},
                { "Logger", new PropertyMappingValue(new List<string>() { "Logger" } )},
                { "Properties", new PropertyMappingValue(new List<string>() { "Properties" } )},
                { "Callsite", new PropertyMappingValue(new List<string>() { "Callsite" } )},
                { "Exception", new PropertyMappingValue(new List<string>() { "Exception" } )}
            };

        private IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();
        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<LoginAuditDto, LoginAudit>(_loginAuditMapping));
            propertyMappings.Add(new PropertyMapping<UserDto, User>(_userMapping));
        
            propertyMappings.Add(new PropertyMapping<NLogDto, NLog>(_nLogMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;

        }

    }
}
