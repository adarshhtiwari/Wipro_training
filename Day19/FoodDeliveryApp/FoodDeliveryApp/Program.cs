var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// US-101: Default route -> /Restaurant/Menu
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// US-102: Custom route for food ordering -> /order-food
app.MapControllerRoute(
    name: "foodOrder",
    pattern: "order-food",
    defaults: new { controller = "Restaurant", action = "Menu" });

app.Run();