
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Address", Schema = "MasterData")]
    public class Address:Base
    {
        public int? addressCategoryId { get; set; }
        public AddressCategory? addressCategory { get; set; }

        public int? resourceId { get; set; }
        public Resource? resource { get; set; }

        //public int? organizationId { get; set; }
        //public Organization organization { get; set; }

        public int? countryId { get; set; }
        public Country? country { get; set; }

        public int? divisionId { get; set; }
        public Division? division { get; set; }

        public int? districtId { get; set; }
        public District? district { get; set; }

        public int? thanaId { get; set; }
        public Thana? thana { get; set; }

        public string? union { get; set; }

        public string? postOffice { get; set; }
                     
        public string? postCode { get; set; }
                     
        public string? blockSector { get; set; }
                     
        public string? houseVillage { get; set; }
        public int? companyId { get; set; }
      

        public string? type { get; set; }  //Organization or Resourse
      

    }
}
