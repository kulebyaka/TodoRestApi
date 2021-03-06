using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApp.Web.Configuration
{
	public static class SwaggerConfig
	{
		/// <summary>
		/// Add Swagger middleware
		/// </summary>
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(Constants.Swagger.Version, new OpenApiInfo {Title = Constants.Swagger.ApiName, Version = Constants.Swagger.Version});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				if (File.Exists(xmlPath))
				{
					c.IncludeXmlComments(xmlPath);
				}
			});

			return services;
		}
	}
}