using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Models.MasterData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color = DotNetCore06ProjectConfig.Data.Entity.MasterData.Color;
using Size = DotNetCore06ProjectConfig.Data.Entity.MasterData.Size;

namespace DotNetCore06ProjectConfig.Services.MasterData.Interfaces
{
    public interface IMasterDataService
    {
        #region Address Category
        Task<bool> SaveAddressCategory(AddressCategory addressCategory);
        Task<IEnumerable<AddressCategory>> GetAddressCategory();
        Task<AddressCategory> GetAddressCategoryById(int id);
        Task<bool> DeleteAddressCategoryById(int id);
        Task<int> GetAddressCategoryCheck(int id, string name);
        #endregion

        #region Address 
        Task<bool> SaveCountry(Country country);
        Task<IEnumerable<Country>> GetAllContry();
        Task<Country> GetContryById(int id);
        Task<bool> DeleteContryById(int id);

        Task<bool> SaveDivision(Division division);
        Task<IEnumerable<Division>> GetAllDivision();
        Task<Division> GetDivisionById(int id);
        Task<bool> DeleteDivisionById(int id);
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);

        Task<bool> SaveDistrict(District district);
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictById(int id);
        Task<bool> DeleteDistrictById(int id);
        Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId);

        Task<bool> SaveThana(Thana thana);
        Task<IEnumerable<Thana>> GetAllThana();
        Task<Thana> GetThanaById(int id);
        Task<bool> DeleteThanaById(int id);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId);

        Task<IEnumerable<Address>> GetAddressListByResourceId(int id);
        #endregion

        #region Color
        Task<bool> SaveColor(Color color);
        Task<IEnumerable<Color>> GetAllColor();
        Task<Color> GetColorById(int id);
        Task<bool> DeleteColorById(int id);
        #endregion

        #region Size
        Task<bool> SaveSize(Size size);
        Task<IEnumerable<Size>> GetAllSize();
        Task<Size> GetSizeById(int id);
        Task<bool> DeleteSizeById(int id);
        #endregion

        #region Brand
        Task<bool> SaveBrand(Brand brand);
        Task<IEnumerable<Brand>> GetAllBrand();
        Task<Brand> GetBrandById(int id);
        Task<bool> DeleteBrandById(int id);
        #endregion

        #region Gender
        Task<bool> SaveGender(Gender gender);
        Task<IEnumerable<Gender>> GetAllGender();
        Task<Gender> GetGenderById(int id);
        Task<bool> DeleteGenderById(int id);
        #endregion

        #region CompanyType
        Task<bool> SaveCompanyType(CompanyType type);
        Task<IEnumerable<CompanyType>> GetAllCompanyType();
        Task<CompanyType> GetCompanyTypeById(int id);
        Task<bool> DeleteCompanyTypeById(int id);
        #endregion

        #region Business Type
        Task<bool> SaveBusinessType(BusinessType type);
        Task<IEnumerable<BusinessType>> GetAllBusinessType();
        Task<BusinessType> GetBusinessTypeById(int id);
        Task<bool> DeleteBusinessTypeById(int id);
        #endregion

        #region Company
        Task<int> SaveCompany(Company company);
        Task<List<Company>> GetAllCompany();
        Task<Company> GetCompanyById(int id);
        Task<bool> DeleteCompanyById(int id);
        #endregion

        #region MyRegion
        #endregion
        #region MyRegion
        #endregion




    }
}
