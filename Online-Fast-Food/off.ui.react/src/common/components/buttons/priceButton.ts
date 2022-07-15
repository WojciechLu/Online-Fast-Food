import styled from "styled-components";

export const PriceButton = styled.button`
   &&&{
        width:20em;
        border-radius:10rem; 
        height:2rem;
        margin:0.3rem;
        padding:0.5rem; 
        background-color:white; 
        border:none;
        position: absolute;
        right: 1rem;
        bottom: 1rem;
        :hover{
            cursor: pointer;
            background-color:#eeeeee;
        }
   }
`;