using AutoMapper;

namespace CalculationTools.Core
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToData>();
            });

            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
