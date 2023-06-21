using ImplementingLikeButton.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ImplementingLikeButton.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Variables()
        {
            //int -stores integers(whole numbers), without decimals, such as 123 or - 123
            int myNum = 5;

            //double -stores floating point numbers, with decimals, such as 19.99 or - 19.99
            double myDoubleNum = 5.99;

            //char -stores single characters, such as 'a' or 'B'.Char values are surrounded by single quotes
            char myLetter = 'D';

            //string -stores text, such as "Hello World".String values are surrounded by double quotes
            string myText = "Hello";

            //bool -stores values with two states: true or false
            bool myBool = true;

            //In summary, these lines of code are used to pass data between the controller and the view in an ASP.NET MVC application, using the ViewBag, ViewData, and TempData objects.

            //ViewBag.MyNum = myNum;: This line sets the value of a property called MyNum on the ViewBag object to the value of the variable myNum.
            //The ViewBag object is a dynamic object that allows you to pass data between the controller and the view in an MVC application.
            ViewBag.MyNum = myNum;

            //ViewBag.MyDoubleNum = myDoubleNum;: This line sets the value of a property called MyDoubleNum on the ViewBag object to the value of the variable myDoubleNum.
            //This line is similar to the first line, but sets a different property on the ViewBag object.
            ViewBag.MyDoubleNum = myDoubleNum;

            //ViewData["MyLetter"] = myLetter;: This line sets the value of a key-value pair in the ViewData dictionary.
            //The key is a string "MyLetter", and the value is the value of the variable myLetter.
            //The ViewData dictionary is another way to pass data between the controller and the view in an MVC application.
            ViewData["MyLetter"] = myLetter;

            //TempData["MyText"] = myText;: This line sets the value of a key-value pair in the TempData dictionary.
            //The key is a string "MyText", and the value is the value of the variable myText.
            //The TempData dictionary is similar to the ViewData dictionary, but the data stored in TempData persists only for the current and next request.
            TempData["MyText"] = myText;

            //TempData["MyBool"] = myBool;: This line sets the value of a key-value pair in the TempData dictionary.
            //The key is a string "MyBool", and the value is the value of the variable myBool.
            //This line is similar to the previous line, but sets a different key-value pair in the TempData dictionary.
            TempData["MyBool"] = myBool;

            return View();
        }
        public IActionResult Arrays()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}