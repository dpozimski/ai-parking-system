using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ParkingSystem.UI.Utils
{
    public class AutoScrollToLastItemBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
                collection.CollectionChanged += collection_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
                collection.CollectionChanged -= collection_CollectionChanged;
        }

        private void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScrollToLastItem();
            }
        }

        private void ScrollToLastItem()
        {
            int count = AssociatedObject.Items.Count;
            if (count > 0)
            {
                try
                {
                    var last = AssociatedObject.Items[count - 1];
                    AssociatedObject.ScrollIntoView(last);
                }
                catch(InvalidOperationException) { /* ignored */ }
            }
        }
    }
}
