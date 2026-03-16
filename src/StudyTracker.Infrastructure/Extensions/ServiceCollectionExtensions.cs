using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Domain.Interfaces;
using StudyTracker.Infrastructure.Data;
using StudyTracker.Infrastructure.Data.Repositories;

namespace StudyTracker.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IStudySessionRepository, StudySessionRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();

        return services;
    }
}