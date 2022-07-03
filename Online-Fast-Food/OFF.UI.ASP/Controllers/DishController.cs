using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Interfaces.Infrastructure;

namespace Online_Fast_Food.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DishController : ControllerBase
{
    private readonly IDishSrv _dishSrv;
    public DishController(IDishSrv dishSrv)
    {
        _dishSrv=dishSrv;
    }

    [HttpPost("addDish")]
    public ActionResult AddDish([FromForm] AddDishDTO addDishDTO)
    {
        return Ok(_dishSrv.AddDish(addDishDTO));
    }

    [HttpPost("editDish")]
    public ActionResult EditDish([FromForm] EditDishDTO editDishDTO)
    {
        return Ok(_dishSrv.EditDish(editDishDTO));
    }

    [HttpPost("getDishById")]
    public ActionResult GetDishById([FromBody] GetDishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.GetDishById(getDishDTO));
    }

    [HttpPost("getDiseshByCategory")]
    public ActionResult GetDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetDishesByCategory(getDishDTO));
    }

    [HttpPost("getDishes")]
    public ActionResult GetDishes()
    {
        return Ok(_dishSrv.GetDishes());
    }

    [HttpPost("getAvaibleDishesByCategory")]
    public ActionResult GetAvaibleDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetAvaibleDishesByCategory(getDishDTO));
    }

    [HttpPost("getAvaibleDishes")]
    public ActionResult GetAvaibleDishes()
    {
        return Ok(_dishSrv.GetAvaibleDishes());
    }

}
