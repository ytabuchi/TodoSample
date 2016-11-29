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

            // TodoPageから受け取ったitemか、Dateのみを今日に指定した_itemを利用。
            if (item != null)
                _item = item;
            else
                _item = new TodoItem()
                {
                    Date = DateTime.Now
                };

            // _itemをバインディング対象に。
            this.BindingContext = _item;

            // ItemPageの情報を格納してTodoItemManager.SaveItemを実行。
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

            // Idを指定してTodoItemManager.DeleteItemを実行。
            DeleteButton.Clicked += async (sender, e) =>
            {
                await manager.DeleteItem(_item.Id);
                await Navigation.PopAsync();
            };
        }
    }
}
