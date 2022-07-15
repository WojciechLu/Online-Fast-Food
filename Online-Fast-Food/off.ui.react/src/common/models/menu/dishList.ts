import dish from "./dish";

export default interface Dictionary<V> {
  [Key: string] : V[]
}

export const initialState: Dictionary<dish> = {
  dishesByCategory: []
};
