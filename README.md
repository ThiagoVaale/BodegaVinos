**BODEGA DE VINOS**

Esta aplicacion consiste en realizar un sistema simple de gestion de inventario. El sistema debe permitir **Registar vinos**, **Actualizar costos**, **Consultar Inventario**.
La aplicacion con la que se desarrollara dicho sistema, es una **ASP.NET Core web API**. Como gestion y almacenamiento de datos utilizaremos una base de datos SQL(SQLite).

**¿Como correr la aplicacion?**

1. **Abrir terminal o consola:**
   Se puede realizar mediante el cmd, PowerShell o Git Bash.
2. **Clonar el repositorio**
   En este segundo paso, se tiene que escribir el siguiente comando para clonar el repositorio:
   git clone https://github.com/ThiagoVaale/BodegaVinos.git
3. **Navegar al directorio del proyecto donde se clono el repositorio:**
   cd BodegaVinos
4. **Ejcutar solucion en visual studio:**
   Abrir la solucion (.sln) en visual studio. Luego ejecutar la aplicacion haciendo clic en el boton verde, a la derecha de "plataformas de solucion".
   
**Interactuar con la API**

La aplicacion va a comunicarse con la API atraves de los endpoint que se solicitaran atraves del SWAGGER o POSTMAN. A continuacion presentare lo hecho para la primera iteracion del proyecto.

1. **Lo primero que habia que hacer era registrar vinos con sus detalles:**

   Lo que hice fue realizar un DTO para que el usuario cuando solicite registrar un nuevo vino, le pase los datos de      las propiedades que ingreso el usuario.

```
public List<WineEntity> RegisterNewWine(RegisterNewWineDto RegisterWineDto)
{
    WineEntity wineRegister = new WineEntity()
    {
        Name = RegisterWineDto.Name,
        Variety = RegisterWineDto.Variety,
        Year = RegisterWineDto.Year,
        Region = RegisterWineDto.Region,
        Stock = RegisterWineDto.Stock
    };
    _winesRepository.Wines.Add(wineRegister);
    return _winesRepository.Wines;
}
```
   Luego de la asignacion a cada propiedad del vino, agregue al repositorio ese vino registrado y luego retorne a la 
   lista del repositorio la cual se habia agregado ese registro.
   
   Luego, habia que recibir esa peticion por el controlador, lo cual utilice un HttpPost que nos permite hacer la 
   creacion de un nuevo recurso.

```
      [HttpPost]
     public IActionResult RegisterNewWine([FromBody] RegisterNewWineDto registerNewWineDto)
     {
         if(registerNewWineDto.Name == null)
         {
             return BadRequest("El nombre del vino es OBLIGATORIO");
         }
         else if (registerNewWineDto.Year < 1990 || registerNewWineDto.Year > 2024)
         {
             return BadRequest("El año del vino tiene que estar entre 1990 y 2024");
         }
         else
         {
             return Ok(_wineService.RegisterNewWine(registerNewWineDto));
         }
     }
```
   Como se ve en el codigo, verifique que si registerNewWineDto.Name == null y registerNewWineDto.Year no estaba entre 
   1990 y 2024 devuelva una badRequest. La data notation de la propiedad Year del DTO la cree yo, el nombre del vino 
   era requerido.

   **Ejemplo de uso:**

**Registro de vino con sus detalles**

   --> Endpoint: POST /api/Wine
   
   --> Request:
   
   ```
   {
  "name": "Cabernet Sauvignon",
  "variety": "Tinto",
  "year": 2019,
  "region": "Napa Valley",
  "stock": 50
},
  {
  "name": "Dv. Catena",
  "variety": "Tinto",
  "year": 2000,
  "region": "Napa Valley",
  "stock": 0
}
```

   --> Http: Ok(200)
   
   --> **Endpoint** GET /api/Wine **Para corroborar registro exitoso**

   --> Body response:
   
```
      {
  "name": "Cabernet Sauvignon",
  "variety": "Tinto",
  "year": 2019,
  "region": "Napa Valley",
  "stock": 50
},
  {
  "name": "Dv. Catena",
  "variety": "Tinto",
  "year": 2000,
  "region": "Napa Valley",
  "stock": 0
}
```

   **Posibles Errores:**
   1. Si el nombre esta vacio:
      {
  "error": "El nombre del vino es OBLIGATORIO"
}
   2. Si el año esta fuera del rango(1990-2024):
      {
  "error": "El año del vino tiene que estar entre 1990 y 2024"
}


2. **Lo segundo que habia que hacer era consultar el inventario actual para ver los vinos disponibles y sus cantidades:**

   A partir de que la propiedad stock tenia que ser mayor que 0 en el WineEntity, filtre utilizando LINQ en el 
   WineService que me traiga los vinos que esten disponibles, que tengan el stock mayor que 0.

```
    public List<WineEntity> GetAvailabilityWines()
 {
     {
        return _winesRepository.Wines.Where(aw => aw.Stock > 0).ToList();
     };

 }
```

 Una vez en el controlador, tuve que llamar el get de una forma diferente, ya que para verificar que en el paso 
 anterior que el vino se habia registrado de una forma exitosa, utilice un get, para que me traiga todos esos vinos 
 registrados.

```
 [HttpGet("/Vinos disponibles")]
public IActionResult GetWineAvailability()
{
    return Ok(_wineService.GetAvailabilityWines());
}
```

**Ejemplo de uso:**
   --> Endpoint: GET /Vinos disponibles
   
   --> Request:
   
   ```
      {
  "name": "Cabernet Sauvignon",
  "variety": "Tinto",
  "year": 2019,
  "region": "Napa Valley",
  "stock": 50
}
```

   Al haber dos vinos registrados y solo devuelve uno, quiere decir que de la otra variedad de vino, no queda mas 
   stock.

   **Posibles errores:**
   1. Que el nombre de usuario este vacio.
   2. Que la contraseña no supere al menos los 8 carecteres.
   

3. **Como tercer y ultimo paso, habia que crear un usuario:**
   
   Lo primero que hice fue crear el DTO, definir sus propiedades para cuando se solicite la creacion de ese nuevo 
   usuario, poder pasarle los datos al Userservice y asi poder guardar los datos en el repositorio.
   
   DTO:
   
   ```
    public class CreateUserDto
 {
     public int Id { get; set; }
     [Required]
     public string Username { get; set; }
     [Required]
     [MinLength(8)]
     public string Password { get; set; }
 }
   ```

   Despues de crear el DTO, hice la logica desde el UserService:

```
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
```
   En el Userservice lo que hice fue pasar los datos que viene por el DTO a las propiedades ya definidas de User. 
   Luego utilizo la inyeccion que hice del userRepository en el UserService para agregar a la lista, el user creado y 
   retornar a esa lista actualizada.

   Como ultimo paso, habia creer el endpoint en el controlador para recibir esa peticion:

```
   [HttpPost]
public IActionResult createUser(CreateUserDto newUser)
{
    return Ok(_userService.createUser(newUser));
}
```

**Ejemplo de uso:**


   --> Endpoint: POST /api/User
   
   --> Request:

   ```
   {
  "username": "usuarioNuevo",
  "password": "contraseña123"
}
```

   Http: Ok(200)

   -->Body Response:

   ```
     {
  "username": "usuarioNuevo",
  "password": "contraseña123"
}
```


