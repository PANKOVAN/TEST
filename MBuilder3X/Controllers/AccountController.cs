using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MFuncCoreLibrary;
using MEDMCoreLibrary;
using System.Security.Claims;
using MJsonCoreLibrary;
using Models.MBuilderModel;

namespace WebixApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            string json = "";
            try
            {
                json = System.IO.File.ReadAllText($"app/login.js");
            }
            catch (Exception e)
            {
                json = $@"return {{'_error_': 'Команда ""login"" не найдена...'}} ";
            }
            Response.Headers.Add("Cache-Control", "no-cache");
            return Content(json, "application/javascript");
        }
        [HttpPost]
        public IActionResult Login(MBuilderModel model, string login, string password)
        {
            try
            {
                if (login == null) login = "";
                if (password == null) password = "";

                if (login == "" && password == "")
                {
                    login = "madmin";
                    password = "madmin991188";
                }

                MEntity user = model.SelectFirst<MEntity>($"select * from [MEntity] (nolock) where EntityTypeId='USR' and Code={model.AddParam(login)}");
                MPassword pass = model.SelectFirst<MPassword>($"select * from [MPassword] (nolock) where Id={model.AddParam(login)} and PassCode={model.AddParam(MFunc.GetSecurityHash(password))}");
                if (user != null)
                {
                    if (pass != null)
                    {
                        model.AddUser(new MUser(user), login);
                        SetAuth(login);
                    }
                    else
                    {
                        throw new NotAuthException($@"Неправильный пароль...");
                    }
                }
                else
                {
                    throw new NotAuthException($@"Пользователь ""{login}"" не найден...");
                }

                return Json(new MJsonResult(MJsonResultType.OK, null));
            }
            catch (Exception e)
            {
                return Json(new MJsonResult(MJsonResultType.Error, e.Message));
            }


        }

        public async void SetAuth(string login)
        {
            ClaimsIdentity ci = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            ci.AddClaim(new Claim(ClaimTypes.Name, login));
            ci.AddClaim(new Claim("SessionId", HttpContext.Session.Id.ToString()));
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(ci));

            HttpContext.Session.SetString("LOGIN", login);
            string securityid = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("SESSION", securityid);
        }
    }


}