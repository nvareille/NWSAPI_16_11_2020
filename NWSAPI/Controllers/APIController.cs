using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NWSAPI.Entities;
using NWSAPI.Filters;

namespace NWSAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : Controller
    {
        public async Task<JsonResult> Index([FromBody]Pagination? pagination)
        {
            HttpClient client = new HttpClient();
            string str = await client.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
            IEnumerable<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(str);

            if (pagination != null)
            {
                if (pagination.Title != null)
                    photos = photos.Where(i => i.Title.Contains(pagination.Title));

                int skip = pagination.Elements * (pagination.Page - 1);

                photos = photos.Skip(skip).Take(pagination.Elements);
            }

            return (Json(photos));
        }
    }
}
