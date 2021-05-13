using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GFDSystems.Vigitech.DAO.CustomResponse
{
    /// <summary>
    /// Responde un estatus y mensaje
    /// </summary>
    public class StatusResponse
    {
        [System.ComponentModel.DefaultValue(HttpStatusCode.OK)]
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Responde un estatus y mensajen junto con un objeto de tipo dinamico 
    /// </summary>
    /// <typeparam name="TValue"> Representa un objeto el cual responder de acuerdo al tipo que se asigne</typeparam>
    public class StatusResponse<TValue>
    {
        [System.ComponentModel.DefaultValue(HttpStatusCode.OK)]
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public TValue Value { get; set; }
    }
}
