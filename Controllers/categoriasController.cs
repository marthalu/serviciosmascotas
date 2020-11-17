using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using Domain;
using Infrastructure;

namespace Services.Controllers
{
    public class categoriasController : ApiController
    {
        private DB_A6A0DD_proyectonetEntities db = new DB_A6A0DD_proyectonetEntities();

        // GET: api/categorias
        public List<Categoria> Getcategorias()
        {
            return db.categorias.Select(x => new Categoria
            {
                id = x.id,
                descripcion = x.descripcion
            }).ToList();
        }
    }
}
