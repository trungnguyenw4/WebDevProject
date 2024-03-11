For my Web Application Development coursework 1, I would like to develop an application using Visual Code Studio,.NET 8.0 and Entity
Framework. This application will be able to Implement JWT token for user authentication, integrate Identity Framework for user management, 
roles, and access control. It can also trigger email service upon user sign-up. During the development of my project, 
I would use GitHub repository for version control. After successfully developed my application locally, I would deploy it as well as migrate 
its database on Azure.


To start off, I needed to create a new webapi inside my empty WebDevProject folder, then I opened this folder with Visual Code Studio and
started building my web application.

My application will perform as an Insuarance PlatformThis Platform will give users fair and insightful Insurance Policies based on what they 
provide such as: Personal Information, Financial Information, Heath Status, historical Insurance Claims, and so forth, .... Because of that, 
in side my application's Models folder, I created corresponding files/classes such as: Customers.cs, FinancialStatus.cs, HeathInformationStatus.cs,
InsuranceClaim.cs, InsurancePolicy.cs, OccupationInformation.cs. Those classes might not capture the whole picture of giving Insurance policies 
recommendation practice but they are the foundation ones of one user. 

After creating those classes, I would like to add the InsuranceContext.cs file to the Models folder. This class will help the application 
accessing and interacting with its database that will be automatically created and updated by Entity Framework based on my Model classes. 


namespace WebDevelopmentProject.Models
{
	public class InsuranceContext: IdentityDbContext<IdentityUser>
    {

        public InsuranceContext(DbContextOptions<InsuranceContext> options): base(options)
        {

        }


        public DbSet<Customer> C { get; set; }
        public DbSet<FinancialInformation> FinancialInformation { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaim { get; set; }
        public DbSet<OccupationInformation> OccupationInformation { get; set; }
        public DbSet<HealthInformation> HealthInformation { get; set; }
        public DbSet<InsurancePolicy> InsurancePolics { get; set; }

   

    }
}


Next, I had to update the appsettings.json with connection string:
"ConnectionStrings": {
    "Connection": "Data Source=insurance.db;"
  },
This insurance.db is the local database of the application.


After that, I would inject dependency for database connection into Program.cs:

builder.Services.AddControllers();
builder.Services.AddDbContext<InsuranceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Connection")));

At this stage, my application was ready for the first migration and Database update.

I continued by generating the basic controllers of my application with the help of:

dotnet tool install --local dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Adding controllers:

dotnet aspnet-codegenerator controller -name CustomersController -async -api -m Customer -dc InsuranceContext -outDir Controllers
dotnet aspnet-codegenerator controller -name InsurancePoliciesController -async -api -m InsurancePolicy -dc InsuranceContext -outDir Controllers
dotnet aspnet-codegenerator controller -name FinancialStatusController -async -api -m FinancialStatus -dc InsuranceContext -outDir Controllers
dotnet aspnet-codegenerator controller -name HealthInformationController -async -api -m HealthInformation -dc InsuranceContext -outDir Controllers
dotnet aspnet-codegenerator controller -name OccupationInformationController -async -api -m OccupationInformation -dc InsuranceContext -outDir Controllers
dotnet aspnet-codegenerator controller -name InsuranceClaimController -async -api -m InsuranceClaim -dc InsuranceContext -outDir Controllers


It is noteworthy that we could exclude fields that should be ignored from the endpoints by adding [JsonIgnore].


In order to add identity support in my application such as user accounts, sign up, sign in and sign out, It is required to create an email 
service on sign up by updating InsuranceContext.cs file 


From
public class InsuranceContext : DbContext
to
public class InsuranceContext : IdentityDbContext<IdentityUser>

and injecting into Program.cs

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SchoolContext>().AddDefaultTokenProviders();

After that, I needed to migrate and update the database so that I can reflex the new context properly.


Next step, I would add the registration with email verification and sign in functionalities to my application by adding a new class called 
AuthModel.cs to Models folder.

public class AuthModel
 {
 public string Email { get; set; }
 public string Password { get; set; }
 }


Next, I created a new folder named Services in the project directory, and put two new classes named EmailService.cs and EmailSettings.cs 
inside Services folder.


public class EmailService
{
 private readonly EmailSettings _emailSettings;
 public EmailService(IOptions<EmailSettings> emailSettings)
 {
 _emailSettings = emailSettings.Value;
 }
 public void SendEmail(string toEmail, string subject, string body)
 {
 var message = new MimeMessage();
 message.From.Add(new MailboxAddress("Support CareApp", _emailSettings.SmtpUsername));
 message.To.Add(new MailboxAddress("Reciever Name", toEmail));
 message.Subject = subject;
 var textPart = new TextPart("plain")
 {
 Text = body
 };
 message.Body = textPart;
 using (var client = new SmtpClient())
 {
 client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort,
SecureSocketOptions.StartTls);
 client.Authenticate(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
 client.Send(message);
 client.Disconnect(true);
 }
 }
}


public class EmailSettings
 {
 public string SmtpServer { get; set; }
 public int SmtpPort { get; set; }
 public string SmtpUsername { get; set; }
 public string SmtpPassword { get; set; }
 }


namespace WebDevProject.Controllers
{
    [Route("api/[controller]")]
 [ApiController]
 public class AccountController : ControllerBase
 {
 private readonly UserManager<IdentityUser> _userManager;
 private readonly SignInManager<IdentityUser> _signInManager;
 private readonly EmailService _emailService;
 public AccountController(UserManager<IdentityUser> userManager,
SignInManager<IdentityUser> signInManager, EmailService emailService)
 {
 _userManager = userManager;
 _signInManager = signInManager;
 _emailService = emailService;
 }
 
 [HttpPost("register")]
 public async Task<IActionResult> Register(AuthModel model)
 {
 var user = new IdentityUser { UserName = model.Email, Email = model.Email };
 var result = await _userManager.CreateAsync(user, model.Password);
 if (result.Succeeded)
 {
 // Generate an email verification token
 var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
 // Create the verification link
 var verificationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token =
token }, Request.Scheme);
 // Send the verification email
 var emailSubject = "Email Verification";
 var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
 _emailService.SendEmail(user.Email, emailSubject, emailBody);

 return Ok("User registered successfully. An email verification link has been sent.");
 }
 return BadRequest(result.Errors);
 }
 // Add an action to handle email verification
 
 [HttpGet("verify-email")]
 public async Task<IActionResult> VerifyEmail(string userId, string token)
 {
 var user = await _userManager.FindByIdAsync(userId);
 if (user == null)
 {
 return NotFound("User not found.");
 }
 var result = await _userManager.ConfirmEmailAsync(user, token);
 if (result.Succeeded)
 {
 return Ok("Email verification successful.");
 }
 return BadRequest("Email verification failed.");
 }
 [HttpPost("login")]
 public async Task<IActionResult> Login(AuthModel model)
 {
 var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
isPersistent: false, lockoutOnFailure: false);
 if (result.Succeeded)
 {
 return Ok("Login successful.");
 }
 return Unauthorized("Invalid login attempt.");
 }
 }
}

After that, It is required to register an email service in Program.cs 

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();

and mention this in the appsettings.json file after the connection string:

"EmailSettings": {
 "SmtpServer": "smtp.gmail.com",//only valid for gmail
 "SmtpPort": 587,
 "SmtpUsername": "mygmailaccount@gmail.com",
 "SmtpPassword": "my gmail app password"
 },

It is worth mentioning that the SmtpPassword has been obtained by enabling Gmail Two-Factor Authentication and requesting 
Application Specific Password.

I then could register for my gmail by putting this on Postman (method Post, body raw and JSON)

{
 "Email": "mymail@gmail.com",
 "Password": "mypassword!123456"
}

I later hit the ‘send’ button. The was an verify email sent to my mail box, after clicking that link, it would redirect my to
locahost:5036 (my local host) and the message "Email verification successful." was being shown.

Next step, I would like to add JWT token and for that I needed to install Microsoft.AspNetCore.Authentication.JwtBearer dependency: 

So, I added this in Program.cs:

    builder.Services.AddScoped<RolesController>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    });
   
And in appsettings.json file, I added this:

"Jwt": { 

    "Key": "this is my custom Secret key for authentication",

    "Issuer": "https://localhost:5036)"

    "ExpireHours": 1 

  }, 
  
After that, I updated AccountController.cs


namespace WebDevProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, EmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate an email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Create the verification link
                var verificationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                // Send the verification email
                var emailSubject = "Email Verification";
                var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
                _emailService.SendEmail(user.Email, emailSubject, emailBody);
               
                return Ok("User registered successfully. An email verification link has been sent.");
            }

            return BadRequest(result.Errors);
        }


        // Add an action to handle email verification
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok("Email verification successful.");
            }

            return BadRequest("Email verification failed.");
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user,roles);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid login attempt.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}

After having Token Authorization, I would add privilege management feature for my application by creating RolesController.cs file
inside Controllers folder.

namespace WebDevProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            role.Name = model.NewRoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role updated successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);

            if (!roleExists)
            {
                return NotFound("Role not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);

            if (result.Succeeded)
            {
                return Ok("Role assigned to user successfully.");
            }

            return BadRequest(result.Errors);
        }

    }
}

then I added AssignRoleModel and UpdateRoleModel classes in the Models:

public class AssignRoleModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }

 public class UpdateRoleModel
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
    }
	
In oder to assign role to registered user. We first needed to comment out the [Authorize(Roles = "Admin")] part and create a new Admin and 
assgin this role to the registered user above using Postman. Next, we can login to the application using that registered email to create, and 
assign other roles with other access levels to other users since it contained the corresponding authorized token of that user in the header.

For my application, at this stage I just have two Roles: Admin and Member. While Admin can perform actions on RolesController.cs 
[Authorize(Roles = "Admin")], actions on CustomerControllers can be triggered by both Admin and Member [Authorize(Roles = "Admin, Member")].

Next, I would like to allow my application to generate logs for tracking events when the application is running. The logs can be put on console to
bring insights or put into a .txt file for recording and later use purpose.


In appsetting.json, I describled logging as follows:

"Logging": {
  "PathFormat": "Logs/log-{Date}.txt",
  "IncludeScopes": false,
  "Debug": {
    "LogLevel": {
      "Default": "Information",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "Console": {
    "LogLevel": {
      "Default": "Information",
      "System": "Warning",
      "Microsoft": "Warning"
    }
  },
  "File": {
    "LogLevel": {
      "Default": "Information",
      "System": "Warning",
      "Microsoft": "Warning"
    }
  },
  "LogLevel": {
    "Default": "Information",
    "System": "Warning",
    "Microsoft": "Warning"
  },
  "AllowedHosts": "*"
}


I wanted to organize application recored logs by date order, each date will has its own log, and they will be store inside Log folder.
Besides, those logs only appears when an event hit a certain level of critical, so not all events will be recorded. That helps in term of
optimizing storage usage.


We can do some tests to make sure that the application functions well.

**AccountController actions:**

register user:
POST http://localhost:5206/api/account/register

body raw JSON
{
 "Email": "franks08cfc@gmail.com",
 "Password": "Password123!"
}

login:
POST http://localhost:5206/api/account/login

body raw JSON
{
 "Email": "franks08cfc@gmail.com",
 "Password": "Password123!"
}

logout:

POST http://localhost:5206/api/account/logout

body raw JSON
{
 "Email": "franks08cfc@gmail.com",
 "Password": "Password123!"

For this group of actions, there is no privilege assigning yet since a random web user can register account with my application.

***RolesController action***

GET
body raw JSON
http://localhost:5206/api/Roles

POST
body raw JSON
http://localhost:5206/api/Roles/
{
 "RoleName": "Admin"
}

POST
body raw JSON
http://localhost:5206/api/Roles/assign-role-to-user
{
{"UserId" : "3827362d-2ffb-4da0-9f54-d7c40d213a04",
 "RoleName": "Admin"
}

For this group of actions, there is only "Admin" can add new roles, assign role, and make changes.

****Others****
POST http://localhost:5206/api/Customers

body raw JSON
{
"CustomerId" : 1 ,
"FullName" : "James Nguyen",
"DateOfBirth" : "1990-05-15",
"Gender" :"Male",
"MaritalStatus" : "Married",
"ContactNumber" : "123456789",
"EmailAddress" : "helloword@hotmail.com"
}

GET http://localhost:5206/api/Customers

...
For ending points HeathInformation, OccupationInformation, InsuranceClaim, InsurancePolices we can do the same as we did with Customers.


For this group of actions, both "Admin" and "User" are able to take them. Howver, "User" should only work on their owns data. 
I'm still working on this logic.

Finally, I would like to deploy my web app to Azure. I started by going to Azure Portal and creating new web app template. 
After that, I created a new server and then a new database for my web app. After a new database has been created, I was able to grab 
its connection string as following:
"Server=tcp:insuranceplatform.database.windows.net,1433;Initial Catalog=NewDatabaseForInsurancePlatform;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";"

The above connection string is used for Microsoft Entra passwordless authentication way (ADO.NET) that provides consistent access to 
data sources.


Next, I went back to my Visual Code studio and update the ConnectionString in appsetting.json, from

{"ConnectionStrings": {
  "Connection": "insurance.db;"
},

to

{"ConnectionStrings": {
  "Connection": "Server=tcp:insuranceplatform.database.windows.net,1433;Initial Catalog=NewDatabaseForInsurancePlatform;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;"
},

 
I then needed to migrate my web app to cloud database and update that database by using

dotnet ef migrations add CloudDatabase
dotnet ef database update 


After successfully done that, I had to connect Azure with Visual Studio Code. Right after having established a connection with Azure, 
I can right click on my newly created WebApp template and deploy my finished web app.


***Test***
GET
raw JSON
insuranceplatform.database.windows.net/weatherforecast


POST
raw JSON
insuranceplatform.database.windows.net/api/account/logout


GET
raw JSON
insuranceplatform.database.windows.net/api/Roles

*******



During this project, I faced a major problem on connecting my web app with the server. Although successfully migrated my application to Azure, 
and provided the correct connection string of the target database, the streaming log of my application keeps giving me 



Error Number: 18456, State: 1, Class: 14 is related to login failure

I then made sure all Azure services are allowed to connect to my target databse. Next, I added the IP of my web app to the firewall rule
and set the connection string of my web app manually on Azure but it still does not work. Finally, I reached the limits of my database and
face this error.

Error Number:42119,State:1,Class:20 is related to connection to the Azure SQL Database was blocked due to excessive resource usage.

Overall, this project met its requirements that are:

• The backend service should provide RESTful APIs for basic CRUD operations on relevant
entities.
• Implement user authentication using Identity Framework.
• Configure user roles (e.g., Admin, User) with different access permissions.
• JWT tokens should be used for user authentication and authorization.
• An email service should be integrated to send a confirmation email upon user sign-up.
• Proper error handling and validation should be implemented.
• Use Dependency Injection for better code organization.
• Implement logging for tracking application events.


However, this project still has a lot of space for improvement. I still need to develop a model to give users recommendations
and apply it into my program. Besides, more information needed to be added into Models to provide enough input for the recommendation 
algorithm. I also need to work on the connection of the web app and the cloud server.

