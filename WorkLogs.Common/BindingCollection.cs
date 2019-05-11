using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WorkLogs.Common
{
    /// </summary>
    public class BindingCollection<T> : BindingList<T>
    {
        private bool _isSorted;
        private PropertyDescriptor _sortProperty;
        private ListSortDirection _sortDirection;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BindingCollection()
            : base()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list">IList类型的列表对象</param>
        public BindingCollection(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// 自定义排序操作
        /// </summary>
        /// <param name="property"></param>
        /// <param name="direction"></param>
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                ObjectPropertyCompare<T> pc = new ObjectPropertyCompare<T>(property, direction);
                items.Sort(pc);
                _isSorted = true;
            }
            else
            {
                _isSorted = false;
            }

            _sortProperty = property;
            _sortDirection = direction;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// 获取一个值，指示列表是否已排序
        /// </summary>
        protected override bool IsSortedCore
        {
            get
            {
                return _isSorted;
            }
        }

        /// <summary>
        /// 获取一个值，指示列表是否支持排序
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 获取一个只，指定类别排序方向
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return _sortDirection;
            }
        }

        /// <summary>
        /// 获取排序属性说明符
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return _sortProperty;
            }
        }

        /// <summary>
        /// 移除默认实现的排序
        /// </summary>
        protected override void RemoveSortCore()
        {
            _isSorted = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    public class ObjectPropertyCompare<T> : IComparer<T>
    {
        private PropertyDescriptor _property;
        private ListSortDirection _direction;

        // 构造函数
        public ObjectPropertyCompare(PropertyDescriptor property, ListSortDirection direction)
        {
            this._property = property;
            this._direction = direction;
        }

        // 实现IComparer中方法
        public int Compare(T x, T y)
        {
            object xValue = x.GetType().GetProperty(_property.Name).GetValue(x, null);
            object yValue = y.GetType().GetProperty(_property.Name).GetValue(y, null);

            int returnValue;

            if (xValue is IComparable)
            {
                returnValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (xValue.Equals(yValue))
            {
                returnValue = 0;
            }
            else
            {
                returnValue = xValue.ToString().CompareTo(yValue.ToString());
            }

            if (_direction == ListSortDirection.Ascending)
            {
                return returnValue;
            }
            else
            {
                return returnValue * -1;
            }
        }
    }
}
