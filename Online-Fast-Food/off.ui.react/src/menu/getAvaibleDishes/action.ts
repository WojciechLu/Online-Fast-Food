import { createAsyncThunk } from "@reduxjs/toolkit";
import { GET_AVAIBLE_DISHES } from "../../common/consts/actionTypes";
import dish from "../../common/models/menu/dish";
import Dictionary from "../../common/models/menu/dishList";
import { dishSrv } from "../../common/services/dish/dishSrv";

export const getAvailableDishesAction = createAsyncThunk(
    GET_AVAIBLE_DISHES,
    async () => {
      try {
        return await dishSrv.getAvailableDishes() as Dictionary<dish[]>;
      } catch (e: any) {
        return e.json();
      }
    }
  );
  