export default interface user {
    id: number;
    firstName: string;
    lastName: string;
    email:string;
    token: string;
  }
  
  export const initialState: user = {
    id: 0,
    firstName: "",
    lastName: "",
    email:"",
    token: "",
  };