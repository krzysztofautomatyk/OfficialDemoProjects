using Microsoft.AspNetCore.Mvc;
using WebApplication_Basic_API.Models;
using WebApplication_Basic_API.Services;

namespace WebApplication_Basic_API.Controllers;


[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();

    // GET by Id action

    // | ASP.NET Coreaction result | HTTP status code | Description                                                                                                                                                                                                    |
    // |---------------------------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
    // | Ok is implied             | 200              | A product that matches the provided id parameter exists in the in-memory cache.The product is included in the response body in the media type, as defined in the accept HTTP request header (JSON by default). |
    // | NotFound                  | 404              | A product that matches the provided id parameter doesn't exist in the in-memory cache.                                                                                                                         |

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action

    // | ASP.NET Coreaction result | HTTP status code | Description                                                                                                                                                              |
    // |---------------------------|------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
    // | CreatedAtAction           | 201              | The pizza was added to the in-memory cache.The pizza is included in the response body in the media type, as defined in the accept HTTP request header (JSON by default). |
    // | BadRequest is implied     | 400              | The request body's pizza object is invalid.                                                                                                                              |

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // PUT action

    // | ASP.NET Coreaction result | HTTP status code | Description                                                     |
    // |---------------------------|------------------|-----------------------------------------------------------------|
    // | NoContent                 | 204              | The pizza was updated in the in-memory cache.                   |
    // | BadRequest                | 400              | The request body's Id value doesn't match the route's id value. |
    // | BadRequest is implied     | 400              | The request body's Pizza object is invalid.                     |

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE action

    // | ASP.NET Coreaction result | HTTP status code | Description                                                                          |
    // |---------------------------|------------------|--------------------------------------------------------------------------------------|
    // | NoContent                 | 204              | The pizza was deleted from the in-memory cache.                                      |
    // | NotFound                  | 404              | A pizza that matches the provided id parameter doesn't exist in the in-memory cache. |

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}

