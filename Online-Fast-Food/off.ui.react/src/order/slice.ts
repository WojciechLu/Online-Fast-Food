import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import dish from "../common/models/menu/dish";
import { item } from "../common/models/order/item";
import { initialState } from "../common/models/order/order";
import { RootState } from "../common/store/rootReducer";
import { makeOrderAction } from "./action";


export const orderSlice = createSlice({
    name: "order",
    initialState,
    reducers: {
        addToOrder: (state, action) => {
            if (action.payload !== undefined) {
                let isCreated: boolean = false;
                let indexOfId: number;
                indexOfId = Object.keys(action.payload).indexOf('id');

                let id : String = Object.values(action.payload)[indexOfId] as String
                state.dishes.map((item: item) => {
                    let indexOfIdItem: number;
                    let dishItem = JSON.parse(JSON.stringify(item.Dish));
                    indexOfIdItem = Object.keys(dishItem).indexOf('id');
                    let idItem : String = Object.values(dishItem)[indexOfIdItem] as String
                    if (idItem === id) {
                        item.Quantity += 1;
                        isCreated = true;
                    }
                }
                );
                if (!isCreated) {
                    let item: item = {
                        Dish: action.payload,
                        Quantity: 1
                    }
                    state.dishes.push(item)
                }
            }
        },
    },
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

export const { addToOrder } = orderSlice.actions;

export const SelectOrder = (state: RootState) => {
    return state.currentOrder
};