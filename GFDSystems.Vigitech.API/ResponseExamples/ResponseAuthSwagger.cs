using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAO.CustomResponse;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.ResponseExamples
{

    #region FirstRegister
    public class SuccessResponseFirstRegister : IExamplesProvider<Response>
    {
        public Response GetExamples()
        {
            return new Response
            {
                Message = "Usuario agregado satisfactoriamente",
                Status = "Ok"
            };
        }
    }

    #endregion

    #region CompleteRegister
    public class SuccessResponseCompleteRegister : IExamplesProvider<Response>
    {
        public Response GetExamples()
        {
            return new Response
            {
                Message = "Registro Completado Correctamente",
                Status = "Ok"
            };
        }
    }

    public class BadResponseCompleteRegister : IExamplesProvider<Response>
    {
        public Response GetExamples()
        {
            return new Response
            {
                Message = "No se pudo completar registro",
                Status = "Error"
            };
        }
    }

    #endregion

    #region VerifyRegisterCode
    public class SuccessResponseVerifyCode : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Validación exitosa",
                apiKey = "abcdAWETR0147PouyVBhLp8932Tv7M"
            };
        }
    }

    public class BadResponseVerifyCode : IExamplesProvider<Response>
    {
        public Response GetExamples()
        {
            return new Response
            {
                Status = "Error",
                Message = "Código proporcionado no coincide, favor de verificarlo"
            };
        }
    }
    #endregion
}
