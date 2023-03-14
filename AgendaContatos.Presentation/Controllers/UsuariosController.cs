using AgendaContatos.Data.Helpers;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Presentation.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public IActionResult MinhaConta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MinhaConta(AlterarSenhaViewModel model)
        {
            if(ModelState.IsValid) 
            {
                try
                {
                    var data = User.Identity.Name;
                    var identityViewModel = JsonConvert.DeserializeObject<IdentityViewModel>(data);

                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Update(identityViewModel.IdUsuario, MD5Helper.Encrypt(model.NovaSenha));

                    TempData["MensagemSucesso"] = "Senha alterada com sucesso.";

                    ModelState.Clear();

                }
                catch (Exception e) 
                {
                    TempData["MensagemErro"] = "Falha ao atualizar a senha do usuário: " + e.Message;   
                }
                
            }
            
            return View();
        }
    }
}
