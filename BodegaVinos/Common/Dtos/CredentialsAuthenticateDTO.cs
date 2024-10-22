namespace BodegaVinos.Common.Dtos
{
    public class CredentialsAuthenticateDTO
    {
        // Nombre de usuario, requerido y único
        public string Username { get; set; } = string.Empty;

        // Contraseña, al menos 8 caracteres
        public string Password { get; set; }
    }
}
