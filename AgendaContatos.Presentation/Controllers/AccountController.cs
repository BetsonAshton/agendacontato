using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Helpers;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Messages.Services;
using AgendaContatos.Presentation.Models;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AgendaContatos.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.getByEmailAndSenha(model.Email, MD5Helper.Encrypt(model.Senha));

                    if(usuario != null) //usuário encontrado
                    {
                        #region auntenticando o usuário
                        var identityViewModel = new IdentityViewModel();

                        identityViewModel.IdUsuario = usuario.IdUsuario;
                        identityViewModel.Nome = usuario.Nome;
                        identityViewModel.Email = usuario.Email;
                        identityViewModel.DataHoraAcesso = DateTime.Now;

                        //serializando os dados do usuário autenticado para Json
                        var claimsIdentity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(identityViewModel))
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravando cookie de autenticação
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        //Direcionandoo usuário pra Hom/Index
                        return RedirectToAction("Index", "Home");

                        #endregion


                    }
                    else 
                    {
                        TempData["MensagemAlerta"] = "Acesso negado, usuário não encontrado.";
                    }
                }
                catch (Exception e)
                {

                    TempData["MensagemErro"] = $"Falha ao autenticar usuário: {e.Message}";

                }

            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";

            }

            return View();
        }

        public IActionResult Register() 
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    var usuarioRepository = new UsuarioRepository();

                    if(usuarioRepository.GetByEmail(model.Email) !=null)
                    {
                        TempData["MensagemAlerta"] = "O email informado já está cadastrado no sistema, tente outro.";

                    }
                    else 
                    {
                        var usuario = new Usuario();
                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome= model.Nome;
                        usuario.Email= model.Email;
                        usuario.Senha = MD5Helper.Encrypt(model.Senha);

                        usuarioRepository.Add(usuario);

                        TempData["MensagemSucesso"] = "Parábens sua conta foi cadastrada com sucesso.";

                        ModelState.Clear();

                    }
                
                }
                catch(Exception e) 
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar usuário:{e.Message}";
                
                }

            }
            else
            {

                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário";

            }

            return View();
        }

        public IActionResult PasswordRecover() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(PasswordRecoverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if (usuario != null) 
                    {
                        var novaSenha = new Faker().Internet.Password();

                        var emailDest = usuario.Email;
                        var assunto = "Recuperação de senha - Agenda Contatos";
                        var mensagem = $@"
                            <h3>Olá, {usuario.Nome}</h3>
                            <p>Uma nova senha foi gerada com sucesso para o seu usuário.</p>
                            <p>Acesse o sistema com a senha: {novaSenha}</p>
                            <p>Após acessar o sistema, você pode alterar esta senha para uma nova de sua preferência.</p>
                            <br/>
                            <p>Att, <br/>Equipe Agenda Contatos</p>
                        ";

                        EmailService.EnviarMensagem(emailDest, assunto, mensagem);
                        usuarioRepository.Update(usuario.IdUsuario, MD5Helper.Encrypt(novaSenha));

                        TempData["MensagemSucesso"] = "Recuperação de senha realizada com sucesso.";
                        
                        ModelState.Clear();

                    }

                }
                catch (Exception e) 
                {
                    TempData["MensagemAlerta"] = "Usuário não encontrado, verifique o email informado.";

                }

            }
            return View();
        }

        //Account/Logout
        public IActionResult Logout()
        {
            //destruir o cookie de autenticação(identificação do usuário)
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar de volta para a página de login
            return RedirectToAction("Login", "Account");
        }
    }
}
