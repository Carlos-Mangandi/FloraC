using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// queda registrada la sesion iniciada del usuario
// Configuracion de la autenticación de usuarios | Constructor = builder | en desarrollo web  cookie = nos manda archivos cuando hacemos una peticion al servidor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Usuario/Login");
    options.ExpireTimeSpan = TimeSpan.FromHours(8); // <--- tiempo que va durar el inicio de sesion del usuario
    options.SlidingExpiration = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


//habilitar autenticacion en la aplicacion web 
app.UseAuthentication();  //Colocar en el proyecto


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
