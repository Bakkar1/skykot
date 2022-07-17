using Microsoft.Extensions.DependencyInjection;
using SkyKotApp.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyKotApp.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISkyKotRepository, SkyKotPartialRepository>();
        }
    }
}
