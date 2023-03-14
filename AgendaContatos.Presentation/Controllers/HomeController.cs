using AgendaContatos.Data.Repositories;
using AgendaContatos.Presentation.Models;
using AgendaContatos.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var lista = new List<DashboardModel>();

            try
            {

                var usuario = JsonConvert.DeserializeObject<IdentityViewModel>(User.Identity.Name);


                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAllByUsuario(usuario.IdUsuario);

                //os dados irão inicalizar com valor 0

                var totalAmigos = 0;
                var totalFamilia = 0;
                var totalTrabalho = 0;
                var totalOutros = 0;

                foreach (var item in contatos)
                {

                    switch (item.Tipo)
                    {
                        case TipoContato.Amigos:
                            totalAmigos++;
                            break;

                        case TipoContato.Trabalho:
                            totalTrabalho++;
                            break;

                        case TipoContato.Familia:
                            totalFamilia++;
                            break;

                        default:
                            totalOutros++;
                            break;

                    }

                    
                }


                lista.Add(new DashboardModel { TipoContato = "Amigos", Quantidade = totalAmigos });
                lista.Add(new DashboardModel { TipoContato = "Trabalho", Quantidade = totalTrabalho });
                lista.Add(new DashboardModel { TipoContato = "Familia", Quantidade = totalFamilia });
                lista.Add(new DashboardModel { TipoContato = "Outros", Quantidade = totalOutros });




            }
            catch (Exception e) 
            {
                TempData["MensagemErro"] = "Falha ao gerar dashboard: "+ e.Message;

            }

            return View(lista);
        }


    }
}
