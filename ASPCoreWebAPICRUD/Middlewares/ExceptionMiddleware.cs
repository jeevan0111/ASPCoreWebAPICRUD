using System;
using ASPCoreWebAPICRUD.Contracts;

namespace ASPCoreWebAPICRUD.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context )
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int) StatusCodes.Status500InternalServerError;
				
				var error = new Error {
					StatusCode = context.Response.StatusCode.ToString(),
					Message= ex.Message.ToString()
				};

					
				await context.Response.WriteAsync(error.ToString());
			}
		}
	}
}

