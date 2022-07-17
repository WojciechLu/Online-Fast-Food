import { useDispatch } from "react-redux";
import { PriceButton } from "../common/components/buttons/priceButton";
import { CategoryContainer } from "../common/components/containers/categoryContainer";
import { DishContainer } from "../common/components/containers/dishContainter";
import { DishesContainter } from "../common/components/containers/dishesContainter";
import { MenuContainer } from "../common/components/containers/menuContainter";
import dish from "../common/models/menu/dish";
import Dictionary from "../common/models/menu/dishList";
import { useAppSelector } from "../common/store/rootReducer";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";
import { SelectAllDishes } from "./selectors";

export const Menu = () => {
  const dispatch = useDispatch();
  //dispatch(getAvailableDishesAction())
  const dishes = useAppSelector((state) => SelectAllDishes(state)).dishesByCategory;
  const handleSubmit = () => async () => {
    dispatch(getAvailableDishesAction())
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
                  <CategoryContainer>{key}</CategoryContainer>
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
                            <PriceButton>{dishes.price} PLN</PriceButton>
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


