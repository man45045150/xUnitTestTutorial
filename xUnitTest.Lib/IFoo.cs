public interface IFoo
{
    bool DoSomething(string value);
    string ProcessString(string value);
    bool TryParse(string value,out string outputValue);
    bool GetCount();
    bool Add(int amount);

    string Name {get;set;}
    IBaz SomeBaz {get;}
    int SomeOtherProperty{get;set;}
}