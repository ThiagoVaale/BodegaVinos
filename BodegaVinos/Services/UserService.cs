using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Repositories;
using BodegaVinos.Entities;

namespace BodegaVinos.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserEntity> GetUser()
        {
            return _userRepository.Users;
        }
        public List<UserEntity> createUser(CreateUserDto newUser)
        {
            UserEntity userCreate = new UserEntity()
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Password = newUser.Password
            };
            _userRepository.Users.Add(userCreate);
            return _userRepository.Users;
        }
    }
}
