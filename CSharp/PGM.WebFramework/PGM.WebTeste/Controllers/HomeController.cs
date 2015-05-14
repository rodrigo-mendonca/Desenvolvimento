using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PGM.WebTeste.Models;

namespace PGM.WebTeste.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            Cadastro Cad = new Cadastro() {Id=1,Nome="Rodrigo",Email="diu.mendonca@hotmail.com" };

            ViewBag.Id = Cad.Id;
            ViewBag.Nome = Cad.Nome;
            ViewData["Email"] = Cad.Email;

            return View();
        }
	}
}