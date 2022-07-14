using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Common.Helpers;
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
    [Authorize(Roles = "Admin")]
    public ActionResult AddDish([FromForm] AddDishDTO addDishDTO)
    {
        return Ok(_dishSrv.AddDish(addDishDTO));
    }

    [HttpPost("editDish")]
    [Authorize(Roles = "Admin")]
    public ActionResult EditDish([FromForm] EditDishDTO editDishDTO)
    {
        return Ok(_dishSrv.EditDish(editDishDTO));
    }

    [HttpPost("getDishById")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetDishById([FromBody] DishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.GetDishById(getDishDTO));
    }

    [HttpPost("getDiseshByCategory")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetDishesByCategory(getDishDTO).DishesByCategory);
    }

    [HttpPost("getDishes")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetDishes()
    {
        return Ok(_dishSrv.GetDishes().DishesByCategory);
    }

    [HttpPost("getAvailableDishesByCategory")]
    [AllowAnonymous]
    public ActionResult GetAvailableDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetAvailableDishesByCategory(getDishDTO).DishesByCategory);
    }

    [HttpPost("getAvailableDishes")]
    [AllowAnonymous]
    public ActionResult GetAvailableDishes()
    {
        return Ok(_dishSrv.GetAvailableDishes().DishesByCategory);
    }

    [HttpPost("getUnavailableDishesByCategory")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetUnavailableDishesByCategory([FromBody] GetDishCategoryDTO getDishDTO)
    {
        return Ok(_dishSrv.GetUnavailableDishesByCategory(getDishDTO).DishesByCategory);
    }

    [HttpPost("getUnavailableDishes")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetUnavailableDishes()
    {
        return Ok(_dishSrv.GetUnavailableDishes().DishesByCategory);
    }

    [HttpPost("removeDishFromMenu")]
    [Authorize(Roles = "Admin")]
    public ActionResult RemoveDishFromMenu([FromBody] DishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.RemoveDishFromMenu(getDishDTO));
    }

    [HttpPost("returnDishBackToMenu")]
    [Authorize(Roles = "Admin")]
    public ActionResult ReturnDishBackToMenu([FromBody] DishIdDTO getDishDTO)
    {
        return Ok(_dishSrv.ReturnDishBackToMenu(getDishDTO));
    }

    [HttpPost("addToOrder")]
    [Authorize(Roles = "Admin, User")]
    public ActionResult AddToOrder([FromBody] AddToOrderDTO addToOrder)
    {
        return Ok(_dishSrv.AddToOrder(addToOrder));
    }

}

