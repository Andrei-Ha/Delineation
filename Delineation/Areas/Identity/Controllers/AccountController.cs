﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomIdentity.ViewModels;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using Delineation.Models;

namespace CustomIdentity.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly string _oraclePirr2n;
        private readonly string _mssqlPirr2n;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _oraclePirr2n = _configuration.GetConnectionString("OraclePirr2n");
            _mssqlPirr2n = _configuration.GetConnectionString("MSsqlPirr2n");
        }
        [HttpGet]
        public IActionResult Register()
        {
            List<SelList> myList = new List<SelList>();
            using (OracleConnection con = new OracleConnection(_oraclePirr2n))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "select m_linom,fio,login from mail where m_linom!=777777 order by fio";
                    OracleDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            myList.Add(new SelList() { Id = reader["m_linom"].ToString(), Text = "" + reader["fio"].ToString() + " - " + reader["login"].ToString() + "@brestenergo.by" });
                        }
                    }
                }
            }
            /*using (SqlConnection con = new SqlConnection(_mssqlPirr2n))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "select m_linom,fio,login from dbo.mail where m_linom!=777777 order by fio";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        myList.Add(new SelList() { Id = reader["m_linom"].ToString(), Text = "" + reader["fio"].ToString() + " - " + reader["login"].ToString() + "@brestenergo.by" });
                    }
                    reader.Dispose();
                }
            }*/
            ViewData["Person"] = new SelectList(myList, "Id", "Text");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = model.Email, UserName = model.UserName, Linom = model.Linom };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, false);
                    //return RedirectToAction("Index", "Home",new { Area = "" });

                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // check. is email confirmed
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой Email!");
                        return View(model);
                    }
                    var result =
                        await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // проверяем, принадлежит ли URL приложению
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                }
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль!");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home",new { Area = "" });
            else
                return View("Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public IActionResult Index()
        {
            //ViewBag.userName = _signInManager.
            return View();
        }
        public IActionResult AccessDenied() => View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() => View();
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    //if user with this email is missing in Database, we hide this information
                    return View("ForgotPassword");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return Content("Для сброса пороля перейдите по ссылке в письме,отправленном вам на email.");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}