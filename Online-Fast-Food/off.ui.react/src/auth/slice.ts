import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import user, { initialState } from "../common/models/user/user";
import { registerAction } from "./register/action";
import { RootState } from "../common/store/rootReducer";
import { loginAction } from "./login/action";

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loadAuthLocalStorage(state) {
      if (localStorage.getItem("id") !== null) {
        state.id = parseInt(localStorage.getItem("id")!);
        state.lastName = localStorage.getItem("lastName")!;
        state.firstName = localStorage.getItem("firstName")!;
        state.token = localStorage.getItem("userToken")!;
        state.email = localStorage.getItem("email")!;
        state.role = localStorage.getItem("role")!;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(
        registerAction.fulfilled,
        (state: user, action: PayloadAction<user, string>) => {
          if (action.payload !== undefined) {
            localStorage.setItem("userToken", action.payload.token);
            localStorage.setItem("id", action.payload.id.toString());
            localStorage.setItem("firstName", action.payload.firstName);
            localStorage.setItem("lastName", action.payload.lastName);
            localStorage.setItem("email", action.payload.email);
            localStorage.setItem("role", action.payload.role);
            state.id = action.payload.id;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.token = action.payload.token;
            state.email = action.payload.email;
            state.role = action.payload.role;
          }
        }
      )
      .addCase(
        loginAction.fulfilled,
        (state: user, action: PayloadAction<user, string>) => {
          if (action.payload !== undefined) {
            localStorage.setItem("userToken", action.payload.token);
            localStorage.setItem("id", action.payload.id.toString());
            localStorage.setItem("firstName", action.payload.firstName);
            localStorage.setItem("lastName", action.payload.lastName);
            localStorage.setItem("email", action.payload.email);
            localStorage.setItem("role", action.payload.role);
            state.id = action.payload.id;
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.token = action.payload.token;
            state.email = action.payload.email;
            state.role = action.payload.role;
          }
        }
      )
  },
});

export const { loadAuthLocalStorage } = authSlice.actions;
export default authSlice.reducer;

export const SelectUser = (state: RootState) => {
  return state.currentUser
};
