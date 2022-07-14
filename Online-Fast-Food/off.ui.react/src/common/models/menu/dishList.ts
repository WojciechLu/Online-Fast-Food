import dish from "./dish";

export default interface Dictionary<V> {
  [Key: string] : V
}

// export interface menuInterface{
//   dishesByCategory: Dictionary<dish[]>
// }

// export const initialState: menuInterface = {
//   dishesByCategory: {},
// };
export const initialState: Dictionary<dish[]> = {

};
