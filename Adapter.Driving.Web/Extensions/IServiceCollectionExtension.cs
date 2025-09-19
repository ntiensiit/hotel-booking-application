using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driven.NHibernate.Persistence;
using Adapter.Driven.NHibernate.Repositories;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Persistence;
using Shared.Settings;

namespace Adapter.Driving.Web.Extensions;

internal static class IServiceCollectionExtension
{
    public static IServiceCollection AddNHibernate(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Get connection string or throw
        var connectionStrings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        ArgumentException.ThrowIfNullOrEmpty(connectionStrings?.ResourceServerDBConnection);

        NHibernateHelper.SetConnectionString(connectionStrings.ResourceServerDBConnection);

        // Register NHibernate session
        services.AddScoped(_ => NHibernateHelper.OpenSession());

        // Register NHibernate repositories
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // Register NHibernate unit of work
        services.AddScoped(typeof(IUnitOfWork), typeof(NHibernateUnitOfWork));

        return services;
    }
}
