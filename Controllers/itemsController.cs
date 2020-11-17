using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Infrastructure;
using Domain;

namespace Services.Controllers
{
    public class itemsController : ApiController
    {
        private DB_A6A0DD_proyectonetEntities db = new DB_A6A0DD_proyectonetEntities();

        // GET: api/items
        public List<Items> Getitems()
        {
            return db.items.Select(x => new Items
            {
                id = x.id,
                categoria = new Categoria { id = x.categorias.id, descripcion = x.categorias.descripcion },
                mascota = new Mascota { id = x.mascotas.id, nombre = x.mascotas.nombre },
                descripcion = x.descripcion,
                precio = x.precio
            }).ToList();
        }

        // GET: api/items/5
        [ResponseType(typeof(Items))]
        public async Task<IHttpActionResult> Getitems(int id)
        {
            items x = await db.items.FindAsync(id);
            if (x == null)
            {
                return NotFound();
            }
            return Ok(new Items
            {
                id = x.id,
                categoria = new Categoria { id = x.categorias.id, descripcion = x.categorias.descripcion },
                mascota = new Mascota { id = x.mascotas.id, nombre = x.mascotas.nombre },
                descripcion = x.descripcion,
                precio = x.precio
            });
        }

        // PUT: api/items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putitems(int id, Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            items item = new items();
            item.id = items.id;
            item.fk_categoria = items.categoria.id;
            item.fk_mascota = items.mascota.id;
            item.descripcion = items.descripcion;
            item.precio = items.precio;

            if (id != item.id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/items
        [ResponseType(typeof(items))]
        public async Task<IHttpActionResult> Postitems(Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            items item = new items();
            item.id = items.id;
            item.fk_categoria = items.categoria.id;
            item.fk_mascota = items.mascota.id;
            item.descripcion = items.descripcion;
            item.precio = items.precio;

            db.items.Add(item);
            await db.SaveChangesAsync();

            return Ok(new { id = item.id });
        }

        // DELETE: api/items/5
        [ResponseType(typeof(items))]
        public async Task<IHttpActionResult> Deleteitems(int id)
        {
            items items = await db.items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            db.items.Remove(items);
            await db.SaveChangesAsync();

            return Ok(items);
        }

        private bool itemsExists(int id)
        {
            return db.items.Count(e => e.id == id) > 0;
        }
    }
}
