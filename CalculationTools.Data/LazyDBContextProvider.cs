using System;

namespace CalculationTools.Data
{
    public class LazyDBContextProvider : ICalculationToolsDBContext
    {
        private readonly Lazy<CalculationToolsDBContext> context;
        public LazyDBContextProvider(Func<CalculationToolsDBContext> factory)
        {
            this.context = new Lazy<CalculationToolsDBContext>(factory);
        }

        public CalculationToolsDBContext Context => this.context.Value;
    }
}
