import styled from "styled-components";

export const PriceButton = styled.button`
   &&&{
        width:100%;
        height:60%;
        max-height: 3rem;
        border-radius:10rem;
        margin: auto;
        padding:0.5rem; 
        //color
        background-color:white; 
        border:none;
        display: inline-flex;
        align-items: center; 
        :hover{
            cursor: pointer; 
            //color
            background-color:#eeeeee;
        }
   }
`;