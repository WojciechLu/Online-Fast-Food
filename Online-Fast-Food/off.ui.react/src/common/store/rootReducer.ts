import { combineReducers } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useSelector } from "react-redux";
import authReducer from "../../auth/slice"
import menuReducer from "../../menu/slice"
import orderReducer from "../../order/slice"
const rootReducer = combineReducers({
    currentUser: authReducer,
    currentMenu: menuReducer,
    currentOrder: orderReducer
});

export default rootReducer;
export type RootState = ReturnType<typeof rootReducer>;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;