import register from "../../common/models/user/register";
import { ADD_USER } from "../../common/consts/actionTypes";
import { authSrv } from "../../common/services/user/authSrv";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const registerAction = createAsyncThunk(
  ADD_USER,
  async (credential: register) => {
    try {
      return await authSrv.register(credential);
    } catch (e: any) {
      return e.json();
    }
  }
);
