using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.Assets
{
    [Table("Assets")]
    public class AssetsModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
    }
}
