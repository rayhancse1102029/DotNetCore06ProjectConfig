using DotNetCore06ProjectConfig.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("PaymentMode", Schema = "MasterData")]
    public class PaymentMode : Base
    {
       public string? paymentModeName { get; set; }
    }
}
