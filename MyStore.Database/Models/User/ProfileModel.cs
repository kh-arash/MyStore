using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.User
{
    public class ProfileModel
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
