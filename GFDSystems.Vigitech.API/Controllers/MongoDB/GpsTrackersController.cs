using System.Collections.Generic;
using System.Linq;
using GFDSystems.Vigitech.DAL.Entities.MongoDB;
using GFDSystems.Vigitech.DAL.Entities.Responses;
using GFDSystems.Vigitech.DAO.MongoDB;
using Microsoft.AspNetCore.Mvc;

namespace GFDSystems.Vigitech.API.Controllers.MongoDB
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpsTrackersController : ControllerBase
    {
        private readonly IGpsTrackerDAO _collectionMongoDAO;

        public GpsTrackersController(IGpsTrackerDAO collectionMongoDAO)
        {
            _collectionMongoDAO = collectionMongoDAO;
        }
        [HttpGet]
        public IList<LocationResponse> GetAll()
        {
            var dato =  _collectionMongoDAO.GetAll().ToList();
            return dato;
        }
        [HttpGet("near/{lat}/{lon}")] //{lat}/{long}
        public IList<GpsTrackerNear> GetNear(double lat, double lon)
        {
            var dato = _collectionMongoDAO.GetNear(lat,lon).ToList();
            return dato;
        }
        [HttpGet("Route/{id}")] //{tid}
        public IList<GpsTracker> GetRoute(string id)
        {
            var dato = _collectionMongoDAO.GetByIdAsync(id).ToList();
            return dato;
        }
        //19.0831311,-97.9747896
    }
}