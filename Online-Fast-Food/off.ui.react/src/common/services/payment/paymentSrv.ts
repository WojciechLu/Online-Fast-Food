import register from "../../models/user/register";
import { api } from "../../connectionString"
import login from "../../models/user/login";
import { loadStripe } from "@stripe/stripe-js";
import { useStripe } from "@stripe/react-stripe-js";
import payment from "../../models/payment/pay";

const controllerPath = "Order/";


export const paymentSrv = {
      async pay(credits: payment) {
        try {
          const stripePromise = loadStripe("pk_test_51LHiyMA16oTjMyUCuUwCdk45AuV4EHhyWfBGAPlSv2MqJu0kP9Kietirf0CSSZ1u6yuJqVYIDdEEFghVqlMrDpya007FDcTptt");
          const stripe = await stripePromise;
          return await api
            .post(controllerPath + "payForOrder", credits)
            .then((r) => r.data)
            .then((session) => {
              stripe!.redirectToCheckout(session);
            });
        } catch (e) {
          console.error(e);
        }
      },
};

