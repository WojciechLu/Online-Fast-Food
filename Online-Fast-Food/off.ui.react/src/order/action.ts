import { createAsyncThunk } from "@reduxjs/toolkit";
import { MAKE_ORDER, PAY_FOR_ORDER } from "../common/consts/actionTypes";
import { makeOrder } from "../common/models/order/makeOrder";
import { payForOrder } from "../common/models/order/payForOrder";
import { orderSrv } from "../common/services/order/orderSrv";

export const makeOrderAction = createAsyncThunk(
  MAKE_ORDER,
  async (credential: makeOrder) => {
    try {
      return await orderSrv.makeOrder(credential);
    } catch (e: any) {
      return e.json();
    }
  }
);

export const payForOrderAction = createAsyncThunk(
  PAY_FOR_ORDER,
  async (credential: payForOrder) => {
    try {
      return await orderSrv.payForOrder(credential);
    } catch (e: any) {
      return e.json();
    }
  }
);
