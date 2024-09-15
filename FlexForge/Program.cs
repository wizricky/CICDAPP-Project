using FlexForge.Domain.Identity;
using FlexForge.Repository;
using FlexForge.Repository.Implementation;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;
using FlexForge.Service.Implementation;
using FlexForge.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using FlexForge.Domain.Domain;
using FlexForge.Services.Interface;
using FlexForge.Services.Implementation;
using QuestPDF.Infrastructure;
using Size = FlexForge.Domain.Domain.Size;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionStringCafeBar = builder.Configuration.GetConnectionString("CafeBarConnection") ?? throw new InvalidOperationException("Connection string 'CafeBarConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<CafeBarDBContext>(options =>
    options.UseSqlServer(connectionStringCafeBar));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<FlexForgeApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(ICafeBarProductRepository<>), typeof(CafeBarProductRepository<>));


builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IFavoriteProductsService, FavoriteProductsService>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IOrderService, OrderService>();


builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<FlexForgeApplicationUser>>();
    string email = "admin@gmail.com";
    string password = "Ristematej123!";
    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new FlexForgeApplicationUser();
        user.Email = email;
        user.UserName = "Admin";
        user.FirstName = "Admin";
        user.LastName = "Flexforge";
        user.Address = "Admin address 7";
        user.EmailConfirmed = true;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Sizes.Any())
    {
        context.Sizes.AddRange(
            new Size { SizeType = "XS" },
            new Size { SizeType = "S" },
            new Size { SizeType = "M" },
            new Size { SizeType = "L" },
            new Size { SizeType = "XL" },
            new Size { SizeType = "XXL" },
            new Size { SizeType = "6-8y" },
            new Size { SizeType = "8-10y" },
             new Size { SizeType = "10-12y" },
            new Size { SizeType = "12-14y" }
        );
        await context.SaveChangesAsync();
    }

}
app.Run();

