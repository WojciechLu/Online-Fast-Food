import register from "../../models/user/register";
import { api } from "../../connectionString"

const controllerPath = "Account/";

export const authSrv = {
    //   async login(credential: login) {
    //     try {
    //       return await api
    //         .post(controllerPath + "login", credential)
    //         .then((r) => r.data);
    //     } catch (e) {
    //       console.error(e);
    //     }
    //   },

    async register(credential: register) {
        try {
            return await api
                .post(controllerPath + "register", credential)
                .then((r) => r.data);
        } catch (e) {
            console.error(e);
        }
    },
};
