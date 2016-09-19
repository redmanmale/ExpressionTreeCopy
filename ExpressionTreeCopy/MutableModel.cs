namespace Redmanmale.ExpressionTreeCopy
{ 
    // Корневой объект (изменяемый)
    public class Data
    {
        public IOtherData SomeOtherData { get; set; }

        public IAnotherData SomeAnotherData { get; set; }
    }

    // Изменяемая модель
    public class OtherData : IOtherData
    {
        public string SomeOtherStringProp { get; set; }

        public int SomeOtherIntProp { get; set; }

        public OtherData() {}

        public OtherData(IOtherData iData)
        {
            SomeOtherStringProp = iData.SomeOtherStringProp;
            SomeOtherIntProp = iData.SomeOtherIntProp;
        }

        public static OtherData Cast1(IOtherData iOtherData)
        {
            var otherData = iOtherData as OtherData;
            if (otherData == null)
            {
                otherData = new OtherData
                {
                    SomeOtherStringProp = iOtherData.SomeOtherStringProp,
                    SomeOtherIntProp = iOtherData.SomeOtherIntProp
                };
            }
            return otherData;
        }

        public static OtherData Cast2(IOtherData iOtherData)
        {
            return iOtherData as OtherData ?? new OtherData(iOtherData);
        }
    }
}
