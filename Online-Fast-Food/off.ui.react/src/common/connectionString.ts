import axios from "axios";
import AxiosError from "axios-error";
import { toast } from "react-toastify";

export const api = axios.create({
    baseURL: "https://localhost:7037/api/",
    withCredentials: true,
  });

  api.interceptors.request.use((request: any) => {
    var token = localStorage.getItem("userToken");
    request.headers.Authorization = `Bearer ${token}`;
    return request;
  });

  // api.interceptors.response.use(
  //   (response) => {
  //     if(response.request?.responseURL.toString().includes("changePassword")){
  //       toast.success("Password Changed")
  //     }
  //     if(response.request?.responseURL.toString().includes("Event/create")){
  //       toast.success("Event Added")
  //     }
  //     if(response.request?.responseURL.toString().includes("register")){
  //       toast.success("Account created. Check your email to verify it!");
  //     }
  //     return response;
  //   },
  //   (error: AxiosError) => {
  //     toast.error(error.response ? error.response.data.message : error.message);
  //   }
  // );
  