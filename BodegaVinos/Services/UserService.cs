using BodegaVinos.Common.Dtos;
using BodegaVinos.Data.Context;
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
            return _userRepository.GetUser();
        }
        public List<UserEntity> createUser(CreateUserDto newUser)
        {
           return _userRepository.createUser(newUser);
        }

        //Metodo para corroborar credenciales.
        public UserEntity AuthenticateUser(string username, string password)
        {
           return _userRepository.AuthenticateUser(username, password);
        }
    }
}
