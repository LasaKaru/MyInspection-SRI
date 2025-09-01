using MyInspection.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Application.Interfaces
{
    public interface IOracleService
    {
        Task<PrefilledDataDto> GetPackingListDetailsAsync(string purchaseOrderNumber);
    }
}
