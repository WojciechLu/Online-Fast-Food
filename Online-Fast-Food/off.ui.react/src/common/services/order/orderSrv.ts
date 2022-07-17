import { api } from "../../connectionString"
import { makeOrder } from "../../models/order/makeOrder";
import { payForOrder } from "../../models/order/payForOrder";

const controllerPath = "Order/";

export const orderSrv = {
  async makeOrder(credential: makeOrder) {
    try {
      return await api
        .post(controllerPath + "makeOrder", credential)
        .then((r) => r.data);
    } catch (e) {
      console.error(e);
    }
  },
  async payForOrder(credential: payForOrder) {
    try {
      return await api
        .post(controllerPath + "payForOrder", credential)
        .then((r) => r.data);
    } catch (e) {
      console.error(e);
    }
  },
};
