using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;

namespace DotNetCore06ProjectConfig.Services.MasterData.Interfaces
{
    public interface IPaymentModeService
    {
        #region Payment Mode
        Task<int> SavePaymentMode(PaymentMode paymentMode);


        Task<IEnumerable<PaymentMode>> GetAllPaymentMode();


        Task<bool> DeletePaymentbyId(int id);


        #endregion
    }
}
