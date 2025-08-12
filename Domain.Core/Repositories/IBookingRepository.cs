using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IBookingRepository : IGenericRepository<Booking, int>
{
}