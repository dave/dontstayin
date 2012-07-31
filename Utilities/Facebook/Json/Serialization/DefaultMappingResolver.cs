﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Facebook.Json.Utilities;

namespace Facebook.Json.Serialization
{
  public class DefaultMappingResolver : IMappingResolver
  {
    internal static readonly IMappingResolver Instance = new DefaultMappingResolver();

    private readonly Dictionary<Type, JsonMemberMappingCollection> TypeMemberMappingsCache = new Dictionary<Type, JsonMemberMappingCollection>();

    public BindingFlags MemberSearchFlags { get; set; }

    public DefaultMappingResolver()
    {
      MemberSearchFlags = BindingFlags.Public | BindingFlags.Instance;
    }
    
    public virtual JsonMemberMappingCollection ResolveMappings(Type type)
    {
      JsonMemberMappingCollection memberMappings;

      if (TypeMemberMappingsCache.TryGetValue(type, out memberMappings))
        return memberMappings;

      // double check locking to avoid threading issues
      lock (TypeMemberMappingsCache)
      {
        if (TypeMemberMappingsCache.TryGetValue(type, out memberMappings))
          return memberMappings;

        memberMappings = CreateMemberMappings(type);
        TypeMemberMappingsCache[type] = memberMappings;

        return memberMappings;
      }
    }

    protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
    {
      return ReflectionUtils.GetFieldsAndProperties(objectType, MemberSearchFlags);
    }

    private JsonMemberMappingCollection CreateMemberMappings(Type objectType)
    {
      MemberSerialization memberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType);

      List<MemberInfo> members = GetSerializableMembers(objectType);
      if (members == null)
        throw new JsonSerializationException("Null collection of seralizable members returned.");

      JsonMemberMappingCollection memberMappings = new JsonMemberMappingCollection();

      foreach (MemberInfo member in members)
      {
        JsonMemberMapping memberMapping = CreateMemberMapping(memberSerialization, member);

        memberMappings.AddMapping(memberMapping);
      }

      return memberMappings;
    }

    protected virtual JsonMemberMapping CreateMemberMapping(MemberSerialization memberSerialization, MemberInfo member)
    {
      JsonPropertyAttribute propertyAttribute = ReflectionUtils.GetAttribute<JsonPropertyAttribute>(member, true);
      bool hasIgnoreAttribute = member.IsDefined(typeof(JsonIgnoreAttribute), true);

      string mappedName = (propertyAttribute != null && propertyAttribute.PropertyName != null)
                            ? propertyAttribute.PropertyName
                            : member.Name;

      string resolvedMappedName = ResolveMappingName(mappedName);

      bool required = (propertyAttribute != null) ? propertyAttribute.IsRequired : false;

      bool ignored = (hasIgnoreAttribute
                      || (memberSerialization == MemberSerialization.OptIn && propertyAttribute == null));

      bool readable = ReflectionUtils.CanReadMemberValue(member);
      bool writable = ReflectionUtils.CanSetMemberValue(member);

      JsonConverter memberConverter = JsonTypeReflector.GetConverter(member, ReflectionUtils.GetMemberUnderlyingType(member));

      DefaultValueAttribute defaultValueAttribute = ReflectionUtils.GetAttribute<DefaultValueAttribute>(member, true);
      object defaultValue = (defaultValueAttribute != null) ? defaultValueAttribute.Value : null;

      return new JsonMemberMapping(resolvedMappedName, member, ignored, readable, writable, memberConverter, defaultValue, required);
    }

    protected virtual string ResolveMappingName(string mappedName)
    {
      return mappedName;
    }
  }
}
