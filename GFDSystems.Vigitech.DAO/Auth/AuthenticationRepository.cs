using AutoMapper;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.CustomResponse;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Settings;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Auth
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentitySettings _identitySettings;
        private readonly ICustomSystemLog _customSystemLog;
        private readonly IMapper _mapper;

        public AuthenticationRepository(
            ApplicationDbContext context, UserManager<AppUser> userManager, ICustomSystemLog customSystemLog,
            IMapper mapper, IOptions<AppIdentitySettings> identitySettings)
        {
            _context = context;
            _userManager = userManager;
            _customSystemLog = customSystemLog;
            _mapper = mapper;
            _identitySettings = identitySettings.Value;
        }

        public async Task<StatusResponse> Login(string UserName, string Password)
        {
            StatusResponse response = new StatusResponse();
            AppUser user = null;
            try
            {
                user = await _userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Message = "Usuario o contraseña incorrectos. Favor de verificar";
                    return response;
                }
                else
                {
                    if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
                    {
                        var LockEnd = await _userManager.GetLockoutEndDateAsync(user);
                        response.StatusCode = System.Net.HttpStatusCode.Conflict;
                        response.Message = string.Format("La cuenta se bloqueó temporalmente por seguridad. Intente dentro de {0} minutos", Math.Round((LockEnd.Value - DateTimeOffset.Now).TotalMinutes));
                        return response;
                    }
                    if (await _userManager.CheckPasswordAsync(user, Password))
                    {
                        if (!user.IsActive)
                        {
                            response.StatusCode = System.Net.HttpStatusCode.Conflict;
                            response.Message = "Su cuenta ha sido bloqueada permanentemente. Favor de comunicarse a servicios de Cuatlancingo";
                            return response;
                        }
                        if (_userManager.SupportsUserLockout && await _userManager.GetAccessFailedCountAsync(user) > 0)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                        }
                        //TODO: ver respuesta
                        var Entitykey = await _context.ApiKeyUsers.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
                        Entitykey.IsActive = true;
                        _context.Entry(Entitykey).State = EntityState.Modified;
                        _context.SaveChanges();

                        response.StatusCode = System.Net.HttpStatusCode.OK;
                        response.Message = "Inicio de sesión exitoso";
                        return response;
                    }
                    else
                    {
                        if (_userManager.SupportsUserLockout && await _userManager.GetLockoutEnabledAsync(user))
                        {

                            var contador = await _userManager.GetAccessFailedCountAsync(user);
                            if (await _userManager.GetAccessFailedCountAsync(user) >= 4)
                            {
                                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddMinutes(_identitySettings.Lockout.DefaultLockoutTimeSpanInMins));
                                await _userManager.ResetAccessFailedCountAsync(user);
                                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                                response.Message = string.Format("Su cuenta ha sido bloqueada termporalmente. Intente despues de {0} minutos", _identitySettings.Lockout.DefaultLockoutTimeSpanInMins);
                                return response;
                            }
                            else
                            {
                                await _userManager.AccessFailedAsync(user);
                                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                                response.Message = string.Format("Solo quedan {0} intentos antes de bloquear la cuenta", (_identitySettings.Lockout.MaxFailedAccessAttempts - await _userManager.GetAccessFailedCountAsync(user)));
                                return response;

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = e.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(new { UserName = UserName, Password = Password });
                _customSystemLog.AddLog(systemLog);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = "Error al intentar ingresar en la aplicación";
            }
            
            return response;
        }
    }
}
