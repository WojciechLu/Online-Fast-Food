import { Link, MemoryRouter, Route, Routes, useNavigate } from "react-router-dom";
import LoginIcon from "@mui/icons-material/Login";
import { Register } from "../auth/register/Register";
import { Home } from "../home/Home";
import { Container, Nav, Navbar } from "react-bootstrap";

export const NavbarRouter = () => {
  return (
    <>
      <Navbar bg="light" expand="lg" sticky="top">
        <Container>
          <Navbar.Brand href="/">Online-Fast-Food</Navbar.Brand>
          <Navbar.Toggle />
          <Navbar.Collapse className="justify-content-end">
            <Nav.Link href="/">Home</Nav.Link>
            <Nav.Link href="/register">Register</Nav.Link>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Routes>
        <Route path="/register" element={<Register />}></Route>
        <Route path="/" element={<Home />}></Route>
      </Routes>
    </>
  );
};
