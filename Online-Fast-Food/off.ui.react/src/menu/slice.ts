import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import dish from "../common/models/menu/dish";
import Dictionary, { initialState } from "../common/models/menu/dishList";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";


export const menuSlice = createSlice({
  name: "menu",
  initialState,
  reducers: {
    loadMenuLocalStorage(state) {
      if (localStorage.getItem("dishesByCategory") !== null) {
        state.dishesByCategory = JSON.parse(localStorage.getItem("dishesByCategory")!);
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAvailableDishesAction.fulfilled, (state, action) => {
        if (action.payload !== undefined) {
          state.dishesByCategory = action.payload;
          localStorage.setItem("dishesByCategory", JSON.stringify(action.payload));
        }
      })
  },
});

export default menuSlice.reducer;

export const { loadMenuLocalStorage } = menuSlice.actions;
