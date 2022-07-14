import dish from "./dish";

export default interface dishes {
  dishes: dish[];
}

export const initialState: dishes = {
  dishes: [],
};
