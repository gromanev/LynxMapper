    }
}
```

### 2. Register Transformators in Startup.cs:
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IUserTransformator, UserTransformator>();
        
        var build = services.BuildServiceProvider();
        
        services.AddLynxMapper(options =>
        {
            options.RegisterTransformator<UserViewModel, Users>(build.GetService<IUserTransformator>().ToUserViewModel);
        });
    }
}
```

#### OR

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddLynxMapperTransformators(o =>
            {
                o.Reg<IUserTransformator, UserTransformator>();
            })
            .AddLynxMapper(options =>
            {
                options.RegisterFor<UserViewModel, Users>(options.GetTransformator<IUserTransformator>().ToUserViewModel);
            });
    }
}
```

### 3. Use it!
```csharp
using LynxMapper;

namespace SimpleWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/users/[action]")]
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private readonly ILynxMapper _mapper;

        public TripController(ILynxServiceProvider LynxServiceProvider, ILynxMapper mapper)
        {
            _userService = LynxServiceProvider.UserService;
            _mapper = mapper;
        }
