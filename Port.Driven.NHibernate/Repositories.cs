using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate;

public interface IBookingRepository : IGenericRepository<Booking, int>, IPagingAndSortingRepository<Booking, int>;

public interface IHotelRepository : IGenericRepository<Hotel, int>, IPagingAndSortingRepository<Hotel, int>;

public interface IPhotoRepository : IGenericRepository<Photo, int>, IPagingAndSortingRepository<Photo, int>;

public interface IReviewRepository : IGenericRepository<Review, int>, IPagingAndSortingRepository<Review, int>
{
    Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId);
}

public interface IRoomRepository : IGenericRepository<Room, int>, IPagingAndSortingRepository<Room, int>
{
    Task<IEnumerable<Room>> FindRoomsByHotelIdAsync(int hotelId);
}

public interface IServiceRepository : IGenericRepository<Service, int>, IPagingAndSortingRepository<Service, int>;

public interface IUserInfoRepository : IGenericRepository<UserInfo, int>, IPagingAndSortingRepository<UserInfo, int>
{
    Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
