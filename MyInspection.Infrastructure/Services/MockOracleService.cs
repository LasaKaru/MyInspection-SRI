using MyInspection.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInspection.Application.Interfaces;

namespace MyInspection.Infrastructure.Services
{
    public class MockOracleService : IOracleService
    {
        public Task<PrefilledDataDto> GetPackingListDetailsAsync(string purchaseOrderNumber)
        {
            // Simulate a database lookup based on the PO number
            // In a real app, this would query the Oracle DB.
            if (purchaseOrderNumber == "25382020250411")
            {
                var data = new PrefilledDataDto
                {
                    SblOrderNumber = "E10223",
                    ProductDescription = "AIRWALK DENVER 26IN",
                    StyleNumber = "419-6941",
                    TotalQuantity = 100
                };
                return Task.FromResult(data);
            }

            // Return default/empty data if PO not found
            return Task.FromResult(new PrefilledDataDto());
        }
    }
}
