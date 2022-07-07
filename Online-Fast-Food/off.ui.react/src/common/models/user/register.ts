export default interface register {
    email: string;
    firstName: string;
    lastName: string;
    password: string;
  }

  export const registerInitialState: register = {
    email: "",
    firstName: "",
    lastName: "",
    password: "",
  };
  