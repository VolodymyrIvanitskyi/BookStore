using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Contractors
{
    public abstract class Field
    {
        public string Label { get; }

        public string Name { get; }

        public string Value { get; }

        protected Field(string label, string name, string value)
        {
            Label = label;
            Name = name;
            Value = value;
        }
    }

    public class HiddenField : Field
    {
        public HiddenField(string label, string name, string value)
            : base(label, name, value)
        {

        }
    }

    public class SelectionField : Field
    {
        public IReadOnlyDictionary<string,string> Items { get; } //При виборі наприклад міста доставки це пара значень, наприклад Місто і id яке відповідає йому
                                                          //Місто буде показуватися користувачу, а id буде приховане
        public SelectionField(string label, string name, string value, IReadOnlyDictionary<string,string> items)
            : base(label, name, value)
        {
            Items = items;
        }
    }

}
