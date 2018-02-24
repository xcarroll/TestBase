﻿using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestBase
{
    class DeSerializeExcludingFieldsContractResolver : DefaultContractResolver
    {
        readonly Type type;
        readonly Func<JsonProperty,bool> ignoreProperty;

        public DeSerializeExcludingFieldsContractResolver(Type type, Func<JsonProperty,bool> ignoreProperty)
        {
            this.type = type;
            this.ignoreProperty = ignoreProperty;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.Ignored = property.DeclaringType == type && ignoreProperty(property);
            return property;
        }
    }
}
