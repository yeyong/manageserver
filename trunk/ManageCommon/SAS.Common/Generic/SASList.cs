using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SAS.Common.Generic
{
    /// <summary>
    /// 列表泛型类
    /// </summary>
    /// <typeparam name="T">占位符(下同)</typeparam>
    [Serializable]
    public class List<T> : System.Collections.Generic.List<T>, ISASCollection<T> //:Collection<T>  // where T : new()
    {

        public List() : base() { }

        public List(IEnumerable<T> collection) : base(collection) { }

        public List(int capacity) : base(capacity) { }


        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        private int _fixedsize = default(int);
        public int FixedSize
        {
            get
            {
                return _fixedsize;
            }
            set
            {
                _fixedsize = value;
            }
        }

        public bool IsFull
        {
            get
            {
                if ((FixedSize != default(int)) && (this.Count >= FixedSize))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Author
        {
            get
            {
                return "yeyong";
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public new void Add(T value)
        {
            if (!this.IsFull)
            {
                base.Add(value);
            }
        }

        public void Accept(ISASVisitor<T> visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("访问器为空");
            }

            //for (int i = 0; i < this.Count; i++)
            //{
            //    visitor.Visit(this[i]);

            //    if (visitor.HasCompleted)
            //    {
            //        break;
            //    }
            //}

            System.Collections.Generic.List<T>.Enumerator enumerator = this.GetEnumerator();

            while (enumerator.MoveNext())
            {
                visitor.Visit(enumerator.Current);

                if (visitor.HasDone)
                {
                    return;
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (obj.GetType() == this.GetType())
            {
                List<T> l = obj as List<T>;

                return this.Count.CompareTo(l.Count);
            }
            else
            {
                return this.GetType().FullName.CompareTo(obj.GetType().FullName);
            }
        }

    }
}
