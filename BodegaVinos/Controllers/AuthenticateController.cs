using BodegaVinos.Common.Dtos;
using BodegaVinos.Entities;
using BodegaVinos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BodegaVinos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;
        public AuthenticateController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] CredentialsAuthenticateDTO credentialDto)
        {
            //Validamos las credenciales recibidad por el DTO de la request utilizando el metodo creado en UserService,
            //que se contacta con el repositorio y comprueba credenciales.
            UserEntity userAuthenticate = _userService.AuthenticateUser(credentialDto.Username, credentialDto.Password);
            if(userAuthenticate is not null)
            {
                //Estas dos primeras lineas de la generacion del token, es el: SIGNATURE.
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json;

                //Donde agarra el header + payload + secretkey(se encuentra en el appsettingJSON) y a todo ESTO lo HASHEA.
                SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                //Los claims son datos en clave->valor que nos permite guardar data del usuario.
                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticate.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
                claimsForToken.Add(new Claim("given_name", userAuthenticate.Username)); //Lo mismo para given_name y family_name, son las convenciones para nombre y apellido. Ustedes pueden usar lo que quieran, pero si alguien que no conoce la app

                var jwtSecurityToken = new JwtSecurityToken(//Acá es donde se crea el token con toda la data que le pasamos antes.
                  _config["Authentication:Issuer"], //ISSUER Y AUDIENCE valores que estan en el appsettingJSON.
                  _config["Authentication:Audience"],
                  claimsForToken, //Objeto que definimos arriba, llamdas CLAIMS(CLAVE : VALOR)
                  DateTime.UtcNow, //Fecha de creacion del token
                  DateTime.UtcNow.AddHours(1), //Fecha de expiracion del token
                  signature); //La firma del token

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); //Aca se encuentra el token
                return Ok(tokenToReturn);
            }
            return Unauthorized();
        } 
    }
}
