using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using TodoSample.Models;

namespace TodoSample.Views
{
    public partial class ItemPage : ContentPage
    {
        TodoItemManager manager = new TodoItemManager();
        TodoItem _item;

        public ItemPage(TodoItem item)
        {
            InitializeComponent();

            if (item != null)
                _item = item;
            else
                _item = new TodoItem()
                {
                    Date = DateTime.Now
                };

            this.BindingContext = _item;

            SaveButton.Clicked += async (sender, e) =>
            {
                var tmpItem = new TodoItem
                {
                    Id = _item.Id,
                    Name = _item.Name,
                    Notes = _item.Notes,
                    Date = _item.Date,
                    Done = _item.Done
                };
                await manager.SaveItem(tmpItem);
                await Navigation.PopAsync();
            };

            DeleteButton.Clicked += async (sender, e) =>
            {
                await manager.DeleteItem(_item.Id);
                await Navigation.PopAsync();
            };
        }
    }
}
