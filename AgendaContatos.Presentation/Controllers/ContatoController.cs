using AgendaContatos.Data.Entities;
using AgendaContatos.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AgendaContatos.Data.Enums;
using AgendaContatos.Data.Repositories;
using System.Linq.Expressions;

namespace AgendaContatos.Presentation.Controllers
{
    [Authorize]
    public class ContatoController : Controller
    {
        public IActionResult Cadastro()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContatoCadastroViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var contato = new Contato();

                    contato.IdContato = Guid.NewGuid();
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.DataNascimento = model.DataNascimento.Value;
                    contato.Tipo = (TipoContato?)model.Tipo;
                    contato.Telefone = model.Telefone;
                    contato.IdUsuario = UsuarioAutenticado.IdUsuario;

                    var contatoRepository = new ContatoRepository();

                    contatoRepository.Add(contato);

                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";

                    model = new ContatoCadastroViewModel();

                    ModelState.Clear();


                }
                catch (Exception e) 
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar o contato: " + e.Message;
                
                }
            }
            else 
                {
                    TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento da agenda.";



                }
           
                                
                return View(model);
        }

        public IActionResult Consulta()
        {
            var Lista = new List<ContatoConsultaViewModel>();

            try 
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetByUsuario(UsuarioAutenticado.IdUsuario);


                foreach (var item in contato)
                {
                    var lista = new ContatoConsultaViewModel();

                    lista.IdContato = item.IdContato;
                    lista.Nome = item.Nome;
                    lista.Telefone = item.Telefone;
                    lista.Email = item.Email;
                    lista.DataNascimento = item.DataNascimento?.ToString("dd/MM/yyyy");
                    lista.Tipo = item.Tipo.ToString();

                    Lista.Add(lista);
                }

            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao consultar contatos: {e.Message}";

            }

            return View(Lista);
        
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                if (contato != null && contato.IdUsuario == UsuarioAutenticado.IdUsuario)
                {
                    contatoRepository.Delete(contato);

                    TempData["MensagemSucesso"] = "Contato Excluída com sucesso.";

                }

            }
            catch (Exception e) 
            {

                TempData["MensagemErro"] = "Falha ao excluir contato: " + e.Message;

            }
           

            return RedirectToAction("Consulta");
        }

     

        public IActionResult Edicao(Guid id) 
        {
            var model = new ContatoEdicaoViewModel();

            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                if(contato != null && contato.IdUsuario == UsuarioAutenticado.IdUsuario) 
                {
                   

                    model.Id = contato.IdContato;
                    model.Nome = contato.Nome;
                    model.Email = contato.Email;
                    model.DataNascimento = contato.DataNascimento;
                    model.Tipo = (int?)contato.Tipo;
                    model.Telefone = contato.Telefone;

                }
                else 
                {
                    TempData["MensagemErro"] = "Contato não encontrado.";
                    return RedirectToAction("Consulta");
                }
               

            }
            catch(Exception e) 
            {

                TempData["MensagemErro"] = "Falha ao exibir os dados do contato: " + e.Message;
                return RedirectToAction("Consulta");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ContatoEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try 
                { 
                    var contatoRepository = new ContatoRepository();
                    var contato = contatoRepository.GetById(model.Id);
                    if(contato != null && contato.IdUsuario == UsuarioAutenticado.IdUsuario)
                    {
                        contato.Nome = model.Nome;
                        contato.Email = model.Email;
                        contato.DataNascimento = model.DataNascimento;
                        contato.Tipo =(TipoContato?) model.Tipo.Value;
                        contato.Telefone = model.Telefone;
                        
                        contatoRepository.Update(contato);

                        TempData["MensagemSucesso"] = "Contato atualizado com sucesso.";
                        return RedirectToAction("Consulta");

                    }
                
                }
                catch(Exception e) 
                {
                    TempData["MensagemErro"] = "Falha ao atualizar contato: " + e.Message;

                }
            
            }

            return View(model);
        }

        private IdentityViewModel UsuarioAutenticado
        {
            get
            {
                var data = User.Identity.Name;
                return JsonConvert.DeserializeObject<IdentityViewModel>(data);
            }

        }

    }
}


