using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Context;
using BodegaVinos.Entities;

namespace BodegaVinos.Data.Repositories
{
    public class UserRepository
    {
        private readonly WineDbContext _userContext;

        public UserRepository(WineDbContext wineDbContext)
        {
            _userContext = wineDbContext;
        }

        public List<UserEntity> GetUser()
        {
            return _userContext.Users.ToList();
        }

        public List<UserEntity> createUser(CreateUserDto newUser)
        {
            UserEntity userCreate = new UserEntity()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };
            _userContext.Users.Add(userCreate);
            _userContext.SaveChanges(); //SaveChanges() nos guarda los datos del nuevo usuario para luego retornar a esa lista
            return _userContext.Users.ToList();
        }

        public UserEntity AuthenticateUser(string username, string password)
        {
            UserEntity userAuthenticate = _userContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return userAuthenticate;
        }
    }
}
