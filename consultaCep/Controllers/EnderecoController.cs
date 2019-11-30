using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using consultaCep.DAO;
using consultaCep.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// Aluno Roberto Zotz - 1730970

namespace consultaCep.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly EnderecoDAO _enderecoDAO;
        public EnderecoController(EnderecoDAO enderecoDAO)
        {
            
            _enderecoDAO = enderecoDAO;
        }
        public IActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            return View(_enderecoDAO.Listar());
        }
        public IActionResult CadastrarEndereco()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarEndereco(Endereco e)
        {
            //string url = @"https://api.postmon.com.br/v1/cep/" + e.Cep ;
            string url = @"https://viacep.com.br/ws/"+e.Cep+"/json/";
            WebClient client = new WebClient();
            string resultado = client.DownloadString(url);
            Endereco enderecoNovo = JsonConvert.DeserializeObject<Endereco>(resultado);

            //_pedidoDAO.Cadastrar(pedido);
            _enderecoDAO.Cadastrar(enderecoNovo);

            // return View();
            return RedirectToAction("Index");
        }


    }
}