// import { WindowSharp } from "@mui/icons-material";
// import { useDispatch } from "react-redux";
// import { SubmitButton } from "../common/components/buttons/submitButton";
// import { RegisterFormContainer } from "../common/components/containers/registerFormContainer";
// import { paymentAction } from "./action";

// export const Payment = () => {
//     const dispatch = useDispatch();
//     const handleSubmit = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
//         // dispatch(paymentAction());
//     };

//     return (
//         <RegisterFormContainer>
//             <p>Payment</p>
//             <SubmitButton onClick={(e) => handleSubmit(e)}>
//                 Pay
//             </SubmitButton>
//         </RegisterFormContainer>
//     );
// }

import {
  Elements,
  CardElement,
  useElements,
  useStripe
} from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { useState } from "react";
import { useDispatch } from "react-redux";
import payment from "../common/models/payment/pay";
import { paymentAction } from "./action";

const stripePromise = loadStripe("pk_test_51LHiyMA16oTjMyUCuUwCdk45AuV4EHhyWfBGAPlSv2MqJu0kP9Kietirf0CSSZ1u6yuJqVYIDdEEFghVqlMrDpya007FDcTptt");

const PaymentForm = () => {
  const stripe = useStripe();
  const elements = useElements();
  const dispatch = useDispatch();
  var credits: payment = {
    PriceId: "price_1LJuAtA16oTjMyUCqtnZnGA1"
  }

  const handleSubmit = (stripe: any, elements: any) => async () => {
    const cardElement = elements.getElement(CardElement);
  
    const {error, paymentMethod} = await stripe.createPaymentMethod({
      type: 'card',
      card: cardElement,
    });
  
    if (error) {
      console.log('[error]', error);
    } else {
      console.log('[PaymentMethod]', paymentMethod);
      dispatch(paymentAction(credits));
    }
  };
  return (
    <>
      <h1>stripe form</h1>
      <CardElement />
      <button onClick={handleSubmit(stripe, elements)}>Buy</button>
    </>
  );
}

export const StripePaymentForm  = () => (
  <Elements stripe={stripePromise}>
    <PaymentForm />
  </Elements>
);