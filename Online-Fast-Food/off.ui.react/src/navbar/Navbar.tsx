import { Link, Route, Routes, useNavigate } from "react-router-dom";
import { Register } from "../auth/register/Register";
import { Home } from "../home/Home";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Login } from "../auth/login/Login";
import { useEffect, useState } from "react";
import { useAppSelector } from "../common/store/rootReducer";
import { SelectUser } from "../auth/slice";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";
import ReceiptLongIcon from '@mui/icons-material/ReceiptLong';
import RestaurantMenuIcon from '@mui/icons-material/RestaurantMenu';
import { StripePaymentForm } from "../payment/Payment";
import { Menu } from "../menu/Menu";
import { Order } from "../order/Order";

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
              <Link to="/menu">
                <RestaurantMenuIcon />
                <span>Menu</span>
              </Link>
              <Link to="/Order">
                <ReceiptLongIcon />
                <span>Order</span>
              </Link>
            {!isLogged && (
              <Link to="/login">
                <LoginIcon />
                <span>Sign in</span>
              </Link>
            )}
            {isLogged && (
              <>
                <a onClick={handleLogout}>
                  <LogoutIcon />
                  <span>Sign out</span>
                </a>

                <Link to="/payment">
                  <span>Payment</span>
                </Link>
              </>
            )}
            {currentUser.role === "Admin" && (
              <Link to="/">
                <span>ADMIN PANEL</span>
              </Link>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Routes>
        <Route path="/register" element={<Register />}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/menu" element={<Menu />}></Route>
        <Route path="/order" element={<Order />}></Route>
        <Route path="/payment" element={<StripePaymentForm />}></Route>
        <Route path="/" element={<Home />}></Route>
      </Routes>
    </>
  );
};
