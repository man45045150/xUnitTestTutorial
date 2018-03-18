using System;
using System.Dynamic;

public class Null<T> : DynamicObject where T : class
{
    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result){
        result = Activator.CreateInstance(
            typeof(T).GetMethod(binder.ToString()).ReturnType
        );
        return true;
    }
}