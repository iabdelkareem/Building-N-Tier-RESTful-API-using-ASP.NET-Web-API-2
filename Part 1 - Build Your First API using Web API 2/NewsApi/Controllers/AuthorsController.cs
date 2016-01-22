using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NewsApi.Models;
using NewsApi.ViewModels;

namespace NewsApi.Controllers
{
    public class AuthorsController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (NewsDbContext ctx = new NewsDbContext())
            {
                var authors = await ctx.Authors.Select(o => new AuthorViewModel()
                {
                   Id = o.Id,
                   Name = o.Name
                }).ToListAsync();

                return Ok(authors);
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            using (var ctx = new NewsDbContext())
            {
                var author = await ctx.Authors.FirstOrDefaultAsync(o => o.Id == id);
                if (author == null)
                    return NotFound();

                var data = new AuthorViewModel()
                {
                    Id = author.Id,
                    Name = author.Name
                };

                return Ok(data);
            }
        }
        public async Task<IHttpActionResult> Post(CreateAuthorViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new NewsDbContext())
            {
                var author = new Author()
                {
                    Name = model.Name
                };

                ctx.Authors.Add(author);
                await ctx.SaveChangesAsync();
                
                var data = new AuthorViewModel()
                {
                    Id = author.Id,
                    Name =  author.Name
                };

                return Created(new Uri(Request.RequestUri + "api/authors" + data.Id), data);
            }
        }

        public async Task<IHttpActionResult> Put(EditAuthorViewModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new NewsDbContext())
            {
                var author = new Author()
                {
                    Id = model.Id.Value,
                    Name = model.Name
                };

                ctx.Authors.Attach(author);
                ctx.Entry(author).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var ctx = new NewsDbContext())
            {
                var author = await ctx.Authors.FirstOrDefaultAsync(o => o.Id == id);
                if (author == null)
                    return NotFound();

                ctx.Authors.Remove(author);
                await ctx.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}