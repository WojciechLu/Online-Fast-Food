import Dictionary from "../menu/dishList";
import { item } from "./item";

export interface order{
    orderId: number,
    customerId: number,
    dishes: item[]
}

export const initialState: order = {
    customerId: 0,
    orderId: 0,
    dishes: []
};
