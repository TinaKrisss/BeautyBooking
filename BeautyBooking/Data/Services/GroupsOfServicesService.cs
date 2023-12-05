using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyBooking.Data.Services
{
    public class GroupsOfServicesService : EntityBaseRepository<GroupOfServices>, IGroupsOfServicesService
	{
		private readonly AppDbContext _context;

		public GroupsOfServicesService(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
