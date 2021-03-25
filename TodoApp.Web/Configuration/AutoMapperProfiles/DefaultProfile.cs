using AutoMapper;
using TodoApp.Infrastructure.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Configuration.AutoMapperProfiles
{
	public class DefaultProfile : Profile
	{
		public DefaultProfile()
		{
			CreateMap<TodoDTO, TodoViewModel>();
			// .ForMember(a=>a.Completed,
			// src => src.ConvertUsing<BoolStateResolver, State>(vm=>vm.State));

			CreateMap<TodoViewModel, TodoDTO>();
		}

		private class StateBoolResolver : IValueConverter<bool, State>
		{
			public State Convert(bool sourceMember, ResolutionContext context)
			{
				return sourceMember ? State.Completed : State.NotStarted;
			}
		}

		private class BoolStateResolver : IValueConverter<State, bool>
		{
			public bool Convert(State sourceMember, ResolutionContext context)
			{
				return sourceMember == State.Completed;
			}
		}
	}
}