using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Reflection;

public static class DynamicObjectValidator
{
    public static bool Validate<T>(dynamic dynamicObject, T typedObject)
    {
        if (dynamicObject == null && typedObject == null)
        {
            return true;
        }
        else if ((dynamicObject == null && typedObject != null) || (dynamicObject != null && typedObject == null))
        {
            return false;
        }
        else
        {
            if (dynamicObject is IDynamicMetaObjectProvider)
            {
                bool passed = true;
                //IEnumerable<string> members = dynamicObject.GetDynamicMemberNames();
                //foreach (var prop in members)
                foreach (KeyValuePair<string, object> kvp in dynamicObject)
                {
                    string prop = kvp.Key;
                    var typedType = typedObject.GetType();
                    var typedProp = typedObject.GetType().GetField(prop);
                    Type nestedType = typedProp.GetValue(typedObject).GetType();
                    MethodInfo method = typeof(DynamicObjectValidator).GetMethod("Validate").MakeGenericMethod(new Type[] {nestedType});
                    var typedValue = Convert.ChangeType(typedProp.GetValue(typedObject), nestedType);
                    var dynamicValue = kvp.Value;
                    if (typedProp != null)
                    {
                        passed= (bool) method.Invoke(null, new object[] {dynamicValue, typedValue});
                        if (!passed)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return (dynamicObject == typedObject);
            }
        }
        return true;
    }
}
