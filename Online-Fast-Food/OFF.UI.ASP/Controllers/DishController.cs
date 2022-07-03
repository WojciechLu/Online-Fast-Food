using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Common.Helpers;
using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Interfaces.Infrastructure;

namespace Online_Fast_Food.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
//[Authorize(Roles = "Admin, User")]

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
    public ActionResult GetDishById([FromBody] DishIdDTO getDishDTO)
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

    [HttpPost("getAvailableDishesByCategory")]
    [AllowAnonymous]
    public ActionResult GetAvailableDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetAvailableDishesByCategory(getDishDTO));
    }

    [HttpPost("getAvailableDishes")]
    [AllowAnonymous]
    public ActionResult GetAvailableDishes()
    {
        return Ok(_dishSrv.GetAvailableDishes());
    }

    [HttpPost("getUnavailableDishesByCategory")]
    public ActionResult GetUnavailableDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetUnavailableDishesByCategory(getDishDTO));
    }

    [HttpPost("getUnavailableDishes")]
    public ActionResult GetUnavailableDishes()
    {
        return Ok(_dishSrv.GetUnavailableDishes());
    }

    [HttpPost("removeDishFromMenu")]
    public ActionResult RemoveDishFromMenu([FromBody] DishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.RemoveDishFromMenu(getDishDTO));
    }

    [HttpPost("returnDishBackToMenu")]
    public ActionResult ReturnDishBackToMenu([FromBody] DishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.ReturnDishBackToMenu(getDishDTO));
    }

}

