namespace Redmanmale.ExpressionTreeCopy
{
    /// <summary>
    /// В нём метод обновления модели (бизнес-логика, вызывается из разных мест для обновления разных полей корневого объекта)
    /// </summary>
    public class DataUpdater
    {
        /// <summary>
        /// Копирование тупым способом
        /// </summary>
        public void Update0(Data data, string someString)
        {
            var otherData = data.SomeOtherData as OtherData;
            if (otherData == null)
            {
                otherData = new OtherData
                {
                    SomeOtherStringProp = data.SomeOtherData.SomeOtherStringProp,
                    SomeOtherIntProp = data.SomeOtherData.SomeOtherIntProp
                };
                data.SomeOtherData = otherData;
            }

            otherData.SomeOtherStringProp = someString;
        }

        /// <summary>
        /// Копирование через конструктор
        /// </summary>
        public void Update1(Data data, string someString)
        {
            var otherData = data.SomeOtherData as OtherData ?? new OtherData(data.SomeOtherData);
            data.SomeOtherData = otherData;

            otherData.SomeOtherStringProp = someString;
        }

        /// <summary>
        /// Копирование статическим методом в классе типа
        /// </summary>
        public void Update2(Data data, string someString)
        {
            var otherData = OtherData.Cast2(data.SomeOtherData);
            data.SomeOtherData = otherData;

            otherData.SomeOtherStringProp = someString;
        }

        /// <summary>
        /// Копирование через reflection
        /// </summary>
        public void Update3(Data data, string someString)
        {
            var otherData = data.SomeOtherData.Cast1<IOtherData, OtherData>();
            data.SomeOtherData = otherData;

            otherData.SomeOtherStringProp = someString;
        }

        /// <summary>
        /// Копирование через expression tree
        /// </summary>
        public void Update4(Data data, string someString)
        {
            var otherData = data.SomeOtherData.Cast2<IOtherData, OtherData>();
            data.SomeOtherData = otherData;

            otherData.SomeOtherStringProp = someString;
        }
    }
}
