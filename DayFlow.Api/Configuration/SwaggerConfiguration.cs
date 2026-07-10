using Microsoft.OpenApi;

namespace DayFlow.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// 註冊 Swagger 服務
        /// 
        /// Add
        /// <PropertyGroup>
        ///     <GenerateDocumentationFile>true</GenerateDocumentationFile>
        ///     <NoWarn>1591</NoWarn>
        /// </PropertyGroup>
        /// </summary>
        public static IServiceCollection AddSwaggerConfiguration(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DayFlow API",
                    Version = "v1",
                    Description = "DayFlow Modular Monolith"
                });

                //options.AddSecurityDefinition(
                //    "Bearer",
                //    new OpenApiSecurityScheme
                //    {
                //        Name = "Authorization",
                //        Type = SecuritySchemeType.Http,
                //        Scheme = "Bearer",
                //        BearerFormat = "JWT",
                //        In = ParameterLocation.Header,
                //        Description = "Bearer {token}"
                //    });

                //options.AddSecurityRequirement(
                //    new OpenApiSecurityRequirement
                //    {
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference =
                //                new OpenApiReference
                //                {
                //                    Id="Bearer",
                //                    Type=ReferenceType.SecurityScheme
                //                }
                //        },
                //        Array.Empty<string>()
                //    }
                //    });

                // XML 註解
                var xml = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, xml);

                if (File.Exists(path))
                    options.IncludeXmlComments(path);

            });
            return services;
        }

        /// <summary>
        /// 啟用 Swagger Middleware
        /// </summary>
        public static IApplicationBuilder UseSwaggerConfiguration(
            this IApplicationBuilder app,
            bool isDevelopment)
        {

            if (isDevelopment) //Development
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "DayFlow API V1");

                    // 將 Swagger 設為首頁
                    options.RoutePrefix = string.Empty;
                });
            }

            return app;

        }
    }
}
