import { useDispatch } from "react-redux";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";

export const Menu = () => {

  const dispatch = useDispatch();
  const handleSubmit = () => async () => {
    console.log("menu");
    dispatch(getAvailableDishesAction())
  };

  return (
    <>
      <h1>Jestę menu</h1>
      <button onClick={handleSubmit()}>Get menu</button>
    </>
  );
};


