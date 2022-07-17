import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "../common/models/menu/dishList";
import { getAvailableDishesAction } from "./getAvaibleDishes/action";


export const menuSlice = createSlice({
  name: "menu",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getAvailableDishesAction.fulfilled, (state, action) => {
        if (action.payload !== undefined) {
          state.dishesByCategory = action.payload;
        }
      })
  },
});

export default menuSlice.reducer;