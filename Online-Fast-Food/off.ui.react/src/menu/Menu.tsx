import { useDispatch } from "react-redux";
import { PriceButton } from "../common/components/buttons/priceButton";
import { CategoryNameContainer } from "../common/components/containers/categoryNameContainer";
import { DishContainer } from "../common/components/containers/dishContainter";
import { DishesContainter } from "../common/components/containers/dishesContainter";
import { MenuContainer } from "../common/components/containers/menuContainter";
import { CategoriesContainer } from "../common/components/containers/categoriesContainer";
import dish from "../common/models/menu/dish";
import { useAppSelector } from "../common/store/rootReducer";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";
import { SelectAllDishes } from "./selectors";
import { addToOrder } from "../order/slice";

export const Menu = () => {
  const dispatch = useDispatch();
  const dishes = useAppSelector((state) => SelectAllDishes(state)).dishesByCategory;

  const handleSubmit = () => async () => {
    dispatch(getAvailableDishesAction())
  };

  const handleAdd = (dish: dish) => {
    let indexOfId: number;
    indexOfId = Object.keys(dish).indexOf('id');
    dispatch(addToOrder(dish));
  };

  return (
    <MenuContainer>
      {dishes.length === 0 && (
        <>
          <h1>Jestę menu</h1>
          <button onClick={handleSubmit()}>Get menu</button>
        </>
      )}
      {
        Object.keys(dishes).map((key, index) => {
          return (
            <>
              {dishes[key].length > 0 && (
                <>
                  <CategoryNameContainer>{key}</CategoryNameContainer>
                  <DishesContainter>
                    {
                      dishes[key].map((dishes: dish[]) => {
                        let description;
                        if (dishes.description === null) description = "Lorem ipsum dolor sit amet, consectetur."
                        else description = dishes.description;
                        return (
                          <DishContainer>
                            <h2 className="dishName">{dishes.name}</h2>
                            <img className="dishImg" src={`data:image/jpeg;base64,${dishes.productImage}`} />
                            <p className="dishDescription">{description}</p>
                            <CategoriesContainer>
                              {dishes.categories.map((category: string) => {
                                return <div className="dishCategory">{category}</div>
                              })}
                            </CategoriesContainer>
                            <PriceButton onClick={() => handleAdd(dishes)}>{dishes.price} PLN</PriceButton>
                          </DishContainer>
                        )
                      }
                      )
                    }
                  </DishesContainter>
                </>
              )}
            </>
          )
        }
        )}
    </MenuContainer>
  );
};


