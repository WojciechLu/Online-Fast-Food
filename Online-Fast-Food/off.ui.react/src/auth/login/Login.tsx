import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useAppSelector } from "../../common/store/rootReducer";
import { SelectUser } from "../slice";
import { BasicInput } from "../../common/components/inputs/basicInput";
import { SubmitButton } from "../../common/components/buttons/submitButton";
import { toast } from "react-toastify";
import { ActionButton } from "../../common/components/buttons/actionButton";
import { loginInitialState } from "../../common/models/user/login";
import { loginAction } from "./action";
import { LoginFormContainer } from "../../common/components/containers/loginFormContainer";

const loginValidator = (fieldName: string, value: string) => {
    switch (fieldName) {
        case "email":
            var emailValidate = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
            if (emailValidate) {
                return true;
            }
            toast.error("Please enter a valid email address");
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

export const Login = () => {
    const dispatch = useDispatch();
    const currentUser = useAppSelector((state) => SelectUser(state));
    const navigate = useNavigate();
    const [credits, setCredits] = useState(loginInitialState);

    useEffect(() => {
        if (currentUser.token !== "") {
            navigate("../", { replace: true });
        }
    }, [currentUser.token, navigate]);

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;
        setCredits((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleSubmit = (
        event: React.MouseEvent<HTMLButtonElement, MouseEvent>
    ) => {
        // event.preventDefault();
        // for (let [key, value] of Object.entries(credits)) {
        //     if (!loginValidator(key, value)) {
        //         return;
        //     }
        // }
        dispatch(loginAction(credits));
    };

    return (
        <LoginFormContainer>
            <p>Sign In</p>

            <BasicInput
                name="email"
                placeholder="Email"
                value={credits.email}
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
            <h2>Do not have an account?</h2>
            <ActionButton
                className="new-account"
                onClick={() => navigate("/register")}
            >
                Create an account
            </ActionButton>
        </LoginFormContainer>
    );
};
