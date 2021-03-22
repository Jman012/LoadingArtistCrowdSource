using System.Linq;
using System.Text.Json.Serialization;

using LoadingArtistCrowdSource.Server.Data;
using LoadingArtistCrowdSource.Server.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace LoadingArtistCrowdSource.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			/*
			 * Default Skeleton
			 */
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
				{
					options.IdentityResources["openid"].UserClaims.Add("role");
					options.ApiResources.Single().UserClaims.Add("role");
				});

			System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
				.DefaultInboundClaimTypeMap.Remove("role");


			services.AddAuthentication()
				.AddIdentityServerJwt()
				// Custom: Add Discord OAuth
				.AddDiscord(options =>
				{
					options.ClientId = Configuration.GetValue<string>("Discord:ClientId");
					options.ClientSecret = Configuration.GetValue<string>("Discord:ClientSecret");
				});

			services.AddControllersWithViews()
				.AddJsonOptions(options => {
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});
			services.AddRazorPages();

			/*
			 * Custom
			 */

			// Add IEmailSender for ASP.NET Core Identity services
			services.AddTransient<IEmailSender, Services.MailJetEmailSender>();

			services.Configure<IdentityOptions>(options =>
			{
				// Required in order to have UserManager<T>.GetUserId() to work.
				options.ClaimsIdentity.UserIdClaimType = System.Security.Claims.ClaimTypes.NameIdentifier;
			});

			services.AddScoped<Services.IRazorPartialToStringRenderer, Services.RazorPartialToStringRenderer>();
			services.AddScoped<Services.HistoryLogger>();
			services.AddScoped<Services.TagRepository>();

			if (Configuration.GetValue<string>("LACS:DistributedCache") == "Redis")
			{
				services.AddDistributedRedisCache(option => 
				{
					option.Configuration = Configuration.GetValue<string>("Redis:Connection");
				});
			}
			else
			{
				services.AddDistributedMemoryCache();
			}
			services.AddSingleton(typeof(Services.JsonDistributedCache<>));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			Services.IdentitySeed.SeedData(userManager, roleManager);
			Services.ServerConfig.AssertConfigAvailable(Configuration);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
