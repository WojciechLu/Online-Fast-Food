import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router";
import { toast } from "react-toastify";
import { SubmitButton } from "../../common/components/buttons/submitButton";
import { RegisterFormContainer } from "../../common/components/containers/registerFormContainer";
import { BasicInput } from "../../common/components/inputs/basicInput";
import { registerInitialState } from "../../common/models/user/register";
import { useAppSelector } from "../../common/store/rootReducer";
import { SelectUser } from "../slice";
import { registerAction } from "./action";

const registerValidator = (fieldName: string, value: string) => {
  switch (fieldName) {
    case "email":
      var emailValidate = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
      if (emailValidate) {
        return true;
      }
      toast.error("Please enter a valid email address");
      return false;
    case "firstName":
      if (value.length >= 2) {
        return true;
      }
      toast.error("First name must be at least 2 characters");
      return false;
    case "lastName":
      if (value.length >= 2) {
        return true;
      }
      toast.error("Last name must be at least 2 characters");
      return false;
    case "password":
      if (value.length >= 6) {
        return true;
      }
      toast.error("Password must be at least 6 characters");
      return false;
    default:
      return true;
  }
};

export const Register = () => {
  const dispatch = useDispatch();
  const currentUser = useAppSelector((state) => SelectUser(state));
  const navigate = useNavigate();
  const [credits, setCredits] = useState(registerInitialState);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setCredits((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => {
    event.preventDefault();
    for (let [key, value] of Object.entries(credits)) {
      if (!registerValidator(key, value)) {
        return;
      }
    }
    dispatch(registerAction(credits));
    setCredits(registerInitialState);
  };

  return (
    <RegisterFormContainer>
      <p>Register</p>

      <BasicInput
        name="email"
        placeholder="Email"
        value={credits.email}
        required
        onChange={(e) => handleChange(e)}
      />

      <BasicInput
        name="firstName"
        placeholder="First Name"
        value={credits.firstName}
        required
        onChange={(e) => handleChange(e)}
      />

      <BasicInput
        name="lastName"
        placeholder="Last Name"
        value={credits.lastName}
        required
        onChange={(e) => handleChange(e)}
      />

      <BasicInput
        name="password"
        placeholder="Password"
        value={credits.password}
        required
        onChange={(e) => handleChange(e)}
        type="password"
      />
      <SubmitButton
        onClick={(e) => handleSubmit(e)}
      >
        Submit
      </SubmitButton>
    </RegisterFormContainer>
  );
};
