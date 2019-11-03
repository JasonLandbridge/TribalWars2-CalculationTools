using CalculationTools.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalculationTools.Data
{
    public class CalculationToolsDataStore : ICalculationToolsDataStore
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CalculationToolsDataStore(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void UpdateVillages(List<IVillage> villages)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CalculationToolsDBContext>();

                db.AddRange(villages);
                db.SaveChanges();
            }


        }
    }
}
