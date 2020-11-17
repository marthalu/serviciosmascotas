using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using Domain;
using Infrastructure;

namespace Services.Controllers
{
    public class mascotasController : ApiController
    {
        private DB_A6A0DD_proyectonetEntities db = new DB_A6A0DD_proyectonetEntities();

        // GET: api/mascotas
        public List<Mascota> Getmascotas()
        {
            return db.mascotas.Select(x => new Mascota
            {
                id = x.id,
                nombre = x.nombre
            }).ToList();
        }
    }
}
