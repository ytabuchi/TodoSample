using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using TodoDemo.Models;

namespace TodoDemo.Views
{
    public partial class TodoPage : ContentPage
    {
        TodoItemManager manager = new TodoItemManager();
        ObservableCollection<TodoItem> Items = new ObservableCollection<TodoItem>();

        public TodoPage()
        {
            InitializeComponent();
            this.BindingContext = Items;

            this.search.TextChanged += async (sender, e) =>
            {
                using (var connection = await manager.CreateConnection())
                {
                    Items.Clear();
                    foreach (var item in (from x in connection.Table<TodoItem>() orderby x.Id select x))
                    {
                        if (item.Name.Contains(search.Text))
                            Items.Add(item);
                    }
                }

            };


            this.TodoList.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = e.SelectedItem as TodoItem;
                if (item == null)
                    return;

                await Navigation.PushAsync(new ItemPage(item));

                TodoList.SelectedItem = null;
            };

            this.addButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new ItemPage(null));
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using (var connection = await manager.CreateConnection())
            {
                Items.Clear();
                foreach (var item in (from x in connection.Table<TodoItem>() orderby x.Id select x))
                {
                    Items.Add(item);
                }
            }


        }


    }
}
