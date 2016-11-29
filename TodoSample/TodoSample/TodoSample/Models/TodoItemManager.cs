using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PCLStorage;
using SQLite;

namespace TodoSample.Models
{
    public class TodoItemManager
    {
        public TodoItemManager()
        {
        }

        public async Task<SQLiteConnection> CreateConnection()
        {
            const string DatabaseFileName = "item.db3";
            // ルートフォルダを取得する
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            // DBファイルの存在チェックを行う
            var result = await rootFolder.CheckExistsAsync(DatabaseFileName);
            if (result == ExistenceCheckResult.NotFound)
            {
                // 存在しなかった場合、新たにDBファイルを作成しテーブルも併せて新規作成する
                IFile file = await rootFolder.CreateFileAsync(DatabaseFileName, CreationCollisionOption.ReplaceExisting);
                var connection = new SQLiteConnection(file.Path);
                connection.CreateTable<TodoItem>();
                return connection;
            }
            else
            {
                // 存在した場合、そのままコネクションを作成する
                IFile file = await rootFolder.CreateFileAsync(DatabaseFileName, CreationCollisionOption.OpenIfExists);
                return new SQLiteConnection(file.Path);
            }
        }

        //public async IEnumerable<TodoItem> GetItem()
        //{
        //    using (var connection = await CreateConnection()){
        //        return connection.Table<TodoItem>().Where(m => m.Id > 0);
        //    }

        //}

        public async Task<int> SaveItem(TodoItem item)
        {
            // コネクションを張り、TodoItemを新規作成(Id==0)でなければUpdateします。
            using (var connection = await CreateConnection())
            {
                if (item.Id != 0)
                {
                    connection.Update(item);
                    return item.Id;
                }
                return connection.Insert(item);
            }
        }

        public async Task<int> DeleteItem(int id)
        {
            // コネクションを張り、Idを指定してDeleteします。
            using (var connection = await CreateConnection())
            {
                return connection.Delete<TodoItem>(id);
            }
        }
    }
}
