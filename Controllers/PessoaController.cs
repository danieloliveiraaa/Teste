using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sistema.Models;
using Sistema.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sistema.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public PessoaController(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }

        public IActionResult Index()
        {
            //var pessoas = _pessoaRepositorio.BuscarTodos();

            var pessoas = BuscarTodasPessoasAPI();

            return View(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var create = CreateAPI(pessoa);
                    TempData["MensagemSucesso"] = "Cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(pessoa);
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult Edit(int id)
        {
            Pessoa pessoa = _pessoaRepositorio.BuscarPorId(id);

            return View(pessoa);
        }

        [HttpPost]
        public IActionResult Edit(Pessoa pessoa)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var EditApi = EditAPI(pessoa);

                    if(EditApi != null)
                    {
                        TempData["MensagemSucesso"] = "Alterado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }

                return View("Editar", pessoa);
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            Pessoa pessoa = _pessoaRepositorio.BuscarPorId(id);
            return View(pessoa);
        }

        public IActionResult Apagar (int id)
        {
            try
            {
                Pessoa pessoa = _pessoaRepositorio.BuscarPorId(id);

                if(pessoa != null)
                {
                    var apagar = DeleteAPI(pessoa);

                    if (apagar == true)
                    {
                        TempData["MensagemSucesso"] = "Deletado com sucesso!";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Erro ao deletar!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        #region Ações via API'S
        public List<Pessoa> BuscarTodasPessoasAPI()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:44373/api/Pessoa/BuscarTodasPessoas");

                request.ContentType = "application/json";
                request.Method = "GET";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                var contentPost = string.Empty;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                    contentPost = sr.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
                var ret = JsonConvert.DeserializeObject<List<Pessoa>>(contentPost, settings);

                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Erro:", ex.Message));
                return null;
            }
        }

        public Pessoa CreateAPI(Pessoa model)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:44373/api/Pessoa/Create");

                request.ContentType = "application/json";
                request.Method = "POST";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(model);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var contentPost = string.Empty;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                    contentPost = sr.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
                var ret = JsonConvert.DeserializeObject<Pessoa>(contentPost, settings);

                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Erro:", ex.Message));
                return null;
            }
        }
        
        public Pessoa EditAPI(Pessoa model)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:44373/api/Pessoa/Edit");

                request.ContentType = "application/json";
                request.Method = "PUT";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(model);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var contentPost = string.Empty;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                    contentPost = sr.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
                var ret = JsonConvert.DeserializeObject<Pessoa>(contentPost, settings);
                
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Erro:", ex.Message));
                return null;
            }
        }

        public bool DeleteAPI(Pessoa model)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:44373/api/Pessoa/Delete");

                request.ContentType = "application/json";
                request.Method = "DELETE";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(model);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var contentPost = string.Empty;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                    contentPost = sr.ReadToEnd();

                return true;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }


        #endregion
    }
}
