For my Web Application Development coursework 1, I would like to develop an application using Visual Code Studio, .NET 8.0 and Entity
Framework. After successfully developed my application locally, I would deploy it as well as migrate its database on Azure.

First of all, I needed to create a new webapi inside my empty WebDevProject folder, then I opened this folder with Visual Code Studio and
started building my web application.

My application will perform as an Insuarance PlatformThis Platform will give users fair and insightful Insurance Policies based on what they provide such as:
Personal Information, Financial Status, Heath Status, historical Insurance Claims, and so forth, .... Because of that, in side my application's Models folder, 
I created corresponding files/classes such as: Customers.cs, FinancialStatus.cs, HeathInformationStatus.cs, InsuranceClaim.cs, InsurancePolicy.cs, OccupationInformation.cs. 
Those classes might not capture the whole picture of giving Insurance policies recommendation practice but they are the foundation ones of one user. 

After creating those classes, I would like to add the InsuranceContext.cs file to the Models folder. This class will help the application accessing and interacting 
with its database that will be automatically created and updated by Entity Framework based on my Model classes. 


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

It is noteworthy that we could exclude fields that should be ignored from the endpoints by adding [JsonIgnore] in those 5 foudation classes.

In order to add identity support in my application such as user accounts, sign up, sign in and sign out, It is required to create an email service on sign up by 
updating InsuranceContext.cs file 

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

Next, I created a new folder named Services in the project directory, and put two new classes named EmailService.cs and EmailSettings.cs inside 
Services folder.

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







 
