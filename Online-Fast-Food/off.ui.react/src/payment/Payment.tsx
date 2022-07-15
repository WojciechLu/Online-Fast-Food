import {
  Elements,
  CardElement,
  useElements,
  useStripe
} from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useLocation } from "react-router-dom";
import { payForOrder } from "../common/models/order/payForOrder";
import payment from "../common/models/payment/pay";
import { paymentAction } from "./action";

const stripePromise = loadStripe("pk_test_51LHiyMA16oTjMyUCuUwCdk45AuV4EHhyWfBGAPlSv2MqJu0kP9Kietirf0CSSZ1u6yuJqVYIDdEEFghVqlMrDpya007FDcTptt");

const PaymentForm = () => {
  const stripe = useStripe();
  const elements = useElements();
  const dispatch = useDispatch();
  const location = useLocation();
  let pay: payForOrder = location.state as payForOrder
  var credits: payment = {
    Id: pay.Id,
    CustomerId: pay.CustomerId
  }
  
  console.log(pay)

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
      { <CardElement /> }
      <button onClick={handleSubmit(stripe, elements)}>Buy</button>
    </>
  );
}

export const StripePaymentForm  = (props: payForOrder) => (
  <Elements stripe={stripePromise}>
    <PaymentForm/>
  </Elements>
);