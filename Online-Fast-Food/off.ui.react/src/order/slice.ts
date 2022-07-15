import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "../common/models/order/order";
import { RootState } from "../common/store/rootReducer";
import { makeOrderAction } from "./action";


export const orderSlice = createSlice({
    name: "order",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(makeOrderAction.fulfilled, (state, action) => {
                if (action.payload !== undefined) {
                    state.orderId = action.payload.orderId;
                    state.customerId = action.payload.customerId;
                    state.dishes = action.payload.dishes;
                }
            })
    },
});

export default orderSlice.reducer;

export const SelectOrder = (state: RootState) => {
    return state.currentOrder
  };