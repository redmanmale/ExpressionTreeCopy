namespace Redmanmale.ExpressionTreeCopy
{
    /// <summary>
    /// Неизменяемая модель
    /// </summary>
    public interface IOtherData
    {
        string SomeOtherStringProp { get; }

        int SomeOtherIntProp { get; }
    }

    public interface IAnotherData
    {
        string SomeOtherStringProp { get; }

        int SomeOtherIntProp { get; }
    }
}
