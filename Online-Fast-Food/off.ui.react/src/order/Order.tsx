import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { SelectUser } from "../auth/slice";
import { makeOrder } from "../common/models/order/makeOrder";
import { payForOrder } from "../common/models/order/payForOrder";
import { useAppSelector } from "../common/store/rootReducer";
import { makeOrderAction, payForOrderAction } from "./action";
import { SelectOrder } from "./slice";

export const Order = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentOrder = useAppSelector((state) => SelectOrder(state));
  const currentUser = useAppSelector((state) => SelectUser(state));

  let userOrder: makeOrder = {
    CustomerId: currentUser.id,
    ProductIdAndQuantity: { "prod_M2s8vq5ntsmN8W": 1, "prod_M3sPqG4H7ueOft": 2, }
  }
  const handleSubmit = () => async () => {
    dispatch(makeOrderAction(userOrder));
  }
  const handleSubmit2 = () => async () => {
    navigate("/payment", { state: { Id:currentOrder.orderId, CustomerId: currentUser.id } });
  }

  return (
    <>
      <h1>Jestę orderę</h1>
      <button onClick={handleSubmit()}>Order</button>
      <button onClick={handleSubmit2()}>Pay</button>
    </>
  );
};
