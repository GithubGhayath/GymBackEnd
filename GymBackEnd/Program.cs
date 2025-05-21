using GymBackEnd.Data;
using GymBackEnd.Endpoints;
using GymBackEnd.Endpoints.EndpointsConfigurations;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString=builder.Configuration.GetConnectionString("GymDataBase");
builder.Services.AddSqlite<GymContext>(ConnectionString);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
var app = builder.Build();

app.BuildEndPoints();

app.MigrateDb();
app.UseAuthorization();
app.Run();
