import { useDispatch } from "react-redux";
import dish from "../common/models/menu/dish";
import Dictionary from "../common/models/menu/dishList";
import { useAppSelector } from "../common/store/rootReducer";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";
import { SelectAllDishes } from "./selectors";

export const Menu = () => {
  const dispatch = useDispatch();
  const dishes = useAppSelector((state) => SelectAllDishes(state)).dishesByCategory;

  const handleSubmit = () => async () => {
    dispatch(getAvailableDishesAction())
  };

  return (
    <>
      {dishes && (
        <>
          <h1>Jestę menu</h1>
          <button onClick={handleSubmit()}>Get menu</button>
        </>
      )}
      {
        Object.keys(dishes).map((key, index) => {
          return (
            <><h1>{key}</h1>
              {
                dishes[key].map((dishes: dish[]) => {
                  
                  return (
                    <div>
                      <img src={`data:image/jpeg;base64,${dishes.productImage}`} />
                      <img className={dishes.id}></img>
                      <h2>{dishes.name}</h2>
                    </div>
                  )
                }
                )
              }
            </>
          )
        }
        )}
    </>
  );
};


