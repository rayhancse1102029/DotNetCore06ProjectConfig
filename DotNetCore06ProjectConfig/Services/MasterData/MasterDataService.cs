using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Models.MasterData;
using DotNetCore06ProjectConfig.Services.MasterData.Interfaces;

namespace DotNetCore06ProjectConfig.Services.MasterData
{
    public class MasterDataService : IMasterDataService
    {
        private readonly ApplicationDbContext _context;

        public MasterDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Address Category
        public async Task<IEnumerable<AddressCategory>> GetAddressCategory()
        {
            return await _context.addressCategories.AsNoTracking().ToListAsync();
        }
        public async Task<AddressCategory> GetAddressCategoryById(int id)
        {
            return await _context.addressCategories.FindAsync(id);
        }
        public async Task<bool> SaveAddressCategory(AddressCategory addressCategory)
        {
            if (addressCategory.Id != 0)
                _context.addressCategories.Update(addressCategory);
            else
                _context.addressCategories.Add(addressCategory);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAddressCategoryById(int id)
        {
            _context.addressCategories.Remove(_context.addressCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }  
        public async Task<int> GetAddressCategoryCheck(int id, string name)
        {
            var Result = await _context.addressCategories.Where(x => x.name == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Country
        public async Task<IEnumerable<Country>> GetAllContry()
        {
            return await _context.Countries.AsNoTracking().ToListAsync();
        }
        public async Task<Country> GetContryById(int id)
        {
            return await _context.Countries.FindAsync(id);
        }
        public async Task<bool> SaveCountry(Country country)
        {
            if (country.Id != 0)
                _context.Countries.Update(country);
            else
                _context.Countries.Add(country);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteContryById(int id)
        {
            _context.Countries.Remove(_context.Countries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Division
        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            return await _context.Divisions.AsNoTracking().ToListAsync();
        }
        public async Task<Division> GetDivisionById(int id)
        {
            return await _context.Divisions.FindAsync(id);
        }
        public async Task<bool> SaveDivision(Division division)
        {
            if (division.Id != 0)
                _context.Divisions.Update(division);
            else
                _context.Divisions.Add(division);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDivisionById(int id)
        {
            _context.Divisions.Remove(_context.Divisions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            return await _context.Divisions.Where(X => X.countryId == CntId).ToListAsync();
        }
        #endregion

        #region District
        public async Task<IEnumerable<District>> GetAllDistrict()
        {
            return await _context.Districts.Include(x => x.division).OrderBy(a => a.districtName).AsNoTracking().ToListAsync();
        }
        public async Task<District> GetDistrictById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }
        public async Task<bool> SaveDistrict(District district)
        {
            if (district.Id != 0)
                _context.Districts.Update(district);
            else
                _context.Districts.Add(district);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDistrictById(int id)
        {
            _context.Districts.Remove(_context.Districts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        {
            return await _context.Districts.Where(X => X.divisionId == DivisionId).ToListAsync();
        }
        #endregion

        #region Thana
        public async Task<IEnumerable<Thana>> GetAllThana()
        {
            return await _context.Thanas.Include(x => x.district.division.country).Include(x => x.district.division).Include(x => x.district).AsNoTracking().ToListAsync();
        }
        public async Task<Thana> GetThanaById(int id)
        {
            return await _context.Thanas.FindAsync(id);
        }
        public async Task<bool> SaveThana(Thana thana)
        {
            if (thana.Id != 0)
                _context.Thanas.Update(thana);
            else
                _context.Thanas.Add(thana);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteThanaById(int id)
        {
            _context.Thanas.Remove(_context.Thanas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId)
        {
            return await _context.Thanas.Where(x => x.districtId == DistrictId).ToListAsync();
        }
        public async Task<IEnumerable<Address>> GetAddressListByResourceId(int id)
        {
            return await _context.Addresses.Where(x => x.resourceId == id)
                .Include(x => x.division)
                .Include(x => x.district)
                .Include(x => x.thana)
                .Include(x => x.addressCategory)
                .AsNoTracking().ToListAsync();
        }

        #endregion

        #region Color
        public async Task<bool> SaveColor(Color color)
        {
            if (color.Id != 0)
            {
                _context.Colors.Update(color);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.Colors.AddAsync(color);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Color>> GetAllColor()
        {

            return await _context.Colors.ToListAsync();
        }
        public async Task<Color> GetColorById(int id)
        {
            return await _context.Colors.FindAsync(id);
        }
        public async Task<bool> DeleteColorById(int id)
        {
            _context.Colors.Remove(_context.Colors.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region Size
        public async Task<bool> SaveSize(Size size)
        {
            if (size.Id != 0)
            {
                _context.Sizes.Update(size);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.Sizes.AddAsync(size);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Size>> GetAllSize()
        {

            return await _context.Sizes.ToListAsync();
        }
        public async Task<Size> GetSizeById(int id)
        {
            return await _context.Sizes.FindAsync(id);
        }
        public async Task<bool> DeleteSizeById(int id)
        {
            _context.Sizes.Remove(_context.Sizes.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region Brand
        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            return await _context.brands.AsNoTracking().ToListAsync();
        }

        public async Task<Brand> GetBrandById(int id)
        {
            return await _context.brands.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> SaveBrand(Brand brand)
        {
            try
            {
                if (brand.Id != 0)
                {
                    _context.brands.Update(brand);
                }
                else
                {
                    _context.brands.Add(brand);
                }
                return 1 == await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> DeleteBrandById(int id)
        {
            _context.brands.Remove(_context.brands.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Gender
        public async Task<bool> SaveGender(Gender gender)
        {
            if (gender.Id != 0)
            {
                _context.Genders.Update(gender);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.Genders.AddAsync(gender);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Gender>> GetAllGender()
        {

            return await _context.Genders.ToListAsync();
        }
        public async Task<Gender> GetGenderById(int id)
        {
            return await _context.Genders.FindAsync(id);
        }
        public async Task<bool> DeleteGenderById(int id)
        {
            _context.Genders.Remove(_context.Genders.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region CompanyType
        public async Task<bool> SaveCompanyType(CompanyType data)
        {
            if (data.Id != 0)
            {
                _context.CompanyTypes.Update(data);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.CompanyTypes.AddAsync(data);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<CompanyType>> GetAllCompanyType()
        {
            return await _context.CompanyTypes.OrderBy(x=>x.ShortOrder).ToListAsync();
        }
        public async Task<CompanyType> GetCompanyTypeById(int id)
        {
            return await _context.CompanyTypes.FindAsync(id);
        }
        public async Task<bool> DeleteCompanyTypeById(int id)
        {
            _context.CompanyTypes.Remove(_context.CompanyTypes.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region BusinessType
        public async Task<bool> SaveBusinessType(BusinessType data)
        {
            if (data.Id != 0)
            {
                _context.BusinessTypes.Update(data);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.BusinessTypes.AddAsync(data);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<BusinessType>> GetAllBusinessType()
        {
            return await _context.BusinessTypes.OrderBy(x => x.ShortOrder).ToListAsync();
        }
        public async Task<BusinessType> GetBusinessTypeById(int id)
        {
            return await _context.BusinessTypes.FindAsync(id);
        }
        public async Task<bool> DeleteBusinessTypeById(int id)
        {
            _context.BusinessTypes.Remove(_context.BusinessTypes.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region Company
        public async Task<int> SaveCompany(Company data)
        {
            if (data.Id != 0)
            {
                _context.Companys.Update(data);
                await _context.SaveChangesAsync();
                return data.Id;
            }
            else
            {
                await _context.Companys.AddAsync(data);
                await _context.SaveChangesAsync();
                return data.Id;
            }
        }
        public async Task<List<Company>> GetAllCompany()
        {
            List<Company> data = await (from c in _context.Companys
                              join t in _context.CompanyTypes on c.CompanyTypeId equals t.Id
                              join b in _context.BusinessTypes on c.BusinessTypeId equals b.Id
                              select new Company
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  Email = c.Email,
                                  Address = c.Address,
                                  Web = c.Web,
                                  Logo = c.Logo,
                                  ContactPerson = c.ContactPerson,
                                  ContactPersonNumber = c.ContactPersonNumber,
                                  ContactPersonEmail = c.ContactPersonEmail,
                                  ContactPersonDesignation = c.ContactPersonDesignation,
                                  IncorporateNumber = c.IncorporateNumber,
                                  Tin = c.Tin,
                                  Bin = c.Bin,
                                  CompanyTypeId = c.CompanyTypeId,
                                  CompanyType = t,
                                  BusinessTypeId = c.BusinessTypeId,
                                  BusinessType = b,
                                  CountUser = _context.Users.Where(x => x.CompanyId == c.Id).Count(),
                                  CreatedAt = c.CreatedAt,
                                  CreatedBy = c.CreatedBy,
                                  UpdatedAt = c.UpdatedAt,
                                  UpdatedBy = c.UpdatedBy
                              }).OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();

            return data;
        }
        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.Companys.FindAsync(id);
        }
        public async Task<bool> DeleteCompanyById(int id)
        {
            _context.Companys.Remove(_context.Companys.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
