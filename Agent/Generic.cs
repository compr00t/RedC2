using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Agent
{
    public sealed class GenericObjectResult : SharpSploitResult
    {
        public object Result { get; }
        protected internal override IList<SharpSploitResultProperty> ResultProperties
        {
            get
            {
                return new List<SharpSploitResultProperty>
                    {
                        new SharpSploitResultProperty
                        {
                            Name = this.Result.GetType().Name,
                            Value = this.Result
                        }
                    };
            }
        }

        public GenericObjectResult(object Result)
        {
            this.Result = Result;
        }
    }

    public class SharpSploitResultList<T> : IList<T> where T : SharpSploitResult
    {
        private List<T> Results { get; } = new List<T>();

        public int Count => Results.Count;
        public bool IsReadOnly => ((IList<T>)Results).IsReadOnly;


        private const int PROPERTY_SPACE = 3;

        public string FormatList()
        {
            return this.ToString();
        }

        private string FormatTable()
        {
            // TODO
            return "";
        }

        public override string ToString()
        {
            if (this.Results.Count > 0)
            {
                StringBuilder labels = new StringBuilder();
                StringBuilder underlines = new StringBuilder();
                List<StringBuilder> rows = new List<StringBuilder>();
                for (int i = 0; i < this.Results.Count; i++)
                {
                    rows.Add(new StringBuilder());
                }
                for (int i = 0; i < this.Results[0].ResultProperties.Count; i++)
                {
                    labels.Append(this.Results[0].ResultProperties[i].Name);
                    underlines.Append(new string('-', this.Results[0].ResultProperties[i].Name.Length));
                    int maxproplen = 0;
                    for (int j = 0; j < rows.Count; j++)
                    {
                        SharpSploitResultProperty property = this.Results[j].ResultProperties[i];
                        string ValueString = property.Value.ToString();
                        rows[j].Append(ValueString);
                        if (maxproplen < ValueString.Length)
                        {
                            maxproplen = ValueString.Length;
                        }
                    }
                    if (i != this.Results[0].ResultProperties.Count - 1)
                    {
                        labels.Append(new string(' ', Math.Max(2, maxproplen + 2 - this.Results[0].ResultProperties[i].Name.Length)));
                        underlines.Append(new string(' ', Math.Max(2, maxproplen + 2 - this.Results[0].ResultProperties[i].Name.Length)));
                        for (int j = 0; j < rows.Count; j++)
                        {
                            SharpSploitResultProperty property = this.Results[j].ResultProperties[i];
                            string ValueString = property.Value.ToString();
                            rows[j].Append(new string(' ', Math.Max(this.Results[0].ResultProperties[i].Name.Length - ValueString.Length + 2, maxproplen - ValueString.Length + 2)));
                        }
                    }
                }
                labels.AppendLine();
                labels.Append(underlines.ToString());
                foreach (StringBuilder row in rows)
                {
                    labels.AppendLine();
                    labels.Append(row.ToString());
                }
                return labels.ToString();
            }
            return "";
        }

        public T this[int index] { get => Results[index]; set => Results[index] = value; }

        public IEnumerator<T> GetEnumerator()
        {
            return Results.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Results.Cast<T>().GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return Results.IndexOf(item);
        }

        public void Add(T t)
        {
            Results.Add(t);
        }

        public void AddRange(IEnumerable<T> range)
        {
            Results.AddRange(range);
        }

        public void Insert(int index, T item)
        {
            Results.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Results.RemoveAt(index);
        }

        public void Clear()
        {
            Results.Clear();
        }

        public bool Contains(T item)
        {
            return Results.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Results.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Results.Remove(item);
        }
    }
    public abstract class SharpSploitResult
    {
        protected internal abstract IList<SharpSploitResultProperty> ResultProperties { get; }
    }

    public class SharpSploitResultProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}