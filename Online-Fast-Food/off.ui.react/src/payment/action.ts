import { createAsyncThunk } from "@reduxjs/toolkit";
import { PAY } from "../common/consts/actionTypes";
import { paymentSrv } from "../common/services/payment/paymentSrv";
import payment from "../common/models/payment/pay"

export const paymentAction = createAsyncThunk(
  PAY,
  async (credits: payment) => {
    try {
      return await paymentSrv.pay(credits);
    } catch (e: any) {
      return e.json();
    }
  }
);