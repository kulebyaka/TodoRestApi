using AutoMapper;
using TodoApp.Infrastructure.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.AutoMapperProfiles
{
	public class DefaultProfile : Profile
	{
		public DefaultProfile()
		{
			CreateMap<TodoDTO, TodoOverviewViewModel>();
			CreateMap<TodoDTO, TodoViewModel>();
		}
	}
}