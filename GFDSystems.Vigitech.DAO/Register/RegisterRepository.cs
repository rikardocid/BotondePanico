using AutoMapper;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.Usuarios;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.CustomResponse;
using GFDSystems.Vigitech.DAO.Models;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Security;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace GFDSystems.Vigitech.DAO.Register
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomSystemLog _customSystemLog;
        private readonly IUserToolkitRepository _userToolkitRepository;
        private readonly IMapper _mapper;
        public RegisterRepository(ApplicationDbContext context, UserManager<AppUser> userManager, ICustomSystemLog customSystemLog, IMapper mapper, IUserToolkitRepository userToolkitRepository)
        {
            _context = context;
            _userManager = userManager;
            _customSystemLog = customSystemLog;
            _mapper = mapper;
            _userToolkitRepository = userToolkitRepository;
        }

        public async Task<StatusResponse<AppUser>> FirstRegister(PreRegisterDAO registerDAO)
        {
            var response = new StatusResponse<AppUser>();
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    AppUser user = new AppUser()
                    {
                        Email = registerDAO.Email,
                        Type = "TU004",
                        UserName = registerDAO.Email,
                        PhoneNumber = registerDAO.PhoneNumber,
                        Name = "PRE-REGISTER",
                        MiddleName = "PRE-REGISTER",
                        LastName = "PRE-REGISTER",
                        AuthValidationCode = KeyGenerator.GetUniqueKey(10),
                        IsActive = true
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, registerDAO.Password);
                    if (result.Succeeded)
                    {
                        MobileDeviceRegistrationTemp registrationTemp = new MobileDeviceRegistrationTemp()
                        {
                            AppUserId = user.Id,
                            CellComapny = registerDAO.CellCompany,
                            DateRegister = DateTime.Now,
                            DeviceId = registerDAO.DeviceId,
                            MakeModel = registerDAO.MakeModel,
                            NumberPhone = registerDAO.PhoneNumber,
                            LatLangRegister = string.Format("{0},{1}", registerDAO.Latitude, registerDAO.Longitude),
                            Platform = registerDAO.Platform,
                            VersionOS = registerDAO.VersionOS
                        };
                        _context.MobileDeviceRegistrationTemps.Add(registrationTemp);


                        _context.SaveChanges();

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Usuario agregado satisfactoriamente";
                        response.Value = user;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = string.Join(",", UtilitiesAIO.AddErrors(result));
                        response.Value = null;
                    }

                    scope.Complete();
                }
               
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = e.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(registerDAO);
                _customSystemLog.AddLog(systemLog);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Error al intentar hacer el registro";
                response.Value = null;
            }


            return response;
        }

        public async Task<StatusResponse<string>> VerifyRegisterCode(string Code, string deviceId)
        {
            var response = new StatusResponse<string>();
            var user = await _context.appUsers.Where(x => x.AuthValidationCode == Code).FirstOrDefaultAsync();
            var mobile = await _context.MobileDeviceRegistrationTemps.Where(x => x.DeviceId == deviceId).FirstOrDefaultAsync();

            if(user != null && mobile != null)
            {
                if(user.PhoneNumber == mobile.NumberPhone)
                {
                    user.IsVerified = true;
                    user.EmailConfirmed = true;
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();

                    ApiKeyUser apiKey = new ApiKeyUser()
                    {
                        AppUserId = user.Id,
                        User = user,
                        IsActive = false,
                        Key = KeyGenerator.GetUniqueKey(30)
                    };

                    _context.ApiKeyUsers.Add(apiKey);
                    _context.SaveChanges();

                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "Validación exitosa";
                    response.Value = apiKey.Key;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "El código no coicide con los datos registrados, favor de verificar";
                    response.Value = null;
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Código proporcionado no coincide o dispositivo diferente en el que inicio sesión, favor de verificarlo";
                response.Value = null;
            }
            return response;
        }

        public async Task<StatusResponse<bool>> CompleteRegister(CompleteRegisterDAO completeRegisterDAO)
        {
            var response = new StatusResponse<bool>();
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var address = _mapper.Map<Address>(completeRegisterDAO.AddressInfo);
                    var medical = _mapper.Map<MedicalRecord>(completeRegisterDAO.MedicalInfo);
                    var contacts = _mapper.Map<List<EmergencyContact>>(completeRegisterDAO.ContactInfos);

                    var validDevice = await _context.MobileDeviceRegistrationTemps
                                            .Where(x => x.DeviceId == completeRegisterDAO.DeviceId && x.IsCompleteRegister == false)
                                            .FirstOrDefaultAsync();
                    if (validDevice != null)
                    {
                        var user = await _context.appUsers.FindAsync(validDevice.AppUserId);
                        user.Name = completeRegisterDAO.PersonalInfo.Name;
                        user.MiddleName = completeRegisterDAO.PersonalInfo.MiddleName;
                        user.LastName = completeRegisterDAO.PersonalInfo.LastName;
                        //TODO: completar con datos de la tabla [AspNetUsers] de los campos de firebase para poder enviar y recibir notificaciones

                        Citizen citizen = new Citizen
                        {
                            AspNetUserId = user.Id,
                            appUser = user,
                            CURP = completeRegisterDAO.PersonalInfo.Curp,
                            DateOfBirth = completeRegisterDAO.PersonalInfo.DateOfBirth,
                            Sex = completeRegisterDAO.PersonalInfo.Sex
                        };

                        _context.Entry(user).State = EntityState.Modified;
                        _context.citizens.Add(citizen);
                        await _context.SaveChangesAsync();

                        var suburb = await _context.suburbs.FindAsync(address.SuburbId);

                        if (suburb == null)
                        {
                            response.StatusCode = HttpStatusCode.NotFound;
                            response.Message = "Validar envio de colonia para poder completar su registro";
                            response.Value = false;
                            return response;
                        }

                        address.CitizenId = citizen.CitizenId;
                        address.citizen = citizen;

                        medical.CitizenId = citizen.CitizenId;
                        medical.citizen = citizen;

                        contacts.ForEach(x =>
                        {
                            x.CitizenId = citizen.CitizenId;
                            x.citizen = citizen;
                        });

                        await _context.addresses.AddAsync(address);
                        await _context.medicalRecords.AddAsync(medical);
                        await _context.emergencyContacts.AddRangeAsync(contacts);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = "El dispositico donde incio el registro no es mismo, favor de verificar para continuar con el registro";
                        response.Value = false;
                        return response;
                    }

                    validDevice.IsCompleteRegister = true;
                    _context.Entry(validDevice).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    scope.Complete();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "Registro completado correctamente su cuenta sera validada en breve, agradecemos su comprensión";
                    response.Value = true;

                    return response;
                }
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = e.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(completeRegisterDAO);
                _customSystemLog.AddLog(systemLog);
            }
            return null;
        }

        #region Register user for use into of platform web
        public async Task<StatusResponse<AppUser>> RegisterUserPlatform(RegisterUserPlatformDAO registerUser, string userName, string password, string type)
        {
            var response = new StatusResponse<AppUser>();
            try
            {
                AppUser user = new AppUser()
                {
                    Email = registerUser.Email,
                    Type = type,
                    UserName = userName,
                    Name = registerUser.Name,
                    MiddleName = registerUser.LastName,
                    LastName = registerUser.SecondLastName,
                    AuthValidationCode = password,
                    IsActive = true,
                    Area = registerUser.Employment
                };
                IdentityResult result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    result = null;
                    result = await _userManager.AddToRoleAsync(user, registerUser.RoleName);
                    if (result.Succeeded)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Usuario agregado satisfactoriamente";
                        response.Value = user;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = string.Join(",", UtilitiesAIO.AddErrors(result));
                        response.Value = null;
                    }
                   
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = string.Join(",", UtilitiesAIO.AddErrors(result));
                    response.Value = null;
                }
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = e.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(new { Register = registerUser, User = userName, Password = password });
                _customSystemLog.AddLog(systemLog);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Error al intentar hacer el registro";
            }
            return response;
        }

        public async Task<StatusResponse<List<RegisterUserPlatformDAO>>> GetAllUsers()
        {
            var types = new string[] { "TU001", "TU002" };
            var users = await _context.appUsers.Where(x => types.Contains(x.Type)).ToListAsync();
            List<RegisterUserPlatformDAO> registers = new List<RegisterUserPlatformDAO>();
            foreach (var item in users)
            {
                var role = await _userManager.GetRolesAsync(item);
                registers.Add(new RegisterUserPlatformDAO
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    Employment = item.Area,
                    Name = item.Name,
                    LastName = item.MiddleName,
                    SecondLastName = item.LastName,
                    RoleName = string.Join(",", role),
                    IsActive = item.IsActive,
                    Id = item.Id
                });
            }
            return new StatusResponse<List<RegisterUserPlatformDAO>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "List Completa de usuarios registrados",
                Value = registers
            };
        }

        public async Task<StatusResponse> UpdateUser(UpdateInformationFromUserDAO update)
        {
            var user = await _userManager.FindByNameAsync(update.UserName);
            user.IsActive = update.IsActive;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            if (update.IsRoleReversal)
            {
                var roles = await _userManager.GetRolesAsync(user);
                bool IsSuccess = true;
                IdentityResult removeRole = null;
                IdentityResult addRole = null;

                foreach (var item in roles)
                {
                    if (item == update.OldRoleName)
                    {
                        removeRole = await _userManager.RemoveFromRoleAsync(user, update.OldRoleName);
                        addRole = await _userManager.AddToRoleAsync(user, update.NewRoleName);
                    }
                    if (!removeRole.Succeeded || !addRole.Succeeded)
                    {
                        IsSuccess = false;
                    }
                }

                if (!IsSuccess)
                {
                    return new StatusResponse
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        Message = "Error al intentar actualizar informacion del usuario, favor de verificar"
                    };
                }
                else
                {
                    return new StatusResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Información actualizada correctamente"
                    };
                }
            }
            else
            {
                return new StatusResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Información actualizada correctamente"
                };
            }
            
        }

        #endregion
    }
}
