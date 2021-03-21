using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace TodoApp.Web.Configuration
{
	public static class MiddlewareConfig
	{
		/// <summary>
		/// Use swagger UI and endpoint
		/// </summary>
		public static IApplicationBuilder UseSwaggerWithOptions(this IApplicationBuilder app)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger(c =>
			{
				c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
				{
					if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host")) return;

					var serverUrl = $"{httpRequest.Headers["X-Forwarded-Proto"]}://" +
					                $"{httpRequest.Headers["X-Forwarded-Host"]}" +
					                $"{httpRequest.Headers["X-Forwarded-Prefix"]}";

					swaggerDoc.Servers = new List<OpenApiServer>()
					{
						new OpenApiServer {Url = serverUrl}
					};
				});
			});

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c => { c.SwaggerEndpoint(Constants.Swagger.EndPoint, Constants.Swagger.ApiName); });

			return app;
		}
	}
}