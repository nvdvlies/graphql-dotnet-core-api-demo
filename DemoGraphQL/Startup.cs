using DemoGraphQL.Schema;
using DemoGraphQL.Events;
using DemoGraphQL.Stores;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;

namespace DemoGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMovieStore, MovieStore>();
            services.AddSingleton<IDirectorStore, DirectorStore>();
            services.AddSingleton(typeof(IEventStream<>), typeof(EventStream<>));

            services.AddSingleton<DirectorType>();
            services.AddSingleton<MovieType>();
            services.AddSingleton<MoviesQuery>();
            services.AddSingleton<ISchema, MoviesSchema>();

            services.AddSingleton<MovieCreateInputType>();
            services.AddSingleton<MoviesMutation>();

            services.AddSingleton<MovieCreatedEventType>();
            services.AddSingleton<MoviesSubscription>();

            services.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(type => c.GetRequiredService(type)));

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
            .AddWebSockets()
            .AddDataLoader();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWebSockets();

            app.UseGraphQLWebSockets<ISchema>();
            app.UseGraphQL<ISchema>();

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
