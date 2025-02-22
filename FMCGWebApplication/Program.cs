using FMCGWebApplication.Data;
using FMCGWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // ✅ Keep user signed in for 30 days
    options.SlidingExpiration = true;

    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // For testing over HTTP 110225

    options.Cookie.SameSite = SameSiteMode.None; // Allow cross-site cookies//110225
    options.Cookie.HttpOnly = true;  // Prevent client-side JavaScript access
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthorization();


var app = builder.Build();

//Ensure database migrations are applied
////using (var scope = app.Services.CreateScope())
////{
////    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
////    dbContext.Database.Migrate(); // Apply pending migrations

////    //    // Call SeedData
////    //    await SeedData.InitializeAsync(scope.ServiceProvider);
////}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    //GlobalVariables.SelectedCompany = context.Request.Cookies["SelectedCompany"];
    if (short.TryParse(context.Request.Cookies["SelectedCompany"], out short selectedCompany))
    {
        GlobalVariables.SelectedCompany = selectedCompany;
    }
    else
    {
        GlobalVariables.SelectedCompany = 0; // Default value if parsing fails
    }

    GlobalVariables.SelectedCompanyName = context.Request.Cookies["SelectedCompanyName"] ?? string.Empty;
    await next();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();// ⚠️ Required for cookies to work
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");
    

app.Run();
// pattern: "{controller=Account}/{action=Login}/{id?}"); // 👈 Set Login as the first page