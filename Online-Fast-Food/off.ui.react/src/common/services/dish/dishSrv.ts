import { api } from "../../connectionString"
import dish from "../../models/menu/dish";
import Dictionary from "../../models/menu/dishList";

const controllerPath = "Dish/";

export const dishSrv = {
      async getAvailableDishes() {
        try {
          return await api
            .post(controllerPath + "getAvailableDishes")
            .then((r) => r.data as Dictionary<dish[]>);
        } catch (e) {
          console.error(e);
        }
      },
};
