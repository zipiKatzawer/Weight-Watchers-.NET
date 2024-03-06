using AutoMapper;
using WeightWatchers.core.Interface.DAL;
using WeightWatchers.core.Interface.Service;
using WeightWatchers.DAL;
using WeightWatchers.Services;

namespace WeightWatchers.Configurations
{
    public static class Configuration
    {
        public static void ConfigurationService(this IServiceCollection services)
        {
            services.AddScoped<IWeightWatchersService, WeightWatchersService>();
            services.AddScoped<IWeightWatchersRepository, WeightWatchersRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WeightWatchersProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

    }
}
