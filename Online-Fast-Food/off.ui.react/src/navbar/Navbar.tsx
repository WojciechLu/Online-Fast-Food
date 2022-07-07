import { Link, MemoryRouter, Route, Routes, useNavigate } from "react-router-dom";
import { Register } from "../auth/register/Register";
import { Home } from "../home/Home";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Login } from "../auth/login/Login";
import { useEffect, useState } from "react";
import { useAppSelector } from "../common/store/rootReducer";
import { SelectUser } from "../auth/slice";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";

export const NavbarRouter = () => {
  const navigate = useNavigate();
  const [isLogged, setIsLogged] = useState(false);
  let currentUser = useAppSelector((state) => SelectUser(state));

  useEffect(() => {
    if (localStorage.getItem("userToken") !== null) {
      setIsLogged(true);
    } else {
      setIsLogged(false);
    }
  }, [currentUser.token, localStorage]);

  const handleLogout = () => {
    localStorage.clear();
    navigate("/");
    window.location.reload();
  };

  return (
    <>
      <Navbar bg="light" expand="lg" sticky="top">
        <Container>
          <Navbar.Brand href="/">Online-Fast-Food</Navbar.Brand>
          <Navbar.Toggle />
          <Navbar.Collapse className="justify-content-end">
            <Nav.Link href="/">Home</Nav.Link>
            {!isLogged && (
              <Link to="/login">
                <LoginIcon />
                <span>Sign in</span>
              </Link>
            )}
            {isLogged && (
              <a onClick={handleLogout}>
                <LogoutIcon />
                <span>Sign out</span>
              </a>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Routes>
        <Route path="/register" element={<Register />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/" element={<Home />}></Route>
      </Routes>
    </>
  );
};
