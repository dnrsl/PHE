using Client2.Contracts;
using Client2.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Client2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AccountController(IAccountRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this.repository = repository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterVM registerVM)
        {
            if(registerVM.PhotoFile != null)
            {
                var fileName = DateTime.Now.ToString("MMddyyyyHHmmss");
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "files", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await registerVM.PhotoFile.CopyToAsync(stream);
                }
                registerVM.Photo = fileName;
            }
            else
            {
                registerVM.Photo = "default.jpg";
            }
            var result = await repository.Register(registerVM);
            if (result.Code == 200)
            {
                TempData["Success"] = "Register Success";
                return RedirectToAction("login", "account");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}. Check your input data and try again";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = await repository.Login(loginVM);
            if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        
    }
}
