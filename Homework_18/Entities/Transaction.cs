using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_18.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Operation { get; set; }

        public int ClientRefId { get; set; }

        [ForeignKey(nameof(ClientRefId))]
        public Client Client { get; set; }
    }
}
