using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Services.MasterData.Interfaces;

namespace DotNetCore06ProjectConfig.Services.MasterData
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly ApplicationDbContext _context;

        public PaymentModeService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Payment Mode
        public async Task<int> SavePaymentMode(PaymentMode paymentMode)
        {
            if (paymentMode.Id != 0)
                _context.PaymentModes.Update(paymentMode);
            else
                _context.PaymentModes.Add(paymentMode);
            await _context.SaveChangesAsync();
            return paymentMode.Id;
        }

        public async Task<IEnumerable<PaymentMode>> GetAllPaymentMode()
        {

            List<PaymentMode> paymentModes = await _context.PaymentModes.AsNoTracking().ToListAsync();

            return paymentModes;
        }

        public async Task<bool> DeletePaymentbyId(int id)
        {
            _context.PaymentModes.Remove(_context.PaymentModes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
