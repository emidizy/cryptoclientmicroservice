using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Entities
{
    [Table("Clients")]
    public class Clients
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ClientId { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ClientName { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string WalletAddress { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CurrencyType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ProfileStatus { get; set; }
    }
}
