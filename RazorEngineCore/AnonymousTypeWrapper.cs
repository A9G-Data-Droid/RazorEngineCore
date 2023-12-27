﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace RazorEngineCore
{
    public class AnonymousTypeWrapper : DynamicObject
    {
        private readonly object _model;

        public AnonymousTypeWrapper(object model)
        {
            this._model = model;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            PropertyInfo? propertyInfo = this._model.GetType().GetProperty(binder.Name);

            if (propertyInfo == null)
            {
                result = null;
                return false;
            }

            result = propertyInfo.GetValue(this._model, null);

            if (result == null)
            {
                return true;
            }

            //var type = result.GetType();

            if (result.IsAnonymous())
            {
                result = new AnonymousTypeWrapper(result);
            }

            if (result is IDictionary dictionary)
            {
                List<object> keys = dictionary.Keys.Cast<object>().ToList();

                foreach(object key in keys)
                {
                    if (dictionary[key]?.IsAnonymous() ?? false)
                    {
                        dictionary[key] = new AnonymousTypeWrapper(dictionary[key]!);
                    }
                }
            }
            else if (result is IEnumerable enumerable and not string)
            {
                result = enumerable.Cast<object>()
                        .Select(e => e.IsAnonymous() ? new AnonymousTypeWrapper(e) : e)
                        .ToList();
            }
            
            return true;
        }
    }
}