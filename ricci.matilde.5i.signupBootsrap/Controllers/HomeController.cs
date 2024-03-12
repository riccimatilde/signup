using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ricci.matilde._5i.signupBootsrap.Models;


namespace ricci.matilde._5i.signupBootsrap.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static List<Prodotto> Prodotti = new List<Prodotto>();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        string? IsAuthenticated = HttpContext.Session.GetString("isAuthenticated");
        if (IsAuthenticated == "OK")
        {
            return View();
        }
        else
        {
            return Redirect("\\Home\\Login");
        }
    }
        
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(Utente u)
    {
        
        // Verifica delle credenziali
        if (u.Email == "q" && u.Password == "q")
        {
            // Imposta la variabile di sessione per indicare che l'utente è autenticato
            HttpContext.Session.SetString("isAuthenticated", "OK");
            return RedirectToAction("GiaRegistrato", "Home"); // Reindirizza alla home page dopo il login
        }
        else
        {
            ViewBag("", "Nome utente o password non validi.");
            return View();
        }

    }
    public IActionResult GiaRegistrato()
    {
        HttpContext.Session.SetString("isAuthenticated", "OK");
        return View();
    }
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SignUp(Utente u)
    {
        return View(u);
    }

    public IActionResult Conferma(Utente u)
    {
        return View(u);
    }
    public IActionResult AggiungiOrdine()
    {
        string? nomeUtente = HttpContext.Session.GetString("isAuthenticated");
        if (string.IsNullOrEmpty(nomeUtente))
            return Redirect("\\home\\Login");
        return View ();
    }
    public IActionResult Cart(Prodotto p)
    {
        dbContext db = new dbContext();
        db.Prodotti.Add(p);
        db.SaveChanges();
        string? nomeUtente = HttpContext.Session.GetString("isAuthenticated");
        if (string.IsNullOrEmpty(nomeUtente))
            return Redirect("\\home\\Login");        
        return View( db.Prodotti.ToList() );
    }
[HttpPost]
public IActionResult RimuoviDalCarrello(int id)
{
    dbContext db = new dbContext();
    var prodottoDaRimuovere = db.Prodotti.FirstOrDefault(prod => prod.Id == id);
    if (prodottoDaRimuovere != null)
    {
        db.Prodotti.Remove(prodottoDaRimuovere);
        db.SaveChanges();
    }

    return RedirectToAction("Cart");
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
