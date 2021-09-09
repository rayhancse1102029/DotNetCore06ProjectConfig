using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore06ProjectConfig.Models.MasterData;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Service.MasterData.Interfaces;

namespace DotNetCore06ProjectConfig.Service.MasterData
{
    public class SliderService : ISliderService
    {
        private readonly ApplicationDbContext _context;


        public SliderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveSlider(Slider slider)
        {
            if (slider.Id != 0)
            {
                _context.Sliders.Update(slider);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Slider>> GetAllSlider()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetSliderById(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        public async Task<bool> DeleteSliderById(int id)
        {
            _context.Sliders.Remove(_context.Sliders.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Slider>> GetAllActiveSlider()
        {
            return await _context.Sliders.Where(x=>x.isActive == true).ToListAsync();
        }

        public async Task<bool> IsActive(int id)
        {
           Slider slider = await  _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

           if (slider.isActive == true)
           {
               slider.isActive = false;
           }
           else
           {
               slider.isActive = true;
           }

           _context.Sliders.Update(slider);
           await _context.SaveChangesAsync();


           return true;
        }

    }
}
