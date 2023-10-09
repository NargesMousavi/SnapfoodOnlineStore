using Microsoft.AspNetCore.Mvc;
using OnlineStore.Presentation.Dtos;

namespace OnlineStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet("{id}")]
        public ProductDto Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] ProductDto productDto)
        {
        }
        [HttpPut("{id}")]
        public void IncreseInventory(int id, int count)
        {
        }
        [HttpPut("{id}")]
        public void Buy(int id, [FromBody] ProductDto productDto)
        {
        } 
    }
}
