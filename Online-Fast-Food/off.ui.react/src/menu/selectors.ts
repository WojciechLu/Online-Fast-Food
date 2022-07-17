import { RootState } from "../common/store/rootReducer";

export const SelectAllDishes = (state: RootState) => {
    return state.currentMenu;
};