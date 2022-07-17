import Dictionary from "../menu/dishList";

export interface makeOrder{
    CustomerId: number,
    ProductIdAndQuantity: Dictionary<number>
}