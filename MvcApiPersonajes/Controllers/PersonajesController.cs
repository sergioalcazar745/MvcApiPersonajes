using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajes.Models;
using MvcApiPersonajes.Services;

namespace MvcApiPersonajes.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;
        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.service.GetPersonajesAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await this.service.FindPersonajeAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.service.FindPersonajeAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }
    }
}
