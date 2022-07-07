import { createAsyncThunk } from "@reduxjs/toolkit";
import { FETCH_USER } from "../../common/consts/actionTypes";
import login from "../../common/models/user/login";
import { authSrv } from "../../common/services/user/authSrv";

export const loginAction = createAsyncThunk(
  FETCH_USER,
  async (cridentials: login) => {
    try {
      return await authSrv.login(cridentials);
    } catch (e: any) {
      return e.json();
    }
  }
);
