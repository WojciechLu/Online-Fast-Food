import { api } from "../../connectionString"

const controllerPath = "Dish/";

export const dishSrv = {
      async getAvailableDishes() {
        try {
          return await api
            .post(controllerPath + "getAvailableDishes")
            .then((r) => r.data);
        } catch (e) {
          console.error(e);
        }
      },
};
