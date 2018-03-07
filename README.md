# LynxMapper
[![Travis](https://img.shields.io/travis/gromanev/LynxMapper.svg)](https://travis-ci.org/gromanev/LynxMapper)
[![NuGet](https://img.shields.io/nuget/v/LynxMapper.svg)](https://www.nuget.org/packages/LynxMapper/)
[![license](https://img.shields.io/github/license/gromanev/lynxmapper.svg)](https://github.com/gromanev/LynxMapper)
[![GitHub repo size in bytes](https://img.shields.io/github/repo-size/gromanev/lynxmapper.svg)](https://github.com/gromanev/LynxMapper)

Faster Mapper (Transformator) for .NET Core 2 (.NET Standart 2)

## Description

A simple library for comparing models (mapping) to .NET Core for use in real production.

Mapping is carried out with the help of special methods - transformers (transformators), which gives greater flexibility in use for solving real problems.

## Install

Install LynxMapper and it's dependencies using NuGet.

`Install-Package LynxMapper`

All versions can be found [here](https://www.nuget.org/packages/LynxMapper/)

## Use

Use LinxMapper is very simple.

### 1. Create Transformator:

#### 1.1 Create Transformator Abstractions:
```csharp
using LynxMapper;

namespace Services.Abstractions.Transformators
{
    public interface IUserTransformator: ILynxTransformator
    {
        UserViewModel ToUserViewModel(Users user);
    }
}
```

#### 1.2 Create Transformator Implementations:
```csharp
namespace Services.Implementations.Transformators
{
    public class UserTransformator : IUserTransformator
    {
        public UserViewModel ToUserViewModel(Users user)
        {
            try
            {
                return new UserViewModel
                {
                    Name = $"{user.LastName} {user.FirstName} {user.Patronimic}",
                    Age = (int) ((DateTime.Now - user.YearOfBirth).TotalDays / 365.2425)
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
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

        /// <summary>
        /// Get list all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(ICollection<UserViewModel>))]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                var result = users.Select(x => _mapper.Map<UserViewModel, Users>(x)).ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Users bad request.");
            }
        }
    }
}
```
