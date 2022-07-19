import { Route, Routes, useNavigate } from "react-router-dom";
import { Register } from "../auth/register/Register";
import { Home } from "../home/Home";
import { Container, Nav, Navbar } from "react-bootstrap";
import { Login } from "../auth/login/Login";
import { useEffect, useState } from "react";
import { useAppSelector } from "../common/store/rootReducer";
import { loadAuthLocalStorage, SelectUser } from "../auth/slice";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";
import ReceiptLongIcon from '@mui/icons-material/ReceiptLong';
import RestaurantMenuIcon from '@mui/icons-material/RestaurantMenu';
import { StripePaymentForm } from "../payment/Payment";
import { Menu } from "../menu/Menu";
import { Order } from "../order/Order";
import { loadOrderLocalStorage, SelectOrder } from "../order/slice";
import { OrderNavbarIcon } from "../common/components/containers/orderNavbarItem";
import { store } from "..";
import { loadMenuLocalStorage } from "../menu/slice";

export const NavbarRouter = () => {
  const navigate = useNavigate();
  const [isLogged, setIsLogged] = useState(false);
  let currentUser = useAppSelector((state) => SelectUser(state));
  let currentOrder = useAppSelector((state) => SelectOrder(state));
  
  useEffect(() => {
    store.dispatch(loadMenuLocalStorage());
    store.dispatch(loadAuthLocalStorage());
    store.dispatch(loadOrderLocalStorage());
  }, []);

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
            <Nav.Link href="/menu">
              <RestaurantMenuIcon />
              <span>Menu</span>
            </Nav.Link>
            <Nav.Link href="/Order">
              {currentOrder.dishes.length > 0 && (
                <OrderNavbarIcon>
                  <ReceiptLongIcon />
                  <div className="numberCircle">{currentOrder.dishes.length}</div>
                </OrderNavbarIcon>
              )}
              {currentOrder.dishes.length === 0 && (
                <ReceiptLongIcon />
              )}
              <span>Order</span>
            </Nav.Link>
            {!isLogged && (
              <Nav.Link href="/login">
                <LoginIcon />
                <span>Sign in</span>
              </Nav.Link>
            )}
            {isLogged && (
              <>
                <a onClick={handleLogout}>
                  <LogoutIcon />
                  <span>Sign out</span>
                </a>

                <Nav.Link href="/payment">
                  <span>Payment</span>
                </Nav.Link>
              </>
            )}
            {currentUser.role === "Admin" && (
              <Nav.Link href="/">
                <span>ADMIN PANEL</span>
              </Nav.Link>
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
