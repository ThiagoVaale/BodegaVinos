**BODEGA DE VINOS**

Esta aplicacion consiste en realizar un sistema simple de gestion de inventario. El sistema debe permitir **Registar vinos**, **Actualizar costos**, **Consultar Inventario**.
La aplicacion con la que se desarrollara dicho sistema, es una **ASP.NET Core web API**. Como gestion y almacenamiento de datos utilizaremos una base de datos SQL(SQLite).


   **PRIMERA ITERACION**

                                                
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

1) **Lo primero que habia que hacer era registrar vinos con sus detalles:**

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
      ```
      {
       "error": "El nombre del vino es OBLIGATORIO"
      }
      ```
      
   2. Si el año esta fuera del rango(1990-2024):
      ```
      {
      "error": "El año del vino tiene que estar entre 1990 y 2024"
      }
      ```
      
2) **Lo segundo que habia que hacer era consultar el inventario actual para ver los vinos disponibles y sus cantidades:**

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
   
      "1. Que el nombre de usuario este vacio."
      
      "2.  Que la contraseña no supere al menos los 8 carecteres."
   

3) **Como tercer y ultimo paso, habia que crear un usuario:**
   
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

  " Despues de crear el DTO, hice la logica desde el UserService:"

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
  " En el Userservice lo que hice fue pasar los datos que viene por el DTO a las propiedades ya definidas de User. 
   Luego utilizo la inyeccion que hice del userRepository en el UserService para agregar a la lista, el user creado y 
   retornar a esa lista actualizada.

   Como ultimo paso, habia creer el endpoint en el controlador para recibir esa peticion:"

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

   "Http: Ok(200)

   --> Body Response:"

```
{
  "username": "usuarioNuevo",
  "password": "contraseña123"
}
```

   **SEGUNDA ITERACION**

1) **Implementar base de datos con SQLite**:

   Como PRIMER instancia lo que hice fue configurar el appseting.json para la conexion con la base de datos. Esto
   nos vas a servir para que una vez tengamos el appsettings.json configurado, desde el program, podamos ejecutar esa
   misma configuracion. No solamente configurar, si no tambien configurar el contexto. El appsettings.json y el       
   program.cs se ven asi:

   **appsettings.json**

   ```
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "WinesAPIDBConnectionString": "Data Source=WineDbContext.db" 
     }
    ```

   **Program.cs**

    ```
    builder.Services.AddDbContext<WineDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:WinesAPIDBConnectionString"]));
    ```

    Luego, cree el contexto de la base de datos. El contexto es el puente de comunicacion entre la aplicacion y la         base de datos utilizada. Las tablas que querramos que se vean reflejadas en la Base de datos, las representaremos      con **DbSet**<>.

```
   public class WineDbContext : DbContext
    {
    //Aca se va a definir la tabla de vinos, users, cata dentro de la base de datos.
    public **DbSet**<WineEntity> Wines { get; set; }
    public **DbSet**<UserEntity> Users { get; set; }
    public **DbSet**<CataEntity> Catas { get; set; }

    //Constructor que recibe las opciones desde el program.cs y los data sets de las entidades que queremos guardar en     la base de datos.
      public WineDbContext(DbContextOptions<WineDbContext> options) : base(options)
      {
        
      }
```

     En el codigo anteriormente proporcionado es donde se registra el contexto para que EFC lo utilice.

2) **Agregar Repositorios**:

   Una vez que hice la configuracion y conexion referida al appsettings.json, program.cs y DbContext, debia cambiar
   el repositorio hardcodeado.
   Como primera instancia inyecte el DbContext en el repositorio para una mejor separacion de responsabilidades,       
   facilidad de prueba, persistencia, etc. Como consecuencia de haber cambiado el repositorio, tuve que cambiar la   
   logica en la que estaba hecho el WineService, inyectando el WineRepository. que es quien va a consultar a la base      de datos.

   **WineRepository**

```
    private readonly WineDbContext _wineDbContext;

    public WinesRepository(WineDbContext wineDbContext)
     {
       _wineDbContext = wineDbContext;
     }
```

     **WineService**
   
   ```
     private readonly WinesRepository _wineRepository;
     public WineService(WinesRepository wineRepository)
     {
         _wineRepository = wineRepository;
     }
   ```

3) **Autenticación con JWT**:

   Al trabajar con Autenticacion, lo que hice fue crear un archivo AuthenticateController.cs en la carpeta de             Controller. Es donde vamos a validar credenciales y generar el token.

   Lo primero que hice fue inyectar el UserService en el AuthenticateController.cs, ¿Por que UserService? porque es el    service quien va a consultar a la base de datos si las credenciales son correctas, para luego dar lugar a la     
   creacion del token. Tambien inyecte una interfaz de IConfiguration que es donde nosotros vamos a traer la llave 
   secreta de nuestra API desde el appsetting.json, recordemos que esa llave la conoce SOLO la API. Como hay que          recibir datos de la request, cree un dto donde voy a recibir el **username** y **password** El codigo se ve 
   asi:

  **AuthenticateController:**
  
```
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
                var securityPassword = new                                                            symmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json;

                //Donde agarra el header + payload + secretkey(se encuentra en el appsettingJSON) y a todo ESTO lo                     HASHEA.
                SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                //Los claims son datos en clave->valor que nos permite guardar data del usuario.
                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticate.Id.ToString())); //"sub" es una key estándar que                  significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos                    con la key "sub".
                claimsForToken.Add(new Claim("given_name", userAuthenticate.Username)); //Lo mismo para given_name y                    family_name, son las convenciones para nombre y apellido. Ustedes pueden usar lo que quieran, pero si                  alguien que no conoce la app

                var jwtSecurityToken = new JwtSecurityToken(//Acá es donde se crea el token con toda la data que le                    pasamos antes.
                  _config["Authentication:Issuer"], //ISSUER Y AUDIENCE valores que estan en el appsettingJSON.
                  _config["Authentication:Audience"],
                  claimsForToken, //Objeto que definimos arriba, llamdas CLAIMS(CLAVE : VALOR)
                  DateTime.UtcNow, //Fecha de creacion del token
                  DateTime.UtcNow.AddHours(1), //Fecha de expiracion del token
                  signature); //La firma del token

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); //Aca se encuentra                  el token
                return Ok(tokenToReturn);
            }
            return Unauthorized();
        } 
      }
```

   Aca es donde se define todo lo referido a la creacion del token en caso de que las credenciales sean correctas.
   Recordemos que el token esta generado por 3 partes: Header + payload + signature. **¿Como verifica la API, en caso     de venir un token ya validado si es correcto?** Lo que hace es desglosar ese token, ¿Como? diviendo al token en 3      partes, header + payload + signature(firma del token que lo tiene solo la API) y a todo eso lo hashea. Si el       
   resultado es igual al token que llego, verifica que ese token no fue tocado, adulterado.

4) **CONSULTAR POR VARIEDAD DE VINO**:

  Cree un endpoint [HttpGet({"variety})] que es donde vamos a recibir el dato por la ruta de la request. Este endpoint
  se conectara con el WineService donde este le consultara al WineRepository la consulta del vino atraves de esa         variedad. Es importante pasarle como parametro el tipo que vamos a recibir por ruta.

  WineController:

   ```
        [HttpGet("{variety}")]
        public IActionResult VarietyWines([FromRoute] string variety)
        {
            return Ok(_wineService.VarietyWines(variety));
        }
   ```

  WineService:

  ```
     public List<WineEntity> VarietyWines(string variety)
     {
         return _wineRepository.VarietyWines(variety);
     }
  ```

  WineRepository:

 ```
    public List<WineEntity> VarietyWines(string variety)
    {
        return _wineDbContext.Wines.Where(v => v.Variety.Contains(variety)).ToList();
    }
  ```

    Importante: La busqueda en la base de datos la realice mediante LINQ, obteniendo los vinos que CONTENGAN la            palabra obtenida por ruta en la request.


5) **ACTUALIZAR STOCK POR ID**:

   Atraves de un [HttpPut("{idWineForUpdate}/stock")] fue donde cree el metodo que recibiria la request. ¿Por que un      HttpPut? Como el item nos pedia la actualizacion por Id, lo que lograriamos con el HttpPUt es exactamente eso, ya      que con este verbo lo que hacemos es la actualizacion parcial de "X" recurso. Recibimos el Id por ruta y el nuevo      stock por ruta. 

  **WineController**:
  
```
  [HttpPut("{idWineForUpdate}/stock")]
  public IActionResult UpdateWineStock([FromRoute]int idWineForUpdate, [FromBody] int newStock)
  {
      try
      {
          return Ok(_wineService.UpdateWineStock(idWineForUpdate, newStock));
      }
      catch (WineException error)
      {
          return BadRequest(error.Message);
      }
      
  }
 ```

**WineService**:
```
 public WineEntity? UpdateWineStock(int idWineForUpdate, int newStock)
 {
     WineEntity? updateWineId = _wineRepository.UpdateWineStock(idWineForUpdate, newStock);

     if (updateWineId == null)
     {
         throw new WineException($"El id {idWineForUpdate} no EXISTE");
     }
     else
     {
         return _wineRepository.UpdateWineStock(idWineForUpdate, newStock);
     }
    
 }

```

El metodo retorna un WineEntity? ya que el valor del metodo puede ser NULL, por eso es que con ? permitimos que la funcion devuelva dicho valor. En el Service es donde vamos a recibir el valor del repositorio, ya sea null o contenga
la actualizacion de "X" vino. Haciendo un bloque if dependiendo del valor recibido, devolvemos mensaje aclaratorio o los datos verificados en la base de datos.

**WineRepository**:

```
  public WineEntity? UpdateWineStock(int idWineForUpdate, int newStock)
  {
      WineEntity? idWineUpdate = _wineDbContext.Wines.FirstOrDefault(i => i.Id == idWineForUpdate);

      if (idWineUpdate == null)
      {
          return null;
      }

      idWineUpdate.Stock = newStock;

      _wineDbContext.Wines.Update(idWineUpdate);
      _wineDbContext.SaveChanges();
      return idWineUpdate;
  }
```
Lo que hice fue una busqueda atraves LINQ para ver si existia dicho ID. Si la variable "idWineUpdate" devolvia null, retornaba a null. Si no, asignaba el stock del id a newStock(pasado por body). Luego actualice el valor del stock, guarde los cambios(IMPORTANTE) y retorne al idWineUpdate;

   --> **POSIBLES ERRORES**:
       **QUE EL ID SEA NULO/NO EXISTA**:
    ```
      if (updateWineId == null)
      {
      throw new WineException($"El id {idWineForUpdate} no EXISTE");
      }

    ```

    Como se ve, hay un WineExeption donde devuelvo un mensaje de erro. Eso lo cree para que cuando haya un error,          poder enviar un mensaje aclaratorio a la response. Lo que hice fue crear un archivo "WineExeption" en la carpeta       Exeption.
    
  ```
        public class WineException : Exception
    {
        public WineException(string message) : base(message)
        {

        }
    }
   ```

6) **AGREGAR ENTIDAD CATA**:

   .Entidad Cata:
   
    ```
     public class CataEntity
      {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int IdTesting { get; set; }
      public string Date { get; set; }
      public string Name { get; set; }
      public List<WineEntity> Wines { get; set; } = new List<WineEntity>(); //Voy a configurar una relacion de 1 : N         donde el 1 es la cata y el N los vinos.
      public List<string> InvitedPeople { get; set; } = new List<string>();
      }
     ```

     .Como segundo paso agregue a la entidad Vino la relacion con la entidad CataEntity.
     
      ```
     public CataEntity? Catas { get; set; }
      ```

      Definiendo CataEntity el tipo de retorno del metodo. Esto lo que nos permite, es aclarar la relacion que               queremos tener, 1 : N --> A que cada vino pertenece a la cata.

      .En tercer lugar, tenemos que agregar al contexto, la nueva entidad creada para verla reflejada en la base de           datos:

      ```
        public DbSet<CataEntity> Catas { get; set; }
      ```

      .En cuarto lugar, definir la relacion con la que van a contar las dos tablas atraves de la clave foranea.

       ```
       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
     base.OnModelCreating(modelBuilder);

     modelBuilder.Entity<CataEntity>()
         .HasMany(c => c.Wines) // Una cata posee muchos vinos
         .WithOne(v => v.Catas); // Un vino pertenece a una cata
       }
       ```
      

   

  

        





   

   
   
   

   


