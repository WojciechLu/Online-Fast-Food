import { createAsyncThunk } from "@reduxjs/toolkit";
import { GET_AVAIBLE_DISHES } from "../../common/consts/actionTypes";
import { dishSrv } from "../../common/services/dish/dishSrv";

export const getAvailableDishesAction = createAsyncThunk(
    GET_AVAIBLE_DISHES,
    async () => {
      try {
        return await dishSrv.getAvailableDishes();
      } catch (e: any) {
        return e.json();
      }
    }
  );
  