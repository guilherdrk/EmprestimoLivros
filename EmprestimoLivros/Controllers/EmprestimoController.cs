using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {

        readonly private ApplicationDBContext _db; //variavel apenas de leitura e privada para esta classe

        public EmprestimoController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos;

            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id) 
        {
            if(id == null || id == 0) 
            {
                return NotFound();

            }

            EmprestimosModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimo == null)
            {
                return NotFound();
            }


            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimos) 
        {
            if (ModelState.IsValid) 
            {
                _db.Emprestimos.Add(emprestimos);           //Adicionando dados no bando de dados
                _db.SaveChanges();                          //Salvando esses dados 

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
