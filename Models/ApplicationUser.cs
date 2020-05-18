using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShoesStoreApi.Models {
    public class ApplicationUser : IdentityUser {
        [Column (TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}