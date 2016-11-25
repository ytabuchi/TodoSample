using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDemo.Models
{
    [Table("TodoItems")]
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; } // 主キー
        public string Name { get; set; } // 名前
        public string Notes { get; set; } // メモ
        public DateTime Date { get; set; } // 日時
        public bool Done { get; set; } // Doneフラグ
    }
}
